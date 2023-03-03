// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.Encoder.DataMatrixImageEncoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MessagingToolkit.Barcode.DataMatrix.Encoder
{
  internal class DataMatrixImageEncoder
  {
    public static readonly int DefaultDotSize = 5;
    public static readonly int DefaultMargin = 10;
    public static readonly System.Windows.Media.Color DefaultBackColor = Colors.White;
    public static readonly System.Windows.Media.Color DefaultForeColor = Colors.Black;
    private static NumberFormatInfo _dotFormatProvider;

    public WriteableBitmap EncodeImageMosaic(string val) => this.EncodeImageMosaic(val, DataMatrixImageEncoder.DefaultDotSize);

    public WriteableBitmap EncodeImageMosaic(string val, int dotSize) => this.EncodeImageMosaic(val, dotSize, DataMatrixImageEncoder.DefaultMargin);

    public WriteableBitmap EncodeImageMosaic(string val, int dotSize, int margin)
    {
      DataMatrixImageEncoderOptions options = new DataMatrixImageEncoderOptions()
      {
        MarginSize = margin,
        ModuleSize = dotSize
      };
      return this.EncodeImageMosaic(val, options);
    }

    public WriteableBitmap EncodeImageMosaic(
      string val,
      DataMatrixImageEncoderOptions options)
    {
      return this.EncodeImage(val, options, true);
    }

    private WriteableBitmap EncodeImage(
      string val,
      DataMatrixImageEncoderOptions options,
      bool isMosaic)
    {
      DataMatrixEncode encode = new DataMatrixEncode()
      {
        ModuleSize = options.ModuleSize,
        MarginSize = options.MarginSize,
        SizeIdxRequest = options.SizeIdx,
        Width = options.Width,
        Height = options.Height,
        QuietZone = options.QuietZone
      };
      byte[] dataAndSetEncoding = DataMatrixImageEncoder.GetRawDataAndSetEncoding(val, options, encode);
      if (isMosaic)
        encode.EncodeDataMosaic(dataAndSetEncoding);
      else
        encode.EncodeDataMatrix(new System.Windows.Media.Color?(options.ForeColor), new System.Windows.Media.Color?(options.BackColor), dataAndSetEncoding);
      return DataMatrixImageEncoder.CopyDataToBitmap(encode.Image.Pxl, encode.Image.Width, encode.Image.Height);
    }

    private static byte[] GetRawDataAndSetEncoding(
      string code,
      DataMatrixImageEncoderOptions options,
      DataMatrixEncode encode)
    {
      byte[] collection = Encoding.GetEncoding(options.CharacterSet).GetBytes(code);
      encode.Scheme = options.Scheme;
      if (options.Scheme == DataMatrixScheme.SchemeAsciiGS1)
      {
        List<byte> byteList = new List<byte>((IEnumerable<byte>) new byte[1]
        {
          (byte) 232
        });
        byteList.AddRange((IEnumerable<byte>) collection);
        collection = byteList.ToArray();
        encode.Scheme = DataMatrixScheme.SchemeAscii;
      }
      return collection;
    }

    public WriteableBitmap EncodeImage(string val) => this.EncodeImage(val, DataMatrixImageEncoder.DefaultDotSize, DataMatrixImageEncoder.DefaultMargin);

    public WriteableBitmap EncodeImage(string val, int dotSize) => this.EncodeImage(val, dotSize, DataMatrixImageEncoder.DefaultMargin);

    public WriteableBitmap EncodeImage(string val, int dotSize, int margin)
    {
      DataMatrixImageEncoderOptions options = new DataMatrixImageEncoderOptions()
      {
        MarginSize = margin,
        ModuleSize = dotSize
      };
      return this.EncodeImage(val, options);
    }

    public WriteableBitmap EncodeImage(
      string val,
      DataMatrixImageEncoderOptions options)
    {
      return this.EncodeImage(val, options, false);
    }

    public string EncodeSvgImage(string val) => this.EncodeSvgImage(val, DataMatrixImageEncoder.DefaultDotSize, DataMatrixImageEncoder.DefaultMargin, DataMatrixImageEncoder.DefaultForeColor, DataMatrixImageEncoder.DefaultBackColor);

    public string EncodeSvgImage(string val, int dotSize) => this.EncodeSvgImage(val, dotSize, DataMatrixImageEncoder.DefaultMargin, DataMatrixImageEncoder.DefaultForeColor, DataMatrixImageEncoder.DefaultBackColor);

    public string EncodeSvgImage(string val, int dotSize, int margin) => this.EncodeSvgImage(val, dotSize, margin, DataMatrixImageEncoder.DefaultForeColor, DataMatrixImageEncoder.DefaultBackColor);

    public string EncodeSvgImage(
      string val,
      int dotSize,
      int margin,
      System.Windows.Media.Color foreColor,
      System.Windows.Media.Color backColor)
    {
      DataMatrixImageEncoderOptions options = new DataMatrixImageEncoderOptions()
      {
        ModuleSize = dotSize,
        MarginSize = margin,
        ForeColor = foreColor,
        BackColor = backColor
      };
      return this.EncodeSvgImage(val, options);
    }

    public bool[,] EncodeRawData(string val) => this.EncodeRawData(val, new DataMatrixImageEncoderOptions());

    public bool[,] EncodeRawData(string val, DataMatrixImageEncoderOptions options)
    {
      DataMatrixEncode encode = new DataMatrixEncode()
      {
        ModuleSize = 1,
        MarginSize = 0,
        Width = options.Width,
        Height = options.Height,
        QuietZone = options.QuietZone,
        SizeIdxRequest = options.SizeIdx,
        Scheme = options.Scheme
      };
      byte[] dataAndSetEncoding = DataMatrixImageEncoder.GetRawDataAndSetEncoding(val, options, encode);
      encode.EncodeDataMatrixRaw(dataAndSetEncoding);
      return encode.RawData;
    }

    public string EncodeSvgImage(string val, DataMatrixImageEncoderOptions options)
    {
      DataMatrixEncode dataMatrixEncode = new DataMatrixEncode()
      {
        ModuleSize = options.ModuleSize,
        MarginSize = options.MarginSize,
        SizeIdxRequest = options.SizeIdx,
        Width = options.Width,
        Height = options.Height,
        QuietZone = options.QuietZone,
        Scheme = options.Scheme
      };
      byte[] dataAndSetEncoding = DataMatrixImageEncoder.GetRawDataAndSetEncoding(val, options, dataMatrixEncode);
      dataMatrixEncode.EncodeDataMatrix(new System.Windows.Media.Color?(options.ForeColor), new System.Windows.Media.Color?(options.BackColor), dataAndSetEncoding);
      return this.EncodeSvgFile(dataMatrixEncode, "", options.ModuleSize, options.MarginSize, options.ForeColor, options.BackColor);
    }

    private static int ConvertColor(System.Windows.Media.Color color)
    {
      int num = (int) color.A + 1;
      return (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
    }

    internal static WriteableBitmap CopyDataToBitmap(
      byte[] data,
      int width,
      int height)
    {
      data = DataMatrixImageEncoder.InsertPaddingBytes(data, width, height, 24);
      Bitmap source = new Bitmap(width, height);
      BitmapData bitmapdata = source.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
      Marshal.Copy(data, 0, bitmapdata.Scan0, data.Length);
      source.UnlockBits(bitmapdata);
      return new WriteableBitmap(BarcodeHelper.ToBitmapSource(source));
    }

    private static byte[] InsertPaddingBytes(byte[] data, int width, int height, int bitsPerPixel)
    {
      int num1 = 4 * ((width * bitsPerPixel + 31) / 32);
      int num2 = num1 - 3 * width;
      if (num2 == 0)
        return data;
      byte[] numArray = new byte[num1 * height];
      for (int index1 = 0; index1 < height; ++index1)
      {
        for (int index2 = 0; index2 < width; ++index2)
        {
          numArray[index1 * num1 + 3 * index2] = data[3 * (index1 * width + index2)];
          numArray[index1 * num1 + 3 * index2 + 1] = data[3 * (index1 * width + index2) + 1];
          numArray[index1 * num1 + 3 * index2 + 2] = data[3 * (index1 * width + index2) + 2];
        }
        for (int index3 = 0; index3 < num2; ++index3)
        {
          numArray[index1 * num1 + 3 * index3] = byte.MaxValue;
          numArray[index1 * num1 + 3 * index3 + 1] = byte.MaxValue;
          numArray[index1 * num1 + 3 * index3 + 2] = byte.MaxValue;
        }
      }
      return numArray;
    }

    internal string EncodeSvgFile(
      DataMatrixEncode enc,
      string format,
      int moduleSize,
      int margin,
      System.Windows.Media.Color foreColor,
      System.Windows.Media.Color backColor)
    {
      bool flag = false;
      string str1 = (string) null;
      string str2 = "";
      if (DataMatrixImageEncoder._dotFormatProvider == null)
        DataMatrixImageEncoder._dotFormatProvider = new NumberFormatInfo()
        {
          NumberDecimalSeparator = "."
        };
      if (format == "svg:")
      {
        flag = true;
        str1 = format.Substring(4);
      }
      if (string.IsNullOrEmpty(str1))
        str1 = "dmtx_0001";
      int num1 = 2 * enc.MarginSize + enc.Region.SymbolCols * enc.ModuleSize;
      int num2 = 2 * enc.MarginSize + enc.Region.SymbolRows * enc.ModuleSize;
      int symbolAttribute1 = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribSymbolCols, enc.Region.SizeIdx);
      int symbolAttribute2 = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribSymbolRows, enc.Region.SizeIdx);
      if (!flag)
        str2 += string.Format("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?>\n<!-- Created with MessagingToolkit.Barcode.DataMatrix.Pdf417Encoder (http://datamatrixnet.sourceforge.Pdf417Encoder/) -->\n<svg\nxmlns:svg=\"http://www.w3.org/2000/svg\"\nxmlns=\"http://www.w3.org/2000/svg\"\nxmlns:xlink=\"http://www.w3.org/1999/xlink\"\nversion=\"1.0\"\nwidth=\"{0}\"\nheight=\"{1}\"\nid=\"svg2\">\n<defs>\n<symbol id=\"{2}\">\n    <desc>Layout:{0}x%{1} Symbol:{3}x{4} data Matrix</desc>\n", (object) num1, (object) num2, (object) str1, (object) symbolAttribute1, (object) symbolAttribute2);
      if (backColor != Colors.White)
      {
        string str3 = string.Format("style=\"fill:#{0}{1}{2};fill-opacity:{3};stroke:none\" ", (object) backColor.R.ToString("X2"), (object) backColor.G.ToString("X2"), (object) backColor.B.ToString("X2"), (object) ((double) backColor.A / (double) byte.MaxValue).ToString("0.##", (IFormatProvider) DataMatrixImageEncoder._dotFormatProvider));
        str2 += string.Format("    <rect width=\"{0}\" height=\"{1}\" x=\"0\" y=\"0\" {2}/>\n", (object) num1, (object) num2, (object) str3);
      }
      for (int symbolRow = 0; symbolRow < enc.Region.SymbolRows; ++symbolRow)
      {
        int num3 = enc.Region.SymbolRows - symbolRow - 1;
        for (int symbolCol = 0; symbolCol < enc.Region.SymbolCols; ++symbolCol)
        {
          int num4 = enc.Message.SymbolModuleStatus(enc.Region.SizeIdx, symbolRow, symbolCol);
          string str4 = string.Format("style=\"fill:#{0}{1}{2};fill-opacity:{3};stroke:none\" ", (object) foreColor.R.ToString("X2"), (object) foreColor.G.ToString("X2"), (object) foreColor.B.ToString("X2"), (object) ((double) foreColor.A / (double) byte.MaxValue).ToString("0.##", (IFormatProvider) DataMatrixImageEncoder._dotFormatProvider));
          if ((num4 & DataMatrixConstants.DataMatrixModuleOn) != 0)
            str2 += string.Format("    <rect width=\"{0}\" height=\"{1}\" x=\"{2}\" y=\"{3}\" {4}/>\n", (object) moduleSize, (object) moduleSize, (object) (symbolCol * moduleSize + margin), (object) (num3 * moduleSize + margin), (object) str4);
        }
      }
      string str5 = str2 + "  </symbol>\n";
      if (!flag)
        str5 += string.Format("</defs>\n<use xlink:href=\"#{0}\" x='0' y='0' style=\"fill:#000000;fill-opacity:1;stroke:none\" />\n\n</svg>\n", (object) str1);
      return str5;
    }
  }
}
