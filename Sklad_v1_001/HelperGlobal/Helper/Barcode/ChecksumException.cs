﻿// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.ChecksumException
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode
{
  public sealed class ChecksumException : BarcodeDecoderException
  {
    public static readonly ChecksumException Instance = new ChecksumException();

    private ChecksumException()
    {
    }
  }
}