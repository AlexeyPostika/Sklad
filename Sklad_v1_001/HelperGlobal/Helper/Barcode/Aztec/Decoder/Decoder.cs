// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Aztec.Decoder.Decoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using MessagingToolkit.Barcode.Common.ReedSolomon;
using System.Collections.Generic;
using System.Text;

namespace MessagingToolkit.Barcode.Aztec.Decoder
{
  public sealed class Decoder
  {
    private static readonly int[] NB_BITS_COMPACT = new int[5]
    {
      0,
      104,
      240,
      408,
      608
    };
    private static readonly int[] NB_BITS = new int[33]
    {
      0,
      128,
      288,
      480,
      704,
      960,
      1248,
      1568,
      1920,
      2304,
      2720,
      3168,
      3648,
      4160,
      4704,
      5280,
      5888,
      6528,
      7200,
      7904,
      8640,
      9408,
      10208,
      11040,
      11904,
      12800,
      13728,
      14688,
      15680,
      16704,
      17760,
      18848,
      19968
    };
    private static readonly int[] NB_DATABLOCK_COMPACT = new int[5]
    {
      0,
      17,
      40,
      51,
      76
    };
    private static readonly int[] NB_DATABLOCK = new int[33]
    {
      0,
      21,
      48,
      60,
      88,
      120,
      156,
      196,
      240,
      230,
      272,
      316,
      364,
      416,
      470,
      528,
      588,
      652,
      720,
      790,
      864,
      940,
      1020,
      920,
      992,
      1066,
      1144,
      1224,
      1306,
      1392,
      1480,
      1570,
      1664
    };
    private static readonly string[] UPPER_TABLE = new string[32]
    {
      "CTRL_PS",
      " ",
      "A",
      "B",
      "C",
      "D",
      "E",
      "F",
      "G",
      "H",
      "I",
      "J",
      "K",
      "L",
      "M",
      "N",
      "O",
      "P",
      "Q",
      "R",
      "S",
      "T",
      "U",
      "V",
      "W",
      "X",
      "Y",
      "Z",
      "CTRL_LL",
      "CTRL_ML",
      "CTRL_DL",
      "CTRL_BS"
    };
    private static readonly string[] LOWER_TABLE = new string[32]
    {
      "CTRL_PS",
      " ",
      "a",
      "b",
      "c",
      "d",
      "e",
      "f",
      "g",
      "h",
      "i",
      "j",
      "k",
      "l",
      "m",
      "n",
      "o",
      "p",
      "q",
      "r",
      "s",
      "t",
      "u",
      "v",
      "w",
      "x",
      "y",
      "z",
      "CTRL_US",
      "CTRL_ML",
      "CTRL_DL",
      "CTRL_BS"
    };
    private static readonly string[] MIXED_TABLE = new string[32]
    {
      "CTRL_PS",
      " ",
      "\\1",
      "\\2",
      "\\3",
      "\\4",
      "\\5",
      "\\6",
      "\\7",
      "\b",
      "\t",
      "\n",
      "\\13",
      "\f",
      "\r",
      "\\33",
      "\\34",
      "\\35",
      "\\36",
      "\\37",
      "@",
      "\\",
      "^",
      "_",
      "`",
      "|",
      "~",
      "\\177",
      "CTRL_LL",
      "CTRL_UL",
      "CTRL_PL",
      "CTRL_BS"
    };
    private static readonly string[] PUNCT_TABLE = new string[32]
    {
      "",
      "\r",
      "\r\n",
      ". ",
      ", ",
      ": ",
      "!",
      "\"",
      "#",
      "$",
      "%",
      "&",
      "'",
      "(",
      ")",
      "*",
      "+",
      ",",
      "-",
      ".",
      "/",
      ":",
      ";",
      "<",
      "=",
      ">",
      "?",
      "[",
      "]",
      "{",
      "}",
      "CTRL_UL"
    };
    private static readonly string[] DIGIT_TABLE = new string[16]
    {
      "CTRL_PS",
      " ",
      "0",
      "1",
      "2",
      "3",
      "4",
      "5",
      "6",
      "7",
      "8",
      "9",
      ",",
      ".",
      "CTRL_UL",
      "CTRL_US"
    };
    private int numCodewords;
    private int codewordSize;
    private AztecDetectorResult ddata;
    private int invertedBitCount;

