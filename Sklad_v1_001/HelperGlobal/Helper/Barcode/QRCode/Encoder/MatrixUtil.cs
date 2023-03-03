// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.QRCode.Encoder.MatrixUtil
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using MessagingToolkit.Barcode.QRCode.Decoder;

namespace MessagingToolkit.Barcode.QRCode.Encoder
{
  internal sealed class MatrixUtil
  {
    private const int VERSION_INFO_POLY = 7973;
    private const int TYPE_INFO_POLY = 1335;
    private const int TYPE_INFO_MASK_PATTERN = 21522;
    private static readonly int[][] POSITION_DETECTION_PATTERN = new int[7][]
    {
      new int[7]{ 1, 1, 1, 1, 1, 1, 1 },
      new int[7]{ 1, 0, 0, 0, 0, 0, 1 },
      new int[7]{ 1, 0, 1, 1, 1, 0, 1 },
      new int[7]{ 1, 0, 1, 1, 1, 0, 1 },
      new int[7]{ 1, 0, 1, 1, 1, 0, 1 },
      new int[7]{ 1, 0, 0, 0, 0, 0, 1 },
      new int[7]{ 1, 1, 1, 1, 1, 1, 1 }
    };
    private static readonly int[][] POSITION_ADJUSTMENT_PATTERN = new int[5][]
    {
      new int[5]{ 1, 1, 1, 1, 1 },
      new int[5]{ 1, 0, 0, 0, 1 },
      new int[5]{ 1, 0, 1, 0, 1 },
      new int[5]{ 1, 0, 0, 0, 1 },
      new int[5]{ 1, 1, 1, 1, 1 }
    };
    private static readonly int[][] POSITION_ADJUSTMENT_PATTERN_COORDINATE_TABLE = new int[40][]
    {
      new int[7]{ -1, -1, -1, -1, -1, -1, -1 },
      new int[7]{ 6, 18, -1, -1, -1, -1, -1 },
      new int[7]{ 6, 22, -1, -1, -1, -1, -1 },
      new int[7]{ 6, 26, -1, -1, -1, -1, -1 },
      new int[7]{ 6, 30, -1, -1, -1, -1, -1 },
      new int[7]{ 6, 34, -1, -1, -1, -1, -1 },
      new int[7]{ 6, 22, 38, -1, -1, -1, -1 },
      new int[7]{ 6, 24, 42, -1, -1, -1, -1 },
      new int[7]{ 6, 26, 46, -1, -1, -1, -1 },
      new int[7]{ 6, 28, 50, -1, -1, -1, -1 },
      new int[7]{ 6, 30, 54, -1, -1, -1, -1 },
      new int[7]{ 6, 32, 58, -1, -1, -1, -1 },
      new int[7]{ 6, 34, 62, -1, -1, -1, -1 },
      new int[7]{ 6, 26, 46, 66, -1, -1, -1 },
      new int[7]{ 6, 26, 48, 70, -1, -1, -1 },
      new int[7]{ 6, 26, 50, 74, -1, -1, -1 },
      new int[7]{ 6, 30, 54, 78, -1, -1, -1 },
      new int[7]{ 6, 30, 56, 82, -1, -1, -1 },
      new int[7]{ 6, 30, 58, 86, -1, -1, -1 },
      new int[7]{ 6, 34, 62, 90, -1, -1, -1 },
      new int[7]{ 6, 28, 50, 72, 94, -1, -1 },
      new int[7]{ 6, 26, 50, 74, 98, -1, -1 },
      new int[7]{ 6, 30, 54, 78, 102, -1, -1 },
      new int[7]{ 6, 28, 54, 80, 106, -1, -1 },
      new int[7]{ 6, 32, 58, 84, 110, -1, -1 },
      new int[7]{ 6, 30, 58, 86, 114, -1, -1 },
      new int[7]{ 6, 34, 62, 90, 118, -1, -1 },
      new int[7]{ 6, 26, 50, 74, 98, 122, -1 },
      new int[7]{ 6, 30, 54, 78, 102, 126, -1 },
      new int[7]{ 6, 26, 52, 78, 104, 130, -1 },
      new int[7]{ 6, 30, 56, 82, 108, 134, -1 },
      new int[7]{ 6, 34, 60, 86, 112, 138, -1 },
      new int[7]{ 6, 30, 58, 86, 114, 142, -1 },
      new int[7]{ 6, 34, 62, 90, 118, 146, -1 },
      new int[7]{ 6, 30, 54, 78, 102, 126, 150 },
      new int[7]{ 6, 24, 50, 76, 102, 128, 154 },
      new int[7]{ 6, 28, 54, 80, 106, 132, 158 },
      new int[7]{ 6, 32, 58, 84, 110, 136, 162 },
      new int[7]{ 6, 26, 54, 82, 110, 138, 166 },
      new int[7]{ 6, 30, 58, 86, 114, 142, 170 }
    };
    private static readonly int[][] TYPE_INFO_COORDINATES = new int[15][]
    {
      new int[2]{ 8, 0 },
      new int[2]{ 8, 1 },
      new int[2]{ 8, 2 },
      new int[2]{ 8, 3 },
      new int[2]{ 8, 4 },
      new int[2]{ 8, 5 },
      new int[2]{ 8, 7 },
      new int[2]{ 8, 8 },
      new int[2]{ 7, 8 },
      new int[2]{ 5, 8 },
      new int[2]{ 4, 8 },
      new int[2]{ 3, 8 },
      new int[2]{ 2, 8 },
      new int[2]{ 1, 8 },
      new int[2]{ 0, 8 }
    };

