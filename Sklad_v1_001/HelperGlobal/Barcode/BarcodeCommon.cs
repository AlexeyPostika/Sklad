// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.BarcodeCommon
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode
{
  internal abstract class BarcodeCommon
  {
    protected string rawData = "";
    protected List<string> _errors = new List<string>();

    public string RawData => this.rawData;

    public List<string> Errors => this._errors;

    public void Error(string ErrorMessage)
    {
      this._errors.Add(ErrorMessage);
      throw new Exception(ErrorMessage);
    }

    public bool IsNumeric(string input)
    {
      try
      {
        int result = 0;
        if (!int.TryParse(input, out result))
        {
          foreach (char c in input)
          {
            if (!char.IsDigit(c))
              return false;
          }
        }
        return true;
      }
      catch
      {
        return false;
      }
    }
  }
}
