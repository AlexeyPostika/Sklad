// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.UPCEANExtension5Support
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System.Collections.Generic;
using System.Text;

namespace MessagingToolkit.Barcode.OneD
{
  internal sealed class UPCEANExtension5Support
  {
    private static readonly int[] CHECK_DIGIT_ENCODINGS = new int[10]
    {
      24,
      20,
      18,
      17,
      12,
      6,
      3,
      10,
      9,
      5
    };
    private readonly int[] decodeMiddleCounters;
    private readonly StringBuilder decodeRowStringBuffer;

    public UPCEANExtension5Support()
    {
      this.decodeMiddleCounters = new int[4];
      this.decodeRowStringBuffer = new StringBuilder();
    }

    internal Result DecodeRow(int rowNumber, BitArray row, int[] extensionStartRange)
    {
      StringBuilder decodeRowStringBuffer = this.decodeRowStringBuffer;
      decodeRowStringBuffer.Length = 0;
      int x = this.DecodeMiddle(row, extensionStartRange, decodeRowStringBuffer);
      string str = decodeRowStringBuffer.ToString();
      Dictionary<ResultMetadataType, object> extensionString = UPCEANExtension5Support.ParseExtensionString(str);
      Result result = new Result(str, (byte[]) null, new ResultPoint[2]
      {
        new ResultPoint((float) (extensionStartRange[0] + extensionStartRange[1]) / 2f, (float) rowNumber),
        new ResultPoint((float) x, (float) rowNumber)
      }, BarcodeFormat.UPCEANExtension);
      if (extensionString != null)
        result.PutAllMetadata(extensionString);
      return result;
    }

    internal int DecodeMiddle(BitArray row, int[] startRange, StringBuilder resultString)
    {
      int[] decodeMiddleCounters = this.decodeMiddleCounters;
      decodeMiddleCounters[0] = 0;
      decodeMiddleCounters[1] = 0;
      decodeMiddleCounters[2] = 0;
      decodeMiddleCounters[3] = 0;
      int size = row.GetSize();
      int nextUnset = startRange[1];
      int lgPatternFound = 0;
      for (int index = 0; index < 5 && nextUnset < size; ++index)
      {
        int num1 = UPCEANDecoder.DecodeDigit(row, decodeMiddleCounters, nextUnset, UPCEANDecoder.LAndGPatterns);
        resultString.Append((char) (48 + num1 % 10));
        foreach (int num2 in decodeMiddleCounters)
          nextUnset += num2;
        if (num1 >= 10)
          lgPatternFound |= 1 << 4 - index;
        if (index != 4)
        {
          int nextSet = row.GetNextSet(nextUnset);
          nextUnset = row.GetNextUnset(nextSet);
        }
      }
      if (resultString.Length != 5)
        throw NotFoundException.Instance;
      int checkDigit = UPCEANExtension5Support.DetermineCheckDigit(lgPatternFound);
      if (UPCEANExtension5Support.ExtensionChecksum(resultString.ToString()) != checkDigit)
        throw NotFoundException.Instance;
      return nextUnset;
    }

    private static int ExtensionChecksum(string s)
    {
      int length = s.Length;
      int num1 = 0;
      for (int index = length - 2; index >= 0; index -= 2)
        num1 += (int) s[index] - 48;
      int num2 = num1 * 3;
      for (int index = length - 1; index >= 0; index -= 2)
        num2 += (int) s[index] - 48;
      return num2 * 3 % 10;
    }

    private static int DetermineCheckDigit(int lgPatternFound)
    {
      for (int checkDigit = 0; checkDigit < 10; ++checkDigit)
      {
        if (lgPatternFound == UPCEANExtension5Support.CHECK_DIGIT_ENCODINGS[checkDigit])
          return checkDigit;
      }
      throw NotFoundException.Instance;
    }

    private static Dictionary<ResultMetadataType, object> ParseExtensionString(
      string raw)
    {
      if (raw.Length != 5)
        return (Dictionary<ResultMetadataType, object>) null;
      object extension5String = (object) UPCEANExtension5Support.ParseExtension5String(raw);
      if (extension5String == null)
        return (Dictionary<ResultMetadataType, object>) null;
      return new Dictionary<ResultMetadataType, object>()
      {
        {
          ResultMetadataType.SuggestedPrice,
          extension5String
        }
      };
    }

    private static string ParseExtension5String(string raw)
    {
      string str1;
      switch (raw[0])
      {
        case '0':
          str1 = "£";
          break;
        case '5':
          str1 = "$";
          break;
        case '9':
          if ("90000".Equals(raw))
            return (string) null;
          if ("99991".Equals(raw))
            return "0.00";
          if ("99990".Equals(raw))
            return "Used";
          str1 = "";
          break;
        default:
          str1 = "";
          break;
      }
      int num1 = int.Parse(raw.Substring(1));
      string str2 = (num1 / 100).ToString();
      int num2 = num1 % 100;
      string str3 = num2 < 10 ? "0" + (object) num2 : num2.ToString();
      return str1 + str2 + (object) '.' + str3;
    }
  }
}
