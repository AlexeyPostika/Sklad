// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Code128
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;
using System.Collections.Generic;
using System.Data;

namespace MessagingToolkit.Barcode.OneD
{
  internal class Code128 : BarcodeCommon, IBarcode
  {
    private DataTable C128_Code = new DataTable("C128");
    private List<string> _FormattedData = new List<string>();
    private List<string> _EncodedData = new List<string>();
    private DataRow StartCharacter;
    private Code128.Types type;

    public Code128(string input) => this.rawData = input;

    public Code128(string input, Code128.Types type)
    {
      this.type = type;
      this.rawData = input;
    }

    private string Encode_Code128()
    {
      this.init_Code128();
      return this.GetEncoding();
    }

    private void init_Code128()
    {
      this.C128_Code.CaseSensitive = true;
      this.C128_Code.Columns.Add("Value", typeof (string));
      this.C128_Code.Columns.Add("A", typeof (string));
      this.C128_Code.Columns.Add("B", typeof (string));
      this.C128_Code.Columns.Add("C", typeof (string));
      this.C128_Code.Columns.Add("Encoding", typeof (string));
      this.C128_Code.Rows.Add((object) "0", (object) " ", (object) " ", (object) "00", (object) "11011001100");
      this.C128_Code.Rows.Add((object) "1", (object) "!", (object) "!", (object) "01", (object) "11001101100");
      this.C128_Code.Rows.Add((object) "2", (object) "\"", (object) "\"", (object) "02", (object) "11001100110");
      this.C128_Code.Rows.Add((object) "3", (object) "#", (object) "#", (object) "03", (object) "10010011000");
      this.C128_Code.Rows.Add((object) "4", (object) "$", (object) "$", (object) "04", (object) "10010001100");
      this.C128_Code.Rows.Add((object) "5", (object) "%", (object) "%", (object) "05", (object) "10001001100");
      this.C128_Code.Rows.Add((object) "6", (object) "&", (object) "&", (object) "06", (object) "10011001000");
      this.C128_Code.Rows.Add((object) "7", (object) "'", (object) "'", (object) "07", (object) "10011000100");
      this.C128_Code.Rows.Add((object) "8", (object) "(", (object) "(", (object) "08", (object) "10001100100");
      this.C128_Code.Rows.Add((object) "9", (object) ")", (object) ")", (object) "09", (object) "11001001000");
      this.C128_Code.Rows.Add((object) "10", (object) "*", (object) "*", (object) "10", (object) "11001000100");
      this.C128_Code.Rows.Add((object) "11", (object) "+", (object) "+", (object) "11", (object) "11000100100");
      this.C128_Code.Rows.Add((object) "12", (object) ",", (object) ",", (object) "12", (object) "10110011100");
      this.C128_Code.Rows.Add((object) "13", (object) "-", (object) "-", (object) "13", (object) "10011011100");
      this.C128_Code.Rows.Add((object) "14", (object) ".", (object) ".", (object) "14", (object) "10011001110");
      this.C128_Code.Rows.Add((object) "15", (object) "/", (object) "/", (object) "15", (object) "10111001100");
      this.C128_Code.Rows.Add((object) "16", (object) "0", (object) "0", (object) "16", (object) "10011101100");
      this.C128_Code.Rows.Add((object) "17", (object) "1", (object) "1", (object) "17", (object) "10011100110");
      this.C128_Code.Rows.Add((object) "18", (object) "2", (object) "2", (object) "18", (object) "11001110010");
      this.C128_Code.Rows.Add((object) "19", (object) "3", (object) "3", (object) "19", (object) "11001011100");
      this.C128_Code.Rows.Add((object) "20", (object) "4", (object) "4", (object) "20", (object) "11001001110");
      this.C128_Code.Rows.Add((object) "21", (object) "5", (object) "5", (object) "21", (object) "11011100100");
      this.C128_Code.Rows.Add((object) "22", (object) "6", (object) "6", (object) "22", (object) "11001110100");
      this.C128_Code.Rows.Add((object) "23", (object) "7", (object) "7", (object) "23", (object) "11101101110");
      this.C128_Code.Rows.Add((object) "24", (object) "8", (object) "8", (object) "24", (object) "11101001100");
      this.C128_Code.Rows.Add((object) "25", (object) "9", (object) "9", (object) "25", (object) "11100101100");
      this.C128_Code.Rows.Add((object) "26", (object) ":", (object) ":", (object) "26", (object) "11100100110");
      this.C128_Code.Rows.Add((object) "27", (object) ";", (object) ";", (object) "27", (object) "11101100100");
      this.C128_Code.Rows.Add((object) "28", (object) "<", (object) "<", (object) "28", (object) "11100110100");
      this.C128_Code.Rows.Add((object) "29", (object) "=", (object) "=", (object) "29", (object) "11100110010");
      this.C128_Code.Rows.Add((object) "30", (object) ">", (object) ">", (object) "30", (object) "11011011000");
      this.C128_Code.Rows.Add((object) "31", (object) "?", (object) "?", (object) "31", (object) "11011000110");
      this.C128_Code.Rows.Add((object) "32", (object) "@", (object) "@", (object) "32", (object) "11000110110");
      this.C128_Code.Rows.Add((object) "33", (object) "A", (object) "A", (object) "33", (object) "10100011000");
      this.C128_Code.Rows.Add((object) "34", (object) "B", (object) "B", (object) "34", (object) "10001011000");
      this.C128_Code.Rows.Add((object) "35", (object) "C", (object) "C", (object) "35", (object) "10001000110");
      this.C128_Code.Rows.Add((object) "36", (object) "D", (object) "D", (object) "36", (object) "10110001000");
      this.C128_Code.Rows.Add((object) "37", (object) "E", (object) "E", (object) "37", (object) "10001101000");
      this.C128_Code.Rows.Add((object) "38", (object) "F", (object) "F", (object) "38", (object) "10001100010");
      this.C128_Code.Rows.Add((object) "39", (object) "G", (object) "G", (object) "39", (object) "11010001000");
      this.C128_Code.Rows.Add((object) "40", (object) "H", (object) "H", (object) "40", (object) "11000101000");
      this.C128_Code.Rows.Add((object) "41", (object) "I", (object) "I", (object) "41", (object) "11000100010");
      this.C128_Code.Rows.Add((object) "42", (object) "J", (object) "J", (object) "42", (object) "10110111000");
      this.C128_Code.Rows.Add((object) "43", (object) "K", (object) "K", (object) "43", (object) "10110001110");
      this.C128_Code.Rows.Add((object) "44", (object) "L", (object) "L", (object) "44", (object) "10001101110");
      this.C128_Code.Rows.Add((object) "45", (object) "M", (object) "M", (object) "45", (object) "10111011000");
      this.C128_Code.Rows.Add((object) "46", (object) "N", (object) "N", (object) "46", (object) "10111000110");
      this.C128_Code.Rows.Add((object) "47", (object) "O", (object) "O", (object) "47", (object) "10001110110");
      this.C128_Code.Rows.Add((object) "48", (object) "P", (object) "P", (object) "48", (object) "11101110110");
      this.C128_Code.Rows.Add((object) "49", (object) "Q", (object) "Q", (object) "49", (object) "11010001110");
      this.C128_Code.Rows.Add((object) "50", (object) "R", (object) "R", (object) "50", (object) "11000101110");
      this.C128_Code.Rows.Add((object) "51", (object) "S", (object) "S", (object) "51", (object) "11011101000");
      this.C128_Code.Rows.Add((object) "52", (object) "T", (object) "T", (object) "52", (object) "11011100010");
      this.C128_Code.Rows.Add((object) "53", (object) "U", (object) "U", (object) "53", (object) "11011101110");
      this.C128_Code.Rows.Add((object) "54", (object) "V", (object) "V", (object) "54", (object) "11101011000");
      this.C128_Code.Rows.Add((object) "55", (object) "W", (object) "W", (object) "55", (object) "11101000110");
      this.C128_Code.Rows.Add((object) "56", (object) "X", (object) "X", (object) "56", (object) "11100010110");
      this.C128_Code.Rows.Add((object) "57", (object) "Y", (object) "Y", (object) "57", (object) "11101101000");
      this.C128_Code.Rows.Add((object) "58", (object) "Z", (object) "Z", (object) "58", (object) "11101100010");
      this.C128_Code.Rows.Add((object) "59", (object) "[", (object) "[", (object) "59", (object) "11100011010");
      this.C128_Code.Rows.Add((object) "60", (object) "\\", (object) "\\", (object) "60", (object) "11101111010");
      this.C128_Code.Rows.Add((object) "61", (object) "]", (object) "]", (object) "61", (object) "11001000010");
      this.C128_Code.Rows.Add((object) "62", (object) "^", (object) "^", (object) "62", (object) "11110001010");
      this.C128_Code.Rows.Add((object) "63", (object) "_", (object) "_", (object) "63", (object) "10100110000");
      this.C128_Code.Rows.Add((object) "64", (object) "\0", (object) "`", (object) "64", (object) "10100001100");
      this.C128_Code.Rows.Add((object) "65", (object) Convert.ToChar(1).ToString(), (object) "a", (object) "65", (object) "10010110000");
      this.C128_Code.Rows.Add((object) "66", (object) Convert.ToChar(2).ToString(), (object) "b", (object) "66", (object) "10010000110");
      this.C128_Code.Rows.Add((object) "67", (object) Convert.ToChar(3).ToString(), (object) "c", (object) "67", (object) "10000101100");
      this.C128_Code.Rows.Add((object) "68", (object) Convert.ToChar(4).ToString(), (object) "d", (object) "68", (object) "10000100110");
      this.C128_Code.Rows.Add((object) "69", (object) Convert.ToChar(5).ToString(), (object) "e", (object) "69", (object) "10110010000");
      this.C128_Code.Rows.Add((object) "70", (object) Convert.ToChar(6).ToString(), (object) "f", (object) "70", (object) "10110000100");
      this.C128_Code.Rows.Add((object) "71", (object) Convert.ToChar(7).ToString(), (object) "g", (object) "71", (object) "10011010000");
      this.C128_Code.Rows.Add((object) "72", (object) Convert.ToChar(8).ToString(), (object) "h", (object) "72", (object) "10011000010");
      this.C128_Code.Rows.Add((object) "73", (object) Convert.ToChar(9).ToString(), (object) "idx", (object) "73", (object) "10000110100");
      this.C128_Code.Rows.Add((object) "74", (object) Convert.ToChar(10).ToString(), (object) "j", (object) "74", (object) "10000110010");
      this.C128_Code.Rows.Add((object) "75", (object) Convert.ToChar(11).ToString(), (object) "k", (object) "75", (object) "11000010010");
      this.C128_Code.Rows.Add((object) "76", (object) Convert.ToChar(12).ToString(), (object) "l", (object) "76", (object) "11001010000");
      this.C128_Code.Rows.Add((object) "77", (object) Convert.ToChar(13).ToString(), (object) "m", (object) "77", (object) "11110111010");
      this.C128_Code.Rows.Add((object) "78", (object) Convert.ToChar(14).ToString(), (object) "n", (object) "78", (object) "11000010100");
      this.C128_Code.Rows.Add((object) "79", (object) Convert.ToChar(15).ToString(), (object) "o", (object) "79", (object) "10001111010");
      this.C128_Code.Rows.Add((object) "80", (object) Convert.ToChar(16).ToString(), (object) "p", (object) "80", (object) "10100111100");
      this.C128_Code.Rows.Add((object) "81", (object) Convert.ToChar(17).ToString(), (object) "q", (object) "81", (object) "10010111100");
      this.C128_Code.Rows.Add((object) "82", (object) Convert.ToChar(18).ToString(), (object) "r", (object) "82", (object) "10010011110");
      this.C128_Code.Rows.Add((object) "83", (object) Convert.ToChar(19).ToString(), (object) "s", (object) "83", (object) "10111100100");
      this.C128_Code.Rows.Add((object) "84", (object) Convert.ToChar(20).ToString(), (object) "t", (object) "84", (object) "10011110100");
      this.C128_Code.Rows.Add((object) "85", (object) Convert.ToChar(21).ToString(), (object) "u", (object) "85", (object) "10011110010");
      this.C128_Code.Rows.Add((object) "86", (object) Convert.ToChar(22).ToString(), (object) "v", (object) "86", (object) "11110100100");
      this.C128_Code.Rows.Add((object) "87", (object) Convert.ToChar(23).ToString(), (object) "w", (object) "87", (object) "11110010100");
      this.C128_Code.Rows.Add((object) "88", (object) Convert.ToChar(24).ToString(), (object) "x", (object) "88", (object) "11110010010");
      this.C128_Code.Rows.Add((object) "89", (object) Convert.ToChar(25).ToString(), (object) "y", (object) "89", (object) "11011011110");
      this.C128_Code.Rows.Add((object) "90", (object) Convert.ToChar(26).ToString(), (object) "z", (object) "90", (object) "11011110110");
      this.C128_Code.Rows.Add((object) "91", (object) Convert.ToChar(27).ToString(), (object) "{", (object) "91", (object) "11110110110");
      this.C128_Code.Rows.Add((object) "92", (object) Convert.ToChar(28).ToString(), (object) "|", (object) "92", (object) "10101111000");
      this.C128_Code.Rows.Add((object) "93", (object) Convert.ToChar(29).ToString(), (object) "}", (object) "93", (object) "10100011110");
      this.C128_Code.Rows.Add((object) "94", (object) Convert.ToChar(30).ToString(), (object) "~", (object) "94", (object) "10001011110");
      this.C128_Code.Rows.Add((object) "95", (object) Convert.ToChar(31).ToString(), (object) Convert.ToChar((int) sbyte.MaxValue).ToString(), (object) "95", (object) "10111101000");
      this.C128_Code.Rows.Add((object) "96", (object) Convert.ToChar(202).ToString(), (object) Convert.ToChar(202).ToString(), (object) "96", (object) "10111100010");
      this.C128_Code.Rows.Add((object) "97", (object) Convert.ToChar(201).ToString(), (object) Convert.ToChar(201).ToString(), (object) "97", (object) "11110101000");
      this.C128_Code.Rows.Add((object) "98", (object) "SHIFT", (object) "SHIFT", (object) "98", (object) "11110100010");
      this.C128_Code.Rows.Add((object) "99", (object) "CODE_C", (object) "CODE_C", (object) "99", (object) "10111011110");
      this.C128_Code.Rows.Add((object) "100", (object) "CODE_B", (object) Convert.ToChar(203).ToString(), (object) "CODE_B", (object) "10111101110");
      this.C128_Code.Rows.Add((object) "101", (object) Convert.ToChar(203).ToString(), (object) "CODE_A", (object) "CODE_A", (object) "11101011110");
      this.C128_Code.Rows.Add((object) "102", (object) Convert.ToChar(200).ToString(), (object) Convert.ToChar(200).ToString(), (object) Convert.ToChar(200).ToString(), (object) "11110101110");
      this.C128_Code.Rows.Add((object) "103", (object) "START_A", (object) "START_A", (object) "START_A", (object) "11010000100");
      this.C128_Code.Rows.Add((object) "104", (object) "START_B", (object) "START_B", (object) "START_B", (object) "11010010000");
      this.C128_Code.Rows.Add((object) "105", (object) "START_C", (object) "START_C", (object) "START_C", (object) "11010011100");
      this.C128_Code.Rows.Add((object) "", (object) "STOP", (object) "STOP", (object) "STOP", (object) "11000111010");
    }

