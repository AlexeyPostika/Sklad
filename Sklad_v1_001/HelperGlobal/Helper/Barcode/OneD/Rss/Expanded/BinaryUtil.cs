// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Rss.Expanded.BinaryUtil
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System;
using System.Text;

namespace MessagingToolkit.Barcode.OneD.Rss.Expanded
{
  public sealed class BinaryUtil
  {
    private BinaryUtil()
    {
    }

    public static BitArray BuildBitArrayFromString(string data)
    {
      string str = data.Replace("1", "X").Replace("0", ".");
      BitArray bitArray = new BitArray(str.Replace(" ", "").Length);
      int i = 0;
      for (int index = 0; index < str.Length; ++index)
      {
        if (index % 9 == 0)
        {
          if (str[index] != ' ')
            throw new InvalidOperationException("space expected");
        }
        else
        {
          switch (str[index])
          {
            case 'X':
            case 'x':
              bitArray.Set(i);
              break;
          }
          ++i;
        }
      }
      return bitArray;
    }

    public static BitArray BuildBitArrayFromStringWithoutSpaces(string data)
    {
      StringBuilder stringBuilder = new StringBuilder();
      string str = data.Replace("1", "X").Replace("0", ".");
      int index = 0;
label_4:
      while (index < str.Length)
      {
        stringBuilder.Append(' ');
        int num = 0;
        while (true)
        {
          if (num < 8 && index < str.Length)
          {
            stringBuilder.Append(str[index]);
            ++index;
            ++num;
          }
          else
            goto label_4;
        }
      }
      return BinaryUtil.BuildBitArrayFromString(stringBuilder.ToString());
    }
  }
}
