// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Code39
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode.OneD
{
  internal class Code39 : BarcodeCommon, IBarcode
  {
    private Dictionary<char, string> C39_Code = new Dictionary<char, string>();
    private Dictionary<string, string> ExtC39_Translation = new Dictionary<string, string>();
    private bool _AllowExtended;

    public Code39(string input) => this.rawData = input;

    public Code39(string input, bool AllowExtended)
    {
      this.rawData = input;
      this._AllowExtended = AllowExtended;
    }

    private string Encode_Code39()
    {
      this.init_Code39();
      this.init_ExtendedCode39();
      string FormattedData = "*" + this.rawData.Replace("*", "") + "*";
      if (this._AllowExtended)
        this.InsertExtendedCharsIfNeeded(ref FormattedData);
      string str1 = "";
      foreach (char key in FormattedData)
      {
        try
        {
          str1 += this.C39_Code[key].ToString();
          str1 += "0";
        }
        catch
        {
          if (this._AllowExtended)
            this.Error("Invalid data.");
          else
            this.Error("Invalid data. (Try using Extended Code39)");
        }
      }
      string str2 = str1.Substring(0, str1.Length - 1);
      this.C39_Code.Clear();
      return str2;
    }

    private void init_Code39()
    {
      this.C39_Code.Clear();
      this.C39_Code.Add('0', "101001101101");
      this.C39_Code.Add('1', "110100101011");
      this.C39_Code.Add('2', "101100101011");
      this.C39_Code.Add('3', "110110010101");
      this.C39_Code.Add('4', "101001101011");
      this.C39_Code.Add('5', "110100110101");
      this.C39_Code.Add('6', "101100110101");
      this.C39_Code.Add('7', "101001011011");
      this.C39_Code.Add('8', "110100101101");
      this.C39_Code.Add('9', "101100101101");
      this.C39_Code.Add('A', "110101001011");
      this.C39_Code.Add('B', "101101001011");
      this.C39_Code.Add('C', "110110100101");
      this.C39_Code.Add('D', "101011001011");
      this.C39_Code.Add('E', "110101100101");
      this.C39_Code.Add('F', "101101100101");
      this.C39_Code.Add('G', "101010011011");
      this.C39_Code.Add('H', "110101001101");
      this.C39_Code.Add('I', "101101001101");
      this.C39_Code.Add('J', "101011001101");
      this.C39_Code.Add('K', "110101010011");
      this.C39_Code.Add('L', "101101010011");
      this.C39_Code.Add('M', "110110101001");
      this.C39_Code.Add('N', "101011010011");
      this.C39_Code.Add('O', "110101101001");
      this.C39_Code.Add('P', "101101101001");
      this.C39_Code.Add('Q', "101010110011");
      this.C39_Code.Add('R', "110101011001");
      this.C39_Code.Add('S', "101101011001");
      this.C39_Code.Add('T', "101011011001");
      this.C39_Code.Add('U', "110010101011");
      this.C39_Code.Add('V', "100110101011");
      this.C39_Code.Add('W', "110011010101");
      this.C39_Code.Add('X', "100101101011");
      this.C39_Code.Add('Y', "110010110101");
      this.C39_Code.Add('Z', "100110110101");
      this.C39_Code.Add('-', "100101011011");
      this.C39_Code.Add('.', "110010101101");
      this.C39_Code.Add(' ', "100110101101");
      this.C39_Code.Add('$', "100100100101");
      this.C39_Code.Add('/', "100100101001");
      this.C39_Code.Add('+', "100101001001");
      this.C39_Code.Add('%', "101001001001");
      this.C39_Code.Add('*', "100101101101");
    }

    private void init_ExtendedCode39()
    {
      this.ExtC39_Translation.Clear();
      this.ExtC39_Translation.Add(Convert.ToChar(0).ToString(), "%U");
      this.ExtC39_Translation.Add(Convert.ToChar(1).ToString(), "$A");
      this.ExtC39_Translation.Add(Convert.ToChar(2).ToString(), "$B");
      this.ExtC39_Translation.Add(Convert.ToChar(3).ToString(), "$C");
      this.ExtC39_Translation.Add(Convert.ToChar(4).ToString(), "$D");
      this.ExtC39_Translation.Add(Convert.ToChar(5).ToString(), "$E");
      this.ExtC39_Translation.Add(Convert.ToChar(6).ToString(), "$F");
      this.ExtC39_Translation.Add(Convert.ToChar(7).ToString(), "$G");
      this.ExtC39_Translation.Add(Convert.ToChar(8).ToString(), "$h");
      this.ExtC39_Translation.Add(Convert.ToChar(9).ToString(), "$I");
      this.ExtC39_Translation.Add(Convert.ToChar(10).ToString(), "$J");
      this.ExtC39_Translation.Add(Convert.ToChar(11).ToString(), "$K");
      this.ExtC39_Translation.Add(Convert.ToChar(12).ToString(), "$L");
      this.ExtC39_Translation.Add(Convert.ToChar(13).ToString(), "$M");
      this.ExtC39_Translation.Add(Convert.ToChar(14).ToString(), "$N");
      this.ExtC39_Translation.Add(Convert.ToChar(15).ToString(), "$O");
      this.ExtC39_Translation.Add(Convert.ToChar(16).ToString(), "$P");
      this.ExtC39_Translation.Add(Convert.ToChar(17).ToString(), "$Q");
      this.ExtC39_Translation.Add(Convert.ToChar(18).ToString(), "$R");
      this.ExtC39_Translation.Add(Convert.ToChar(19).ToString(), "$S");
      this.ExtC39_Translation.Add(Convert.ToChar(20).ToString(), "$T");
      this.ExtC39_Translation.Add(Convert.ToChar(21).ToString(), "$U");
      this.ExtC39_Translation.Add(Convert.ToChar(22).ToString(), "$V");
      this.ExtC39_Translation.Add(Convert.ToChar(23).ToString(), "$w");
      this.ExtC39_Translation.Add(Convert.ToChar(24).ToString(), "$X");
      this.ExtC39_Translation.Add(Convert.ToChar(25).ToString(), "$Y");
      this.ExtC39_Translation.Add(Convert.ToChar(26).ToString(), "$Z");
      this.ExtC39_Translation.Add(Convert.ToChar(27).ToString(), "%A");
      this.ExtC39_Translation.Add(Convert.ToChar(28).ToString(), "%B");
      this.ExtC39_Translation.Add(Convert.ToChar(29).ToString(), "%C");
      this.ExtC39_Translation.Add(Convert.ToChar(30).ToString(), "%D");
      this.ExtC39_Translation.Add(Convert.ToChar(31).ToString(), "%E");
      this.ExtC39_Translation.Add("!", "/A");
      this.ExtC39_Translation.Add("\"", "/B");
      this.ExtC39_Translation.Add("#", "/C");
      this.ExtC39_Translation.Add("$", "/D");
      this.ExtC39_Translation.Add("%", "/E");
      this.ExtC39_Translation.Add("&", "/F");
      this.ExtC39_Translation.Add("'", "/G");
      this.ExtC39_Translation.Add("(", "/h");
      this.ExtC39_Translation.Add(")", "/I");
      this.ExtC39_Translation.Add("*", "/J");
      this.ExtC39_Translation.Add("+", "/K");
      this.ExtC39_Translation.Add(",", "/L");
      this.ExtC39_Translation.Add("/", "/O");
      this.ExtC39_Translation.Add(":", "/Z");
      this.ExtC39_Translation.Add(";", "%F");
      this.ExtC39_Translation.Add("<", "%G");
      this.ExtC39_Translation.Add("=", "%h");
      this.ExtC39_Translation.Add(">", "%I");
      this.ExtC39_Translation.Add("?", "%J");
      this.ExtC39_Translation.Add("[", "%K");
      this.ExtC39_Translation.Add("\\", "%L");
      this.ExtC39_Translation.Add("]", "%M");
      this.ExtC39_Translation.Add("^", "%N");
      this.ExtC39_Translation.Add("_", "%O");
      this.ExtC39_Translation.Add("{", "%P");
      this.ExtC39_Translation.Add("|", "%Q");
      this.ExtC39_Translation.Add("}", "%R");
      this.ExtC39_Translation.Add("~", "%S");
      this.ExtC39_Translation.Add("`", "%w");
      this.ExtC39_Translation.Add("@", "%V");
      this.ExtC39_Translation.Add("a", "+A");
      this.ExtC39_Translation.Add("b", "+B");
      this.ExtC39_Translation.Add("c", "+C");
      this.ExtC39_Translation.Add("d", "+D");
      this.ExtC39_Translation.Add("e", "+E");
      this.ExtC39_Translation.Add("f", "+F");
      this.ExtC39_Translation.Add("g", "+G");
      this.ExtC39_Translation.Add("h", "+h");
      this.ExtC39_Translation.Add("idx", "+I");
      this.ExtC39_Translation.Add("j", "+J");
      this.ExtC39_Translation.Add("k", "+K");
      this.ExtC39_Translation.Add("l", "+L");
      this.ExtC39_Translation.Add("m", "+M");
      this.ExtC39_Translation.Add("n", "+N");
      this.ExtC39_Translation.Add("o", "+O");
      this.ExtC39_Translation.Add("p", "+P");
      this.ExtC39_Translation.Add("q", "+Q");
      this.ExtC39_Translation.Add("r", "+R");
      this.ExtC39_Translation.Add("s", "+S");
      this.ExtC39_Translation.Add("t", "+T");
      this.ExtC39_Translation.Add("u", "+U");
      this.ExtC39_Translation.Add("v", "+V");
      this.ExtC39_Translation.Add("w", "+w");
      this.ExtC39_Translation.Add("X", "+X");
      this.ExtC39_Translation.Add("Y", "+Y");
      this.ExtC39_Translation.Add("z", "+Z");
      this.ExtC39_Translation.Add(Convert.ToChar((int) sbyte.MaxValue).ToString(), "%T");
    }

    private void InsertExtendedCharsIfNeeded(ref string FormattedData)
    {
      string str = "";
      foreach (char key in FormattedData)
      {
        try
        {
          this.C39_Code[key].ToString();
          str += (string) (object) key;
        }
        catch
        {
          object obj = (object) this.ExtC39_Translation[key.ToString()];
          str += obj.ToString();
        }
      }
      FormattedData = str;
    }

    public string EncodedValue => this.Encode_Code39();
  }
}
