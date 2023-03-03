// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Rss.DataCharacter
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.OneD.Rss
{
  public class DataCharacter
  {
    private readonly int val;
    private readonly int checksumPortion;

    public DataCharacter(int value, int checksumPortion)
    {
      this.val = value;
      this.checksumPortion = checksumPortion;
    }

    public int Value => this.val;

    public int ChecksumPortion => this.checksumPortion;

    public override string ToString() => this.Value.ToString() + "(" + (object) this.ChecksumPortion + (object) ')';

    public override bool Equals(object o)
    {
      if (!(o is DataCharacter))
        return false;
      DataCharacter dataCharacter = (DataCharacter) o;
      return this.Value == dataCharacter.Value && this.ChecksumPortion == dataCharacter.ChecksumPortion;
    }

    public override int GetHashCode() => this.Value ^ this.ChecksumPortion;
  }
}