    private List<DataRow> FindStartorCodeCharacter(string s, ref int col)
    {
      List<DataRow> startorCodeCharacter = new List<DataRow>();
      if (s.Length > 1 && char.IsNumber(s[0]) && char.IsNumber(s[1]))
      {
        if (this.StartCharacter == null)
        {
          this.StartCharacter = this.C128_Code.Select("A = 'START_C'")[0];
          startorCodeCharacter.Add(this.StartCharacter);
        }
        else
          startorCodeCharacter.Add(this.C128_Code.Select("A = 'CODE_C'")[0]);
        col = 1;
      }
      else
      {
        bool flag1 = false;
        bool flag2 = false;
        foreach (DataRow row in (InternalDataCollectionBase) this.C128_Code.Rows)
        {
          try
          {
            if (!flag1 && s == row["A"].ToString())
            {
              flag1 = true;
              col = 2;
              if (this.StartCharacter == null)
              {
                this.StartCharacter = this.C128_Code.Select("A = 'START_A'")[0];
                startorCodeCharacter.Add(this.StartCharacter);
              }
              else
                startorCodeCharacter.Add(this.C128_Code.Select("B = 'CODE_A'")[0]);
            }
            else if (!flag2 && s == row["B"].ToString())
            {
              flag2 = true;
              col = 1;
              if (this.StartCharacter == null)
              {
                this.StartCharacter = this.C128_Code.Select("A = 'START_B'")[0];
                startorCodeCharacter.Add(this.StartCharacter);
              }
              else
                startorCodeCharacter.Add(this.C128_Code.Select("A = 'CODE_B'")[0]);
            }
            else if (flag1)
            {
              if (flag2)
                break;
            }
          }
          catch (Exception ex)
          {
            this.Error("EC128-1: " + ex.Message);
          }
        }
        if (startorCodeCharacter.Count <= 0)
          this.Error("EC128-2: Could not determine start character.");
      }
      return startorCodeCharacter;
    }

