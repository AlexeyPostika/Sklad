// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Rss.Expanded.Decoders.DecodedObject
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.OneD.Rss.Expanded.Decoders
{
  internal abstract class DecodedObject
  {
    protected internal readonly int newPosition;

    internal DecodedObject(int newPosition) => this.newPosition = newPosition;

    internal int NewPosition => this.newPosition;
  }
}