    public DecoderResult Decode(AztecDetectorResult detectorResult)
    {
      this.ddata = detectorResult;
      BitMatrix matrix = detectorResult.Bits;
      if (!this.ddata.IsCompact())
        matrix = MessagingToolkit.Barcode.Aztec.Decoder.Decoder.RemoveDashedLines(this.ddata.Bits);
      return new DecoderResult((byte[]) null, this.GetEncodedData(this.CorrectBits(this.ExtractBits(matrix))), (List<byte[]>) null, (string) null);
    }

    private string GetEncodedData(bool[] correctedBits)
    {
      int num1 = this.codewordSize * this.ddata.GetNbDatablocks() - this.invertedBitCount;
      if (num1 > correctedBits.Length)
        throw FormatException.Instance;
      MessagingToolkit.Barcode.Aztec.Decoder.Decoder.Table table1 = MessagingToolkit.Barcode.Aztec.Decoder.Decoder.Table.UPPER;
      MessagingToolkit.Barcode.Aztec.Decoder.Decoder.Table table2 = MessagingToolkit.Barcode.Aztec.Decoder.Decoder.Table.UPPER;
      int startIndex = 0;
      StringBuilder stringBuilder = new StringBuilder(20);
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      bool flag4 = false;
      while (!flag1)
      {
        if (flag2)
          flag3 = true;
        else
          table1 = table2;
        if (flag4)
        {
          if (num1 - startIndex >= 5)
          {
            int num2 = MessagingToolkit.Barcode.Aztec.Decoder.Decoder.ReadCode(correctedBits, startIndex, 5);
            startIndex += 5;
            if (num2 == 0)
            {
              if (num1 - startIndex >= 11)
              {
                num2 = MessagingToolkit.Barcode.Aztec.Decoder.Decoder.ReadCode(correctedBits, startIndex, 11) + 31;
                startIndex += 11;
              }
              else
                break;
            }
            for (int index = 0; index < num2; ++index)
            {
              if (num1 - startIndex < 8)
              {
                flag1 = true;
                break;
              }
              int num3 = MessagingToolkit.Barcode.Aztec.Decoder.Decoder.ReadCode(correctedBits, startIndex, 8);
              stringBuilder.Append((char) num3);
              startIndex += 8;
            }
            flag4 = false;
          }
          else
            break;
        }
        else if (table2 == MessagingToolkit.Barcode.Aztec.Decoder.Decoder.Table.BINARY)
        {
          if (num1 - startIndex >= 8)
          {
            int num4 = MessagingToolkit.Barcode.Aztec.Decoder.Decoder.ReadCode(correctedBits, startIndex, 8);
            startIndex += 8;
            stringBuilder.Append((char) num4);
          }
          else
            break;
        }
        else
        {
          int length = 5;
          if (table2 == MessagingToolkit.Barcode.Aztec.Decoder.Decoder.Table.DIGIT)
            length = 4;
          if (num1 - startIndex >= length)
          {
            int code = MessagingToolkit.Barcode.Aztec.Decoder.Decoder.ReadCode(correctedBits, startIndex, length);
            startIndex += length;
            string character = MessagingToolkit.Barcode.Aztec.Decoder.Decoder.GetCharacter(table2, code);
            if (character.StartsWith("CTRL_"))
            {
              table2 = MessagingToolkit.Barcode.Aztec.Decoder.Decoder.GetTable(character[5]);
              if (character[6] == 'S')
              {
                flag2 = true;
                if (character[5] == 'B')
                  flag4 = true;
              }
            }
            else
              stringBuilder.Append(character);
          }
          else
            break;
        }
        if (flag3)
        {
          table2 = table1;
          flag2 = false;
          flag3 = false;
        }
      }
      return stringBuilder.ToString();
    }

