// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.MaxiCode.Decoder.Decoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using MessagingToolkit.Barcode.Common.ReedSolomon;
using System;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode.MaxiCode.Decoder
{
  public sealed class Decoder
  {
    private const int ALL = 0;
    private const int EVEN = 1;
    private const int ODD = 2;
    private readonly ReedSolomonDecoder rsDecoder;

    public Decoder() => this.rsDecoder = new ReedSolomonDecoder(GenericGF.MaxicodeField64);

    public DecoderResult Decode(BitMatrix bits) => this.Decode(bits, (Dictionary<DecodeOptions, object>) null);

    public DecoderResult Decode(
      BitMatrix bits,
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      byte[] numArray1 = new BitMatrixParser(bits).ReadCodeWords();
      this.CorrectErrors(numArray1, 0, 10, 10, 0);
      int mode = (int) numArray1[0] & 15;
      byte[] numArray2;
      switch (mode)
      {
        case 2:
        case 3:
        case 4:
          this.CorrectErrors(numArray1, 20, 84, 40, 1);
          this.CorrectErrors(numArray1, 20, 84, 40, 2);
          numArray2 = new byte[94];
          break;
        case 5:
          this.CorrectErrors(numArray1, 20, 68, 56, 1);
          this.CorrectErrors(numArray1, 20, 68, 56, 2);
          numArray2 = new byte[78];
          break;
        default:
          throw MessagingToolkit.Barcode.FormatException.Instance;
      }
      Array.Copy((Array) numArray1, 0, (Array) numArray2, 0, 10);
      Array.Copy((Array) numArray1, 20, (Array) numArray2, 10, numArray2.Length - 10);
      return DecodedBitStreamParser.decode(numArray2, mode);
    }

    private void CorrectErrors(
      byte[] codewordBytes,
      int start,
      int dataCodewords,
      int ecCodewords,
      int mode)
    {
      int num1 = dataCodewords + ecCodewords;
      int num2 = mode == 0 ? 1 : 2;
      int[] received = new int[num1 / num2];
      for (int index = 0; index < num1; ++index)
      {
        if (mode == 0 || index % 2 == mode - 1)
          received[index / num2] = (int) codewordBytes[index + start] & (int) byte.MaxValue;
      }
      try
      {
        this.rsDecoder.Decode(received, ecCodewords / num2);
      }
      catch (ReedSolomonException ex)
      {
        throw ChecksumException.Instance;
      }
      for (int index = 0; index < dataCodewords; ++index)
      {
        if (mode == 0 || index % 2 == mode - 1)
          codewordBytes[index + start] = (byte) received[index / num2];
      }
    }
  }
}
