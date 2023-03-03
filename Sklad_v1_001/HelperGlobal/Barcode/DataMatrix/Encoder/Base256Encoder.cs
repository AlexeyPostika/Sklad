// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.Encoder.Base256Encoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;
using System.Text;

namespace MessagingToolkit.Barcode.DataMatrix.Encoder
{
  internal class Base256Encoder : MessagingToolkit.Barcode.DataMatrix.Encoder.Encoder
  {
    public virtual int EncodingMode => 5;

    public virtual void Encode(EncoderContext context)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(char.MinValue);
      while (context.HasMoreCharacters())
      {
        char currentChar = context.CurrentChar;
        stringBuilder.Append(currentChar);
        ++context.pos;
        int encoding = HighLevelEncoder.LookAheadTest(context.msg, context.pos, this.EncodingMode);
        if (encoding != this.EncodingMode)
        {
          context.SignalEncoderChange(encoding);
          break;
        }
      }
      int num1 = stringBuilder.Length - 1;
      int num2 = 1;
      int len = context.CodewordCount + num1 + num2;
      context.UpdateSymbolInfo(len);
      bool flag = context.symbolInfo.dataCapacity - len > 0;
      if (context.HasMoreCharacters() || flag)
      {
        if (num1 <= 249)
        {
          stringBuilder[0] = (char) num1;
        }
        else
        {
          if (num1 <= 249 || num1 > 1555)
            throw new ArgumentException("Message length not in valid ranges: " + (object) num1);
          stringBuilder[0] = (char) (num1 / 250 + 249);
          stringBuilder.Insert(1, Convert.ToString(num1 % 250));
        }
      }
      int index = 0;
      for (int length = stringBuilder.Length; index < length; ++index)
        context.WriteCodeword(Base256Encoder.Randomize255State(stringBuilder[index], context.CodewordCount + 1));
    }

    private static char Randomize255State(char ch, int codewordPosition)
    {
      int num1 = 149 * codewordPosition % (int) byte.MaxValue + 1;
      int num2 = (int) ch + num1;
      return num2 <= (int) byte.MaxValue ? (char) num2 : (char) (num2 - 256);
    }
  }
}
