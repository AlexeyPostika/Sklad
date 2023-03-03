// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Multi.GenericMultipleBarcodeDecoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System.Collections.Generic;

namespace MessagingToolkit.Barcode.Multi
{
  public class GenericMultipleBarcodeDecoder : MultipleBarcodeDecoder
  {
    private const int MIN_DIMENSION_TO_RECUR = 100;
    private const int MAX_DEPTH = 4;
    private readonly IDecoder decoder;

    public GenericMultipleBarcodeDecoder(IDecoder decoder) => this.decoder = decoder;

    public Result[] DecodeMultiple(BinaryBitmap image) => this.DecodeMultiple(image, (Dictionary<DecodeOptions, object>) null);

    public Result[] DecodeMultiple(
      BinaryBitmap image,
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      IList<Result> results = (IList<Result>) new List<Result>();
      this.DoDecodeMultiple(image, decodingOptions, results, 0, 0, 0);
      int length = results.Count != 0 ? results.Count : throw NotFoundException.Instance;
      Result[] resultArray = new Result[length];
      for (int index = 0; index < length; ++index)
        resultArray[index] = results[index];
      return resultArray;
    }

    private void DoDecodeMultiple(
      BinaryBitmap image,
      Dictionary<DecodeOptions, object> decodingOptions,
      IList<Result> results,
      int xOffset,
      int yOffset,
      int currentDepth)
    {
      if (currentDepth > 4)
        return;
      Result result1;
      try
      {
        result1 = this.decoder.Decode(image, decodingOptions);
      }
      catch (BarcodeDecoderException ex)
      {
        return;
      }
      bool flag = false;
      foreach (Result result2 in (IEnumerable<Result>) results)
      {
        if (result2.Text.Equals(result1.Text))
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        results.Add(GenericMultipleBarcodeDecoder.TranslateResultPoints(result1, xOffset, yOffset));
      ResultPoint[] resultPoints = result1.ResultPoints;
      if (resultPoints == null || resultPoints.Length == 0)
        return;
      int width1 = image.Width;
      int height1 = image.Height;
      float width2 = (float) width1;
      float height2 = (float) height1;
      float left = 0.0f;
      float top = 0.0f;
      foreach (ResultPoint resultPoint in resultPoints)
      {
        float x = resultPoint.X;
        float y = resultPoint.Y;
        if ((double) x < (double) width2)
          width2 = x;
        if ((double) y < (double) height2)
          height2 = y;
        if ((double) x > (double) left)
          left = x;
        if ((double) y > (double) top)
          top = y;
      }
      if ((double) width2 > 100.0)
        this.DoDecodeMultiple(image.Crop(0, 0, (int) width2, height1), decodingOptions, results, xOffset, yOffset, currentDepth + 1);
      if ((double) height2 > 100.0)
        this.DoDecodeMultiple(image.Crop(0, 0, width1, (int) height2), decodingOptions, results, xOffset, yOffset, currentDepth + 1);
      if ((double) left < (double) (width1 - 100))
        this.DoDecodeMultiple(image.Crop((int) left, 0, width1 - (int) left, height1), decodingOptions, results, xOffset + (int) left, yOffset, currentDepth + 1);
      if ((double) top >= (double) (height1 - 100))
        return;
      this.DoDecodeMultiple(image.Crop(0, (int) top, width1, height1 - (int) top), decodingOptions, results, xOffset, yOffset + (int) top, currentDepth + 1);
    }

    private static Result TranslateResultPoints(Result result, int xOffset, int yOffset)
    {
      ResultPoint[] resultPoints1 = result.ResultPoints;
      if (resultPoints1 == null)
        return result;
      ResultPoint[] resultPoints2 = new ResultPoint[resultPoints1.Length];
      for (int index = 0; index < resultPoints1.Length; ++index)
      {
        ResultPoint resultPoint = resultPoints1[index];
        resultPoints2[index] = new ResultPoint(resultPoint.X + (float) xOffset, resultPoint.Y + (float) yOffset);
      }
      return new Result(result.Text, result.RawBytes, resultPoints2, result.BarcodeFormat);
    }
  }
}
