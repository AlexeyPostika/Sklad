// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.EAN13Encoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode.OneD
{
  public sealed class EAN13Encoder : UPCEANEncoder
  {
    private const int CODE_WIDTH = 95;

    public override BitMatrix Encode(
      string contents,
      BarcodeFormat format,
      int width,
      int height,
      Dictionary<EncodeOptions, object> encodingOptions)
    {
      if (format != BarcodeFormat.EAN13)
        throw new ArgumentException("Can only encode EAN_13, but got " + (object) format);
      return base.Encode(contents, format, width, height, encodingOptions);
    }

    public override bool[] Encode(string contents)
    {
      if (contents.Length != 13)
        throw new ArgumentException("Requested contents should be 13 digits long, but got " + (object) contents.Length);
      try
      {
        if (!UPCEANDecoder.CheckStandardUPCEANChecksum(contents))
          throw new ArgumentException("Contents do not pass checksum");
      }
      catch (MessagingToolkit.Barcode.FormatException ex)
      {
        throw new ArgumentException("Illegal contents");
      }
      int index1 = int.Parse(contents.Substring(0, 1));
      int firstDigitEncoding = EAN13Decoder.FirstDigitEncodings[index1];
      bool[] target = new bool[95];
      int pos1 = 0;
      int pos2 = pos1 + OneDEncoder.AppendPattern(target, pos1, UPCEANDecoder.StartEndPattern, true);
      for (int startIndex = 1; startIndex <= 6; ++startIndex)
      {
        int index2 = int.Parse(contents.Substring(startIndex, startIndex + 1 - startIndex));
        if ((firstDigitEncoding >> 6 - startIndex & 1) == 1)
          index2 += 10;
        pos2 += OneDEncoder.AppendPattern(target, pos2, UPCEANDecoder.LAndGPatterns[index2], false);
      }
      int pos3 = pos2 + OneDEncoder.AppendPattern(target, pos2, UPCEANDecoder.MiddlePattern, false);
      for (int startIndex = 7; startIndex <= 12; ++startIndex)
      {
        int index3 = int.Parse(contents.Substring(startIndex, startIndex + 1 - startIndex));
        pos3 += OneDEncoder.AppendPattern(target, pos3, UPCEANDecoder.LPatterns[index3], true);
      }
      int num = pos3 + OneDEncoder.AppendPattern(target, pos3, UPCEANDecoder.StartEndPattern, true);
      return target;
    }
  }
}
