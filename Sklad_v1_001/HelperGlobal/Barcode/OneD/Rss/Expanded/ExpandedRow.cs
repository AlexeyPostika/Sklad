// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Rss.Expanded.ExpandedRow
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System.Collections.Generic;

namespace MessagingToolkit.Barcode.OneD.Rss.Expanded
{
  internal sealed class ExpandedRow
  {
    internal ExpandedRow(List<ExpandedPair> pairs, int rowNumber, bool wasReversed)
    {
      this.Pairs = new List<ExpandedPair>((IEnumerable<ExpandedPair>) pairs);
      this.RowNumber = rowNumber;
      this.IsReversed = wasReversed;
    }

    internal List<ExpandedPair> Pairs { get; private set; }

    internal int RowNumber { get; private set; }

    internal bool IsReversed { get; private set; }

    internal bool IsEquivalent(List<ExpandedPair> otherPairs) => this.Pairs.Equals((object) otherPairs);

    public override string ToString() => "{ " + (object) this.Pairs + " }";

    public override bool Equals(object o)
    {
      if (!(o is ExpandedRow))
        return false;
      ExpandedRow expandedRow = (ExpandedRow) o;
      return this.Pairs.Equals((object) expandedRow.Pairs) && this.IsReversed == expandedRow.IsReversed;
    }

    public override int GetHashCode() => this.Pairs.GetHashCode() ^ this.IsReversed.GetHashCode();
  }
}
