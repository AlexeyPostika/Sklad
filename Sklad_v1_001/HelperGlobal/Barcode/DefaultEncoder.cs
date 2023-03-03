// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DefaultEncoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode
{
  internal class DefaultEncoder : IEncoder
  {
    public BitMatrix Encode(string contents, BarcodeFormat format, int width, int height) => throw new NotImplementedException("Not implementation. This is the default encoder");

    public BitMatrix Encode(
      string contents,
      BarcodeFormat format,
      int width,
      int height,
      Dictionary<EncodeOptions, object> encodingOptions)
    {
      throw new NotImplementedException("Not implementation. This is the default encoder");
    }
  }
}
