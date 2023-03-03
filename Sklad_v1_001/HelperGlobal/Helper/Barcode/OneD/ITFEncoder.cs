// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.ITFEncoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode.OneD
{
  public sealed class ITFEncoder : OneDEncoder
  {
    private static readonly int[] START_PATTERN = new int[4]
    {
      1,
      1,
      1,
      1
    };
    private static readonly int[] END_PATTERN = new int[3]
    {
      3,
      1,
      1
    };

    public override BitMatrix Encode(
      string contents,
      BarcodeFormat format,
      int width,
      int height,
      Dictionary<EncodeOptions, object> encodingOptions)
    {
      if (format != BarcodeFormat.ITF14)
        throw new ArgumentException("Can only encode ITF, but got " + (object) format);
      return base.Encode(contents, format, width, height, encodingOptions);
    }

    public override bool[] Encode(string contents)
    {
      int length = contents.Length;
      if (length % 2 != 0)
        throw new ArgumentException("The length of the input should be even");
      bool[] target = length <= 80 ? new bool[9 + 9 * length] : throw new ArgumentException("Requested contents should be less than 80 digits long, but got " + (object) length);
      int pos = OneDEncoder.AppendPattern(target, 0, ITFEncoder.START_PATTERN, true);
      for (int index1 = 0; index1 < length; index1 += 2)
      {
        int int32_1 = Convert.ToInt32(Convert.ToString(contents[index1]), 10);
        int int32_2 = Convert.ToInt32(Convert.ToString(contents[index1 + 1]), 10);
        int[] pattern = new int[18];
        for (int index2 = 0; index2 < 5; ++index2)
        {
          pattern[index2 << 1] = ITFDecoder.PATTERNS[int32_1][index2];
          pattern[(index2 << 1) + 1] = ITFDecoder.PATTERNS[int32_2][index2];
        }
        pos += OneDEncoder.AppendPattern(target, pos, pattern, true);
      }
      OneDEncoder.AppendPattern(target, pos, ITFEncoder.END_PATTERN, true);
      return target;
    }
  }
}
