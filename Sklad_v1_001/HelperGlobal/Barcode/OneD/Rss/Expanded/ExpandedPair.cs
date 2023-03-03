// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Rss.Expanded.ExpandedPair
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.OneD.Rss.Expanded
{
  internal sealed class ExpandedPair
  {
    private readonly bool mayBeLast;
    private readonly DataCharacter leftChar;
    private readonly DataCharacter rightChar;
    private readonly FinderPattern finderPattern;

    internal ExpandedPair(
      DataCharacter leftChar,
      DataCharacter rightChar,
      FinderPattern finderPattern,
      bool mayBeLast)
    {
      this.leftChar = leftChar;
      this.rightChar = rightChar;
      this.finderPattern = finderPattern;
      this.mayBeLast = mayBeLast;
    }

    internal bool MayBeLast => this.mayBeLast;

    internal DataCharacter LeftChar => this.leftChar;

    internal DataCharacter RightChar => this.rightChar;

    internal FinderPattern FinderPattern => this.finderPattern;

    public bool MustBeLast => this.rightChar == null;

    public override string ToString() => "[ " + (object) this.LeftChar + " , " + (object) this.RightChar + " : " + (this.FinderPattern == null ? (object) "null" : (object) this.FinderPattern.Value.ToString()) + " ]";

    public override bool Equals(object o)
    {
      if (!(o is ExpandedPair))
        return false;
      ExpandedPair expandedPair = (ExpandedPair) o;
      return ExpandedPair.EqualsOrNull((object) this.LeftChar, (object) expandedPair.LeftChar) && ExpandedPair.EqualsOrNull((object) this.RightChar, (object) expandedPair.RightChar) && ExpandedPair.EqualsOrNull((object) this.FinderPattern, (object) expandedPair.FinderPattern);
    }

    private static bool EqualsOrNull(object o1, object o2) => o1 != null ? o1.Equals(o2) : o2 == null;

    public override int GetHashCode() => ExpandedPair.hashNotNull((object) this.LeftChar) ^ ExpandedPair.hashNotNull((object) this.RightChar) ^ ExpandedPair.hashNotNull((object) this.FinderPattern);

    private static int hashNotNull(object o) => o != null ? o.GetHashCode() : 0;
  }
}
