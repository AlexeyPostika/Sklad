// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Aztec.AztecDetectorResult
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;

namespace MessagingToolkit.Barcode.Aztec
{
  public sealed class AztecDetectorResult : DetectorResult
  {
    private readonly bool compact;
    private readonly int nbDatablocks;
    private readonly int nbLayers;

    public AztecDetectorResult(
      BitMatrix bits,
      ResultPoint[] points,
      bool compact,
      int nbDatablocks,
      int nbLayers)
      : base(bits, points)
    {
      this.compact = compact;
      this.nbDatablocks = nbDatablocks;
      this.nbLayers = nbLayers;
    }

    public int GetNbLayers() => this.nbLayers;

    public int GetNbDatablocks() => this.nbDatablocks;

    public bool IsCompact() => this.compact;
  }
}
