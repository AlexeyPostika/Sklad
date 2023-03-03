// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.MsiEncoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode.OneD
{
  public sealed class MsiEncoder : OneDEncoder
  {
    private static readonly int[] StartWidths = new int[2]
    {
      2,
      1
    };
    private static readonly int[] EndWidths = new int[3]
    {
      1,
      2,
      1
    };
    private static readonly int[][] NumberWidths = new int[10][]
    {
      new int[8]{ 1, 2, 1, 2, 1, 2, 1, 2 },
      new int[8]{ 1, 2, 1, 2, 1, 2, 2, 1 },
      new int[8]{ 1, 2, 1, 2, 2, 1, 1, 2 },
      new int[8]{ 1, 2, 1, 2, 2, 1, 2, 1 },
      new int[8]{ 1, 2, 2, 1, 1, 2, 1, 2 },
      new int[8]{ 1, 2, 2, 1, 1, 2, 2, 1 },
      new int[8]{ 1, 2, 2, 1, 2, 1, 1, 2 },
      new int[8]{ 1, 2, 2, 1, 2, 1, 2, 1 },
      new int[8]{ 2, 1, 1, 2, 1, 2, 1, 2 },
      new int[8]{ 2, 1, 1, 2, 1, 2, 2, 1 }
    };

    public override BitMatrix Encode(
      string contents,
      BarcodeFormat format,
      int width,
      int height,
      Dictionary<EncodeOptions, object> encodingOptions)
    {
      if (format != BarcodeFormat.MSIMod10)
        throw new ArgumentException("Can only encode MSI, but got " + (object) format);
      return base.Encode(contents, format, width, height, encodingOptions);
    }

    public override bool[] Encode(string contents)
    {
      int length = contents.Length;
      for (int index = 0; index < length; ++index)
      {
        if (MsiDecoder.AlphabetString.IndexOf(contents[index]) < 0)
          throw new ArgumentException("Requested contents contains a not encodable character: '" + (object) contents[index] + "'");
      }
      bool[] target = new bool[3 + length * 12 + 4];
      int pos = OneDEncoder.AppendPattern(target, 0, MsiEncoder.StartWidths, true);
      for (int index1 = 0; index1 < length; ++index1)
      {
        int index2 = MsiDecoder.AlphabetString.IndexOf(contents[index1]);
        int[] numberWidth = MsiEncoder.NumberWidths[index2];
        pos += OneDEncoder.AppendPattern(target, pos, numberWidth, true);
      }
      OneDEncoder.AppendPattern(target, pos, MsiEncoder.EndWidths, true);
      return target;
    }
  }
}
