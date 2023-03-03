// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.QRCode.Decoder.Decoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using MessagingToolkit.Barcode.Common.ReedSolomon;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode.QRCode.Decoder
{
  public sealed class Decoder
  {
    private readonly ReedSolomonDecoder rsDecoder;

    public Decoder() => this.rsDecoder = new ReedSolomonDecoder(GenericGF.QRCodeField256);

    public DecoderResult Decode(bool[][] image) => this.Decode(image, (Dictionary<DecodeOptions, object>) null);

    public DecoderResult Decode(
      bool[][] image,
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      int length = image.Length;
      BitMatrix bits = new BitMatrix(length);
      for (int y = 0; y < length; ++y)
      {
        for (int x = 0; x < length; ++x)
        {
          if (image[y][x])
            bits.Set(x, y);
        }
      }
      return this.Decode(bits, decodingOptions);
    }

    public DecoderResult Decode(BitMatrix bits) => this.Decode(bits, (Dictionary<DecodeOptions, object>) null);

    public DecoderResult Decode(
      BitMatrix bits,
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      BitMatrixParser bitMatrixParser = new BitMatrixParser(bits);
      Version version = bitMatrixParser.ReadVersion();
      ErrorCorrectionLevel errorCorrectionLevel = bitMatrixParser.ReadFormatInformation().GetErrorCorrectionLevel();
      DataBlock[] dataBlocks = DataBlock.GetDataBlocks(bitMatrixParser.ReadCodewords(), version, errorCorrectionLevel);
      int length = 0;
      foreach (DataBlock dataBlock in dataBlocks)
        length += dataBlock.NumDataCodewords;
      byte[] bytes = new byte[length];
      int num = 0;
      foreach (DataBlock dataBlock in dataBlocks)
      {
        byte[] codewords = dataBlock.Codewords;
        int numDataCodewords = dataBlock.NumDataCodewords;
        this.CorrectErrors(codewords, numDataCodewords);
        for (int index = 0; index < numDataCodewords; ++index)
          bytes[num++] = codewords[index];
      }
      return DecodedBitStreamParser.Decode(bytes, version, errorCorrectionLevel, decodingOptions);
    }

    private void CorrectErrors(byte[] codewordBytes, int numDataCodewords)
    {
      int length = codewordBytes.Length;
      int[] received = new int[length];
      for (int index = 0; index < length; ++index)
        received[index] = (int) codewordBytes[index] & (int) byte.MaxValue;
      int twoS = codewordBytes.Length - numDataCodewords;
      try
      {
        this.rsDecoder.Decode(received, twoS);
      }
      catch (ReedSolomonException ex)
      {
        throw ChecksumException.Instance;
      }
      for (int index = 0; index < numDataCodewords; ++index)
        codewordBytes[index] = (byte) received[index];
    }
  }
}
