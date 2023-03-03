// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.Encoder.C40Encoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;
using System.Text;

namespace MessagingToolkit.Barcode.DataMatrix.Encoder
{
  internal class C40Encoder : MessagingToolkit.Barcode.DataMatrix.Encoder.Encoder
  {
    public virtual int EncodingMode => 1;

    public virtual void Encode(EncoderContext context)
    {
      StringBuilder stringBuilder = new StringBuilder();
      while (context.HasMoreCharacters())
      {
        char currentChar = context.CurrentChar;
        ++context.pos;
        int lastCharSize = this.EncodeChar(currentChar, stringBuilder);
        int num1 = stringBuilder.Length / 3 * 2;
        int len = context.CodewordCount + num1;
        context.UpdateSymbolInfo(len);
        int num2 = context.symbolInfo.dataCapacity - len;
        if (!context.HasMoreCharacters())
        {
          StringBuilder removed = new StringBuilder();
          if (stringBuilder.Length % 3 == 2 && (num2 < 2 || num2 > 2))
            lastCharSize = this.BacktrackOneCharacter(context, stringBuilder, removed, lastCharSize);
          while (stringBuilder.Length % 3 == 1 && (lastCharSize <= 3 && num2 != 1 || lastCharSize > 3))
            lastCharSize = this.BacktrackOneCharacter(context, stringBuilder, removed, lastCharSize);
          break;
        }
        if (stringBuilder.Length % 3 == 0)
        {
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

    private int BacktrackOneCharacter(
      EncoderContext context,
      StringBuilder buffer,
      StringBuilder removed,
      int lastCharSize)
    {
      int length = buffer.Length;
      buffer.Remove(length - lastCharSize, lastCharSize);
      --context.pos;
      lastCharSize = this.EncodeChar(context.CurrentChar, removed);
      context.ResetSymbolInfo();
      return lastCharSize;
    }

    internal static void WriteNextTriplet(EncoderContext context, StringBuilder buffer)
    {
      context.WriteCodewords(C40Encoder.EncodeToCodewords(buffer, 0));
      buffer.Remove(0, 3);
    }

    internal virtual void HandleEOD(EncoderContext context, StringBuilder buffer)
    {
      int num1 = buffer.Length / 3 * 2;
      int num2 = buffer.Length % 3;
      int len = context.CodewordCount + num1;
      context.UpdateSymbolInfo(len);
      int num3 = context.symbolInfo.dataCapacity - len;
      if (num2 == 2)
      {
        buffer.Append(char.MinValue);
        while (buffer.Length >= 3)
          C40Encoder.WriteNextTriplet(context, buffer);
        if (context.HasMoreCharacters())
          context.WriteCodeword(HighLevelEncoder.C40_UNLATCH);
      }
      else if (num3 == 1 && num2 == 1)
      {
        while (buffer.Length >= 3)
          C40Encoder.WriteNextTriplet(context, buffer);
        if (context.HasMoreCharacters())
          context.WriteCodeword(HighLevelEncoder.C40_UNLATCH);
        --context.pos;
      }
      else
      {
        if (num2 != 0)
          throw new ArgumentException("Unexpected case. Please report!");
        while (buffer.Length >= 3)
          C40Encoder.WriteNextTriplet(context, buffer);
        if (num3 > 0 || context.HasMoreCharacters())
          context.WriteCodeword(HighLevelEncoder.C40_UNLATCH);
      }
      context.SignalEncoderChange(0);
    }

    internal virtual int EncodeChar(char c, StringBuilder sb)
    {
      if (c == ' ')
      {
        sb.Append('\u0003');
        return 1;
      }
      if (c >= '0' && c <= '9')
      {
        sb.Append((char) ((int) c - 48 + 4));
        return 1;
      }
      if (c >= 'A' && c <= 'Z')
      {
        sb.Append((char) ((int) c - 65 + 14));
        return 1;
      }
      if (c >= char.MinValue && c <= '\u001F')
      {
        sb.Append(char.MinValue);
        sb.Append(c);
        return 2;
      }
      if (c >= '!' && c <= '/')
      {
        sb.Append('\u0001');
        sb.Append((char) ((uint) c - 33U));
        return 2;
      }
      if (c >= ':' && c <= '@')
      {
        sb.Append('\u0001');
        sb.Append((char) ((int) c - 58 + 15));
        return 2;
      }
      if (c >= '[' && c <= '_')
      {
        sb.Append('\u0001');
        sb.Append((char) ((int) c - 91 + 22));
        return 2;
      }
      if (c >= '`' && c <= '\u007F')
      {
        sb.Append('\u0002');
        sb.Append((char) ((uint) c - 96U));
        return 2;
      }
      if (c < '\u0080')
        throw new ArgumentException("Illegal character: " + (object) c);
      sb.Append('\u0001');
      sb.Append('\u001E');
      return 2 + this.EncodeChar((char) ((uint) c - 128U), sb);
    }

    private static string EncodeToCodewords(StringBuilder sb, int startPos)
    {
      int num = 1600 * (int) sb[startPos] + 40 * (int) sb[startPos + 1] + (int) sb[startPos + 2] + 1;
      return new string(new char[2]
      {
        (char) (num / 256),
        (char) (num % 256)
      });
    }
  }
}
