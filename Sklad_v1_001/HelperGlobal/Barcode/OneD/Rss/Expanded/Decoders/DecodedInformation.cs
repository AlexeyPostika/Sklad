// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Rss.Expanded.Decoders.DecodedInformation
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.OneD.Rss.Expanded.Decoders
{
  internal sealed class DecodedInformation : DecodedObject
  {
    private readonly string newString;
    private readonly int remainingValue;
    private readonly bool remaining;

    internal DecodedInformation(int newPosition, string newString)
      : base(newPosition)
    {
      this.newString = newString;
      this.remaining = false;
      this.remainingValue = 0;
    }

    internal DecodedInformation(int newPosition, string newString, int remainingValue)
      : base(newPosition)
    {
      this.remaining = true;
      this.remainingValue = remainingValue;
      this.newString = newString;
    }

    internal string NewString => this.newString;

    internal bool IsRemaining => this.remaining;

    internal int RemainingValue => this.remainingValue;
  }
}
