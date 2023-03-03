// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.Encoder.ASCIIEncoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;

namespace MessagingToolkit.Barcode.DataMatrix.Encoder
{
  internal class ASCIIEncoder : MessagingToolkit.Barcode.DataMatrix.Encoder.Encoder
  {
    public virtual int EncodingMode => 0;

    public virtual void Encode(EncoderContext context)
    {
      if (HighLevelEncoder.DetermineConsecutiveDigitCount(context.msg, context.pos) >= 2)
      {
        context.WriteCodeword(ASCIIEncoder.encodeASCIIDigits(context.msg[context.pos], context.msg[context.pos + 1]));
        context.pos += 2;
      }
      else
      {
        char currentChar = context.CurrentChar;
        int num = HighLevelEncoder.LookAheadTest(context.msg, context.pos, this.EncodingMode);
        if (num != this.EncodingMode)
        {
          switch (num)
          {
            case 1:
              context.WriteCodeword(HighLevelEncoder.LATCH_TO_C40);
              context.SignalEncoderChange(1);
              break;
            case 2:
              context.WriteCodeword(HighLevelEncoder.LATCH_TO_TEXT);
              context.SignalEncoderChange(2);
              break;
            case 3:
              context.WriteCodeword(HighLevelEncoder.LATCH_TO_ANSIX12);
              context.SignalEncoderChange(3);
              break;
            case 4:
              context.WriteCodeword(HighLevelEncoder.LATCH_TO_EDIFACT);
              context.SignalEncoderChange(4);
              break;
            case 5:
              context.WriteCodeword(HighLevelEncoder.LATCH_TO_BASE256);
              context.SignalEncoderChange(5);
              break;
            default:
              throw new ArgumentException("Illegal mode: " + (object) num);
          }
        }
        else if (HighLevelEncoder.IsExtendedASCII(currentChar))
        {
          context.WriteCodeword(HighLevelEncoder.UPPER_SHIFT);
          context.WriteCodeword((char) ((int) currentChar - 128 + 1));
          ++context.pos;
        }
        else
        {
          context.WriteCodeword((char) ((uint) currentChar + 1U));
          ++context.pos;
        }
      }
    }

    private static char encodeASCIIDigits(char digit1, char digit2)
    {
      if (HighLevelEncoder.IsDigit(digit1) && HighLevelEncoder.IsDigit(digit2))
        return (char) (((int) digit1 - 48) * 10 + ((int) digit2 - 48) + 130);
      throw new ArgumentException("not digits: " + (object) digit1 + (object) digit2);
    }
  }
}
