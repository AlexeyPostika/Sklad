// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Aztec.Encoder.Encoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using MessagingToolkit.Barcode.Common.ReedSolomon;
using System;

namespace MessagingToolkit.Barcode.Aztec.Encoder
{
    public static class Encoder
    {
        public const int DEFAULT_EC_PERCENT = 33;
        private const int TABLE_UPPER = 0;
        private const int TABLE_LOWER = 1;
        private const int TABLE_DIGIT = 2;
        private const int TABLE_MIXED = 3;
        private const int TABLE_PUNCT = 4;
        private const int TABLE_BINARY = 5;
        private static readonly int[][] CHAR_MAP = new int[5][]
        {
      new int[256],
      new int[256],
      new int[256],
      new int[256],
      new int[256]
        };
        private static readonly int[][] SHIFT_TABLE = new int[6][]
        {
      new int[6],
      new int[6],
      new int[6],
      new int[6],
      new int[6],
      new int[6]
        };
        private static readonly int[][] LATCH_TABLE = new int[6][]
        {
      new int[6],
      new int[6],
      new int[6],
      new int[6],
      new int[6],
      new int[6]
        };
        private static readonly int[] NB_BITS;
        private static readonly int[] NB_BITS_COMPACT;
        private static readonly int[] WORD_SIZE = new int[33]
        {
      4,
      6,
      6,
      8,
      8,
      8,
      8,
      8,
      8,
      10,
      10,
      10,
      10,
      10,
      10,
      10,
      10,
      10,
      10,
      10,
      10,
      10,
      10,
      12,
      12,
      12,
      12,
      12,
      12,
      12,
      12,
      12,
      12
        };

