// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.Encoder.TextEncoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System.Text;

namespace MessagingToolkit.Barcode.DataMatrix.Encoder
{
  internal sealed class TextEncoder : C40Encoder
  {
    public override int EncodingMode => 2;

    internal override int EncodeChar(char c, StringBuilder sb)
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
      if (c >= 'a' && c <= 'z')
      {
        sb.Append((char) ((int) c - 97 + 14));
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
      if (c == '`')
      {
        sb.Append('\u0002');
        sb.Append((char) ((uint) c - 96U));
        return 2;
      }
      if (c >= 'A' && c <= 'Z')
      {
        sb.Append('\u0002');
        sb.Append((char) ((int) c - 65 + 1));
        return 2;
      }
      if (c >= '{' && c <= '\u007F')
      {
        sb.Append('\u0002');
        sb.Append((char) ((int) c - 123 + 27));
        return 2;
      }
      if (c >= '\u0080')
      {
        sb.Append('\u0001');
        sb.Append('\u001E');
        return 2 + this.EncodeChar((char) ((uint) c - 128U), sb);
      }
      HighLevelEncoder.IllegalCharacter(c);
      return -1;
    }
  }
}
