﻿// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Common.PdfDecoderResult
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System.Collections.Generic;

namespace MessagingToolkit.Barcode.Common
{
  public class PdfDecoderResult : DecoderResult
  {
    private readonly int errorCorrectionCount;
    private readonly MacroPdf417Block macroPdf417Block;

    public PdfDecoderResult(
      byte[] rawBytes,
      string text,
      List<byte[]> byteSegments,
      string ecLevel,
      int errorCorrectionCount,
      MacroPdf417Block macroPdf417Block)
      : base(rawBytes, text, byteSegments, ecLevel)
    {
      this.errorCorrectionCount = errorCorrectionCount;
      this.macroPdf417Block = macroPdf417Block;
    }

    public MacroPdf417Block MacroPdf417Block => this.macroPdf417Block;

    public int ErrorCorrectionCount => this.errorCorrectionCount;
  }
}