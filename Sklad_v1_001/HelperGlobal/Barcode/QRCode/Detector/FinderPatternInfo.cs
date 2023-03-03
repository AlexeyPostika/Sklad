// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.QRCode.Detector.FinderPatternInfo
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.QRCode.Detector
{
  public sealed class FinderPatternInfo
  {
    private FinderPattern bottomLeft;
    private FinderPattern topLeft;
    private FinderPattern topRight;

    public FinderPattern BottomLeft => this.bottomLeft;

    public FinderPattern TopLeft => this.topLeft;

    public FinderPattern TopRight => this.topRight;

    public FinderPatternInfo(FinderPattern[] patternCenters)
    {
      this.bottomLeft = patternCenters[0];
      this.topLeft = patternCenters[1];
      this.topRight = patternCenters[2];
    }
  }
}
