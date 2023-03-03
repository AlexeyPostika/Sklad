// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Rss.Expanded.Decoders.DecodedNumeric
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;

namespace MessagingToolkit.Barcode.OneD.Rss.Expanded.Decoders
{
  internal sealed class DecodedNumeric : DecodedObject
  {
    internal const int Fnc1 = 10;
    private readonly int firstDigit;
    private readonly int secondDigit;

    internal DecodedNumeric(int newPosition, int firstDigit, int secondDigit)
      : base(newPosition)
    {
      this.firstDigit = firstDigit;
      this.secondDigit = secondDigit;
      if (this.firstDigit < 0 || this.firstDigit > 10)
        throw new ArgumentException("Invalid firstDigit: " + (object) firstDigit);
      if (this.secondDigit < 0 || this.secondDigit > 10)
        throw new ArgumentException("Invalid secondDigit: " + (object) secondDigit);
    }

    internal int FirstDigit => this.firstDigit;

    internal int SecondDigit => this.secondDigit;

    internal int GetValue() => this.firstDigit * 10 + this.secondDigit;

    internal bool IsFirstDigitFnc1() => this.firstDigit == 10;

    internal bool IsSecondDigitFnc1() => this.secondDigit == 10;

    internal bool IsAnyFnc1() => this.firstDigit == 10 || this.secondDigit == 10;
  }
}