    private static MessagingToolkit.Barcode.Aztec.Decoder.Decoder.Table GetTable(char t)
    {
      switch (t)
      {
        case 'B':
          return MessagingToolkit.Barcode.Aztec.Decoder.Decoder.Table.BINARY;
        case 'D':
          return MessagingToolkit.Barcode.Aztec.Decoder.Decoder.Table.DIGIT;
        case 'L':
          return MessagingToolkit.Barcode.Aztec.Decoder.Decoder.Table.LOWER;
        case 'M':
          return MessagingToolkit.Barcode.Aztec.Decoder.Decoder.Table.MIXED;
        case 'P':
          return MessagingToolkit.Barcode.Aztec.Decoder.Decoder.Table.PUNCT;
        default:
          return MessagingToolkit.Barcode.Aztec.Decoder.Decoder.Table.UPPER;
      }
    }

    private static string GetCharacter(MessagingToolkit.Barcode.Aztec.Decoder.Decoder.Table table, int code)
    {
      switch (table)
      {
        case MessagingToolkit.Barcode.Aztec.Decoder.Decoder.Table.UPPER:
          return MessagingToolkit.Barcode.Aztec.Decoder.Decoder.UPPER_TABLE[code];
        case MessagingToolkit.Barcode.Aztec.Decoder.Decoder.Table.LOWER:
          return MessagingToolkit.Barcode.Aztec.Decoder.Decoder.LOWER_TABLE[code];
        case MessagingToolkit.Barcode.Aztec.Decoder.Decoder.Table.MIXED:
          return MessagingToolkit.Barcode.Aztec.Decoder.Decoder.MIXED_TABLE[code];
        case MessagingToolkit.Barcode.Aztec.Decoder.Decoder.Table.DIGIT:
          return MessagingToolkit.Barcode.Aztec.Decoder.Decoder.DIGIT_TABLE[code];
        case MessagingToolkit.Barcode.Aztec.Decoder.Decoder.Table.PUNCT:
          return MessagingToolkit.Barcode.Aztec.Decoder.Decoder.PUNCT_TABLE[code];
        default:
          return "";
      }
    }

    private bool[] CorrectBits(bool[] rawbits)
    {
      GenericGF field;
      if (this.ddata.GetNbLayers() <= 2)
      {
        this.codewordSize = 6;
        field = GenericGF.AztecData6;
      }
      else if (this.ddata.GetNbLayers() <= 8)
      {
        this.codewordSize = 8;
        field = GenericGF.AztecData8;
      }
      else if (this.ddata.GetNbLayers() <= 22)
      {
        this.codewordSize = 10;
        field = GenericGF.AztecData10;
      }
      else
      {
        this.codewordSize = 12;
        field = GenericGF.AztecData12;
      }
      int nbDatablocks = this.ddata.GetNbDatablocks();
      int num1;
      int twoS;
      if (this.ddata.IsCompact())
      {
        num1 = MessagingToolkit.Barcode.Aztec.Decoder.Decoder.NB_BITS_COMPACT[this.ddata.GetNbLayers()] - this.numCodewords * this.codewordSize;
        twoS = MessagingToolkit.Barcode.Aztec.Decoder.Decoder.NB_DATABLOCK_COMPACT[this.ddata.GetNbLayers()] - nbDatablocks;
      }
      else
      {
        num1 = MessagingToolkit.Barcode.Aztec.Decoder.Decoder.NB_BITS[this.ddata.GetNbLayers()] - this.numCodewords * this.codewordSize;
        twoS = MessagingToolkit.Barcode.Aztec.Decoder.Decoder.NB_DATABLOCK[this.ddata.GetNbLayers()] - nbDatablocks;
      }
      int[] received = new int[this.numCodewords];
      for (int index1 = 0; index1 < this.numCodewords; ++index1)
      {
        int num2 = 1;
        for (int index2 = 1; index2 <= this.codewordSize; ++index2)
        {
          if (rawbits[this.codewordSize * index1 + this.codewordSize - index2 + num1])
            received[index1] += num2;
          num2 <<= 1;
        }
      }
      try
      {
        new ReedSolomonDecoder(field).Decode(received, twoS);
      }
      catch (ReedSolomonException ex)
      {
        throw FormatException.Instance;
      }
      int num3 = 0;
      this.invertedBitCount = 0;
      bool[] flagArray = new bool[nbDatablocks * this.codewordSize];
      for (int index3 = 0; index3 < nbDatablocks; ++index3)
      {
        bool flag1 = false;
        int num4 = 0;
        int num5 = 1 << this.codewordSize - 1;
        for (int index4 = 0; index4 < this.codewordSize; ++index4)
        {
          bool flag2 = (received[index3] & num5) == num5;
          if (num4 == this.codewordSize - 1)
          {
            flag1 = flag2 != flag1 ? false : throw FormatException.Instance;
            num4 = 0;
            ++num3;
            ++this.invertedBitCount;
          }
          else
          {
            if (flag1 == flag2)
            {
              ++num4;
            }
            else
            {
              num4 = 1;
              flag1 = flag2;
            }
            flagArray[index3 * this.codewordSize + index4 - num3] = flag2;
          }
          num5 = (int) ((uint) num5 >> 1);
        }
      }
      return flagArray;
    }

