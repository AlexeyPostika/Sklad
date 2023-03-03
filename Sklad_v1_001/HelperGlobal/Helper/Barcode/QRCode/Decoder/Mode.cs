// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.QRCode.Decoder.Mode
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;

namespace MessagingToolkit.Barcode.QRCode.Decoder
{
  public sealed class Mode
  {
    public static readonly Mode Terminator = new Mode(new int[3], 0, "TERMINATOR");
    public static readonly Mode Numeric = new Mode(new int[3]
    {
      10,
      12,
      14
    }, 1, nameof (Numeric));
    public static readonly Mode Alphanumeric = new Mode(new int[3]
    {
      9,
      11,
      13
    }, 2, "ALPHANUMERIC");
    public static readonly Mode StructuredAppend = new Mode(new int[3], 3, "STRUCTURED_APPEND");
    public static readonly Mode Byte = new Mode(new int[3]
    {
      8,
      16,
      16
    }, 4, nameof (Byte));
    public static readonly Mode Eci = new Mode((int[]) null, 7, "ECI");
    public static readonly Mode Kanji = new Mode(new int[3]
    {
      8,
      10,
      12
    }, 8, "KANJI");
    public static readonly Mode Fnc1FirstPosition = new Mode((int[]) null, 5, "FNC1_FIRST_POSITION");
    public static readonly Mode Fnc1SecondPosition = new Mode((int[]) null, 9, "FNC1_SECOND_POSITION");
    public static readonly Mode Hanzi = new Mode(new int[3]
    {
      8,
      10,
      12
    }, 13, "HANZI");
    private int[] characterCountBitsForVersions;
    private int bits;
    private string name;

    public int Bits => this.bits;

    public string Name => this.name;

    private Mode(int[] characterCountBitsForVersions, int bits, string name)
    {
      this.characterCountBitsForVersions = characterCountBitsForVersions;
      this.bits = bits;
      this.name = name;
    }

    public static Mode ForBits(int bits)
    {
      switch (bits)
      {
        case 0:
          return Mode.Terminator;
        case 1:
          return Mode.Numeric;
        case 2:
          return Mode.Alphanumeric;
        case 3:
          return Mode.StructuredAppend;
        case 4:
          return Mode.Byte;
        case 5:
          return Mode.Fnc1FirstPosition;
        case 7:
          return Mode.Eci;
        case 8:
          return Mode.Kanji;
        case 9:
          return Mode.Fnc1SecondPosition;
        case 13:
          return Mode.Hanzi;
        default:
          throw new ArgumentException();
      }
    }

    public int GetCharacterCountBits(Version version)
    {
      if (this.characterCountBitsForVersions == null)
        throw new ArgumentException("Character count doesn't apply to this mode");
      int versionNumber = version.VersionNumber;
      return this.characterCountBitsForVersions[versionNumber > 9 ? (versionNumber > 26 ? 2 : 1) : 0];
    }

    public override string ToString() => this.name;
  }
}
