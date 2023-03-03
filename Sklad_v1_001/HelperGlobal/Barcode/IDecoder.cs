﻿// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.IDecoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System.Collections.Generic;

namespace MessagingToolkit.Barcode
{
  public interface IDecoder
  {
    Result Decode(BinaryBitmap binaryBitmap);

    Result Decode(
      BinaryBitmap binaryBitmap,
      Dictionary<DecodeOptions, object> decodingOptions);

    void Reset();
  }
}