        static Encoder()
        {
            MessagingToolkit.Barcode.Aztec.Encoder.Encoder.CHAR_MAP[0][32] = 1;
            for (int index = 65; index <= 90; ++index)
                MessagingToolkit.Barcode.Aztec.Encoder.Encoder.CHAR_MAP[0][index] = index - 65 + 2;
            MessagingToolkit.Barcode.Aztec.Encoder.Encoder.CHAR_MAP[1][32] = 1;
            for (int index = 97; index <= 122; ++index)
                MessagingToolkit.Barcode.Aztec.Encoder.Encoder.CHAR_MAP[1][index] = index - 97 + 2;
            MessagingToolkit.Barcode.Aztec.Encoder.Encoder.CHAR_MAP[2][32] = 1;
            for (int index = 48; index <= 57; ++index)
                MessagingToolkit.Barcode.Aztec.Encoder.Encoder.CHAR_MAP[2][index] = index - 48 + 2;
            MessagingToolkit.Barcode.Aztec.Encoder.Encoder.CHAR_MAP[2][44] = 12;
            MessagingToolkit.Barcode.Aztec.Encoder.Encoder.CHAR_MAP[2][46] = 13;
            int[] numArray1 = new int[28]
            {
        0,
        32,
        1,
        2,
        3,
        4,
        5,
        6,
        7,
        8,
        9,
        10,
        11,
        12,
        13,
        27,
        28,
        29,
        30,
        31,
        64,
        92,
        94,
        95,
        96,
        124,
        126,
        (int) sbyte.MaxValue
            };
            for (int index = 0; index < numArray1.Length; ++index)
                MessagingToolkit.Barcode.Aztec.Encoder.Encoder.CHAR_MAP[3][numArray1[index]] = index;
            int[] numArray2 = new int[31]
            {
        0,
        13,
        0,
        0,
        0,
        0,
        33,
        39,
        35,
        36,
        37,
        38,
        39,
        40,
        41,
        42,
        43,
        44,
        45,
        46,
        47,
        58,
        59,
        60,
        61,
        62,
        63,
        91,
        93,
        123,
        125
            };
            for (int index = 0; index < numArray2.Length; ++index)
            {
                if (numArray2[index] > 0)
                    MessagingToolkit.Barcode.Aztec.Encoder.Encoder.CHAR_MAP[4][numArray2[index]] = index;
            }
            foreach (int[] numArray3 in MessagingToolkit.Barcode.Aztec.Encoder.Encoder.SHIFT_TABLE)
            {
                //foreach (int num in numArray3)
                //{
                //    num = -1;
                //}
                for (int i = 0; i < numArray3.Length; i++)
                {
                    numArray3[i] = -1;
                }
            }
            foreach (int[] numArray4 in MessagingToolkit.Barcode.Aztec.Encoder.Encoder.LATCH_TABLE)
            {
                for (int i = 0; i < numArray4.Length; i++)
                {
                    numArray4[i] = -1;
                }
                //foreach (int num in numArray4)
                //{
                //    num = -1;
                //}
            }
            MessagingToolkit.Barcode.Aztec.Encoder.Encoder.SHIFT_TABLE[0][4] = 0;
            MessagingToolkit.Barcode.Aztec.Encoder.Encoder.LATCH_TABLE[0][1] = 28;
            MessagingToolkit.Barcode.Aztec.Encoder.Encoder.LATCH_TABLE[0][3] = 29;
            MessagingToolkit.Barcode.Aztec.Encoder.Encoder.LATCH_TABLE[0][2] = 30;
            MessagingToolkit.Barcode.Aztec.Encoder.Encoder.SHIFT_TABLE[0][5] = 31;
            MessagingToolkit.Barcode.Aztec.Encoder.Encoder.SHIFT_TABLE[1][4] = 0;
            MessagingToolkit.Barcode.Aztec.Encoder.Encoder.SHIFT_TABLE[1][0] = 28;
            MessagingToolkit.Barcode.Aztec.Encoder.Encoder.LATCH_TABLE[1][3] = 29;
            MessagingToolkit.Barcode.Aztec.Encoder.Encoder.LATCH_TABLE[1][2] = 30;
            MessagingToolkit.Barcode.Aztec.Encoder.Encoder.SHIFT_TABLE[1][5] = 31;
            MessagingToolkit.Barcode.Aztec.Encoder.Encoder.SHIFT_TABLE[3][4] = 0;
            MessagingToolkit.Barcode.Aztec.Encoder.Encoder.LATCH_TABLE[3][1] = 28;
            MessagingToolkit.Barcode.Aztec.Encoder.Encoder.LATCH_TABLE[3][0] = 29;
            MessagingToolkit.Barcode.Aztec.Encoder.Encoder.LATCH_TABLE[3][4] = 30;
            MessagingToolkit.Barcode.Aztec.Encoder.Encoder.SHIFT_TABLE[3][5] = 31;
            MessagingToolkit.Barcode.Aztec.Encoder.Encoder.LATCH_TABLE[4][0] = 31;
            MessagingToolkit.Barcode.Aztec.Encoder.Encoder.SHIFT_TABLE[2][4] = 0;
            MessagingToolkit.Barcode.Aztec.Encoder.Encoder.LATCH_TABLE[2][0] = 30;
            MessagingToolkit.Barcode.Aztec.Encoder.Encoder.SHIFT_TABLE[2][0] = 31;
            MessagingToolkit.Barcode.Aztec.Encoder.Encoder.NB_BITS_COMPACT = new int[5];
            for (int index = 1; index < MessagingToolkit.Barcode.Aztec.Encoder.Encoder.NB_BITS_COMPACT.Length; ++index)
                MessagingToolkit.Barcode.Aztec.Encoder.Encoder.NB_BITS_COMPACT[index] = (88 + 16 * index) * index;
            MessagingToolkit.Barcode.Aztec.Encoder.Encoder.NB_BITS = new int[33];
            for (int index = 1; index < MessagingToolkit.Barcode.Aztec.Encoder.Encoder.NB_BITS.Length; ++index)
                MessagingToolkit.Barcode.Aztec.Encoder.Encoder.NB_BITS[index] = (112 + 16 * index) * index;
        }

        public static AztecCode Encode(byte[] data) => MessagingToolkit.Barcode.Aztec.Encoder.Encoder.Encode(data, 33);

