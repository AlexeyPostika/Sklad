// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Common.Detector.MathUtils
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;

namespace MessagingToolkit.Barcode.Common.Detector
{
  public sealed class MathUtils
  {
    private MathUtils()
    {
    }

    public static int Round(float d) => (int) ((double) d + 0.5);

    public static float Distance(float aX, float aY, float bX, float bY)
    {
      float num1 = aX - bX;
      float num2 = aY - bY;
      return (float) Math.Sqrt((double) num1 * (double) num1 + (double) num2 * (double) num2);
    }

    public static float Distance(int aX, int aY, int bX, int bY)
    {
      int num1 = aX - bX;
      int num2 = aY - bY;
      return (float) Math.Sqrt((double) (num1 * num1 + num2 * num2));
    }
  }
}
