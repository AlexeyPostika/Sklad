// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Rss.Expanded.BitArrayBuilder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode.OneD.Rss.Expanded
{
  internal sealed class BitArrayBuilder
  {
    private BitArrayBuilder()
    {
    }

    internal static BitArray BuildBitArray(List<ExpandedPair> pairs)
    {
      int num1 = (pairs.Count << 1) - 1;
      if (pairs[pairs.Count - 1].RightChar == null)
        --num1;
      BitArray bitArray = new BitArray(12 * num1);
      int i = 0;
      int num2 = pairs[0].RightChar.Value;
      for (int index = 11; index >= 0; --index)
      {
        if ((num2 & 1 << index) != 0)
          bitArray.Set(i);
        ++i;
      }
      for (int index1 = 1; index1 < pairs.Count; ++index1)
      {
        ExpandedPair pair = pairs[index1];
        int num3 = pair.LeftChar.Value;
        for (int index2 = 11; index2 >= 0; --index2)
        {
          if ((num3 & 1 << index2) != 0)
            bitArray.Set(i);
          ++i;
        }
        if (pair.RightChar != null)
        {
          int num4 = pair.RightChar.Value;
          for (int index3 = 11; index3 >= 0; --index3)
          {
            if ((num4 & 1 << index3) != 0)
              bitArray.Set(i);
            ++i;
          }
        }
      }
      return bitArray;
    }
  }
}
