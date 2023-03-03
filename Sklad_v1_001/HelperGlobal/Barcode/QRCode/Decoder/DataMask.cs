// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.QRCode.Decoder.DataMask
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System;

namespace MessagingToolkit.Barcode.QRCode.Decoder
{
  internal abstract class DataMask
  {
    private static readonly DataMask[] DATA_MASKS = new DataMask[8]
    {
      (DataMask) new DataMask.DataMask000(),
      (DataMask) new DataMask.DataMask001(),
      (DataMask) new DataMask.DataMask010(),
      (DataMask) new DataMask.DataMask011(),
      (DataMask) new DataMask.DataMask100(),
      (DataMask) new DataMask.DataMask101(),
      (DataMask) new DataMask.DataMask110(),
      (DataMask) new DataMask.DataMask111()
    };

    private DataMask()
    {
    }

    internal void UnmaskBitMatrix(BitMatrix bits, int dimension)
    {
      for (int index1 = 0; index1 < dimension; ++index1)
      {
        for (int index2 = 0; index2 < dimension; ++index2)
        {
          if (this.IsMasked(index1, index2))
            bits.Flip(index2, index1);
        }
      }
    }

    internal abstract bool IsMasked(int i, int j);

    internal static DataMask ForReference(int reference) => reference >= 0 && reference <= 7 ? DataMask.DATA_MASKS[reference] : throw new ArgumentException();

    private sealed class DataMask000 : DataMask
    {
      internal override bool IsMasked(int i, int j) => (i + j & 1) == 0;
    }

    private sealed class DataMask001 : DataMask
    {
      internal override bool IsMasked(int i, int j) => (i & 1) == 0;
    }

    private sealed class DataMask010 : DataMask
    {
      internal override bool IsMasked(int i, int j) => j % 3 == 0;
    }

    private sealed class DataMask011 : DataMask
    {
      internal override bool IsMasked(int i, int j) => (i + j) % 3 == 0;
    }

    private sealed class DataMask100 : DataMask
    {
      internal override bool IsMasked(int i, int j) => ((int) ((uint) i >> 1) + j / 3 & 1) == 0;
    }

    private sealed class DataMask101 : DataMask
    {
      internal override bool IsMasked(int i, int j)
      {
        int num = i * j;
        return (num & 1) + num % 3 == 0;
      }
    }

    private sealed class DataMask110 : DataMask
    {
      internal override bool IsMasked(int i, int j)
      {
        int num = i * j;
        return ((num & 1) + num % 3 & 1) == 0;
      }
    }

    private sealed class DataMask111 : DataMask
    {
      internal override bool IsMasked(int i, int j) => ((i + j & 1) + i * j % 3 & 1) == 0;
    }
  }
}
