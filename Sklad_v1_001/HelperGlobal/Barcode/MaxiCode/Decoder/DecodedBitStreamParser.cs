// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.MaxiCode.Decoder.DecodedBitStreamParser
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System.Collections.Generic;
using System.Text;

namespace MessagingToolkit.Barcode.MaxiCode.Decoder
{
  internal sealed class DecodedBitStreamParser
  {
    private const char SHIFTA = '\uFFF0';
    private const char SHIFTB = '\uFFF1';
    private const char SHIFTC = '\uFFF2';
    private const char SHIFTD = '\uFFF3';
    private const char SHIFTE = '\uFFF4';
    private const char TWOSHIFTA = '\uFFF5';
    private const char THREESHIFTA = '\uFFF6';
    private const char LATCHA = '\uFFF7';
    private const char LATCHB = '\uFFF8';
    private const char LOCK = '\uFFF9';
    private const char ECI = '\uFFFA';
    private const char NS = '\uFFFB';
    private const char PAD = '￼';
    private const char FS = '\u001C';
    private const char GS = '\u001D';
    private const char RS = '\u001E';
    private const string NINE_DIGITS = "000000000";
    private const string THREE_DIGITS = "000";
    private static readonly string[] SETS = new string[6]
    {
      "\nABCDEFGHIJKLMNOPQRSTUVWXYZ" + (object) '\uFFFA' + (object) '\u001C' + (object) '\u001D' + (object) '\u001E' + (object) '\uFFFB' + (object) ' ' + (object) '￼' + "\"#$%&'()*+,-./0123456789:" + (object) '\uFFF1' + (object) '\uFFF2' + (object) '\uFFF3' + (object) '\uFFF4' + (object) '\uFFF8',
      "`abcdefghijklmnopqrstuvwxyz" + (object) '\uFFFA' + (object) '\u001C' + (object) '\u001D' + (object) '\u001E' + (object) '\uFFFB' + (object) '{' + (object) '￼' + "}~\u007F;<=>?[\\]^_ ,./:@!|" + (object) '￼' + (object) '\uFFF5' + (object) '\uFFF6' + (object) '￼' + (object) '\uFFF0' + (object) '\uFFF2' + (object) '\uFFF3' + (object) '\uFFF4' + (object) '\uFFF7',
      "ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚ" + (object) '\uFFFA' + (object) '\u001C' + (object) '\u001D' + (object) '\u001E' + "ÛÜÝÞßª¬±\u00B2\u00B3µ\u00B9º\u00BC\u00BD\u00BE\u0080\u0081\u0082\u0083\u0084\u0085\u0086\u0087\u0088\u0089" + (object) '\uFFF7' + (object) ' ' + (object) '\uFFF9' + (object) '\uFFF3' + (object) '\uFFF4' + (object) '\uFFF8',
      "àáâãäåæçèéêëìíîïðñòóôõö÷øùú" + (object) '\uFFFA' + (object) '\u001C' + (object) '\u001D' + (object) '\u001E' + (object) '\uFFFB' + "ûüýþÿ¡¨«¯°´·¸»¿\u008A\u008B\u008C\u008D\u008E\u008F\u0090\u0091\u0092\u0093\u0094" + (object) '\uFFF7' + (object) ' ' + (object) '\uFFF2' + (object) '\uFFF9' + (object) '\uFFF4' + (object) '\uFFF8',
      "\0\u0001\u0002\u0003\u0004\u0005\u0006\a\b\t\n\v\f\r\u000E\u000F\u0010\u0011\u0012\u0013\u0014\u0015\u0016\u0017\u0018\u0019\u001A" + (object) '\uFFFA' + (object) '￼' + (object) '￼' + (object) '\u001B' + (object) '\uFFFB' + (object) '\u001C' + (object) '\u001D' + (object) '\u001E' + "\u001F\u009F ¢£¤¥¦§©\u00AD®¶\u0095\u0096\u0097\u0098\u0099\u009A\u009B\u009C\u009D\u009E" + (object) '\uFFF7' + (object) ' ' + (object) '\uFFF2' + (object) '\uFFF3' + (object) '\uFFF9' + (object) '\uFFF8',
      "\0\u0001\u0002\u0003\u0004\u0005\u0006\a\b\t\n\v\f\r\u000E\u000F\u0010\u0011\u0012\u0013\u0014\u0015\u0016\u0017\u0018\u0019\u001A\u001B\u001C\u001D\u001E\u001F !\"#$%&'()*+,-./0123456789:;<=>?"
    };

