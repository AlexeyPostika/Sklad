// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.UPCAEncoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode.OneD
{
  public class UPCAEncoder : IEncoder
  {
    private readonly EAN13Encoder subEncoder;

    public UPCAEncoder() => this.subEncoder = new EAN13Encoder();

    public virtual BitMatrix Encode(
      string contents,
      BarcodeFormat format,
      int width,
      int height)
    {
      return this.Encode(contents, format, width, height, (Dictionary<EncodeOptions, object>) null);
    }

    public virtual BitMatrix Encode(
      string contents,
      BarcodeFormat format,
      int width,
      int height,
      Dictionary<EncodeOptions, object> encodingOptions)
    {
      if (format != BarcodeFormat.UPCA)
        throw new ArgumentException("Can only encode UPC-A, but got " + (object) format);
      return this.subEncoder.Encode(UPCAEncoder.Preencode(contents), BarcodeFormat.EAN13, width, height, encodingOptions);
    }

    private static string Preencode(string contents)
    {
      switch (contents.Length)
      {
        case 11:
          int num = 0;
          for (int index = 0; index < 11; ++index)
            num += ((int) contents[index] - 48) * (index % 2 == 0 ? 3 : 1);
          contents += (string) (object) ((1000 - num) % 10);
          goto case 12;
        case 12:
          return '0'.ToString() + contents;
        default:
          throw new ArgumentException("Requested contents should be 11 or 12 digits long, but got " + (object) contents.Length);
      }
    }
  }
}
