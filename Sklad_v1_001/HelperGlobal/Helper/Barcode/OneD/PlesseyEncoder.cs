// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.PlesseyEncoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode.OneD
{
  public sealed class PlesseyEncoder : OneDEncoder
  {
    private const string AlphabetString = "0123456789ABCDEF";
    private static readonly int[] StartWidths = new int[8]
    {
      14,
      11,
      14,
      11,
      5,
      20,
      14,
      11
    };
    private static readonly int[] TerminationWidths = new int[1]
    {
      25
    };
    private static readonly int[] EndWidths = new int[8]
    {
      20,
      5,
      20,
      5,
      14,
      11,
      14,
      11
    };
    private static readonly int[][] NumberWidths = new int[16][]
    {
      new int[8]{ 5, 20, 5, 20, 5, 20, 5, 20 },
      new int[8]{ 14, 11, 5, 20, 5, 20, 5, 20 },
      new int[8]{ 5, 20, 14, 11, 5, 20, 5, 20 },
      new int[8]{ 14, 11, 14, 11, 5, 20, 5, 20 },
      new int[8]{ 5, 20, 5, 20, 14, 11, 5, 20 },
      new int[8]{ 14, 11, 5, 20, 14, 11, 5, 20 },
      new int[8]{ 5, 20, 14, 11, 14, 11, 5, 20 },
      new int[8]{ 14, 11, 14, 11, 14, 11, 5, 20 },
      new int[8]{ 5, 20, 5, 20, 5, 20, 14, 11 },
      new int[8]{ 14, 11, 5, 20, 5, 20, 14, 11 },
      new int[8]{ 5, 20, 14, 11, 5, 20, 14, 11 },
      new int[8]{ 14, 11, 14, 11, 5, 20, 14, 11 },
      new int[8]{ 5, 20, 5, 20, 14, 11, 14, 11 },
      new int[8]{ 14, 11, 5, 20, 14, 11, 14, 11 },
      new int[8]{ 5, 20, 14, 11, 14, 11, 14, 11 },
      new int[8]{ 14, 11, 14, 11, 14, 11, 14, 11 }
    };
    private static readonly byte[] crcGrid = new byte[9]
    {
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 0,
      (byte) 1,
      (byte) 0,
      (byte) 0,
      (byte) 1
    };
    private static readonly int[] crc0Widths = new int[2]
    {
      5,
      20
    };
    private static readonly int[] crc1Widths = new int[2]
    {
      14,
      11
    };

    public override BitMatrix Encode(
      string contents,
      BarcodeFormat format,
      int width,
      int height,
      Dictionary<EncodeOptions, object> encodingOptions)
    {
      if (format != BarcodeFormat.ModifiedPlessey)
        throw new ArgumentException("Can only encode Plessey, but got " + (object) format);
      return base.Encode(contents, format, width, height, encodingOptions);
    }

    public override bool[] Encode(string contents)
    {
      int length = contents.Length;
      for (int index = 0; index < length; ++index)
      {
        if ("0123456789ABCDEF".IndexOf(contents[index]) < 0)
          throw new ArgumentException("Requested contents contains a not encodable character: '" + (object) contents[index] + "'");
      }
      bool[] target = new bool[200 + length * 100 + 200 + 25 + 100 + 100];
      byte[] numArray1 = new byte[4 * length + 8];
      int num1 = 0;
      int pos1 = 100;
      int pos2 = OneDEncoder.AppendPattern(target, pos1, PlesseyEncoder.StartWidths, true);
      for (int index1 = 0; index1 < length; ++index1)
      {
        int index2 = "0123456789ABCDEF".IndexOf(contents[index1]);
        int[] numberWidth = PlesseyEncoder.NumberWidths[index2];
        pos2 += OneDEncoder.AppendPattern(target, pos2, numberWidth, true);
        byte[] numArray2 = numArray1;
        int index3 = num1;
        int num2 = index3 + 1;
        int num3 = (int) (byte) (index2 & 1);
        numArray2[index3] = (byte) num3;
        byte[] numArray3 = numArray1;
        int index4 = num2;
        int num4 = index4 + 1;
        int num5 = (int) (byte) (index2 >> 1 & 1);
        numArray3[index4] = (byte) num5;
        byte[] numArray4 = numArray1;
        int index5 = num4;
        int num6 = index5 + 1;
        int num7 = (int) (byte) (index2 >> 2 & 1);
        numArray4[index5] = (byte) num7;
        byte[] numArray5 = numArray1;
        int index6 = num6;
        num1 = index6 + 1;
        int num8 = (int) (byte) (index2 >> 3 & 1);
        numArray5[index6] = (byte) num8;
      }
      for (int index7 = 0; index7 < 4 * length; ++index7)
      {
        if (numArray1[index7] != (byte) 0)
        {
          for (int index8 = 0; index8 < 9; ++index8)
            numArray1[index7 + index8] ^= PlesseyEncoder.crcGrid[index8];
        }
      }
      for (int index = 0; index < 8; ++index)
      {
        switch (numArray1[length * 4 + index])
        {
          case 0:
            pos2 += OneDEncoder.AppendPattern(target, pos2, PlesseyEncoder.crc0Widths, true);
            break;
          case 1:
            pos2 += OneDEncoder.AppendPattern(target, pos2, PlesseyEncoder.crc1Widths, true);
            break;
        }
      }
      int pos3 = pos2 + OneDEncoder.AppendPattern(target, pos2, PlesseyEncoder.TerminationWidths, true);
      OneDEncoder.AppendPattern(target, pos3, PlesseyEncoder.EndWidths, false);
      return target;
    }
  }
}
