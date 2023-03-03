// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Pdf417.Decoder.Decoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using MessagingToolkit.Barcode.Pdf417.Decoder.Ec;

namespace MessagingToolkit.Barcode.Pdf417.Decoder
{
  public sealed class Decoder
  {
    private const int MAX_ERRORS = 3;
    private const int MAX_EC_CODEWORDS = 512;
    private readonly ErrorCorrection errorCorrection;

    public Decoder() => this.errorCorrection = new ErrorCorrection();

    public DecoderResult Decode(bool[][] image)
    {
      int length = image.Length;
      BitMatrix bits = new BitMatrix(length);
      for (int y = 0; y < length; ++y)
      {
        for (int x = 0; x < length; ++x)
        {
          if (image[x][y])
            bits.Set(x, y);
        }
      }
      return this.Decode(bits);
    }

    public DecoderResult Decode(BitMatrix bits)
    {
      BitMatrixParser bitMatrixParser = new BitMatrixParser(bits);
      int[] codewords = bitMatrixParser.ReadCodewords();
      if (codewords.Length == 0)
        throw FormatException.Instance;
      int ecLevel = bitMatrixParser.ECLevel;
      int numECCodewords = 1 << ecLevel + 1;
      int[] erasures = bitMatrixParser.Erasures;
      this.CorrectErrors(codewords, erasures, numECCodewords);
      MessagingToolkit.Barcode.Pdf417.Decoder.Decoder.VerifyCodewordCount(codewords, numECCodewords);
      return DecodedBitStreamParser.Decode(codewords, ecLevel.ToString(), erasures.Length);
    }

    private static void VerifyCodewordCount(int[] codewords, int numECCodewords)
    {
      int num = codewords.Length >= 4 ? codewords[0] : throw FormatException.Instance;
      if (num > codewords.Length)
        throw FormatException.Instance;
      if (num != 0)
        return;
      if (numECCodewords >= codewords.Length)
        throw FormatException.Instance;
      codewords[0] = codewords.Length - numECCodewords;
    }

    private void CorrectErrors(int[] codewords, int[] erasures, int numECCodewords)
    {
      if (erasures.Length > numECCodewords / 2 + 3 || numECCodewords < 0 || numECCodewords > 512)
        throw ChecksumException.Instance;
      this.errorCorrection.Decode(codewords, numECCodewords, erasures);
    }
  }
}
