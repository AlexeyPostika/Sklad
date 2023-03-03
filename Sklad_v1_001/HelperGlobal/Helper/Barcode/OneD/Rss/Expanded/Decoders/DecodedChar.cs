// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Rss.Expanded.Decoders.DecodedChar
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.OneD.Rss.Expanded.Decoders
{
  internal sealed class DecodedChar : DecodedObject
  {
    internal const char Fnc1 = '$';
    private readonly char value;

    internal DecodedChar(int newPosition, char value)
      : base(newPosition)
    {
      this.value = value;
    }

    internal char Value => this.value;

    internal bool IsFnc1 => this.value == '$';
  }
}
