// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Common.StringHelper
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Helper;
using System;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode.Common
{
  public sealed class StringHelper
  {
    public const string SHIFT_JIS = "SHIFT-JIS";
    public const string GB2312 = "GB2312";
    private const string EUC_JP = "EUC-JP";
    private const string UTF8 = "UTF-8";
    private const string ISO88591 = "ISO-8859-1";
    private const string GBK = "GB2312";
    private static string PLATFORM_DEFAULT_ENCODING = "UTF-8";
    private static readonly bool ASSUME_SHIFT_JIS = "SHIFT-JIS".Equals(StringHelper.PLATFORM_DEFAULT_ENCODING, StringComparison.OrdinalIgnoreCase) || "EUC-JP".Equals(StringHelper.PLATFORM_DEFAULT_ENCODING, StringComparison.OrdinalIgnoreCase);

    private StringHelper()
    {
    }

    public static string GuessEncoding(
      byte[] bytes,
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      if (decodingOptions != null)
      {
        string decodeOptionType = (string) BarcodeHelper.GetDecodeOptionType(decodingOptions, DecodeOptions.CharacterSet);
        if (decodeOptionType != null)
          return decodeOptionType;
      }
      int length = bytes.Length;
      bool flag1 = true;
      bool flag2 = true;
      bool flag3 = true;
      bool flag4 = true;
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      int num4 = 0;
      int num5 = 0;
      int num6 = 0;
      int num7 = 0;
      int num8 = 0;
      int num9 = 0;
      int num10 = 0;
      int num11 = 0;
      bool flag5 = bytes.Length > 3 && bytes[0] == (byte) 239 && bytes[1] == (byte) 187 && bytes[2] == (byte) 191;
      for (int index1 = 0; index1 < length && (flag1 || flag2 || flag3 || flag4); ++index1)
      {
        int num12 = (int) bytes[index1] & (int) byte.MaxValue;
        if (flag3)
        {
          if (num1 > 0)
          {
            if ((num12 & 128) == 0)
              flag3 = false;
            else
              --num1;
          }
          else if ((num12 & 128) != 0)
          {
            if ((num12 & 64) == 0)
            {
              flag3 = false;
            }
            else
            {
              ++num1;
              if ((num12 & 32) == 0)
              {
                ++num2;
              }
              else
              {
                ++num1;
                if ((num12 & 16) == 0)
                {
                  ++num3;
                }
                else
                {
                  ++num1;
                  if ((num12 & 8) == 0)
                    ++num4;
                  else
                    flag3 = false;
                }
              }
            }
          }
        }
        if (flag1)
        {
          if (num12 > (int) sbyte.MaxValue && num12 < 160)
            flag1 = false;
          else if (num12 > 159 && (num12 < 192 || num12 == 215 || num12 == 247))
            ++num11;
        }
        if (flag4)
        {
          int index2;
          for (index2 = 0; index2 < length; ++index2)
          {
            num12 = (int) bytes[index2];
            if (num12 >= 128)
            {
              if (index2 + 1 < length)
              {
                int num13 = (int) bytes[index2 + 1];
                if (num12 >= 161 && num12 <= 169 && num13 >= 161 && num13 <= 254 || num12 >= 176 && num12 <= 247 && num13 >= 161 && num13 <= 254 || num12 >= 129 && num12 <= 160 && num13 >= 64 && num13 <= 254 && num13 != (int) sbyte.MaxValue || num12 >= 170 && num12 <= 254 && num13 >= 64 && num13 <= 160 && num13 != (int) sbyte.MaxValue || num12 >= 168 && num12 <= 169 && num13 >= 64 && num13 <= 160 && num13 != (int) sbyte.MaxValue || num12 >= 170 && num12 <= 175 && num13 >= 161 && num13 <= 254 || num12 >= 248 && num12 <= 254 && num13 >= 161 && num13 <= 254 || num12 >= 161 && num12 <= 167 && num13 >= 64 && num13 <= 160 && num13 != (int) sbyte.MaxValue)
                  ++index2;
                else
                  break;
              }
              else
                break;
            }
          }
          if (index2 != length)
            flag4 = false;
        }
        if (flag2)
        {
          if (num5 > 0)
          {
            if (num12 < 64 || num12 == (int) sbyte.MaxValue || num12 > 252)
              flag2 = false;
            else
              --num5;
          }
          else if (num12 == 128 || num12 == 160 || num12 > 239)
            flag2 = false;
          else if (num12 > 160 && num12 < 224)
          {
            ++num6;
            num8 = 0;
            ++num7;
            if (num7 > num9)
              num9 = num7;
          }
          else if (num12 > (int) sbyte.MaxValue)
          {
            ++num5;
            num7 = 0;
            ++num8;
            if (num8 > num10)
              num10 = num8;
          }
          else
          {
            num7 = 0;
            num8 = 0;
          }
        }
      }
      if (flag3 && num1 > 0)
        flag3 = false;
      if (flag2 && num5 > 0)
        flag2 = false;
      return flag4 || (!flag3 || !flag5 && num2 + num3 + num4 <= 0) && (flag2 && (StringHelper.ASSUME_SHIFT_JIS || num9 >= 3 || num10 >= 3) || flag1 && flag2 || flag1 || flag2 || !flag3) ? StringHelper.PLATFORM_DEFAULT_ENCODING : "UTF-8";
    }
  }
}
