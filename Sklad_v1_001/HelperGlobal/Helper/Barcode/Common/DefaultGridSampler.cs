// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Common.DefaultGridSampler
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;

namespace MessagingToolkit.Barcode.Common
{
  public sealed class DefaultGridSampler : GridSampler
  {
    public override BitMatrix SampleGrid(
      BitMatrix image,
      int dimensionX,
      int dimensionY,
      float p1ToX,
      float p1ToY,
      float p2ToX,
      float p2ToY,
      float p3ToX,
      float p3ToY,
      float p4ToX,
      float p4ToY,
      float p1FromX,
      float p1FromY,
      float p2FromX,
      float p2FromY,
      float p3FromX,
      float p3FromY,
      float p4FromX,
      float p4FromY)
    {
      PerspectiveTransform quadrilateral = PerspectiveTransform.QuadrilateralToQuadrilateral(p1ToX, p1ToY, p2ToX, p2ToY, p3ToX, p3ToY, p4ToX, p4ToY, p1FromX, p1FromY, p2FromX, p2FromY, p3FromX, p3FromY, p4FromX, p4FromY);
      return this.SampleGrid(image, dimensionX, dimensionY, quadrilateral);
    }

    public override BitMatrix SampleGrid(
      BitMatrix image,
      int dimensionX,
      int dimensionY,
      PerspectiveTransform transform)
    {
      BitMatrix bitMatrix = dimensionX > 0 && dimensionY > 0 ? new BitMatrix(dimensionX, dimensionY) : throw NotFoundException.Instance;
      float[] points = new float[dimensionX << 1];
      for (int y = 0; y < dimensionY; ++y)
      {
        int length = points.Length;
        float num = (float) y + 0.5f;
        for (int index = 0; index < length; index += 2)
        {
          points[index] = (float) (index >> 1) + 0.5f;
          points[index + 1] = num;
        }
        transform.TransformPoints(points);
        GridSampler.CheckAndNudgePoints(image, points);
        try
        {
          for (int index = 0; index < length; index += 2)
          {
            if (image.Get((int) points[index], (int) points[index + 1]))
              bitMatrix.Set(index >> 1, y);
          }
        }
        catch (IndexOutOfRangeException ex)
        {
          throw NotFoundException.Instance;
        }
      }
      return bitMatrix;
    }
  }
}
