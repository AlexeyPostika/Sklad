// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Common.DecoderResult
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System.Collections.Generic;

namespace MessagingToolkit.Barcode.Common
{
  public class DecoderResult
  {
    private readonly byte[] rawBytes;
    private readonly string text;
    private readonly List<byte[]> byteSegments;
    private readonly string ecLevel;

    public DecoderResult(byte[] rawBytes, string text, List<byte[]> byteSegments, string ecLevel)
    {
      this.rawBytes = rawBytes;
      this.text = text;
      this.byteSegments = byteSegments;
      this.ecLevel = ecLevel;
    }

    public byte[] RawBytes => this.rawBytes;

    public string Text => this.text;

    public List<byte[]> ByteSegments => this.byteSegments;

    public string ECLevel => this.ecLevel;
  }
}
