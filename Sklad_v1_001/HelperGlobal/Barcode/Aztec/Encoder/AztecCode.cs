// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Aztec.Encoder.AztecCode
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;

namespace MessagingToolkit.Barcode.Aztec.Encoder
{
  public sealed class AztecCode
  {
    private bool compact;
    private int size;
    private int layers;
    private int codeWords;
    private BitMatrix matrix;

    public bool Compact
    {
      get => this.compact;
      set => this.compact = value;
    }

    public int Size
    {
      get => this.size;
      set => this.size = value;
    }

    public int Layers
    {
      get => this.layers;
      set => this.layers = value;
    }

    public int CodeWords
    {
      get => this.codeWords;
      set => this.codeWords = value;
    }

    public BitMatrix Matrix
    {
      get => this.matrix;
      set => this.matrix = value;
    }
  }
}
