// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.OneDDecoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode.OneD
{
  public abstract class OneDDecoder : IDecoder
  {
    protected internal const int IntegerMathShift = 8;
    protected internal const int PatternMatchResultScaleFactor = 256;

    public virtual Result Decode(BinaryBitmap image) => this.Decode(image, (Dictionary<DecodeOptions, object>) null);

    public virtual Result Decode(
      BinaryBitmap image,
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      try
      {
        return this.DoDecode(image, decodingOptions);
      }
      catch (NotFoundException ex)
      {
        if (decodingOptions == null || !decodingOptions.ContainsKey(DecodeOptions.TryHarder) || !image.RotateSupported)
          throw ex;
        BinaryBitmap image1 = image.RotateCounterClockwise();
        Result result = this.DoDecode(image1, decodingOptions);
        Dictionary<ResultMetadataType, object> resultMetadata = result.ResultMetadata;
        int num = 270;
        if (resultMetadata != null && resultMetadata.ContainsKey(ResultMetadataType.Orientation))
          num = (num + (int) resultMetadata[ResultMetadataType.Orientation]) % 360;
        result.PutMetadata(ResultMetadataType.Orientation, (object) num);
        ResultPoint[] resultPoints = result.ResultPoints;
        int height = image1.Height;
        for (int index = 0; index < resultPoints.Length; ++index)
          resultPoints[index] = new ResultPoint((float) ((double) height - (double) resultPoints[index].Y - 1.0), resultPoints[index].X);
        return result;
      }
    }

    public virtual void Reset()
    {
    }

    private Result DoDecode(
      BinaryBitmap image,
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      int width = image.Width;
      int height = image.Height;
      BitArray row = new BitArray(width);
      int num1 = height >> 1;
      bool flag1 = decodingOptions != null && decodingOptions.ContainsKey(DecodeOptions.TryHarder);
      int num2 = Math.Max(1, height >> (flag1 ? 8 : 5));
      int num3 = !flag1 ? 15 : height;
      for (int index1 = 0; index1 < num3; ++index1)
      {
        int num4 = index1 + 1 >> 1;
        bool flag2 = (index1 & 1) == 0;
        int num5 = num1 + num2 * (flag2 ? num4 : -num4);
        if (num5 >= 0)
        {
          if (num5 < height)
          {
            try
            {
              row = image.GetBlackRow(num5, row);
            }
            catch (NotFoundException ex)
            {
              continue;
            }
            for (int index2 = 0; index2 < 2; ++index2)
            {
              if (index2 == 1)
              {
                row.Reverse();
                if (decodingOptions != null)
                {
                  if (decodingOptions.ContainsKey(DecodeOptions.NeedResultPointCallback))
                  {
                    Dictionary<DecodeOptions, object> dictionary = new Dictionary<DecodeOptions, object>();
                    IEnumerator<DecodeOptions> enumerator = (IEnumerator<DecodeOptions>) decodingOptions.Keys.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                      DecodeOptions current = enumerator.Current;
                      if (current != DecodeOptions.NeedResultPointCallback)
                        dictionary.Add(current, decodingOptions[current]);
                    }
                    decodingOptions = dictionary;
                  }
                }
              }
              try
              {
                Result result = this.DecodeRow(num5, row, decodingOptions);
                if (index2 == 1)
                {
                  result.PutMetadata(ResultMetadataType.Orientation, (object) 180);
                  ResultPoint[] resultPoints = result.ResultPoints;
                  resultPoints[0] = new ResultPoint((float) ((double) width - (double) resultPoints[0].X - 1.0), resultPoints[0].Y);
                  resultPoints[1] = new ResultPoint((float) ((double) width - (double) resultPoints[1].X - 1.0), resultPoints[1].Y);
                }
                return result;
              }
              catch (BarcodeDecoderException ex)
              {
              }
            }
          }
          else
            break;
        }
        else
          break;
      }
      throw NotFoundException.Instance;
    }

    protected internal static void RecordPattern(BitArray row, int start, int[] counters)
    {
      int length = counters.Length;
      for (int index = 0; index < length; ++index)
        counters[index] = 0;
      int size = row.GetSize();
      if (start >= size)
        throw NotFoundException.Instance;
      bool flag = !row.Get(start);
      int index1 = 0;
      int i;
      for (i = start; i < size; ++i)
      {
        if (row.Get(i) ^ flag)
        {
          ++counters[index1];
        }
        else
        {
          ++index1;
          if (index1 != length)
          {
            counters[index1] = 1;
            flag = !flag;
          }
          else
            break;
        }
      }
      if (index1 != length && (index1 != length - 1 || i != size))
        throw NotFoundException.Instance;
    }

    protected internal static void RecordPatternInReverse(BitArray row, int start, int[] counters)
    {
      int length = counters.Length;
      bool flag = row.Get(start);
      while (start > 0 && length >= 0)
      {
        if (row.Get(--start) != flag)
        {
          --length;
          flag = !flag;
        }
      }
      if (length >= 0)
        throw NotFoundException.Instance;
      OneDDecoder.RecordPattern(row, start + 1, counters);
    }

    protected internal static int PatternMatchVariance(
      int[] counters,
      int[] pattern,
      int maxIndividualVariance)
    {
      int length = counters.Length;
      int num1 = 0;
      int num2 = 0;
      for (int index = 0; index < length; ++index)
      {
        num1 += counters[index];
        num2 += pattern[index];
      }
      if (num1 < num2)
        return int.MaxValue;
      int num3 = (num1 << 8) / num2;
      maxIndividualVariance = maxIndividualVariance * num3 >> 8;
      int num4 = 0;
      for (int index = 0; index < length; ++index)
      {
        int num5 = counters[index] << 8;
        int num6 = pattern[index] * num3;
        int num7 = num5 > num6 ? num5 - num6 : num6 - num5;
        if (num7 > maxIndividualVariance)
          return int.MaxValue;
        num4 += num7;
      }
      return num4 / num1;
    }

    public abstract Result DecodeRow(
      int rowNumber,
      BitArray row,
      Dictionary<DecodeOptions, object> decodingOptions);
  }
}
