// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Rss.Expanded.Decoders.CurrentParsingState
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.OneD.Rss.Expanded.Decoders
{
  internal sealed class CurrentParsingState
  {
    private const int Numeric = 1;
    private const int Alpha = 2;
    private const int IsoIec646 = 4;
    internal int position;
    private int encoding;

    internal CurrentParsingState()
    {
      this.position = 0;
      this.encoding = 1;
    }

    internal bool IsAlpha => this.encoding == 2;

    internal bool IsNumeric => this.encoding == 1;

    internal bool IsIsoIec646 => this.encoding == 4;

    internal void SetNumeric() => this.encoding = 1;

    internal void SetAlpha() => this.encoding = 2;

    internal void SetIsoIec646() => this.encoding = 4;
  }
}