        public static AztecCode Encode(byte[] data, int minECCPercent)
        {
            BitArray bits = MessagingToolkit.Barcode.Aztec.Encoder.Encoder.HighLevelEncode(data);
            int num1 = bits.Size * minECCPercent / 100 + 11;
            int num2 = bits.Size + num1;
            int num3 = 0;
            int num4 = 0;
            BitArray stuffedBits = (BitArray)null;
            int layers;
            for (layers = 1; layers < MessagingToolkit.Barcode.Aztec.Encoder.Encoder.NB_BITS_COMPACT.Length; ++layers)
            {
                if (MessagingToolkit.Barcode.Aztec.Encoder.Encoder.NB_BITS_COMPACT[layers] >= num2)
                {
                    if (num3 != MessagingToolkit.Barcode.Aztec.Encoder.Encoder.WORD_SIZE[layers])
                    {
                        num3 = MessagingToolkit.Barcode.Aztec.Encoder.Encoder.WORD_SIZE[layers];
                        stuffedBits = MessagingToolkit.Barcode.Aztec.Encoder.Encoder.StuffBits(bits, num3);
                    }
                    num4 = MessagingToolkit.Barcode.Aztec.Encoder.Encoder.NB_BITS_COMPACT[layers];
                    if (stuffedBits.Size + num1 <= MessagingToolkit.Barcode.Aztec.Encoder.Encoder.NB_BITS_COMPACT[layers])
                        break;
                }
            }
            bool compact = true;
            if (layers == MessagingToolkit.Barcode.Aztec.Encoder.Encoder.NB_BITS_COMPACT.Length)
            {
                compact = false;
                for (layers = 1; layers < MessagingToolkit.Barcode.Aztec.Encoder.Encoder.NB_BITS.Length; ++layers)
                {
                    if (MessagingToolkit.Barcode.Aztec.Encoder.Encoder.NB_BITS[layers] >= num2)
                    {
                        if (num3 != MessagingToolkit.Barcode.Aztec.Encoder.Encoder.WORD_SIZE[layers])
                        {
                            num3 = MessagingToolkit.Barcode.Aztec.Encoder.Encoder.WORD_SIZE[layers];
                            stuffedBits = MessagingToolkit.Barcode.Aztec.Encoder.Encoder.StuffBits(bits, num3);
                        }
                        num4 = MessagingToolkit.Barcode.Aztec.Encoder.Encoder.NB_BITS[layers];
                        if (stuffedBits.Size + num1 <= MessagingToolkit.Barcode.Aztec.Encoder.Encoder.NB_BITS[layers])
                            break;
                    }
                }
            }
            if (layers == MessagingToolkit.Barcode.Aztec.Encoder.Encoder.NB_BITS.Length)
                throw new ArgumentException("Data too large for an Aztec code");
            int messageSizeInWords = (stuffedBits.Size + num3 - 1) / num3;
            for (int index = messageSizeInWords * num3 - stuffedBits.Size; index > 0; --index)
                stuffedBits.AppendBit(true);
            ReedSolomonEncoder reedSolomonEncoder = new ReedSolomonEncoder(MessagingToolkit.Barcode.Aztec.Encoder.Encoder.GetGF(num3));
            int totalWords = num4 / num3;
            int[] words = MessagingToolkit.Barcode.Aztec.Encoder.Encoder.BitsToWords(stuffedBits, num3, totalWords);
            reedSolomonEncoder.Encode(words, totalWords - messageSizeInWords);
            int numBits = num4 % num3;
            BitArray bitArray = new BitArray();
            bitArray.AppendBits(0, numBits);
            foreach (int val in words)
                bitArray.AppendBits(val, num3);
            BitArray modeMessage = MessagingToolkit.Barcode.Aztec.Encoder.Encoder.GenerateModeMessage(compact, layers, messageSizeInWords);
            int length = compact ? 11 + layers * 4 : 14 + layers * 4;
            int[] numArray = new int[length];
            int num5;
            if (compact)
            {
                num5 = length;
                for (int index = 0; index < numArray.Length; ++index)
                    numArray[index] = index;
            }
            else
            {
                num5 = length + 1 + 2 * ((length / 2 - 1) / 15);
                int num6 = length / 2;
                int num7 = num5 / 2;
                for (int index = 0; index < num6; ++index)
                {
                    int num8 = index + index / 15;
                    numArray[num6 - index - 1] = num7 - num8 - 1;
                    numArray[num6 + index] = num7 + num8 + 1;
                }
            }
            BitMatrix matrix = new BitMatrix(num5);
            int num9 = 0;
            int num10 = 0;
            for (; num9 < layers; ++num9)
            {
                int num11 = compact ? (layers - num9) * 4 + 9 : (layers - num9) * 4 + 12;
                for (int index1 = 0; index1 < num11; ++index1)
                {
                    int num12 = index1 * 2;
                    for (int index2 = 0; index2 < 2; ++index2)
                    {
                        if (bitArray.Get(num10 + num12 + index2))
                            matrix.Set(numArray[num9 * 2 + index2], numArray[num9 * 2 + index1]);
                        if (bitArray.Get(num10 + num11 * 2 + num12 + index2))
                            matrix.Set(numArray[num9 * 2 + index1], numArray[length - 1 - num9 * 2 - index2]);
                        if (bitArray.Get(num10 + num11 * 4 + num12 + index2))
                            matrix.Set(numArray[length - 1 - num9 * 2 - index2], numArray[length - 1 - num9 * 2 - index1]);
                        if (bitArray.Get(num10 + num11 * 6 + num12 + index2))
                            matrix.Set(numArray[length - 1 - num9 * 2 - index1], numArray[num9 * 2 + index2]);
                    }
                }
                num10 += num11 * 8;
            }
            MessagingToolkit.Barcode.Aztec.Encoder.Encoder.DrawModeMessage(matrix, compact, num5, modeMessage);
            if (compact)
            {
                MessagingToolkit.Barcode.Aztec.Encoder.Encoder.DrawBullsEye(matrix, num5 / 2, 5);
            }
            else
            {
                MessagingToolkit.Barcode.Aztec.Encoder.Encoder.DrawBullsEye(matrix, num5 / 2, 7);
                int num13 = 0;
                int num14 = 0;
                while (num13 < length / 2 - 1)
                {
                    for (int index = num5 / 2 & 1; index < num5; index += 2)
                    {
                        matrix.Set(num5 / 2 - num14, index);
                        matrix.Set(num5 / 2 + num14, index);
                        matrix.Set(index, num5 / 2 - num14);
                        matrix.Set(index, num5 / 2 + num14);
                    }
                    num13 += 15;
                    num14 += 16;
                }
            }
            return new AztecCode()
            {
                Compact = compact,
                Size = num5,
                Layers = layers,
                CodeWords = messageSizeInWords,
                Matrix = matrix
            };
        }

