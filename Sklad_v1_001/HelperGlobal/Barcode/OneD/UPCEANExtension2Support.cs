// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.UPCEANExtension2Support
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System.Collections.Generic;
using System.Text;

namespace MessagingToolkit.Barcode.OneD
{
  internal sealed class UPCEANExtension2Support
  {
    private readonly int[] decodeMiddleCounters;
    private readonly StringBuilder decodeRowStringBuffer;

    public UPCEANExtension2Support()
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
      Dictionary<ResultMetadataType, object> extensionString = UPCEANExtension2Support.ParseExtensionString(str);
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
      int num1 = 0;
      for (int index = 0; index < 2 && nextUnset < size; ++index)
      {
        int num2 = UPCEANDecoder.DecodeDigit(row, decodeMiddleCounters, nextUnset, UPCEANDecoder.LAndGPatterns);
        resultString.Append((char) (48 + num2 % 10));
        foreach (int num3 in decodeMiddleCounters)
          nextUnset += num3;
        if (num2 >= 10)
          num1 |= 1 << 1 - index;
        if (index != 1)
        {
          int nextSet = row.GetNextSet(nextUnset);
          nextUnset = row.GetNextUnset(nextSet);
        }
      }
      if (resultString.Length != 2)
        throw NotFoundException.Instance;
      if (int.Parse(resultString.ToString()) % 4 != num1)
        throw NotFoundException.Instance;
      return nextUnset;
    }

    private static Dictionary<ResultMetadataType, object> ParseExtensionString(
      string raw)
    {
      if (raw.Length != 2)
        return (Dictionary<ResultMetadataType, object>) null;
      return new Dictionary<ResultMetadataType, object>()
      {
        {
          ResultMetadataType.IssueNumber,
          (object) int.Parse(raw)
        }
      };
    }
  }
}