    private DecodedBitStreamParser()
    {
    }

    internal static DecoderResult decode(byte[] bytes, int mode)
    {
      StringBuilder stringBuilder = new StringBuilder(144);
      switch (mode)
      {
        case 2:
        case 3:
          string str1 = mode != 2 ? DecodedBitStreamParser.GetPostCode3(bytes) : DecodedBitStreamParser.GetPostCode2(bytes).ToString("0000000000".Substring(0, DecodedBitStreamParser.GetPostCode2Length(bytes)));
          string str2 = DecodedBitStreamParser.GetCountry(bytes).ToString("000");
          string str3 = DecodedBitStreamParser.GetServiceClass(bytes).ToString("000");
          stringBuilder.Append(DecodedBitStreamParser.GetMessage(bytes, 10, 84));
          if (stringBuilder.ToString().StartsWith("[)>" + (object) '\u001E' + "01" + (object) '\u001D'))
          {
            stringBuilder.Insert(9, str1 + (object) '\u001D' + str2 + (object) '\u001D' + str3 + (object) '\u001D');
            break;
          }
          stringBuilder.Insert(0, str1 + (object) '\u001D' + str2 + (object) '\u001D' + str3 + (object) '\u001D');
          break;
        case 4:
          stringBuilder.Append(DecodedBitStreamParser.GetMessage(bytes, 1, 93));
          break;
        case 5:
          stringBuilder.Append(DecodedBitStreamParser.GetMessage(bytes, 1, 77));
          break;
      }
      return new DecoderResult(bytes, stringBuilder.ToString(), (List<byte[]>) null, mode.ToString());
    }

    private static int GetBit(int bit, byte[] bytes)
    {
      --bit;
      return ((int) bytes[bit / 6] & 1 << 5 - bit % 6) != 0 ? 1 : 0;
    }

    private static int GetInt(byte[] bytes, byte[] x)
    {
      int num = 0;
      for (int index = 0; index < x.Length; ++index)
        num += DecodedBitStreamParser.GetBit((int) x[index], bytes) << x.Length - index - 1;
      return num;
    }

    private static int GetCountry(byte[] bytes) => DecodedBitStreamParser.GetInt(bytes, new byte[10]
    {
      (byte) 53,
      (byte) 54,
      (byte) 43,
      (byte) 44,
      (byte) 45,
      (byte) 46,
      (byte) 47,
      (byte) 48,
      (byte) 37,
      (byte) 38
    });

    private static int GetServiceClass(byte[] bytes) => DecodedBitStreamParser.GetInt(bytes, new byte[10]
    {
      (byte) 55,
      (byte) 56,
      (byte) 57,
      (byte) 58,
      (byte) 59,
      (byte) 60,
      (byte) 49,
      (byte) 50,
      (byte) 51,
      (byte) 52
    });

    private static int GetPostCode2Length(byte[] bytes) => DecodedBitStreamParser.GetInt(bytes, new byte[6]
    {
      (byte) 39,
      (byte) 40,
      (byte) 41,
      (byte) 42,
      (byte) 31,
      (byte) 32
    });

