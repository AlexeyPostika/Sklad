// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.QRCode.Detector.AlignmentPattern
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;

namespace MessagingToolkit.Barcode.QRCode.Detector
{
  public sealed class AlignmentPattern : ResultPoint
  {
    private readonly float estimatedModuleSize;

    internal AlignmentPattern(float posX, float posY, float estimatedModuleSize)
      : base(posX, posY)
    {
      this.estimatedModuleSize = estimatedModuleSize;
    }

    internal bool AboutEquals(float moduleSize, float i, float j)
    {
      if ((double) Math.Abs(i - this.Y) > (double) moduleSize || (double) Math.Abs(j - this.X) > (double) moduleSize)
        return false;
      float num = Math.Abs(moduleSize - this.estimatedModuleSize);
      return (double) num <= 1.0 || (double) num <= (double) this.estimatedModuleSize;
    }

    internal AlignmentPattern CombineEstimate(
      float i,
      float j,
      float newModuleSize)
    {
      return new AlignmentPattern((float) (((double) this.X + (double) j) / 2.0), (float) (((double) this.Y + (double) i) / 2.0), (float) (((double) this.estimatedModuleSize + (double) newModuleSize) / 2.0));
    }
  }
}
