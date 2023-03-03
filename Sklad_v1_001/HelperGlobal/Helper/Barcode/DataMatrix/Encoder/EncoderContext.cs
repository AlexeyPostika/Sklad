// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.Encoder.EncoderContext
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;
using System.Text;

namespace MessagingToolkit.Barcode.DataMatrix.Encoder
{
  internal sealed class EncoderContext
  {
    public const string DefaultEncoding = "UTF-8";
    internal string msg;
    private SymbolShapeHint shape;
    private Dimension minSize;
    private Dimension maxSize;
    internal StringBuilder codewords;
    internal int pos;
    internal int newEncoding;
    internal SymbolInfo symbolInfo;
    private int skipAtEnd;

    internal EncoderContext(string msg)
    {
      byte[] bytes;
      try
      {
        bytes = Encoding.GetEncoding("UTF-8").GetBytes(msg);
      }
      catch (Exception ex)
      {
        throw new NotSupportedException("Unsupported encoding: " + ex.Message);
      }
      StringBuilder stringBuilder = new StringBuilder(bytes.Length);
      int index = 0;
      for (int length = bytes.Length; index < length; ++index)
      {
        char ch = (char) ((uint) bytes[index] & (uint) byte.MaxValue);
        if (ch == '?' && msg[index] != '?')
          throw new ArgumentException("Message contains characters outside UTF-8 encoding.");
        stringBuilder.Append(ch);
      }
      this.msg = stringBuilder.ToString();
      this.shape = SymbolShapeHint.ForceNone;
      this.codewords = new StringBuilder(msg.Length);
      this.newEncoding = -1;
    }

    public EncoderContext(byte[] data)
    {
      StringBuilder stringBuilder = new StringBuilder(data.Length);
      int index = 0;
      for (int length = data.Length; index < length; ++index)
      {
        char ch = (char) ((uint) data[index] & (uint) byte.MaxValue);
        stringBuilder.Append(ch);
      }
      this.msg = stringBuilder.ToString();
      this.codewords = new StringBuilder(this.msg.Length);
    }

    public SymbolShapeHint SymbolShape
    {
      set => this.shape = value;
    }

    public void SetSizeConstraints(Dimension minSize, Dimension maxSize)
    {
      this.minSize = minSize;
      this.maxSize = maxSize;
    }

    public string Message => this.msg;

    public int SkipAtEnd
    {
      set => this.skipAtEnd = value;
    }

    public char CurrentChar => this.msg[this.pos];

    public char Current => this.msg[this.pos];

    public void WriteCodewords(string codewords) => this.codewords.Append(codewords);

    public void WriteCodeword(char codeword) => this.codewords.Append(codeword);

    public int CodewordCount => this.codewords.Length;

    public void SignalEncoderChange(int encoding) => this.newEncoding = encoding;

    public void ResetEncoderSignal() => this.newEncoding = -1;

    public bool HasMoreCharacters() => this.pos < this.TotalMessageCharCount;

    private int TotalMessageCharCount => this.msg.Length - this.skipAtEnd;

    public int RemainingCharacters => this.TotalMessageCharCount - this.pos;

    public void UpdateSymbolInfo() => this.UpdateSymbolInfo(this.CodewordCount);

    public void UpdateSymbolInfo(int len)
    {
      if (this.symbolInfo != null && len <= this.symbolInfo.dataCapacity)
        return;
      this.symbolInfo = SymbolInfo.Lookup(len, this.shape, this.minSize, this.maxSize, true);
    }

    public void ResetSymbolInfo() => this.symbolInfo = (SymbolInfo) null;
  }
}
