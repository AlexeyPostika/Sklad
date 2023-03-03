// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Telepen
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode.OneD
{
  internal class Telepen : BarcodeCommon, IBarcode
  {
    private static Dictionary<object, string> Telepen_Code = new Dictionary<object, string>();
    private Telepen.StartStopCode StartCode;
    private Telepen.StartStopCode StopCode = Telepen.StartStopCode.STOP1;
    private int SwitchModeIndex;
    private int iCheckSum;

    public Telepen(string input) => this.rawData = input;

    private string Encode_Telepen()
    {
      if (Telepen.Telepen_Code.Count == 0)
        this.Init_Telepen();
      this.iCheckSum = 0;
      this.SetEncodingSequence();
      string output = Telepen.Telepen_Code[(object) this.StartCode].ToString();
      switch (this.StartCode)
      {
        case Telepen.StartStopCode.START2:
          this.EncodeNumeric(this.RawData.Substring(0, this.SwitchModeIndex), ref output);
          if (this.SwitchModeIndex < this.RawData.Length)
          {
            this.EncodeSwitchMode(ref output);
            this.EncodeASCII(this.RawData.Substring(this.SwitchModeIndex), ref output);
            break;
          }
          break;
        case Telepen.StartStopCode.START3:
          this.EncodeASCII(this.RawData.Substring(0, this.SwitchModeIndex), ref output);
          this.EncodeSwitchMode(ref output);
          this.EncodeNumeric(this.RawData.Substring(this.SwitchModeIndex), ref output);
          break;
        default:
          this.EncodeASCII(this.RawData, ref output);
          break;
      }
      output += Telepen.Telepen_Code[(object) this.Calculate_Checksum(this.iCheckSum)];
      output += Telepen.Telepen_Code[(object) this.StopCode];
      return output;
    }

    private void EncodeASCII(string input, ref string output)
    {
      try
      {
        foreach (char key in input)
        {
          output += Telepen.Telepen_Code[(object) key];
          this.iCheckSum += Convert.ToInt32(key);
        }
      }
      catch
      {
        this.Error("Invalid data when encoding ASCII");
      }
    }

    private void EncodeNumeric(string input, ref string output)
    {
      try
      {
        if (input.Length % 2 > 0)
          this.Error("Numeric encoding attempted on odd number of characters");
        for (int startIndex = 0; startIndex < input.Length; startIndex += 2)
        {
          output += Telepen.Telepen_Code[(object) Convert.ToChar(int.Parse(input.Substring(startIndex, 2)) + 27)];
          this.iCheckSum += int.Parse(input.Substring(startIndex, 2)) + 27;
        }
      }
      catch
      {
        this.Error("Numeric encoding failed");
      }
    }

    private void EncodeSwitchMode(ref string output)
    {
      this.iCheckSum += 16;
      output += Telepen.Telepen_Code[(object) Convert.ToChar(16)];
    }

    private char Calculate_Checksum(int iCheckSum) => Convert.ToChar((int) sbyte.MaxValue - iCheckSum % (int) sbyte.MaxValue);

    private void SetEncodingSequence()
    {
      this.StartCode = Telepen.StartStopCode.START1;
      this.StopCode = Telepen.StartStopCode.STOP1;
      this.SwitchModeIndex = this.rawData.Length;
      int num1 = 0;
      string rawData = this.rawData;
      for (int index = 0; index < rawData.Length && char.IsNumber(rawData[index]); ++index)
        ++num1;
      if (num1 == this.rawData.Length)
      {
        this.StartCode = Telepen.StartStopCode.START2;
        this.StopCode = Telepen.StartStopCode.STOP2;
        if (this.rawData.Length % 2 <= 0)
          return;
        this.SwitchModeIndex = this.RawData.Length - 1;
      }
      else
      {
        int num2 = 0;
        for (int index = this.rawData.Length - 1; index >= 0 && char.IsNumber(this.rawData[index]); --index)
          ++num2;
        if (num1 < 4 && num2 < 4)
          return;
        if (num1 > num2)
        {
          this.StartCode = Telepen.StartStopCode.START2;
          this.StopCode = Telepen.StartStopCode.STOP2;
          this.SwitchModeIndex = num1 % 2 == 1 ? num1 - 1 : num1;
        }
        else
        {
          this.StartCode = Telepen.StartStopCode.START3;
          this.StopCode = Telepen.StartStopCode.STOP3;
          this.SwitchModeIndex = num2 % 2 == 1 ? this.rawData.Length - num2 + 1 : this.rawData.Length - num2;
        }
      }
    }

    private void Init_Telepen()
    {
      Telepen.Telepen_Code.Add((object) Convert.ToChar(0), "1110111011101110");
      Telepen.Telepen_Code.Add((object) Convert.ToChar(1), "1011101110111010");
      Telepen.Telepen_Code.Add((object) Convert.ToChar(2), "1110001110111010");
      Telepen.Telepen_Code.Add((object) Convert.ToChar(3), "1010111011101110");
      Telepen.Telepen_Code.Add((object) Convert.ToChar(4), "1110101110111010");
      Telepen.Telepen_Code.Add((object) Convert.ToChar(5), "1011100011101110");
      Telepen.Telepen_Code.Add((object) Convert.ToChar(6), "1000100011101110");
      Telepen.Telepen_Code.Add((object) Convert.ToChar(7), "1010101110111010");
      Telepen.Telepen_Code.Add((object) Convert.ToChar(8), "1110111000111010");
      Telepen.Telepen_Code.Add((object) Convert.ToChar(9), "1011101011101110");
      Telepen.Telepen_Code.Add((object) Convert.ToChar(10), "1110001011101110");
      Telepen.Telepen_Code.Add((object) Convert.ToChar(11), "1010111000111010");
      Telepen.Telepen_Code.Add((object) Convert.ToChar(12), "1110101011101110");
      Telepen.Telepen_Code.Add((object) Convert.ToChar(13), "1010001000111010");
      Telepen.Telepen_Code.Add((object) Convert.ToChar(14), "1000101000111010");
      Telepen.Telepen_Code.Add((object) Convert.ToChar(15), "1010101011101110");
      Telepen.Telepen_Code.Add((object) Convert.ToChar(16), "1110111010111010");
      Telepen.Telepen_Code.Add((object) Convert.ToChar(17), "1011101110001110");
      Telepen.Telepen_Code.Add((object) Convert.ToChar(18), "1110001110001110");
      Telepen.Telepen_Code.Add((object) Convert.ToChar(19), "1010111010111010");
      Telepen.Telepen_Code.Add((object) Convert.ToChar(20), "1110101110001110");
      Telepen.Telepen_Code.Add((object) Convert.ToChar(21), "1011100010111010");
      Telepen.Telepen_Code.Add((object) Convert.ToChar(22), "1000100010111010");
      Telepen.Telepen_Code.Add((object) Convert.ToChar(23), "1010101110001110");
      Telepen.Telepen_Code.Add((object) Convert.ToChar(24), "1110100010001110");
      Telepen.Telepen_Code.Add((object) Convert.ToChar(25), "1011101010111010");
      Telepen.Telepen_Code.Add((object) Convert.ToChar(26), "1110001010111010");
      Telepen.Telepen_Code.Add((object) Convert.ToChar(27), "1010100010001110");
      Telepen.Telepen_Code.Add((object) Convert.ToChar(28), "1110101010111010");
      Telepen.Telepen_Code.Add((object) Convert.ToChar(29), "1010001010001110");
      Telepen.Telepen_Code.Add((object) Convert.ToChar(30), "1000101010001110");
      Telepen.Telepen_Code.Add((object) Convert.ToChar(31), "1010101010111010");
      Telepen.Telepen_Code.Add((object) ' ', "1110111011100010");
      Telepen.Telepen_Code.Add((object) '!', "1011101110101110");
      Telepen.Telepen_Code.Add((object) '"', "1110001110101110");
      Telepen.Telepen_Code.Add((object) '#', "1010111011100010");
      Telepen.Telepen_Code.Add((object) '$', "1110101110101110");
      Telepen.Telepen_Code.Add((object) '%', "1011100011100010");
      Telepen.Telepen_Code.Add((object) '&', "1000100011100010");
      Telepen.Telepen_Code.Add((object) '\'', "1010101110101110");
      Telepen.Telepen_Code.Add((object) '(', "1110111000101110");
      Telepen.Telepen_Code.Add((object) ')', "1011101011100010");
      Telepen.Telepen_Code.Add((object) '*', "1110001011100010");
      Telepen.Telepen_Code.Add((object) '+', "1010111000101110");
      Telepen.Telepen_Code.Add((object) ',', "1110101011100010");
      Telepen.Telepen_Code.Add((object) '-', "1010001000101110");
      Telepen.Telepen_Code.Add((object) '.', "1000101000101110");
      Telepen.Telepen_Code.Add((object) '/', "1010101011100010");
      Telepen.Telepen_Code.Add((object) '0', "1110111010101110");
      Telepen.Telepen_Code.Add((object) '1', "1011101000100010");
      Telepen.Telepen_Code.Add((object) '2', "1110001000100010");
      Telepen.Telepen_Code.Add((object) '3', "1010111010101110");
      Telepen.Telepen_Code.Add((object) '4', "1110101000100010");
      Telepen.Telepen_Code.Add((object) '5', "1011100010101110");
      Telepen.Telepen_Code.Add((object) '6', "1000100010101110");
      Telepen.Telepen_Code.Add((object) '7', "1010101000100010");
      Telepen.Telepen_Code.Add((object) '8', "1110100010100010");
      Telepen.Telepen_Code.Add((object) '9', "1011101010101110");
      Telepen.Telepen_Code.Add((object) ':', "1110001010101110");
      Telepen.Telepen_Code.Add((object) ';', "1010100010100010");
      Telepen.Telepen_Code.Add((object) '<', "1110101010101110");
      Telepen.Telepen_Code.Add((object) '=', "1010001010100010");
      Telepen.Telepen_Code.Add((object) '>', "1000101010100010");
      Telepen.Telepen_Code.Add((object) '?', "1010101010101110");
      Telepen.Telepen_Code.Add((object) '@', "1110111011101010");
      Telepen.Telepen_Code.Add((object) 'A', "1011101110111000");
      Telepen.Telepen_Code.Add((object) 'B', "1110001110111000");
      Telepen.Telepen_Code.Add((object) 'C', "1010111011101010");
      Telepen.Telepen_Code.Add((object) 'D', "1110101110111000");
      Telepen.Telepen_Code.Add((object) 'E', "1011100011101010");
      Telepen.Telepen_Code.Add((object) 'F', "1000100011101010");
      Telepen.Telepen_Code.Add((object) 'G', "1010101110111000");
      Telepen.Telepen_Code.Add((object) 'H', "1110111000111000");
      Telepen.Telepen_Code.Add((object) 'I', "1011101011101010");
      Telepen.Telepen_Code.Add((object) 'J', "1110001011101010");
      Telepen.Telepen_Code.Add((object) 'K', "1010111000111000");
      Telepen.Telepen_Code.Add((object) 'L', "1110101011101010");
      Telepen.Telepen_Code.Add((object) 'M', "1010001000111000");
      Telepen.Telepen_Code.Add((object) 'N', "1000101000111000");
      Telepen.Telepen_Code.Add((object) 'O', "1010101011101010");
      Telepen.Telepen_Code.Add((object) 'P', "1110111010111000");
      Telepen.Telepen_Code.Add((object) 'Q', "1011101110001010");
      Telepen.Telepen_Code.Add((object) 'R', "1110001110001010");
      Telepen.Telepen_Code.Add((object) 'S', "1010111010111000");
      Telepen.Telepen_Code.Add((object) 'T', "1110101110001010");
      Telepen.Telepen_Code.Add((object) 'U', "1011100010111000");
      Telepen.Telepen_Code.Add((object) 'V', "1000100010111000");
      Telepen.Telepen_Code.Add((object) 'W', "1010101110001010");
      Telepen.Telepen_Code.Add((object) 'X', "1110100010001010");
      Telepen.Telepen_Code.Add((object) 'Y', "1011101010111000");
      Telepen.Telepen_Code.Add((object) 'Z', "1110001010111000");
      Telepen.Telepen_Code.Add((object) '[', "1010100010001010");
      Telepen.Telepen_Code.Add((object) '\\', "1110101010111000");
      Telepen.Telepen_Code.Add((object) ']', "1010001010001010");
      Telepen.Telepen_Code.Add((object) '^', "1000101010001010");
      Telepen.Telepen_Code.Add((object) '_', "1010101010111000");
      Telepen.Telepen_Code.Add((object) '`', "1110111010001000");
      Telepen.Telepen_Code.Add((object) 'a', "1011101110101010");
      Telepen.Telepen_Code.Add((object) 'b', "1110001110101010");
      Telepen.Telepen_Code.Add((object) 'c', "1010111010001000");
      Telepen.Telepen_Code.Add((object) 'd', "1110101110101010");
      Telepen.Telepen_Code.Add((object) 'e', "1011100010001000");
      Telepen.Telepen_Code.Add((object) 'f', "1000100010001000");
      Telepen.Telepen_Code.Add((object) 'g', "1010101110101010");
      Telepen.Telepen_Code.Add((object) 'h', "1110111000101010");
      Telepen.Telepen_Code.Add((object) 'i', "1011101010001000");
      Telepen.Telepen_Code.Add((object) 'j', "1110001010001000");
      Telepen.Telepen_Code.Add((object) 'k', "1010111000101010");
      Telepen.Telepen_Code.Add((object) 'l', "1110101010001000");
      Telepen.Telepen_Code.Add((object) 'm', "1010001000101010");
      Telepen.Telepen_Code.Add((object) 'n', "1000101000101010");
      Telepen.Telepen_Code.Add((object) 'o', "1010101010001000");
      Telepen.Telepen_Code.Add((object) 'p', "1110111010101010");
      Telepen.Telepen_Code.Add((object) 'q', "1011101000101000");
      Telepen.Telepen_Code.Add((object) 'r', "1110001000101000");
      Telepen.Telepen_Code.Add((object) 's', "1010111010101010");
      Telepen.Telepen_Code.Add((object) 't', "1110101000101000");
      Telepen.Telepen_Code.Add((object) 'u', "1011100010101010");
      Telepen.Telepen_Code.Add((object) 'v', "1000100010101010");
      Telepen.Telepen_Code.Add((object) 'w', "1010101000101000");
      Telepen.Telepen_Code.Add((object) 'x', "1110100010101000");
      Telepen.Telepen_Code.Add((object) 'y', "1011101010101010");
      Telepen.Telepen_Code.Add((object) 'z', "1110001010101010");
      Telepen.Telepen_Code.Add((object) '{', "1010100010101000");
      Telepen.Telepen_Code.Add((object) '|', "1110101010101010");
      Telepen.Telepen_Code.Add((object) '}', "1010001010101000");
      Telepen.Telepen_Code.Add((object) '~', "1000101010101000");
      Telepen.Telepen_Code.Add((object) Convert.ToChar((int) sbyte.MaxValue), "1010101010101010");
      Telepen.Telepen_Code.Add((object) Telepen.StartStopCode.START1, "1010101010111000");
      Telepen.Telepen_Code.Add((object) Telepen.StartStopCode.STOP1, "1110001010101010");
      Telepen.Telepen_Code.Add((object) Telepen.StartStopCode.START2, "1010101011101000");
      Telepen.Telepen_Code.Add((object) Telepen.StartStopCode.STOP2, "1110100010101010");
      Telepen.Telepen_Code.Add((object) Telepen.StartStopCode.START3, "1010101110101000");
      Telepen.Telepen_Code.Add((object) Telepen.StartStopCode.STOP3, "1110101000101010");
    }

    public string EncodedValue => this.Encode_Telepen();

    private enum StartStopCode
    {
      START1,
      STOP1,
      START2,
      STOP2,
      START3,
      STOP3,
    }
  }
}
