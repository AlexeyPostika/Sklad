// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.Encoder.X12Encoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System.Text;

namespace MessagingToolkit.Barcode.DataMatrix.Encoder
{
  internal sealed class X12Encoder : C40Encoder
  {
    public override int EncodingMode => 3;

    public override void Encode(EncoderContext context)
    {
      StringBuilder stringBuilder = new StringBuilder();
      while (context.HasMoreCharacters())
      {
        char currentChar = context.CurrentChar;
        ++context.pos;
        this.EncodeChar(currentChar, stringBuilder);
        if (stringBuilder.Length % 3 == 0)
        {
          C40Encoder.WriteNextTriplet(context, stringBuilder);
          int encoding = HighLevelEncoder.LookAheadTest(context.msg, context.pos, this.EncodingMode);
          if (encoding != this.EncodingMode)
          {
            context.SignalEncoderChange(encoding);
            break;
          }
        }
      }
      this.HandleEOD(context, stringBuilder);
    }

    internal override int EncodeChar(char c, StringBuilder sb)
    {
      switch (c)
      {
        case '\r':
          sb.Append(char.MinValue);
          break;
        case ' ':
          sb.Append('\u0003');
          break;
        case '*':
          sb.Append('\u0001');
          break;
        case '>':
          sb.Append('\u0002');
          break;
        default:
          if (c >= '0' && c <= '9')
          {
            sb.Append((char) ((int) c - 48 + 4));
            break;
          }
          if (c >= 'A' && c <= 'Z')
          {
            sb.Append((char) ((int) c - 65 + 14));
            break;
          }
          HighLevelEncoder.IllegalCharacter(c);
          break;
      }
      return 1;
    }

    internal override void HandleEOD(EncoderContext context, StringBuilder buffer)
    {
      context.UpdateSymbolInfo();
      int num = context.symbolInfo.dataCapacity - context.CodewordCount;
      switch (buffer.Length)
      {
        case 1:
          --context.pos;
          if (num > 1)
            context.WriteCodeword(HighLevelEncoder.X12_UNLATCH);
          context.SignalEncoderChange(0);
          break;
        case 2:
          context.WriteCodeword(HighLevelEncoder.X12_UNLATCH);
          context.pos -= 2;
          context.SignalEncoderChange(0);
          break;
      }
    }
  }
}