        private static void DrawBullsEye(BitMatrix matrix, int center, int size)
        {
            for (int index1 = 0; index1 < size; index1 += 2)
            {
                for (int index2 = center - index1; index2 <= center + index1; ++index2)
                {
                    matrix.Set(index2, center - index1);
                    matrix.Set(index2, center + index1);
                    matrix.Set(center - index1, index2);
                    matrix.Set(center + index1, index2);
                }
            }
            matrix.Set(center - size, center - size);
            matrix.Set(center - size + 1, center - size);
            matrix.Set(center - size, center - size + 1);
            matrix.Set(center + size, center - size);
            matrix.Set(center + size, center - size + 1);
            matrix.Set(center + size, center + size - 1);
        }

        internal static BitArray GenerateModeMessage(
          bool compact,
          int layers,
          int messageSizeInWords)
        {
            BitArray stuffedBits = new BitArray();
            BitArray checkWords;
            if (compact)
            {
                stuffedBits.AppendBits(layers - 1, 2);
                stuffedBits.AppendBits(messageSizeInWords - 1, 6);
                checkWords = MessagingToolkit.Barcode.Aztec.Encoder.Encoder.GenerateCheckWords(stuffedBits, 28, 4);
            }
            else
            {
                stuffedBits.AppendBits(layers - 1, 5);
                stuffedBits.AppendBits(messageSizeInWords - 1, 11);
                checkWords = MessagingToolkit.Barcode.Aztec.Encoder.Encoder.GenerateCheckWords(stuffedBits, 40, 4);
            }
            return checkWords;
        }

