// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.Encoder.DataMatrixImageEncoderOptions
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System.Windows.Media;

namespace MessagingToolkit.Barcode.DataMatrix.Encoder
{
  internal sealed class DataMatrixImageEncoderOptions
  {
    public DataMatrixImageEncoderOptions()
    {
      this.BackColor = Color.FromArgb(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
      this.ForeColor = Color.FromArgb((byte) 0, (byte) 0, (byte) 0, (byte) 0);
      this.SizeIdx = DataMatrixSymbolSize.SymbolSquareAuto;
      this.Scheme = DataMatrixScheme.SchemeAscii;
      this.ModuleSize = 5;
      this.MarginSize = 10;
      this.Width = 250;
      this.Height = 250;
      this.QuietZone = 4;
      this.CharacterSet = "ISO-8859-1";
    }

    public int MarginSize { get; set; }

    public int ModuleSize { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public DataMatrixScheme Scheme { get; set; }

    public DataMatrixSymbolSize SizeIdx { get; set; }

    public Color ForeColor { get; set; }

    public Color BackColor { get; set; }

    public int QuietZone { get; set; }

    public string CharacterSet { get; set; }
  }
}
