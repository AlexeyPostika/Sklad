// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.CodaBarEncoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Helper;
using System;

namespace MessagingToolkit.Barcode.OneD
{
  public sealed class CodaBarEncoder : OneDEncoder
  {
    private static readonly char[] START_CHARS = new char[4]
    {
      'A',
      'B',
      'C',
      'D'
    };
    private static readonly char[] END_CHARS = new char[4]
    {
      'T',
      'N',
      '*',
      'E'
    };

    public override bool[] Encode(string contents)
    {
      if (!CodaBarDecoder.ArrayContains(CodaBarEncoder.START_CHARS, char.ToUpper(contents[0])))
        throw new ArgumentException("Codabar should start with one of the following: " + BarcodeHelper.Join<char>(", ", CodaBarEncoder.START_CHARS));
      if (!CodaBarDecoder.ArrayContains(CodaBarEncoder.END_CHARS, char.ToUpper(contents[contents.Length - 1])))
        throw new ArgumentException("Codabar should end with one of the following: " + BarcodeHelper.Join<char>(", ", CodaBarEncoder.END_CHARS));
      int num1 = 20;
      char[] array = new char[4]{ '/', ':', '+', '.' };
      for (int index = 1; index < contents.Length - 1; ++index)
      {
        if (char.IsDigit(contents[index]) || contents[index] == '-' || contents[index] == '$')
        {
          num1 += 9;
        }
        else
        {
          if (!CodaBarDecoder.ArrayContains(array, contents[index]))
            throw new ArgumentException("Cannot Encode : '" + (object) contents[index] + (object) '\'');
          num1 += 10;
        }
      }
      bool[] flagArray = new bool[num1 + (contents.Length - 1)];
      int index1 = 0;
      for (int index2 = 0; index2 < contents.Length; ++index2)
      {
        char ch = char.ToUpper(contents[index2]);
        if (index2 == contents.Length - 1)
        {
          switch (ch)
          {
            case '*':
              ch = 'C';
              break;
            case 'E':
              ch = 'D';
              break;
            case 'N':
              ch = 'B';
              break;
            case 'T':
              ch = 'A';
              break;
          }
        }
        int num2 = 0;
        for (int index3 = 0; index3 < CodaBarDecoder.ALPHABET.Length; ++index3)
        {
          if ((int) ch == (int) CodaBarDecoder.ALPHABET[index3])
          {
            num2 = CodaBarDecoder.CHARACTER_ENCODINGS[index3];
            break;
          }
        }
        bool flag = true;
        int num3 = 0;
        int num4 = 0;
        while (num4 < 7)
        {
          flagArray[index1] = flag;
          ++index1;
          if ((num2 >> 6 - num4 & 1) == 0 || num3 == 1)
          {
            flag = !flag;
            ++num4;
            num3 = 0;
          }
          else
            ++num3;
        }
        if (index2 < contents.Length - 1)
        {
          flagArray[index1] = false;
          ++index1;
        }
      }
      return flagArray;
    }
  }
}
