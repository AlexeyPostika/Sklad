// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Code128Encoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode.OneD
{
  public sealed class Code128Encoder : OneDEncoder
  {
    private const int CODE_START_B = 104;
    private const int CODE_START_C = 105;
    private const int CODE_CODE_B = 100;
    private const int CODE_CODE_C = 99;
    private const int CODE_STOP = 106;
    private const char ESCAPE_FNC_1 = 'ñ';
    private const char ESCAPE_FNC_2 = 'ò';
    private const char ESCAPE_FNC_3 = 'ó';
    private const char ESCAPE_FNC_4 = 'ô';
    private const int CODE_FNC_1 = 102;
    private const int CODE_FNC_2 = 97;
    private const int CODE_FNC_3 = 96;
    private const int CODE_FNC_4_B = 100;

    public override BitMatrix Encode(
      string contents,
      BarcodeFormat format,
      int width,
      int height,
      Dictionary<EncodeOptions, object> encodingOptions)
    {
      if (format != BarcodeFormat.Code128)
        throw new ArgumentException("Can only Encode CODE_128, but got " + (object) format);
      return base.Encode(contents, format, width, height, encodingOptions);
    }

    public override bool[] Encode(string contents)
    {
      int length1 = contents.Length;
      if (length1 < 1 || length1 > 80)
        throw new ArgumentException("Contents length should be between 1 and 80 characters, but got " + (object) length1);
      for (int index = 0; index < length1; ++index)
      {
        char content = contents[index];
        if (content < ' ' || content > '~')
        {
          switch (content)
          {
            case 'ñ':
            case 'ò':
            case 'ó':
            case 'ô':
              continue;
            default:
              throw new ArgumentException("Bad character in input: " + (object) content);
          }
        }
      }
      ICollection<int[]> numArrays = (ICollection<int[]>) new List<int[]>();
      int num1 = 0;
      int num2 = 1;
      int num3 = 0;
      int num4 = 0;
      while (num4 < length1)
      {
        int length2 = num3 == 99 ? 2 : 4;
        int num5 = !Code128Encoder.IsDigits(contents, num4, length2) ? 100 : 99;
        int index;
        if (num5 == num3)
        {
          if (num3 == 100)
          {
            index = (int) contents[num4] - 32;
            ++num4;
          }
          else
          {
            switch (contents[num4])
            {
              case 'ñ':
                index = 102;
                ++num4;
                break;
              case 'ò':
                index = 97;
                ++num4;
                break;
              case 'ó':
                index = 96;
                ++num4;
                break;
              case 'ô':
                index = 100;
                ++num4;
                break;
              default:
                index = int.Parse(contents.Substring(num4, num4 + 2 - num4));
                num4 += 2;
                break;
            }
          }
        }
        else
        {
          index = num3 != 0 ? num5 : (num5 != 100 ? 105 : 104);
          num3 = num5;
        }
        numArrays.Add(Code128Decoder.CODE_PATTERNS[index]);
        num1 += index * num2;
        if (num4 != 0)
          ++num2;
      }
      int index1 = num1 % 103;
      numArrays.Add(Code128Decoder.CODE_PATTERNS[index1]);
      numArrays.Add(Code128Decoder.CODE_PATTERNS[106]);
      int length3 = 0;
      foreach (int[] numArray in (IEnumerable<int[]>) numArrays)
      {
        foreach (int num6 in numArray)
          length3 += num6;
      }
      bool[] target = new bool[length3];
      int pos = 0;
      foreach (int[] pattern in (IEnumerable<int[]>) numArrays)
        pos += OneDEncoder.AppendPattern(target, pos, pattern, true);
      return target;
    }

    private static bool IsDigits(string val, int start, int length)
    {
      int num = start + length;
      int length1 = val.Length;
      for (int index = start; index < num && index < length1; ++index)
      {
        char ch = val[index];
        if (ch < '0' || ch > '9')
        {
          if (ch != 'ñ')
            return false;
          ++num;
        }
      }
      return num <= length1;
    }
  }
}
