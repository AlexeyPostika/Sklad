// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.QRCode.Decoder.ErrorCorrectionLevel
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;

namespace MessagingToolkit.Barcode.QRCode.Decoder
{
  public sealed class ErrorCorrectionLevel
  {
    public static readonly ErrorCorrectionLevel L = new ErrorCorrectionLevel(0, 1, nameof (L));
    public static readonly ErrorCorrectionLevel M = new ErrorCorrectionLevel(1, 0, nameof (M));
    public static readonly ErrorCorrectionLevel Q = new ErrorCorrectionLevel(2, 3, nameof (Q));
    public static readonly ErrorCorrectionLevel H = new ErrorCorrectionLevel(3, 2, "h");
    private static readonly ErrorCorrectionLevel[] FOR_BITS = new ErrorCorrectionLevel[4]
    {
      ErrorCorrectionLevel.M,
      ErrorCorrectionLevel.L,
      ErrorCorrectionLevel.H,
      ErrorCorrectionLevel.Q
    };
    private readonly int ordinal;
    private readonly int bits;
    private readonly string name;

    private ErrorCorrectionLevel(int ordinal, int bits, string name)
    {
      this.ordinal = ordinal;
      this.bits = bits;
      this.name = name;
    }

    public int Ordinal => this.ordinal;

    public int Bits => this.bits;

    public string Name => this.name;

    public override string ToString() => this.name;

    public static ErrorCorrectionLevel ForBits(int bits)
    {
      if (bits < 0 || bits >= ErrorCorrectionLevel.FOR_BITS.Length)
        throw new ArgumentException();
      return ErrorCorrectionLevel.FOR_BITS[bits];
    }
  }
}
