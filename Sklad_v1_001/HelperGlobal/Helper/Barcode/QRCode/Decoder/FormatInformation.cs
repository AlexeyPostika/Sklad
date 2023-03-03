// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.QRCode.Decoder.FormatInformation
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.QRCode.Decoder
{
  internal sealed class FormatInformation
  {
    private const int FORMAT_INFO_MASK_QR = 21522;
    private static readonly int[][] FORMAT_INFO_DECODE_LOOKUP = new int[32][]
    {
      new int[2]{ 21522, 0 },
      new int[2]{ 20773, 1 },
      new int[2]{ 24188, 2 },
      new int[2]{ 23371, 3 },
      new int[2]{ 17913, 4 },
      new int[2]{ 16590, 5 },
      new int[2]{ 20375, 6 },
      new int[2]{ 19104, 7 },
      new int[2]{ 30660, 8 },
      new int[2]{ 29427, 9 },
      new int[2]{ 32170, 10 },
      new int[2]{ 30877, 11 },
      new int[2]{ 26159, 12 },
      new int[2]{ 25368, 13 },
      new int[2]{ 27713, 14 },
      new int[2]{ 26998, 15 },
      new int[2]{ 5769, 16 },
      new int[2]{ 5054, 17 },
      new int[2]{ 7399, 18 },
      new int[2]{ 6608, 19 },
      new int[2]{ 1890, 20 },
      new int[2]{ 597, 21 },
      new int[2]{ 3340, 22 },
      new int[2]{ 2107, 23 },
      new int[2]{ 13663, 24 },
      new int[2]{ 12392, 25 },
      new int[2]{ 16177, 26 },
      new int[2]{ 14854, 27 },
      new int[2]{ 9396, 28 },
      new int[2]{ 8579, 29 },
      new int[2]{ 11994, 30 },
      new int[2]{ 11245, 31 }
    };
    private static readonly int[] BITS_SET_IN_HALF_BYTE = new int[16]
    {
      0,
      1,
      1,
      2,
      1,
      2,
      2,
      3,
      1,
      2,
      2,
      3,
      2,
      3,
      3,
      4
    };
    private readonly ErrorCorrectionLevel errorCorrectionLevel;
    private readonly byte dataMask;

    private FormatInformation(int formatInfo)
    {
      this.errorCorrectionLevel = ErrorCorrectionLevel.ForBits(formatInfo >> 3 & 3);
      this.dataMask = (byte) (formatInfo & 7);
    }

    internal static int NumBitsDiffering(int a, int b)
    {
      a ^= b;
      return FormatInformation.BITS_SET_IN_HALF_BYTE[a & 15] + FormatInformation.BITS_SET_IN_HALF_BYTE[(int) ((uint) a >> 4) & 15] + FormatInformation.BITS_SET_IN_HALF_BYTE[(int) ((uint) a >> 8) & 15] + FormatInformation.BITS_SET_IN_HALF_BYTE[(int) ((uint) a >> 12) & 15] + FormatInformation.BITS_SET_IN_HALF_BYTE[(int) ((uint) a >> 16) & 15] + FormatInformation.BITS_SET_IN_HALF_BYTE[(int) ((uint) a >> 20) & 15] + FormatInformation.BITS_SET_IN_HALF_BYTE[(int) ((uint) a >> 24) & 15] + FormatInformation.BITS_SET_IN_HALF_BYTE[(int) ((uint) a >> 28) & 15];
    }

    internal static FormatInformation DecodeFormatInformation(
      int maskedFormatInfo1,
      int maskedFormatInfo2)
    {
      return FormatInformation.DoDecodeFormatInformation(maskedFormatInfo1, maskedFormatInfo2) ?? FormatInformation.DoDecodeFormatInformation(maskedFormatInfo1 ^ 21522, maskedFormatInfo2 ^ 21522);
    }

    private static FormatInformation DoDecodeFormatInformation(
      int maskedFormatInfo1,
      int maskedFormatInfo2)
    {
      int num1 = int.MaxValue;
      int formatInfo = 0;
      foreach (int[] numArray in FormatInformation.FORMAT_INFO_DECODE_LOOKUP)
      {
        int b = numArray[0];
        if (b == maskedFormatInfo1 || b == maskedFormatInfo2)
          return new FormatInformation(numArray[1]);
        int num2 = FormatInformation.NumBitsDiffering(maskedFormatInfo1, b);
        if (num2 < num1)
        {
          formatInfo = numArray[1];
          num1 = num2;
        }
        if (maskedFormatInfo1 != maskedFormatInfo2)
        {
          int num3 = FormatInformation.NumBitsDiffering(maskedFormatInfo2, b);
          if (num3 < num1)
          {
            formatInfo = numArray[1];
            num1 = num3;
          }
        }
      }
      return num1 <= 3 ? new FormatInformation(formatInfo) : (FormatInformation) null;
    }

    internal ErrorCorrectionLevel GetErrorCorrectionLevel() => this.errorCorrectionLevel;

    internal byte GetDataMask() => this.dataMask;

    public override int GetHashCode() => this.errorCorrectionLevel.Ordinal << 3 | (int) this.dataMask;

    public override bool Equals(object o)
    {
      if (!(o is FormatInformation))
        return false;
      FormatInformation formatInformation = (FormatInformation) o;
      return this.errorCorrectionLevel == formatInformation.errorCorrectionLevel && (int) this.dataMask == (int) formatInformation.dataMask;
    }
  }
}
