// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.Decoders.Decoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using MessagingToolkit.Barcode.Common.ReedSolomon;

namespace MessagingToolkit.Barcode.DataMatrix.Decoders
{
  public sealed class Decoder
  {
    private readonly ReedSolomonDecoder rsDecoder;

    public Decoder() => this.rsDecoder = new ReedSolomonDecoder(GenericGF.DataMatrixField256);

    public DecoderResult Decode(bool[][] image)
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
      return this.Decode(bits);
    }

    public DecoderResult Decode(BitMatrix bits)
    {
      BitMatrixParser bitMatrixParser = new BitMatrixParser(bits);
      Version version = bitMatrixParser.GetVersion();
      DataBlock[] dataBlocks = DataBlock.GetDataBlocks(bitMatrixParser.ReadCodewords(), version);
      int length1 = dataBlocks.Length;
      int length2 = 0;
      for (int index = 0; index < length1; ++index)
        length2 += dataBlocks[index].GetNumDataCodewords();
      byte[] bytes = new byte[length2];
      for (int index1 = 0; index1 < length1; ++index1)
      {
        DataBlock dataBlock = dataBlocks[index1];
        byte[] codewords = dataBlock.GetCodewords();
        int numDataCodewords = dataBlock.GetNumDataCodewords();
        this.CorrectErrors(codewords, numDataCodewords);
        for (int index2 = 0; index2 < numDataCodewords; ++index2)
          bytes[index2 * length1 + index1] = codewords[index2];
      }
      return DecodedBitStreamParser.Decode(bytes);
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
