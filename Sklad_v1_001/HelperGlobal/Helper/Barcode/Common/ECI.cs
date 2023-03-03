// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Common.ECI
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;

namespace MessagingToolkit.Barcode.Common
{
  public abstract class ECI
  {
    private int val;

    public virtual int Value => this.val;

    internal ECI(int value) => this.val = value;

    public static ECI GetECIByValue(int value)
    {
      if (value < 0 || value > 999999)
        throw new ArgumentException("Bad ECI value: " + (object) value);
      return value < 900 ? (ECI) CharacterSetECI.GetCharacterSetECIByValue(value) : (ECI) null;
    }
  }
}