    private MatrixUtil()
    {
    }

    internal static void ClearMatrix(ByteMatrix matrix) => matrix.Clear(byte.MaxValue);

    internal static void BuildMatrix(
      BitArray dataBits,
      ErrorCorrectionLevel ecLevel,
      Version version,
      int maskPattern,
      ByteMatrix matrix)
    {
      MatrixUtil.ClearMatrix(matrix);
      MatrixUtil.EmbedBasicPatterns(version, matrix);
      MatrixUtil.EmbedTypeInfo(ecLevel, maskPattern, matrix);
      MatrixUtil.MaybeEmbedVersionInfo(version, matrix);
      MatrixUtil.EmbedDataBits(dataBits, maskPattern, matrix);
    }

    internal static void EmbedBasicPatterns(Version version, ByteMatrix matrix)
    {
      MatrixUtil.EmbedPositionDetectionPatternsAndSeparators(matrix);
      MatrixUtil.EmbedDarkDotAtLeftBottomCorner(matrix);
      MatrixUtil.MaybeEmbedPositionAdjustmentPatterns(version, matrix);
      MatrixUtil.EmbedTimingPatterns(matrix);
    }

    internal static void EmbedTypeInfo(
      ErrorCorrectionLevel ecLevel,
      int maskPattern,
      ByteMatrix matrix)
    {
      BitArray bits = new BitArray();
      MatrixUtil.MakeTypeInfoBits(ecLevel, maskPattern, bits);
      for (int index = 0; index < bits.Size; ++index)
      {
        bool val = bits.Get(bits.Size - 1 - index);
        int x1 = MatrixUtil.TYPE_INFO_COORDINATES[index][0];
        int y1 = MatrixUtil.TYPE_INFO_COORDINATES[index][1];
        matrix.Set(x1, y1, val);
        if (index < 8)
        {
          int x2 = matrix.Width - index - 1;
          int y2 = 8;
          matrix.Set(x2, y2, val);
        }
        else
        {
          int x3 = 8;
          int y3 = matrix.Height - 7 + (index - 8);
          matrix.Set(x3, y3, val);
        }
      }
    }

