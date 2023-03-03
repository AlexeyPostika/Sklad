// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Rss.Expanded.Decoders.BlockParsedResult
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.OneD.Rss.Expanded.Decoders
{
  internal sealed class BlockParsedResult
  {
    private readonly DecodedInformation decodedInformation;
    private readonly bool finished;

    internal BlockParsedResult()
    {
      this.finished = true;
      this.decodedInformation = (DecodedInformation) null;
    }

    internal BlockParsedResult(bool finished)
    {
      this.finished = finished;
      this.decodedInformation = (DecodedInformation) null;
    }

    internal BlockParsedResult(DecodedInformation information, bool finished)
    {
      this.finished = finished;
      this.decodedInformation = information;
    }

    internal DecodedInformation DecodedInformation => this.decodedInformation;

    internal bool IsFinished => this.finished;
  }
}