    private static int GetPostCode2(byte[] bytes) => DecodedBitStreamParser.GetInt(bytes, new byte[30]
    {
      (byte) 33,
      (byte) 34,
      (byte) 35,
      (byte) 36,
      (byte) 25,
      (byte) 26,
      (byte) 27,
      (byte) 28,
      (byte) 29,
      (byte) 30,
      (byte) 19,
      (byte) 20,
      (byte) 21,
      (byte) 22,
      (byte) 23,
      (byte) 24,
      (byte) 13,
      (byte) 14,
      (byte) 15,
      (byte) 16,
      (byte) 17,
      (byte) 18,
      (byte) 7,
      (byte) 8,
      (byte) 9,
      (byte) 10,
      (byte) 11,
      (byte) 12,
      (byte) 1,
      (byte) 2
    });

    private static string GetPostCode3(byte[] bytes) => new char[6]
    {
      DecodedBitStreamParser.SETS[0][DecodedBitStreamParser.GetInt(bytes, new byte[6]
      {
        (byte) 39,
        (byte) 40,
        (byte) 41,
        (byte) 42,
        (byte) 31,
        (byte) 32
      })],
      DecodedBitStreamParser.SETS[0][DecodedBitStreamParser.GetInt(bytes, new byte[6]
      {
        (byte) 33,
        (byte) 34,
        (byte) 35,
        (byte) 36,
        (byte) 25,
        (byte) 26
      })],
      DecodedBitStreamParser.SETS[0][DecodedBitStreamParser.GetInt(bytes, new byte[6]
      {
        (byte) 27,
        (byte) 28,
        (byte) 29,
        (byte) 30,
        (byte) 19,
        (byte) 20
      })],
      DecodedBitStreamParser.SETS[0][DecodedBitStreamParser.GetInt(bytes, new byte[6]
      {
        (byte) 21,
        (byte) 22,
        (byte) 23,
        (byte) 24,
        (byte) 13,
        (byte) 14
      })],
      DecodedBitStreamParser.SETS[0][DecodedBitStreamParser.GetInt(bytes, new byte[6]
      {
        (byte) 15,
        (byte) 16,
        (byte) 17,
        (byte) 18,
        (byte) 7,
        (byte) 8
      })],
      DecodedBitStreamParser.SETS[0][DecodedBitStreamParser.GetInt(bytes, new byte[6]
      {
        (byte) 9,
        (byte) 10,
        (byte) 11,
        (byte) 12,
        (byte) 1,
        (byte) 2
      })]
    }.ToString();

    private static string GetMessage(byte[] bytes, int start, int len)
    {
      StringBuilder stringBuilder = new StringBuilder();
      int num1 = -1;
      int index1 = 0;
      int num2 = 0;
      for (int index2 = start; index2 < start + len; ++index2)
      {
        char ch = DecodedBitStreamParser.SETS[index1][(int) bytes[index2]];
        switch (ch)
        {
          case '\uFFF0':
          case '\uFFF1':
          case '\uFFF2':
          case '\uFFF3':
          case '\uFFF4':
            num2 = index1;
            index1 = (int) ch - 65520;
            num1 = 1;
            break;
          case '\uFFF5':
            num2 = index1;
            index1 = 0;
            num1 = 2;
            break;
          case '\uFFF6':
            num2 = index1;
            index1 = 0;
            num1 = 3;
            break;
          case '\uFFF7':
            index1 = 0;
            num1 = -1;
            break;
          case '\uFFF8':
            index1 = 1;
            num1 = -1;
            break;
          case '\uFFF9':
            num1 = -1;
            break;
          case '\uFFFB':
            int num3;
            int num4;
            int num5;
            int num6;
            (((int) bytes[num3 = index2 + 1] << 24) + ((int) bytes[num4 = num3 + 1] << 18) + ((int) bytes[num5 = num4 + 1] << 12) + ((int) bytes[num6 = num5 + 1] << 6) + (int) bytes[index2 = num6 + 1]).ToString("000000000");
            break;
          default:
            stringBuilder.Append(ch);
            break;
        }
        if (num1-- == 0)
          index1 = num2;
      }
      while (stringBuilder.Length > 0 && stringBuilder[stringBuilder.Length - 1] == '￼')
        --stringBuilder.Length;
      return stringBuilder.ToString();
    }
  }
}
