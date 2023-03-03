﻿// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Client.Results.AbstractDoCoMoResultParser
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.Client.Results
{
  public abstract class AbstractDoCoMoResultParser : ResultParser
  {
    internal static string[] MatchDoCoMoPrefixedField(string prefix, string rawText, bool trim) => ResultParser.MatchPrefixedField(prefix, rawText, ';', trim);

    internal static string MatchSingleDoCoMoPrefixedField(string prefix, string rawText, bool trim) => ResultParser.MatchSinglePrefixedField(prefix, rawText, ';', trim);
  }
}
