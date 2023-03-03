// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Pdf417.Decoder.Pdf417Line
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System.Collections.Generic;

namespace MessagingToolkit.Barcode.Pdf417.Decoder
{
  public sealed class Pdf417Line
  {
    private Pdf417Symbol leftColumnIndicator;
    private Pdf417Symbol rightColumnIndicator;
    private List<Pdf417Symbol> symbols;

    public Pdf417Line()
    {
      this.leftColumnIndicator = (Pdf417Symbol) null;
      this.rightColumnIndicator = (Pdf417Symbol) null;
    }

    public Pdf417Symbol LeftColumnIndicator
    {
      get => this.leftColumnIndicator;
      set => this.leftColumnIndicator = value;
    }

    public Pdf417Symbol RightColumnIndicator
    {
      get => this.rightColumnIndicator;
      set => this.rightColumnIndicator = value;
    }
  }
}
