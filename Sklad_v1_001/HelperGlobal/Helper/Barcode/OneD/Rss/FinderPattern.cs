// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Rss.FinderPattern
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.OneD.Rss
{
  public sealed class FinderPattern
  {
    private readonly int value;
    private readonly int[] startEnd;
    private readonly ResultPoint[] resultPoints;

    public FinderPattern(int value, int[] startEnd, int start, int end, int rowNumber)
    {
      this.value = value;
      this.startEnd = startEnd;
      this.resultPoints = new ResultPoint[2]
      {
        new ResultPoint((float) start, (float) rowNumber),
        new ResultPoint((float) end, (float) rowNumber)
      };
    }

    public int Value => this.value;

    public int[] StartEnd => this.startEnd;

    public ResultPoint[] ResultPoints => this.resultPoints;

    public override bool Equals(object o) => o is FinderPattern && this.Value == ((FinderPattern) o).Value;

    public override int GetHashCode() => this.Value;
  }
}
