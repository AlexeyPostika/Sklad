// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.QRCode.Decoder.DataBlock
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;

namespace MessagingToolkit.Barcode.QRCode.Decoder
{
  internal sealed class DataBlock
  {
    private readonly int numDataCodewords;
    private readonly byte[] codewords;

    private DataBlock(int numDataCodewords, byte[] codewords)
    {
      this.numDataCodewords = numDataCodewords;
      this.codewords = codewords;
    }

    internal static DataBlock[] GetDataBlocks(
      byte[] rawCodewords,
      Version version,
      ErrorCorrectionLevel ecLevel)
    {
      if (rawCodewords.Length != version.TotalCodewords)
        throw new ArgumentException();
      Version.ECBlocks ecBlocksForLevel = version.GetECBlocksForLevel(ecLevel);
      int length1 = 0;
      Version.ECB[] ecBlocks = ecBlocksForLevel.GetECBlocks();
      foreach (Version.ECB ecb in ecBlocks)
        length1 += ecb.GetCount();
      DataBlock[] dataBlocks = new DataBlock[length1];
      int num1 = 0;
      foreach (Version.ECB ecb in ecBlocks)
      {
        for (int index = 0; index < ecb.GetCount(); ++index)
        {
          int dataCodewords = ecb.GetDataCodewords();
          int length2 = ecBlocksForLevel.ECCodewordsPerBlock + dataCodewords;
          dataBlocks[num1++] = new DataBlock(dataCodewords, new byte[length2]);
        }
      }
      int length3 = dataBlocks[0].codewords.Length;
      int index1 = dataBlocks.Length - 1;
      while (index1 >= 0 && dataBlocks[index1].codewords.Length != length3)
        --index1;
      int num2 = index1 + 1;
      int index2 = length3 - ecBlocksForLevel.ECCodewordsPerBlock;
      int num3 = 0;
      for (int index3 = 0; index3 < index2; ++index3)
      {
        for (int index4 = 0; index4 < num1; ++index4)
          dataBlocks[index4].codewords[index3] = rawCodewords[num3++];
      }
      for (int index5 = num2; index5 < num1; ++index5)
        dataBlocks[index5].codewords[index2] = rawCodewords[num3++];
      int length4 = dataBlocks[0].codewords.Length;
      for (int index6 = index2; index6 < length4; ++index6)
      {
        for (int index7 = 0; index7 < num1; ++index7)
        {
          int index8 = index7 < num2 ? index6 : index6 + 1;
          dataBlocks[index7].codewords[index8] = rawCodewords[num3++];
        }
      }
      return dataBlocks;
    }

    internal int NumDataCodewords => this.numDataCodewords;

    internal byte[] Codewords => this.codewords;
  }
}
