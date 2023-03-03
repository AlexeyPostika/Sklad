// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Common.GridSampler
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.Common
{
  public abstract class GridSampler
  {
    private static GridSampler gridSampler = (GridSampler) new DefaultGridSampler();

    public static void SetGridSampler(GridSampler newGridSampler) => GridSampler.gridSampler = newGridSampler;

    public static GridSampler GetInstance() => GridSampler.gridSampler;

    public abstract BitMatrix SampleGrid(
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
      float p4FromY);

    public abstract BitMatrix SampleGrid(
      BitMatrix image,
      int dimensionX,
      int dimensionY,
      PerspectiveTransform transform);

    protected internal static void CheckAndNudgePoints(BitMatrix image, float[] points)
    {
      int width = image.GetWidth();
      int height = image.GetHeight();
      bool flag1 = true;
      for (int index = 0; index < points.Length && flag1; index += 2)
      {
        int point1 = (int) points[index];
        int point2 = (int) points[index + 1];
        if (point1 < -1 || point1 > width || point2 < -1 || point2 > height)
          throw NotFoundException.Instance;
        flag1 = false;
        if (point1 == -1)
        {
          points[index] = 0.0f;
          flag1 = true;
        }
        else if (point1 == width)
        {
          points[index] = (float) (width - 1);
          flag1 = true;
        }
        if (point2 == -1)
        {
          points[index + 1] = 0.0f;
          flag1 = true;
        }
        else if (point2 == height)
        {
          points[index + 1] = (float) (height - 1);
          flag1 = true;
        }
      }
      bool flag2 = true;
      for (int index = points.Length - 2; index >= 0 && flag2; index -= 2)
      {
        int point3 = (int) points[index];
        int point4 = (int) points[index + 1];
        if (point3 < -1 || point3 > width || point4 < -1 || point4 > height)
          throw NotFoundException.Instance;
        flag2 = false;
        if (point3 == -1)
        {
          points[index] = 0.0f;
          flag2 = true;
        }
        else if (point3 == width)
        {
          points[index] = (float) (width - 1);
          flag2 = true;
        }
        if (point4 == -1)
        {
          points[index + 1] = 0.0f;
          flag2 = true;
        }
        else if (point4 == height)
        {
          points[index + 1] = (float) (height - 1);
          flag2 = true;
        }
      }
    }
  }
}
