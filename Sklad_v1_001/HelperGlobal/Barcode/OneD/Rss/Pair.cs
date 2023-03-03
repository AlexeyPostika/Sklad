// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Rss.Pair
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.OneD.Rss
{
  internal sealed class Pair : DataCharacter
  {
    private readonly FinderPattern finderPattern;
    private int count;

    internal Pair(int value, int checksumPortion, FinderPattern finderPattern)
      : base(value, checksumPortion)
    {
      this.finderPattern = finderPattern;
    }

    internal FinderPattern FinderPattern => this.finderPattern;

    internal int Count => this.count;

    internal void IncrementCount() => ++this.count;
  }
}
