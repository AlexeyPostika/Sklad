// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.Encoder.DmtxChannelGroup
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.DataMatrix.Encoder
{
  internal class DmtxChannelGroup
  {
    private DataMatrixChannel[] _channels;

    internal DataMatrixChannel[] Channels
    {
      get
      {
        if (this._channels == null)
        {
          this._channels = new DataMatrixChannel[6];
          for (int index = 0; index < 6; ++index)
            this._channels[index] = new DataMatrixChannel();
        }
        return this._channels;
      }
    }
  }
}