    internal static void MaybeEmbedVersionInfo(Version version, ByteMatrix matrix)
    {
      if (version.VersionNumber < 7)
        return;
      BitArray bits = new BitArray();
      MatrixUtil.MakeVersionInfoBits(version, bits);
      int i = 17;
      for (int index1 = 0; index1 < 6; ++index1)
      {
        for (int index2 = 0; index2 < 3; ++index2)
        {
          bool val = bits.Get(i);
          --i;
          matrix.Set(index1, matrix.Height - 11 + index2, val);
          matrix.Set(matrix.Height - 11 + index2, index1, val);
        }
      }
    }

    internal static void EmbedDataBits(BitArray dataBits, int maskPattern, ByteMatrix matrix)
    {
      int i = 0;
      int num1 = -1;
      int num2 = matrix.Width - 1;
      int y = matrix.Height - 1;
      for (; num2 > 0; num2 -= 2)
      {
        if (num2 == 6)
          --num2;
        for (; y >= 0 && y < matrix.Height; y += num1)
        {
          for (int index = 0; index < 2; ++index)
          {
            int x = num2 - index;
            if (MatrixUtil.IsEmpty((int) matrix.Get(x, y)))
            {
              bool val;
              if (i < dataBits.Size)
              {
                val = dataBits.Get(i);
                ++i;
              }
              else
                val = false;
              if (maskPattern != -1 && MaskUtil.GetDataMaskBit(maskPattern, x, y))
                val = !val;
              matrix.Set(x, y, val);
            }
          }
        }
        num1 = -num1;
        y += num1;
      }
      if (i != dataBits.Size)
        throw new BarcodeEncoderException("Not all bits consumed: " + (object) i + (object) '/' + (object) dataBits.Size);
    }

    internal static int FindMSBSet(int value)
    {
      int msbSet = 0;
      while (value != 0)
      {
        value = (int) ((uint) value >> 1);
        ++msbSet;
      }
      return msbSet;
    }

    internal static int CalculateBCHCode(int val, int poly)
    {
      int msbSet = MatrixUtil.FindMSBSet(poly);
      val <<= msbSet - 1;
      while (MatrixUtil.FindMSBSet(val) >= msbSet)
        val ^= poly << MatrixUtil.FindMSBSet(val) - msbSet;
      return val;
    }

    internal static void MakeTypeInfoBits(
      ErrorCorrectionLevel ecLevel,
      int maskPattern,
      BitArray bits)
    {
      if (!MessagingToolkit.Barcode.QRCode.Encoder.QRCode.IsValidMaskPattern(maskPattern))
        throw new BarcodeEncoderException("Invalid mask pattern");
      int val = ecLevel.Bits << 3 | maskPattern;
      bits.AppendBits(val, 5);
      int bchCode = MatrixUtil.CalculateBCHCode(val, 1335);
      bits.AppendBits(bchCode, 10);
      BitArray other = new BitArray();
      other.AppendBits(21522, 15);
      bits.Xor(other);
      if (bits.Size != 15)
        throw new BarcodeEncoderException("should not happen but we got: " + (object) bits.GetSize());
    }

    internal static void MakeVersionInfoBits(Version version, BitArray bits)
    {
      bits.AppendBits(version.VersionNumber, 6);
      int bchCode = MatrixUtil.CalculateBCHCode(version.VersionNumber, 7973);
      bits.AppendBits(bchCode, 12);
      if (bits.Size != 18)
        throw new BarcodeEncoderException("should not happen but we got: " + (object) bits.GetSize());
    }

    private static bool IsEmpty(int value) => value == (int) byte.MaxValue;

    private static void EmbedTimingPatterns(ByteMatrix matrix)
    {
      for (int index = 8; index < matrix.Width - 8; ++index)
      {
        int val = (index + 1) % 2;
        if (MatrixUtil.IsEmpty((int) matrix.Get(index, 6)))
          matrix.Set(index, 6, val);
        if (MatrixUtil.IsEmpty((int) matrix.Get(6, index)))
          matrix.Set(6, index, val);
      }
    }

    private static void EmbedDarkDotAtLeftBottomCorner(ByteMatrix matrix)
    {
      if (matrix.Get(8, matrix.Height - 8) == (byte) 0)
        throw new BarcodeEncoderException();
      matrix.Set(8, matrix.Height - 8, 1);
    }