    private string CalculateCheckDigit()
    {
      string str1 = this._FormattedData[0];
      uint num1 = 0;
      for (uint index = 0; (long) index < (long) this._FormattedData.Count; ++index)
      {
        string str2 = this._FormattedData[(int) index].Replace("'", "''");
        DataRow[] dataRowArray = this.C128_Code.Select("A = '" + str2 + "'");
        if (dataRowArray.Length <= 0)
          dataRowArray = this.C128_Code.Select("B = '" + str2 + "'");
        if (dataRowArray.Length <= 0)
          dataRowArray = this.C128_Code.Select("C = '" + str2 + "'");
        uint num2 = uint.Parse(dataRowArray[0]["Value"].ToString()) * (index == 0U ? 1U : index);
        num1 += num2;
      }
      return this.C128_Code.Select("Value = '" + (num1 % 103U).ToString() + "'")[0]["Encoding"].ToString();
    }

    private void BreakUpDataForEncoding()
    {
      string str1 = "";
      string str2 = this.rawData;
      if (this.type == Code128.Types.A || this.type == Code128.Types.B)
      {
        foreach (char ch in this.rawData)
          this._FormattedData.Add(ch.ToString());
      }
      else
      {
        if (this.type == Code128.Types.C)
        {
          if (!this.IsNumeric(this.rawData))
            this.Error("EC128-6: Only numeric values can be encoded with C128-C.");
          if (this.rawData.Length % 2 > 0)
            str2 = "0" + this.rawData;
        }
                foreach (char c in str2)
                {
                    if (char.IsNumber(c))
                    {
                        if (str1 == "")
                        {
                            //str1 += (string)(object)c;
                            str1 = str1 + Int32.Parse(c.ToString());
                        }
                        else
                        {
                            this._FormattedData.Add(str1 + (object)c);
                            str1 = "";
                        }
                    }
                    else
                    {
                        if (str1 != "")
                        {
                            this._FormattedData.Add(str1);
                            str1 = "";
                        }
                        this._FormattedData.Add(c.ToString());
                    }
                }

                if (!(str1 != ""))
          return;
        this._FormattedData.Add(str1);
      }
    }

