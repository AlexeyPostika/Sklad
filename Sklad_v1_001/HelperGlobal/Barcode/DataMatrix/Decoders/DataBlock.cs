// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.Decoders.DataBlock
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;

namespace MessagingToolkit.Barcode.DataMatrix.Decoders
{
  internal sealed class DataBlock
  {
    private readonly int numDataCodewords;
    private readonly byte[] codewords;

    private DataBlock(int numDataCodewords_0, byte[] codewords_1)
    {
      this.numDataCodewords = numDataCodewords_0;
      this.codewords = codewords_1;
    }

    internal static DataBlock[] GetDataBlocks(byte[] rawCodewords, Version version)
    {
      Version.ECBlocks ecBlocks1 = version.GetECBlocks();
      int length1 = 0;
      Version.ECB[] ecBlocks2 = ecBlocks1.GetECBlocks();
      foreach (Version.ECB ecb in ecBlocks2)
        length1 += ecb.GetCount();
      DataBlock[] dataBlocks = new DataBlock[length1];
      int num1 = 0;
      foreach (Version.ECB ecb in ecBlocks2)
      {
        for (int index = 0; index < ecb.GetCount(); ++index)
        {
          int dataCodewords = ecb.GetDataCodewords();
          int length2 = ecBlocks1.GetECCodewords() + dataCodewords;
          dataBlocks[num1++] = new DataBlock(dataCodewords, new byte[length2]);
        }
      }
      int num2 = dataBlocks[0].codewords.Length - ecBlocks1.GetECCodewords();
      int num3 = num2 - 1;
      int num4 = 0;
      for (int index1 = 0; index1 < num3; ++index1)
      {
        for (int index2 = 0; index2 < num1; ++index2)
          dataBlocks[index2].codewords[index1] = rawCodewords[num4++];
      }
      bool flag = version.GetVersionNumber() == 24;
      int num5 = flag ? 8 : num1;
      for (int index = 0; index < num5; ++index)
        dataBlocks[index].codewords[num2 - 1] = rawCodewords[num4++];
      int length3 = dataBlocks[0].codewords.Length;
      for (int index3 = num2; index3 < length3; ++index3)
      {
        for (int index4 = 0; index4 < num1; ++index4)
        {
          int index5 = !flag || index4 <= 7 ? index3 : index3 - 1;
          dataBlocks[index4].codewords[index5] = rawCodewords[num4++];
        }
      }
      if (num4 != rawCodewords.Length)
        throw new ArgumentException();
      return dataBlocks;
    }

    internal int GetNumDataCodewords() => this.numDataCodewords;

    internal byte[] GetCodewords() => this.codewords;
  }
}