    private bool[] ExtractBits(BitMatrix matrix)
    {
      bool[] bits;
      if (this.ddata.IsCompact())
      {
        if (this.ddata.GetNbLayers() > MessagingToolkit.Barcode.Aztec.Decoder.Decoder.NB_BITS_COMPACT.Length)
          throw FormatException.Instance;
        bits = new bool[MessagingToolkit.Barcode.Aztec.Decoder.Decoder.NB_BITS_COMPACT[this.ddata.GetNbLayers()]];
        this.numCodewords = MessagingToolkit.Barcode.Aztec.Decoder.Decoder.NB_DATABLOCK_COMPACT[this.ddata.GetNbLayers()];
      }
      else
      {
        if (this.ddata.GetNbLayers() > MessagingToolkit.Barcode.Aztec.Decoder.Decoder.NB_BITS.Length)
          throw FormatException.Instance;
        bits = new bool[MessagingToolkit.Barcode.Aztec.Decoder.Decoder.NB_BITS[this.ddata.GetNbLayers()]];
        this.numCodewords = MessagingToolkit.Barcode.Aztec.Decoder.Decoder.NB_DATABLOCK[this.ddata.GetNbLayers()];
      }
      int nbLayers = this.ddata.GetNbLayers();
      int height = matrix.GetHeight();
      int num1 = 0;
      int num2 = 0;
      while (nbLayers != 0)
      {
        int num3 = 0;
        for (int index = 0; index < 2 * height - 4; ++index)
        {
          bits[num1 + index] = matrix.Get(num2 + num3, num2 + index / 2);
          bits[num1 + 2 * height - 4 + index] = matrix.Get(num2 + index / 2, num2 + height - 1 - num3);
          num3 = (num3 + 1) % 2;
        }
        int num4 = 0;
        for (int index = 2 * height + 1; index > 5; --index)
        {
          bits[num1 + 4 * height - 8 + (2 * height - index) + 1] = matrix.Get(num2 + height - 1 - num4, num2 + index / 2 - 1);
          bits[num1 + 6 * height - 12 + (2 * height - index) + 1] = matrix.Get(num2 + index / 2 - 1, num2 + num4);
          num4 = (num4 + 1) % 2;
        }
        num2 += 2;
        num1 += 8 * height - 16;
        --nbLayers;
        height -= 4;
      }
      return bits;
    }

    private static BitMatrix RemoveDashedLines(BitMatrix matrix)
    {
      int num = 1 + 2 * ((matrix.GetWidth() - 1) / 2 / 16);
      BitMatrix bitMatrix = new BitMatrix(matrix.GetWidth() - num, matrix.GetHeight() - num);
      int x1 = 0;
      for (int x2 = 0; x2 < matrix.GetWidth(); ++x2)
      {
        if ((matrix.GetWidth() / 2 - x2) % 16 != 0)
        {
          int y1 = 0;
          for (int y2 = 0; y2 < matrix.GetHeight(); ++y2)
          {
            if ((matrix.GetWidth() / 2 - y2) % 16 != 0)
            {
              if (matrix.Get(x2, y2))
                bitMatrix.Set(x1, y1);
              ++y1;
            }
          }
          ++x1;
        }
      }
      return bitMatrix;
    }

    private static int ReadCode(bool[] rawbits, int startIndex, int length)
    {
      int num = 0;
      for (int index = startIndex; index < startIndex + length; ++index)
      {
        num <<= 1;
        if (rawbits[index])
          ++num;
      }
      return num;
    }

    public enum Table
    {
      UPPER,
      LOWER,
      MIXED,
      DIGIT,
      PUNCT,
      BINARY,
    }
  }
}
