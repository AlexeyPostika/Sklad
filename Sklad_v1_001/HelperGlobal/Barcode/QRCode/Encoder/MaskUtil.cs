// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.QRCode.Encoder.MaskUtil
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;

namespace MessagingToolkit.Barcode.QRCode.Encoder
{
  internal sealed class MaskUtil
  {
    private const int N1 = 3;
    private const int N2 = 3;
    private const int N3 = 40;
    private const int N4 = 10;

    public static int ApplyMaskPenaltyRule1(ByteMatrix matrix) => MaskUtil.ApplyMaskPenaltyRule1Internal(matrix, true) + MaskUtil.ApplyMaskPenaltyRule1Internal(matrix, false);

    public static int ApplyMaskPenaltyRule2(ByteMatrix matrix)
    {
      int num1 = 0;
      byte[][] array = matrix.Array;
      int width = matrix.Width;
      int height = matrix.Height;
      for (int index1 = 0; index1 < height - 1; ++index1)
      {
        for (int index2 = 0; index2 < width - 1; ++index2)
        {
          int num2 = (int) array[index1][index2];
          if (num2 == (int) array[index1][index2 + 1] && num2 == (int) array[index1 + 1][index2] && num2 == (int) array[index1 + 1][index2 + 1])
            ++num1;
        }
      }
      return 3 * num1;
    }

    public static int ApplyMaskPenaltyRule3(ByteMatrix matrix)
    {
      int num = 0;
      byte[][] array = matrix.Array;
      int width = matrix.Width;
      int height = matrix.Height;
      for (int index1 = 0; index1 < height; ++index1)
      {
        for (int index2 = 0; index2 < width; ++index2)
        {
          if (index2 + 6 < width && array[index1][index2] == (byte) 1 && array[index1][index2 + 1] == (byte) 0 && array[index1][index2 + 2] == (byte) 1 && array[index1][index2 + 3] == (byte) 1 && array[index1][index2 + 4] == (byte) 1 && array[index1][index2 + 5] == (byte) 0 && array[index1][index2 + 6] == (byte) 1 && (index2 + 10 < width && array[index1][index2 + 7] == (byte) 0 && array[index1][index2 + 8] == (byte) 0 && array[index1][index2 + 9] == (byte) 0 && array[index1][index2 + 10] == (byte) 0 || index2 - 4 >= 0 && array[index1][index2 - 1] == (byte) 0 && array[index1][index2 - 2] == (byte) 0 && array[index1][index2 - 3] == (byte) 0 && array[index1][index2 - 4] == (byte) 0))
            num += 40;
          if (index1 + 6 < height && array[index1][index2] == (byte) 1 && array[index1 + 1][index2] == (byte) 0 && array[index1 + 2][index2] == (byte) 1 && array[index1 + 3][index2] == (byte) 1 && array[index1 + 4][index2] == (byte) 1 && array[index1 + 5][index2] == (byte) 0 && array[index1 + 6][index2] == (byte) 1 && (index1 + 10 < height && array[index1 + 7][index2] == (byte) 0 && array[index1 + 8][index2] == (byte) 0 && array[index1 + 9][index2] == (byte) 0 && array[index1 + 10][index2] == (byte) 0 || index1 - 4 >= 0 && array[index1 - 1][index2] == (byte) 0 && array[index1 - 2][index2] == (byte) 0 && array[index1 - 3][index2] == (byte) 0 && array[index1 - 4][index2] == (byte) 0))
            num += 40;
        }
      }
      return num;
    }

    public static int ApplyMaskPenaltyRule4(ByteMatrix matrix)
    {
      int num1 = 0;
      byte[][] array = matrix.Array;
      int width = matrix.Width;
      int height = matrix.Height;
      for (int index1 = 0; index1 < height; ++index1)
      {
        byte[] numArray = array[index1];
        for (int index2 = 0; index2 < width; ++index2)
        {
          if (numArray[index2] == (byte) 1)
            ++num1;
        }
      }
      int num2 = matrix.Height * matrix.Width;
      return (int) (Math.Abs((double) num1 / (double) num2 - 0.5) * 20.0) * 10;
    }

    public static bool GetDataMaskBit(int maskPattern, int x, int y)
    {
      int num1;
      switch (maskPattern)
      {
        case 0:
          num1 = y + x & 1;
          break;
        case 1:
          num1 = y & 1;
          break;
        case 2:
          num1 = x % 3;
          break;
        case 3:
          num1 = (y + x) % 3;
          break;
        case 4:
          num1 = (int) ((uint) y >> 1) + x / 3 & 1;
          break;
        case 5:
          int num2 = y * x;
          num1 = (num2 & 1) + num2 % 3;
          break;
        case 6:
          int num3 = y * x;
          num1 = (num3 & 1) + num3 % 3 & 1;
          break;
        case 7:
          num1 = y * x % 3 + (y + x & 1) & 1;
          break;
        default:
          throw new ArgumentException("Invalid mask pattern: " + (object) maskPattern);
      }
      return num1 == 0;
    }

    private static int ApplyMaskPenaltyRule1Internal(ByteMatrix matrix, bool isHorizontal)
    {
      int num1 = 0;
      int num2 = isHorizontal ? matrix.Height : matrix.Width;
      int num3 = isHorizontal ? matrix.Width : matrix.Height;
      byte[][] array = matrix.Array;
      for (int index1 = 0; index1 < num2; ++index1)
      {
        int num4 = 0;
        int num5 = -1;
        for (int index2 = 0; index2 < num3; ++index2)
        {
          int num6 = isHorizontal ? (int) array[index1][index2] : (int) array[index2][index1];
          if (num6 == num5)
          {
            ++num4;
          }
          else
          {
            if (num4 >= 5)
              num1 += 3 + (num4 - 5);
            num4 = 1;
            num5 = num6;
          }
        }
        if (num4 > 5)
          num1 += 3 + (num4 - 5);
      }
      return num1;
    }
  }
}
