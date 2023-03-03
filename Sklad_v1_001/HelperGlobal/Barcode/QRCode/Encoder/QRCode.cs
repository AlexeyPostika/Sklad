// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.QRCode.Encoder.QRCode
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.QRCode.Decoder;
using System.Text;

namespace MessagingToolkit.Barcode.QRCode.Encoder
{
  public sealed class QRCode
  {
    public const int NUM_MASK_PATTERNS = 8;
    private Mode mode;
    private ErrorCorrectionLevel ecLevel;
    private Version version;
    private int maskPattern;
    private ByteMatrix matrix;

    public QRCode() => this.maskPattern = -1;

    public Mode Mode
    {
      get => this.mode;
      set => this.mode = value;
    }

    public ErrorCorrectionLevel ECLevel
    {
      get => this.ecLevel;
      set => this.ecLevel = value;
    }

    public Version Version
    {
      get => this.version;
      set => this.version = value;
    }

    public int MaskPattern
    {
      get => this.maskPattern;
      set => this.maskPattern = value;
    }

    public ByteMatrix Matrix
    {
      get => this.matrix;
      set => this.matrix = value;
    }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder(200);
      stringBuilder.Append("<<\n");
      stringBuilder.Append(" mode: ");
      stringBuilder.Append((object) this.mode);
      stringBuilder.Append("\n ecLevel: ");
      stringBuilder.Append((object) this.ecLevel);
      stringBuilder.Append("\n version: ");
      stringBuilder.Append((object) this.version);
      stringBuilder.Append("\n maskPattern: ");
      stringBuilder.Append(this.maskPattern);
      stringBuilder.Append("\n numDataBytes: ");
      if (this.matrix == null)
      {
        stringBuilder.Append("\n matrix: null\n");
      }
      else
      {
        stringBuilder.Append("\n matrix:\n");
        stringBuilder.Append(this.matrix.ToString());
      }
      stringBuilder.Append(">>\n");
      return stringBuilder.ToString();
    }

    public static bool IsValidMaskPattern(int maskPattern) => maskPattern >= 0 && maskPattern < 8;
  }
}
