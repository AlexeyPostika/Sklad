// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.EAN8Encoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode.OneD
{
  public sealed class EAN8Encoder : UPCEANEncoder
  {
    private const int CODE_WIDTH = 67;

    public override BitMatrix Encode(
      string contents,
      BarcodeFormat format,
      int width,
      int height,
      Dictionary<EncodeOptions, object> encodingOptions)
    {
      if (format != BarcodeFormat.EAN8)
        throw new ArgumentException("Can only encode EAN_8, but got " + (object) format);
      return base.Encode(contents, format, width, height, encodingOptions);
    }

    public override bool[] Encode(string contents)
    {
      if (contents.Length != 8)
        throw new ArgumentException("Requested contents should be 8 digits long, but got " + (object) contents.Length);
      bool[] target = new bool[67];
      int pos1 = 0;
      int pos2 = pos1 + OneDEncoder.AppendPattern(target, pos1, UPCEANDecoder.StartEndPattern, true);
      for (int startIndex = 0; startIndex <= 3; ++startIndex)
      {
        int index = int.Parse(contents.Substring(startIndex, startIndex + 1 - startIndex));
        pos2 += OneDEncoder.AppendPattern(target, pos2, UPCEANDecoder.LPatterns[index], false);
      }
      int pos3 = pos2 + OneDEncoder.AppendPattern(target, pos2, UPCEANDecoder.MiddlePattern, false);
      for (int startIndex = 4; startIndex <= 7; ++startIndex)
      {
        int index = int.Parse(contents.Substring(startIndex, startIndex + 1 - startIndex));
        pos3 += OneDEncoder.AppendPattern(target, pos3, UPCEANDecoder.LPatterns[index], true);
      }
      int num = pos3 + OneDEncoder.AppendPattern(target, pos3, UPCEANDecoder.StartEndPattern, true);
      return target;
    }
  }
}
