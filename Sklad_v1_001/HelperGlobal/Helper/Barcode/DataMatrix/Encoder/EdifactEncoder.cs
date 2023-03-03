// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.Encoder.EdifactEncoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;
using System.Text;

namespace MessagingToolkit.Barcode.DataMatrix.Encoder
{
  internal class EdifactEncoder : MessagingToolkit.Barcode.DataMatrix.Encoder.Encoder
  {
    public virtual int EncodingMode => 4;

    public virtual void Encode(EncoderContext context)
    {
      StringBuilder stringBuilder = new StringBuilder();
      while (context.HasMoreCharacters())
      {
        EdifactEncoder.encodeChar(context.CurrentChar, stringBuilder);
        ++context.pos;
        if (stringBuilder.Length >= 4)
        {
          context.WriteCodewords(EdifactEncoder.EncodeToCodewords(stringBuilder, 0));
          stringBuilder.Remove(0, 4);
          if (HighLevelEncoder.LookAheadTest(context.msg, context.pos, this.EncodingMode) != this.EncodingMode)
          {
            context.SignalEncoderChange(0);
            break;
          }
        }
      }
      stringBuilder.Append('\u001F');
      EdifactEncoder.HandleEOD(context, stringBuilder);
    }

    private static void HandleEOD(EncoderContext context, StringBuilder buffer)
    {
      try
      {
        int length = buffer.Length;
        switch (length)
        {
          case 0:
            return;
          case 1:
            context.UpdateSymbolInfo();
            int num1 = context.symbolInfo.dataCapacity - context.CodewordCount;
            if (context.RemainingCharacters == 0 && num1 <= 2)
              return;
            break;
        }
        if (length > 4)
          throw new ArgumentException("Count must not exceed 4");
        int num2 = length - 1;
        string codewords = EdifactEncoder.EncodeToCodewords(buffer, 0);
        bool flag = !context.HasMoreCharacters() && num2 <= 2;
        if (num2 <= 2)
        {
          context.UpdateSymbolInfo(context.CodewordCount + num2);
          if (context.symbolInfo.dataCapacity - context.CodewordCount >= 3)
          {
            flag = false;
            context.UpdateSymbolInfo(context.CodewordCount + codewords.Length);
          }
        }
        if (flag)
        {
          context.ResetSymbolInfo();
          context.pos -= num2;
        }
        else
          context.WriteCodewords(codewords);
      }
      finally
      {
        context.SignalEncoderChange(0);
      }
    }

    private static void encodeChar(char c, StringBuilder sb)
    {
      if (c >= ' ' && c <= '?')
        sb.Append(c);
      else if (c >= '@' && c <= '^')
        sb.Append((char) ((uint) c - 64U));
      else
        HighLevelEncoder.IllegalCharacter(c);
    }

    private static string EncodeToCodewords(StringBuilder sb, int startPos)
    {
      int num1 = sb.Length - startPos;
      if (num1 == 0)
        throw new ArgumentException("StringBuilder must not be empty");
      int num2 = ((int) sb[startPos] << 18) + ((num1 >= 2 ? (int) sb[startPos + 1] : (int) char.MinValue) << 12) + ((num1 >= 3 ? (int) sb[startPos + 2] : (int) char.MinValue) << 6) + (num1 >= 4 ? (int) sb[startPos + 3] : (int) char.MinValue);
      char ch1 = (char) (num2 >> 16 & (int) byte.MaxValue);
      char ch2 = (char) (num2 >> 8 & (int) byte.MaxValue);
      char ch3 = (char) (num2 & (int) byte.MaxValue);
      StringBuilder stringBuilder = new StringBuilder(3);
      stringBuilder.Append(ch1);
      if (num1 >= 2)
        stringBuilder.Append(ch2);
      if (num1 >= 3)
        stringBuilder.Append(ch3);
      return stringBuilder.ToString();
    }
  }
}
