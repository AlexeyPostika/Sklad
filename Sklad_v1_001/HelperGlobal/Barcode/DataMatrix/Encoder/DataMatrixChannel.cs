// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.Encoder.DataMatrixChannel
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.DataMatrix.Encoder
{
  internal class DataMatrixChannel
  {
    private byte[] _encodedWords;

    internal byte[] Input { get; set; }

    internal DataMatrixScheme EncScheme { get; set; }

    internal DataMatrixChannelStatus Invalid { get; set; }

    internal int InputIndex { get; set; }

    internal int EncodedLength { get; set; }

    internal int CurrentLength { get; set; }

    internal int FirstCodeWord { get; set; }

    internal byte[] EncodedWords => this._encodedWords ?? (this._encodedWords = new byte[1558]);
  }
}