    private void InsertStartandCodeCharacters()
    {
      string str = "";
      if (this.type != Code128.Types.Dynamic)
      {
        switch (this.type)
        {
          case Code128.Types.A:
            this._FormattedData.Insert(0, "START_A");
            break;
          case Code128.Types.B:
            this._FormattedData.Insert(0, "START_B");
            break;
          case Code128.Types.C:
            this._FormattedData.Insert(0, "START_C");
            break;
          default:
            this.Error("EC128-4: Unknown start type in fixed type encoding.");
            break;
        }
      }
      else
      {
        try
        {
          for (int index = 0; index < this._FormattedData.Count; ++index)
          {
            int col = 0;
            List<DataRow> startorCodeCharacter = this.FindStartorCodeCharacter(this._FormattedData[index], ref col);
            bool flag1 = false;
            foreach (DataRow dataRow in startorCodeCharacter)
            {
              if (dataRow["A"].ToString().EndsWith(str) || dataRow["B"].ToString().EndsWith(str) || dataRow["C"].ToString().EndsWith(str))
              {
                flag1 = true;
                break;
              }
            }
            if (str == "" || !flag1)
            {
              DataRow dataRow = startorCodeCharacter[0];
              bool flag2 = true;
              while (flag2)
              {
                try
                {
                  str = dataRow[col].ToString().Split('_')[1];
                  flag2 = false;
                }
                catch
                {
                  flag2 = true;
                  if (col++ > dataRow.ItemArray.Length)
                    this.Error("No start character found in CurrentCodeSet.");
                }
              }
              this._FormattedData.Insert(index++, dataRow[col].ToString());
            }
          }
        }
        catch (Exception ex)
        {
          this.Error("EC128-3: Could not insert start and code characters.\n Message: " + ex.Message);
        }
      }
    }

