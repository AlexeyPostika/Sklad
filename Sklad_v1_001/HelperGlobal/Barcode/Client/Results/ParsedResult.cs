// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Client.Results.ParsedResult
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System.Text;

namespace MessagingToolkit.Barcode.Client.Results
{
  public abstract class ParsedResult
  {
    private readonly ParsedResultType type;

    protected internal ParsedResult(ParsedResultType type) => this.type = type;

    public virtual ParsedResultType Type => this.type;

    public abstract string DisplayResult { get; }

    public override sealed string ToString() => this.DisplayResult;

    public static void MaybeAppend(string val, StringBuilder result)
    {
      if (val == null || val.Length <= 0)
        return;
      if (result.Length > 0)
        result.Append('\n');
      result.Append(val);
    }

    public static void MaybeAppend(string[] values, StringBuilder result)
    {
      if (values == null)
        return;
      foreach (string val in values)
        ParsedResult.MaybeAppend(val, result);
    }
  }
}