        private static void DrawModeMessage(
          BitMatrix matrix,
          bool compact,
          int matrixSize,
          BitArray modeMessage)
        {
            if (compact)
            {
                for (int i = 0; i < 7; ++i)
                {
                    if (modeMessage.Get(i))
                        matrix.Set(matrixSize / 2 - 3 + i, matrixSize / 2 - 5);
                    if (modeMessage.Get(i + 7))
                        matrix.Set(matrixSize / 2 + 5, matrixSize / 2 - 3 + i);
                    if (modeMessage.Get(20 - i))
                        matrix.Set(matrixSize / 2 - 3 + i, matrixSize / 2 + 5);
                    if (modeMessage.Get(27 - i))
                        matrix.Set(matrixSize / 2 - 5, matrixSize / 2 - 3 + i);
                }
            }
            else
            {
                for (int i = 0; i < 10; ++i)
                {
                    if (modeMessage.Get(i))
                        matrix.Set(matrixSize / 2 - 5 + i + i / 5, matrixSize / 2 - 7);
                    if (modeMessage.Get(i + 10))
                        matrix.Set(matrixSize / 2 + 7, matrixSize / 2 - 5 + i + i / 5);
                    if (modeMessage.Get(29 - i))
                        matrix.Set(matrixSize / 2 - 5 + i + i / 5, matrixSize / 2 + 7);
                    if (modeMessage.Get(39 - i))
                        matrix.Set(matrixSize / 2 - 7, matrixSize / 2 - 5 + i + i / 5);
                }
            }
        }

        private static BitArray GenerateCheckWords(
          BitArray stuffedBits,
          int totalSymbolBits,
          int wordSize)
        {
            int num = (stuffedBits.Size + wordSize - 1) / wordSize;
            for (int index = num * wordSize - stuffedBits.Size; index > 0; --index)
                stuffedBits.AppendBit(true);
            ReedSolomonEncoder reedSolomonEncoder = new ReedSolomonEncoder(MessagingToolkit.Barcode.Aztec.Encoder.Encoder.GetGF(wordSize));
            int totalWords = totalSymbolBits / wordSize;
            int[] words = MessagingToolkit.Barcode.Aztec.Encoder.Encoder.BitsToWords(stuffedBits, wordSize, totalWords);
            reedSolomonEncoder.Encode(words, totalWords - num);
            int numBits = totalSymbolBits % wordSize;
            BitArray checkWords = new BitArray();
            checkWords.AppendBits(0, numBits);
            foreach (int val in words)
                checkWords.AppendBits(val, wordSize);
            return checkWords;
        }

        private static int[] BitsToWords(BitArray stuffedBits, int wordSize, int totalWords)
        {
            int[] words = new int[totalWords];
            int index1 = 0;
            for (int index2 = stuffedBits.Size / wordSize; index1 < index2; ++index1)
            {
                int num = 0;
                for (int index3 = 0; index3 < wordSize; ++index3)
                    num |= stuffedBits.Get(index1 * wordSize + index3) ? 1 << wordSize - index3 - 1 : 0;
                words[index1] = num;
            }
            return words;
        }

        private static GenericGF GetGF(int wordSize)
        {
            switch (wordSize)
            {
                case 4:
                    return GenericGF.AztecParam;
                case 6:
                    return GenericGF.AztecData6;
                case 8:
                    return GenericGF.AztecData8;
                case 10:
                    return GenericGF.AztecData10;
                case 12:
                    return GenericGF.AztecData12;
                default:
                    return (GenericGF)null;
            }
        }

        internal static BitArray StuffBits(BitArray bits, int wordSize)
        {
            BitArray bitArray = new BitArray();
            int size1 = bits.Size;
            int num1 = (1 << wordSize) - 2;
            for (int index1 = 0; index1 < size1; index1 += wordSize)
            {
                int val = 0;
                for (int index2 = 0; index2 < wordSize; ++index2)
                {
                    if (index1 + index2 >= size1 || bits.Get(index1 + index2))
                        val |= 1 << wordSize - 1 - index2;
                }
                if ((val & num1) == num1)
                {
                    bitArray.AppendBits(val & num1, wordSize);
                    --index1;
                }
                else if ((val & num1) == 0)
                {
                    bitArray.AppendBits(val | 1, wordSize);
                    --index1;
                }
                else
                    bitArray.AppendBits(val, wordSize);
            }
            int size2 = bitArray.Size;
            int num2 = size2 % wordSize;
            if (num2 != 0)
            {
                int num3 = 1;
                for (int index = 0; index < num2; ++index)
                {
                    if (!bitArray.Get(size2 - 1 - index))
                        num3 = 0;
                }
                for (int index = num2; index < wordSize - 1; ++index)
                    bitArray.AppendBit(true);
                bitArray.AppendBit(num3 == 0);
            }
            return bitArray;
        }