    private string GetEncoding()
    {
      this.BreakUpDataForEncoding();
      this.InsertStartandCodeCharacters();
      this.CalculateCheckDigit();
      string str1 = "";
      foreach (string str2 in this._FormattedData)
      {
        string str3 = str2.Replace("'", "''");
        DataRow[] dataRowArray;
        switch (this.type)
        {
          case Code128.Types.Dynamic:
            dataRowArray = this.C128_Code.Select("A = '" + str3 + "'");
            if (dataRowArray.Length <= 0)
            {
              dataRowArray = this.C128_Code.Select("B = '" + str3 + "'");
              if (dataRowArray.Length <= 0)
              {
                dataRowArray = this.C128_Code.Select("C = '" + str3 + "'");
                break;
              }
              break;
            }
            break;
          case Code128.Types.A:
            dataRowArray = this.C128_Code.Select("A = '" + str3 + "'");
            break;
          case Code128.Types.B:
            dataRowArray = this.C128_Code.Select("B = '" + str3 + "'");
            break;
          case Code128.Types.C:
            dataRowArray = this.C128_Code.Select("C = '" + str3 + "'");
            break;
          default:
            dataRowArray = (DataRow[]) null;
            break;
        }
        if (dataRowArray == null || dataRowArray.Length <= 0)
          this.Error("EC128-5: Could not find encoding of a value( " + str3 + " ) in C128 type " + this.type.ToString());
        str1 += dataRowArray[0]["Encoding"].ToString();
        this._EncodedData.Add(dataRowArray[0]["Encoding"].ToString());
      }
      string str4 = str1 + this.CalculateCheckDigit();
      this._EncodedData.Add(this.CalculateCheckDigit());
      string str5 = str4 + this.C128_Code.Select("A = 'STOP'")[0]["Encoding"].ToString();
      this._EncodedData.Add(this.C128_Code.Select("A = 'STOP'")[0]["Encoding"].ToString());
      string encoding = str5 + "11";
      this._EncodedData.Add("11");
      return encoding;
    }

    public string EncodedValue => this.Encode_Code128();

    public enum Types
    {
      Dynamic,
      A,
      B,
      C,
    }
  }
}
