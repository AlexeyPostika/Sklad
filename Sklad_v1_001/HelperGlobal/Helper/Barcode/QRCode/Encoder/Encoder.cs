// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.QRCode.Encoder.Encoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using MessagingToolkit.Barcode.Common.ReedSolomon;
using MessagingToolkit.Barcode.Helper;
using MessagingToolkit.Barcode.QRCode.Decoder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MessagingToolkit.Barcode.QRCode.Encoder
{
  public sealed class Encoder
  {
    internal const string DefaultByteModeEncoding = "UTF-8";
    private static readonly int[] ALPHANUMERIC_TABLE = new int[96]
    {
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      36,
      -1,
      -1,
      -1,
      37,
      38,
      -1,
      -1,
      -1,
      -1,
      39,
      40,
      -1,
      41,
      42,
      43,
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9,
      44,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      10,
      11,
      12,
      13,
      14,
      15,
      16,
      17,
      18,
      19,
      20,
      21,
      22,
      23,
      24,
      25,
      26,
      27,
      28,
      29,
      30,
      31,
      32,
      33,
      34,
      35,
      -1,
      -1,
      -1,
      -1,
      -1
    };

    private Encoder()
    {
    }

    private static int CalculateMaskPenalty(ByteMatrix matrix) => MaskUtil.ApplyMaskPenaltyRule1(matrix) + MaskUtil.ApplyMaskPenaltyRule2(matrix) + MaskUtil.ApplyMaskPenaltyRule3(matrix) + MaskUtil.ApplyMaskPenaltyRule4(matrix);

    public static MessagingToolkit.Barcode.QRCode.Encoder.QRCode Encode(
      string content,
      ErrorCorrectionLevel ecLevel)
    {
      return MessagingToolkit.Barcode.QRCode.Encoder.Encoder.Encode(content, ecLevel, (Dictionary<EncodeOptions, object>) null);
    }

    public static MessagingToolkit.Barcode.QRCode.Encoder.QRCode Encode(
      string content,
      ErrorCorrectionLevel ecLevel,
      Dictionary<EncodeOptions, object> encodingOptions)
    {
      string str = encodingOptions == null ? (string) null : (string) BarcodeHelper.GetEncodeOptionType(encodingOptions, EncodeOptions.CharacterSet);
      if (string.IsNullOrEmpty(str))
        str = "UTF-8";
      Mode mode = MessagingToolkit.Barcode.QRCode.Encoder.Encoder.ChooseMode(content, str);
      BitArray bitArray1 = new BitArray();
      if (mode == Mode.Byte && !"UTF-8".Equals(str))
      {
        CharacterSetECI characterSetEciByName = CharacterSetECI.GetCharacterSetECIByName(str);
        if (characterSetEciByName != null)
          MessagingToolkit.Barcode.QRCode.Encoder.Encoder.AppendECI(characterSetEciByName, bitArray1);
      }
      MessagingToolkit.Barcode.QRCode.Encoder.Encoder.AppendModeInfo(mode, bitArray1);
      BitArray bitArray2 = new BitArray();
      MessagingToolkit.Barcode.QRCode.Encoder.Encoder.AppendBytes(content, mode, bitArray2, str);
      MessagingToolkit.Barcode.QRCode.Decoder.Version version1 = MessagingToolkit.Barcode.QRCode.Encoder.Encoder.ChooseVersion(bitArray1.GetSize() + mode.GetCharacterCountBits(MessagingToolkit.Barcode.QRCode.Decoder.Version.GetVersionForNumber(1)) + bitArray2.GetSize(), ecLevel);
      MessagingToolkit.Barcode.QRCode.Decoder.Version version2 = MessagingToolkit.Barcode.QRCode.Encoder.Encoder.ChooseVersion(bitArray1.GetSize() + mode.GetCharacterCountBits(version1) + bitArray2.GetSize(), ecLevel);
      BitArray bits = new BitArray();
      bits.AppendBitArray(bitArray1);
      MessagingToolkit.Barcode.QRCode.Encoder.Encoder.AppendLengthInfo(mode == Mode.Byte ? bitArray2.GetSizeInBytes() : content.Length, version2, mode, bits);
      bits.AppendBitArray(bitArray2);
      MessagingToolkit.Barcode.QRCode.Decoder.Version.ECBlocks ecBlocksForLevel = version2.GetECBlocksForLevel(ecLevel);
      int numDataBytes = version2.TotalCodewords - ecBlocksForLevel.TotalECCodewords;
      MessagingToolkit.Barcode.QRCode.Encoder.Encoder.TerminateBits(numDataBytes, bits);
      BitArray bitArray3 = MessagingToolkit.Barcode.QRCode.Encoder.Encoder.InterleaveWithECBytes(bits, version2.TotalCodewords, numDataBytes, ecBlocksForLevel.NumBlocks);
      MessagingToolkit.Barcode.QRCode.Encoder.QRCode qrCode = new MessagingToolkit.Barcode.QRCode.Encoder.QRCode();
      qrCode.ECLevel = ecLevel;
      qrCode.Mode = mode;
      qrCode.Version = version2;
      int dimensionForVersion = version2.DimensionForVersion;
      ByteMatrix matrix = new ByteMatrix(dimensionForVersion, dimensionForVersion);
      int maskPattern = MessagingToolkit.Barcode.QRCode.Encoder.Encoder.ChooseMaskPattern(bitArray3, ecLevel, version2, matrix);
      qrCode.MaskPattern = maskPattern;
      MatrixUtil.BuildMatrix(bitArray3, ecLevel, version2, maskPattern, matrix);
      qrCode.Matrix = matrix;
      return qrCode;
    }

    internal static int GetAlphanumericCode(int code) => code < MessagingToolkit.Barcode.QRCode.Encoder.Encoder.ALPHANUMERIC_TABLE.Length ? MessagingToolkit.Barcode.QRCode.Encoder.Encoder.ALPHANUMERIC_TABLE[code] : -1;

    public static Mode ChooseMode(string content) => MessagingToolkit.Barcode.QRCode.Encoder.Encoder.ChooseMode(content, (string) null);

    private static Mode ChooseMode(string content, string encoding)
    {
      if ("Shift-JIS".Equals(encoding))
        return !MessagingToolkit.Barcode.QRCode.Encoder.Encoder.IsOnlyDoubleByteKanji(content) ? Mode.Byte : Mode.Kanji;
      bool flag1 = false;
      bool flag2 = false;
      for (int index = 0; index < content.Length; ++index)
      {
        char code = content[index];
        switch (code)
        {
          case '0':
          case '1':
          case '2':
          case '3':
          case '4':
          case '5':
          case '6':
          case '7':
          case '8':
          case '9':
            flag1 = true;
            break;
          default:
            if (MessagingToolkit.Barcode.QRCode.Encoder.Encoder.GetAlphanumericCode((int) code) == -1)
              return Mode.Byte;
            flag2 = true;
            break;
        }
      }
      if (flag2)
        return Mode.Alphanumeric;
      return flag1 ? Mode.Numeric : Mode.Byte;
    }

    private static bool IsOnlyDoubleByteKanji(string content)
    {
      byte[] bytes;
      try
      {
        bytes = Encoding.GetEncoding("SHIFT-JIS").GetBytes(content);
      }
      catch (Exception ex)
      {
        return false;
      }
      int length = bytes.Length;
      if (length % 2 != 0)
        return false;
      for (int index = 0; index < length; index += 2)
      {
        int num = (int) bytes[index] & (int) byte.MaxValue;
        if ((num < 129 || num > 159) && (num < 224 || num > 235))
          return false;
      }
      return true;
    }

    private static int ChooseMaskPattern(
      BitArray bits,
      ErrorCorrectionLevel ecLevel,
      MessagingToolkit.Barcode.QRCode.Decoder.Version version,
      ByteMatrix matrix)
    {
      int num1 = int.MaxValue;
      int num2 = -1;
      for (int maskPattern = 0; maskPattern < 8; ++maskPattern)
      {
        MatrixUtil.BuildMatrix(bits, ecLevel, version, maskPattern, matrix);
        int maskPenalty = MessagingToolkit.Barcode.QRCode.Encoder.Encoder.CalculateMaskPenalty(matrix);
        if (maskPenalty < num1)
        {
          num1 = maskPenalty;
          num2 = maskPattern;
        }
      }
      return num2;
    }

    private static MessagingToolkit.Barcode.QRCode.Decoder.Version ChooseVersion(
      int numInputBits,
      ErrorCorrectionLevel ecLevel)
    {
      for (int versionNumber = 1; versionNumber <= 40; ++versionNumber)
      {
        MessagingToolkit.Barcode.QRCode.Decoder.Version versionForNumber = MessagingToolkit.Barcode.QRCode.Decoder.Version.GetVersionForNumber(versionNumber);
        if (versionForNumber.TotalCodewords - versionForNumber.GetECBlocksForLevel(ecLevel).TotalECCodewords >= (numInputBits + 7) / 8)
          return versionForNumber;
      }
      throw new BarcodeEncoderException("Data too big");
    }

    internal static void TerminateBits(int numDataBytes, BitArray bits)
    {
      int num1 = numDataBytes << 3;
      if (bits.GetSize() > num1)
        throw new BarcodeEncoderException("data bits cannot fit in the QR Code" + (object) bits.GetSize() + " > " + (object) num1);
      for (int index = 0; index < 4 && bits.GetSize() < num1; ++index)
        bits.AppendBit(false);
      int num2 = bits.GetSize() & 7;
      if (num2 > 0)
      {
        for (int index = num2; index < 8; ++index)
          bits.AppendBit(false);
      }
      int num3 = numDataBytes - bits.GetSizeInBytes();
      for (int index = 0; index < num3; ++index)
        bits.AppendBits((index & 1) == 0 ? 236 : 17, 8);
      if (bits.GetSize() != num1)
        throw new BarcodeEncoderException("Bits size does not equal capacity");
    }

    internal static void GetNumDataBytesAndNumECBytesForBlockID(
      int numTotalBytes,
      int numDataBytes,
      int numRSBlocks,
      int blockID,
      int[] numDataBytesInBlock,
      int[] numECBytesInBlock)
    {
      if (blockID >= numRSBlocks)
        throw new BarcodeEncoderException("Block ID too large");
      int num1 = numTotalBytes % numRSBlocks;
      int num2 = numRSBlocks - num1;
      int num3 = numTotalBytes / numRSBlocks;
      int num4 = num3 + 1;
      int num5 = numDataBytes / numRSBlocks;
      int num6 = num5 + 1;
      int num7 = num3 - num5;
      int num8 = num4 - num6;
      if (num7 != num8)
        throw new BarcodeEncoderException("EC bytes mismatch");
      if (numRSBlocks != num2 + num1)
        throw new BarcodeEncoderException("RS blocks mismatch");
      if (numTotalBytes != (num5 + num7) * num2 + (num6 + num8) * num1)
        throw new BarcodeEncoderException("Total bytes mismatch");
      if (blockID < num2)
      {
        numDataBytesInBlock[0] = num5;
        numECBytesInBlock[0] = num7;
      }
      else
      {
        numDataBytesInBlock[0] = num6;
        numECBytesInBlock[0] = num8;
      }
    }

    internal static BitArray InterleaveWithECBytes(
      BitArray bits,
      int numTotalBytes,
      int numDataBytes,
      int numRSBlocks)
    {
      if (bits.GetSizeInBytes() != numDataBytes)
        throw new BarcodeEncoderException("Number of bits and data bytes does not match");
      int num = 0;
      int val1_1 = 0;
      int val1_2 = 0;
      List<BlockPair> blockPairList = new List<BlockPair>(numRSBlocks);
      for (int blockID = 0; blockID < numRSBlocks; ++blockID)
      {
        int[] numDataBytesInBlock = new int[1];
        int[] numECBytesInBlock = new int[1];
        MessagingToolkit.Barcode.QRCode.Encoder.Encoder.GetNumDataBytesAndNumECBytesForBlockID(numTotalBytes, numDataBytes, numRSBlocks, blockID, numDataBytesInBlock, numECBytesInBlock);
        int length = numDataBytesInBlock[0];
        byte[] numArray = new byte[length];
        bits.ToBytes(8 * num, numArray, 0, length);
        byte[] ecBytes = MessagingToolkit.Barcode.QRCode.Encoder.Encoder.GenerateECBytes(numArray, numECBytesInBlock[0]);
        blockPairList.Add(new BlockPair(numArray, ecBytes));
        val1_1 = Math.Max(val1_1, length);
        val1_2 = Math.Max(val1_2, ecBytes.Length);
        num += numDataBytesInBlock[0];
      }
      if (numDataBytes != num)
        throw new BarcodeEncoderException("Data bytes does not match offset");
      BitArray bitArray = new BitArray();
      for (int index = 0; index < val1_1; ++index)
      {
        foreach (BlockPair blockPair in blockPairList)
        {
          byte[] dataBytes = blockPair.GetDataBytes();
          if (index < dataBytes.Length)
            bitArray.AppendBits((int) dataBytes[index], 8);
        }
      }
      for (int index = 0; index < val1_2; ++index)
      {
        foreach (BlockPair blockPair in blockPairList)
        {
          byte[] errorCorrectionBytes = blockPair.GetErrorCorrectionBytes();
          if (index < errorCorrectionBytes.Length)
            bitArray.AppendBits((int) errorCorrectionBytes[index], 8);
        }
      }
      return numTotalBytes == bitArray.GetSizeInBytes() ? bitArray : throw new BarcodeEncoderException("Interleaving error: " + (object) numTotalBytes + " and " + (object) bitArray.GetSizeInBytes() + " differ.");
    }

    internal static byte[] GenerateECBytes(byte[] dataBytes, int numEcBytesInBlock)
    {
      int length = dataBytes.Length;
      int[] toEncode = new int[length + numEcBytesInBlock];
      for (int index = 0; index < length; ++index)
        toEncode[index] = (int) dataBytes[index] & (int) byte.MaxValue;
      new ReedSolomonEncoder(GenericGF.QRCodeField256).Encode(toEncode, numEcBytesInBlock);
      byte[] ecBytes = new byte[numEcBytesInBlock];
      for (int index = 0; index < numEcBytesInBlock; ++index)
        ecBytes[index] = (byte) toEncode[length + index];
      return ecBytes;
    }

    internal static void AppendModeInfo(Mode mode, BitArray bits) => bits.AppendBits(mode.Bits, 4);

    internal static void AppendLengthInfo(
      int numLetters,
      MessagingToolkit.Barcode.QRCode.Decoder.Version version,
      Mode mode,
      BitArray bits)
    {
      int characterCountBits = mode.GetCharacterCountBits(version);
      if (numLetters >= 1 << characterCountBits)
        throw new BarcodeEncoderException(numLetters.ToString() + " is bigger than " + (object) ((1 << characterCountBits) - 1));
      bits.AppendBits(numLetters, characterCountBits);
    }

    internal static void AppendBytes(string content, Mode mode, BitArray bits, string encoding)
    {
      if (mode.Equals((object) Mode.Numeric))
        MessagingToolkit.Barcode.QRCode.Encoder.Encoder.AppendNumericBytes(content, bits);
      else if (mode.Equals((object) Mode.Alphanumeric))
        MessagingToolkit.Barcode.QRCode.Encoder.Encoder.AppendAlphanumericBytes(content, bits);
      else if (mode.Equals((object) Mode.Byte))
      {
        MessagingToolkit.Barcode.QRCode.Encoder.Encoder.Append8BitBytes(content, bits, encoding);
      }
      else
      {
        if (!mode.Equals((object) Mode.Kanji))
          throw new BarcodeEncoderException("Invalid mode: " + (object) mode);
        MessagingToolkit.Barcode.QRCode.Encoder.Encoder.AppendKanjiBytes(content, bits);
      }
    }

    internal static void AppendNumericBytes(string content, BitArray bits)
    {
      int length = content.Length;
      int index = 0;
      while (index < length)
      {
        int val = (int) content[index] - 48;
        if (index + 2 < length)
        {
          int num1 = (int) content[index + 1] - 48;
          int num2 = (int) content[index + 2] - 48;
          bits.AppendBits(val * 100 + num1 * 10 + num2, 10);
          index += 3;
        }
        else if (index + 1 < length)
        {
          int num = (int) content[index + 1] - 48;
          bits.AppendBits(val * 10 + num, 7);
          index += 2;
        }
        else
        {
          bits.AppendBits(val, 4);
          ++index;
        }
      }
    }

    internal static void AppendAlphanumericBytes(string content, BitArray bits)
    {
      int length = content.Length;
      int index = 0;
      while (index < length)
      {
        int alphanumericCode1 = MessagingToolkit.Barcode.QRCode.Encoder.Encoder.GetAlphanumericCode((int) content[index]);
        if (alphanumericCode1 == -1)
          throw new BarcodeEncoderException();
        if (index + 1 < length)
        {
          int alphanumericCode2 = MessagingToolkit.Barcode.QRCode.Encoder.Encoder.GetAlphanumericCode((int) content[index + 1]);
          if (alphanumericCode2 == -1)
            throw new BarcodeEncoderException();
          bits.AppendBits(alphanumericCode1 * 45 + alphanumericCode2, 11);
          index += 2;
        }
        else
        {
          bits.AppendBits(alphanumericCode1, 6);
          ++index;
        }
      }
    }

    internal static void Append8BitBytes(string content, BitArray bits, string encoding)
    {
      byte[] bytes;
      try
      {
        bytes = Encoding.GetEncoding(encoding).GetBytes(content);
      }
      catch (IOException ex)
      {
        throw new BarcodeEncoderException(ex.ToString());
      }
      foreach (byte val in bytes)
        bits.AppendBits((int) val, 8);
    }

    internal static void AppendKanjiBytes(string content, BitArray bits)
    {
      byte[] bytes;
      try
      {
        bytes = Encoding.GetEncoding("SHIFT-JIS").GetBytes(content);
      }
      catch (IOException ex)
      {
        throw new BarcodeEncoderException(ex.ToString());
      }
      int length = bytes.Length;
      for (int index = 0; index < length; index += 2)
      {
        int num1 = ((int) bytes[index] & (int) byte.MaxValue) << 8 | (int) bytes[index + 1] & (int) byte.MaxValue;
        int num2 = -1;
        if (num1 >= 33088 && num1 <= 40956)
          num2 = num1 - 33088;
        else if (num1 >= 57408 && num1 <= 60351)
          num2 = num1 - 49472;
        if (num2 == -1)
          throw new BarcodeEncoderException("Invalid byte sequence");
        int val = (num2 >> 8) * 192 + (num2 & (int) byte.MaxValue);
        bits.AppendBits(val, 13);
      }
    }

    private static void AppendECI(CharacterSetECI eci, BitArray bits)
    {
      bits.AppendBits(Mode.Eci.Bits, 4);
      bits.AppendBits(eci.Value, 8);
    }
  }
}