        internal static BitArray HighLevelEncode(byte[] data)
        {
            BitArray bits = new BitArray();
            int mode1 = 0;
            int[] numArray1 = new int[5];
            int[] numArray2 = new int[5];
            for (int index1 = 0; index1 < data.Length; ++index1)
            {
                int index2 = (int)data[index1] & (int)byte.MaxValue;
                int index3 = index1 < data.Length - 1 ? (int)data[index1 + 1] & (int)byte.MaxValue : 0;
                int num1 = 0;
                if (index2 == 13 && index3 == 10)
                    num1 = 2;
                else if (index2 == 46 && index3 == 32)
                    num1 = 3;
                else if (index2 == 44 && index3 == 32)
                    num1 = 4;
                else if (index2 == 58 && index3 == 32)
                    num1 = 5;
                if (num1 > 0)
                {
                    if (mode1 == 4)
                    {
                        MessagingToolkit.Barcode.Aztec.Encoder.Encoder.OutputWord(bits, 4, num1);
                        ++index1;
                        continue;
                    }
                    if (MessagingToolkit.Barcode.Aztec.Encoder.Encoder.SHIFT_TABLE[mode1][4] >= 0)
                    {
                        MessagingToolkit.Barcode.Aztec.Encoder.Encoder.OutputWord(bits, mode1, MessagingToolkit.Barcode.Aztec.Encoder.Encoder.SHIFT_TABLE[mode1][4]);
                        MessagingToolkit.Barcode.Aztec.Encoder.Encoder.OutputWord(bits, 4, num1);
                        ++index1;
                        continue;
                    }
                    if (MessagingToolkit.Barcode.Aztec.Encoder.Encoder.LATCH_TABLE[mode1][4] >= 0)
                    {
                        MessagingToolkit.Barcode.Aztec.Encoder.Encoder.OutputWord(bits, mode1, MessagingToolkit.Barcode.Aztec.Encoder.Encoder.LATCH_TABLE[mode1][4]);
                        MessagingToolkit.Barcode.Aztec.Encoder.Encoder.OutputWord(bits, 4, num1);
                        mode1 = 4;
                        ++index1;
                        continue;
                    }
                }
                int num2 = -1;
                int mode2 = -1;
                int mode3 = -1;
                for (int index4 = 0; index4 < 5; ++index4)
                {
                    numArray1[index4] = MessagingToolkit.Barcode.Aztec.Encoder.Encoder.CHAR_MAP[index4][index2];
                    if (numArray1[index4] > 0 && num2 < 0)
                        num2 = index4;
                    if (mode2 < 0 && numArray1[index4] > 0 && MessagingToolkit.Barcode.Aztec.Encoder.Encoder.SHIFT_TABLE[mode1][index4] >= 0)
                        mode2 = index4;
                    numArray2[index4] = MessagingToolkit.Barcode.Aztec.Encoder.Encoder.CHAR_MAP[index4][index3];
                    if (mode3 < 0 && numArray1[index4] > 0 && (index3 == 0 || numArray2[index4] > 0) && MessagingToolkit.Barcode.Aztec.Encoder.Encoder.LATCH_TABLE[mode1][index4] >= 0)
                        mode3 = index4;
                }
                if (mode2 < 0 && mode3 < 0)
                {
                    for (int index5 = 0; index5 < 5; ++index5)
                    {
                        if (numArray1[index5] > 0 && MessagingToolkit.Barcode.Aztec.Encoder.Encoder.LATCH_TABLE[mode1][index5] >= 0)
                        {
                            mode3 = index5;
                            break;
                        }
                    }
                }
                if (numArray1[mode1] > 0)
                    MessagingToolkit.Barcode.Aztec.Encoder.Encoder.OutputWord(bits, mode1, numArray1[mode1]);
                else if (mode3 >= 0)
                {
                    MessagingToolkit.Barcode.Aztec.Encoder.Encoder.OutputWord(bits, mode1, MessagingToolkit.Barcode.Aztec.Encoder.Encoder.LATCH_TABLE[mode1][mode3]);
                    MessagingToolkit.Barcode.Aztec.Encoder.Encoder.OutputWord(bits, mode3, numArray1[mode3]);
                    mode1 = mode3;
                }
                else if (mode2 >= 0)
                {
                    MessagingToolkit.Barcode.Aztec.Encoder.Encoder.OutputWord(bits, mode1, MessagingToolkit.Barcode.Aztec.Encoder.Encoder.SHIFT_TABLE[mode1][mode2]);
                    MessagingToolkit.Barcode.Aztec.Encoder.Encoder.OutputWord(bits, mode2, numArray1[mode2]);
                }
                else
                {
                    if (num2 >= 0)
                    {
                        switch (mode1)
                        {
                            case 2:
                                MessagingToolkit.Barcode.Aztec.Encoder.Encoder.OutputWord(bits, 2, MessagingToolkit.Barcode.Aztec.Encoder.Encoder.LATCH_TABLE[2][0]);
                                mode1 = 0;
                                --index1;
                                continue;
                            case 4:
                                MessagingToolkit.Barcode.Aztec.Encoder.Encoder.OutputWord(bits, 4, MessagingToolkit.Barcode.Aztec.Encoder.Encoder.LATCH_TABLE[4][0]);
                                mode1 = 0;
                                --index1;
                                continue;
                        }
                    }
                    int index6 = index1 + 1;
                    int num3 = 0;
                    for (; index6 < data.Length; ++index6)
                    {
                        int index7 = (int)data[index6] & (int)byte.MaxValue;
                        bool flag = true;
                        for (int index8 = 0; index8 < 5; ++index8)
                        {
                            if (MessagingToolkit.Barcode.Aztec.Encoder.Encoder.CHAR_MAP[index8][index7] > 0)
                            {
                                flag = false;
                                break;
                            }
                        }
                        if (flag)
                        {
                            num3 = 0;
                        }
                        else
                        {
                            if (num3 >= 1)
                            {
                                index6 -= num3;
                                break;
                            }
                            ++num3;
                        }
                    }
                    int val = index6 - index1;
                    switch (mode1)
                    {
                        case 0:
                        case 1:
                        case 3:
                            MessagingToolkit.Barcode.Aztec.Encoder.Encoder.OutputWord(bits, mode1, MessagingToolkit.Barcode.Aztec.Encoder.Encoder.SHIFT_TABLE[mode1][5]);
                            break;
                        case 2:
                            MessagingToolkit.Barcode.Aztec.Encoder.Encoder.OutputWord(bits, mode1, MessagingToolkit.Barcode.Aztec.Encoder.Encoder.LATCH_TABLE[mode1][0]);
                            mode1 = 0;
                            MessagingToolkit.Barcode.Aztec.Encoder.Encoder.OutputWord(bits, mode1, MessagingToolkit.Barcode.Aztec.Encoder.Encoder.SHIFT_TABLE[mode1][5]);
                            break;
                        case 4:
                            MessagingToolkit.Barcode.Aztec.Encoder.Encoder.OutputWord(bits, mode1, MessagingToolkit.Barcode.Aztec.Encoder.Encoder.LATCH_TABLE[mode1][0]);
                            mode1 = 0;
                            MessagingToolkit.Barcode.Aztec.Encoder.Encoder.OutputWord(bits, mode1, MessagingToolkit.Barcode.Aztec.Encoder.Encoder.SHIFT_TABLE[mode1][5]);
                            break;
                    }
                    if (val >= 32 && val < 63)
                        val = 31;
                    if (val > 542)
                        val = 542;
                    if (val < 32)
                        bits.AppendBits(val, 5);
                    else
                        bits.AppendBits(val - 31, 16);
                    while (val > 0)
                    {
                        bits.AppendBits((int)data[index1], 8);
                        --val;
                        ++index1;
                    }
                    --index1;
                }
            }
            return bits;
        }

        private static void OutputWord(BitArray bits, int mode, int value)
        {
            if (mode == 2)
                bits.AppendBits(value, 4);
            else if (mode < 5)
                bits.AppendBits(value, 5);
            else
                bits.AppendBits(value, 8);
        }
    }
}
