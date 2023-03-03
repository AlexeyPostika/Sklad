// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.QRCode.Detector.FinderPattern
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;

namespace MessagingToolkit.Barcode.QRCode.Detector
{
  public sealed class FinderPattern : ResultPoint
  {
    private readonly float estimatedModuleSize;
    private int count;

    internal FinderPattern(float posX, float posY, float estimatedModuleSize)
      : this(posX, posY, estimatedModuleSize, 1)
    {
    }

    private FinderPattern(float posX, float posY, float estimatedModuleSize, int count)
      : base(posX, posY)
    {
      this.estimatedModuleSize = estimatedModuleSize;
      this.count = count;
    }

    public float EstimatedModuleSize => this.estimatedModuleSize;

    internal int Count => this.count;

    internal void IncrementCount() => ++this.count;

    internal bool AboutEquals(float moduleSize, float i, float j)
    {
      if ((double) Math.Abs(i - this.Y) > (double) moduleSize || (double) Math.Abs(j - this.X) > (double) moduleSize)
        return false;
      float num = Math.Abs(moduleSize - this.estimatedModuleSize);
      return (double) num <= 1.0 || (double) num <= (double) this.estimatedModuleSize;
    }

    internal FinderPattern CombineEstimate(float i, float j, float newModuleSize)
    {
      int count = this.count + 1;
      return new FinderPattern(((float) this.count * this.X + j) / (float) count, ((float) this.count * this.Y + i) / (float) count, ((float) this.count * this.estimatedModuleSize + newModuleSize) / (float) count, count);
    }
  }
}