    private static void EmbedHorizontalSeparationPattern(int xStart, int yStart, ByteMatrix matrix)
    {
      for (int index = 0; index < 8; ++index)
      {
        if (!MatrixUtil.IsEmpty((int) matrix.Get(xStart + index, yStart)))
          throw new BarcodeEncoderException();
        matrix.Set(xStart + index, yStart, 0);
      }
    }

    private static void EmbedVerticalSeparationPattern(int xStart, int yStart, ByteMatrix matrix)
    {
      for (int index = 0; index < 7; ++index)
      {
        if (!MatrixUtil.IsEmpty((int) matrix.Get(xStart, yStart + index)))
          throw new BarcodeEncoderException();
        matrix.Set(xStart, yStart + index, 0);
      }
    }

    private static void EmbedPositionAdjustmentPattern(int xStart, int yStart, ByteMatrix matrix)
    {
      for (int index1 = 0; index1 < 5; ++index1)
      {
        for (int index2 = 0; index2 < 5; ++index2)
          matrix.Set(xStart + index2, yStart + index1, MatrixUtil.POSITION_ADJUSTMENT_PATTERN[index1][index2]);
      }
    }

    private static void EmbedPositionDetectionPattern(int xStart, int yStart, ByteMatrix matrix)
    {
      if (MatrixUtil.POSITION_DETECTION_PATTERN[0].Length != 7 || MatrixUtil.POSITION_DETECTION_PATTERN.Length != 7)
        throw new BarcodeEncoderException("Bad position detection pattern");
      for (int index1 = 0; index1 < 7; ++index1)
      {
        for (int index2 = 0; index2 < 7; ++index2)
          matrix.Set(xStart + index2, yStart + index1, MatrixUtil.POSITION_DETECTION_PATTERN[index1][index2]);
      }
    }

    private static void EmbedPositionDetectionPatternsAndSeparators(ByteMatrix matrix)
    {
      int length = MatrixUtil.POSITION_DETECTION_PATTERN[0].Length;
      MatrixUtil.EmbedPositionDetectionPattern(0, 0, matrix);
      MatrixUtil.EmbedPositionDetectionPattern(matrix.Width - length, 0, matrix);
      MatrixUtil.EmbedPositionDetectionPattern(0, matrix.Width - length, matrix);
      int num = 8;
      MatrixUtil.EmbedHorizontalSeparationPattern(0, num - 1, matrix);
      MatrixUtil.EmbedHorizontalSeparationPattern(matrix.Width - num, num - 1, matrix);
      MatrixUtil.EmbedHorizontalSeparationPattern(0, matrix.Width - num, matrix);
      int xStart = 7;
      MatrixUtil.EmbedVerticalSeparationPattern(xStart, 0, matrix);
      MatrixUtil.EmbedVerticalSeparationPattern(matrix.Height - xStart - 1, 0, matrix);
      MatrixUtil.EmbedVerticalSeparationPattern(xStart, matrix.Height - xStart, matrix);
    }

    private static void MaybeEmbedPositionAdjustmentPatterns(Version version, ByteMatrix matrix)
    {
      if (version.VersionNumber < 2)
        return;
      int index1 = version.VersionNumber - 1;
      int[] numArray = MatrixUtil.POSITION_ADJUSTMENT_PATTERN_COORDINATE_TABLE[index1];
      int length = MatrixUtil.POSITION_ADJUSTMENT_PATTERN_COORDINATE_TABLE[index1].Length;
      for (int index2 = 0; index2 < length; ++index2)
      {
        for (int index3 = 0; index3 < length; ++index3)
        {
          int y = numArray[index2];
          int x = numArray[index3];
          if (x != -1 && y != -1 && MatrixUtil.IsEmpty((int) matrix.Get(x, y)))
            MatrixUtil.EmbedPositionAdjustmentPattern(x - 2, y - 2, matrix);
        }
      }
    }
  }
}
