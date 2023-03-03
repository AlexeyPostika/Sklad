// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.BarcodeEncoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Aztec.Encoder;
using MessagingToolkit.Barcode.Common;
using MessagingToolkit.Barcode.DataMatrix;
using MessagingToolkit.Barcode.DataMatrix.Encoder;
using MessagingToolkit.Barcode.Helper;
using MessagingToolkit.Barcode.OneD;
using MessagingToolkit.Barcode.Pdf417;
using MessagingToolkit.Barcode.Pdf417.Encoder;
using MessagingToolkit.Barcode.Provider;
using MessagingToolkit.Barcode.QRCode;
using MessagingToolkit.Barcode.QRCode.Decoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MessagingToolkit.Barcode
{
    public sealed class BarcodeEncoder : IDisposable, IBarcodeEncoder
    {
        private IBarcode iOneDEncoder = (IBarcode)new Blank();
        private IEncoder iQRCodeEncoder = (IEncoder)new DefaultEncoder();
        private IEncoder iDataMatrixEncoder = (IEncoder)new DefaultEncoder();
        private IEncoder iPdf417Encoder = (IEncoder)new DefaultEncoder();
        private IEncoder iAztecEncoder = (IEncoder)new DefaultEncoder();
        private string content = string.Empty;
        private string encodedValue = "";
        private string countryAssigningManufacturerCode = "N/A";
        private BarcodeFormat barcodeFormat;
        private ImageFormat imageFormat = ImageFormat.Jpeg;
        private Font labelFont = new Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Bold);
        private RotateFlipType rotateFlipType;
        private WriteableBitmap encodedImage;
        System.Windows.Controls.Canvas encodedCanvas;
        private System.Windows.Media.Color foreColor = Colors.Black;
        private System.Windows.Media.Color backColor = Colors.White;
        private int width = 300;
        private int height = 150;
        private bool includeLabel;
        private string customeLabel = string.Empty;
        private double encodingTime;
        private DataMatrixScheme dataMatrixScheme = DataMatrixScheme.SchemeBase256;
        private DataMatrixSymbolSize dataMatrixSymbolSize = DataMatrixSymbolSize.SymbolSquareAuto;
        private AlignmentPositions alignment;
        private LabelPositions labelPosition = LabelPositions.BottomCenter;
        private ErrorCorrectionLevel errorCorrectionLevel = ErrorCorrectionLevel.L;
        private string characterSet;
        private int margin = -1;
        private int moduleSize = 5;
        private int marginSize = 10;
        private Compaction compaction;

        public BarcodeEncoder() => this.EncodingOptions = new Dictionary<EncodeOptions, object>(1);

        public BarcodeEncoder(string data)
          : this()
        {
            this.content = data;
        }

        public BarcodeEncoder(string data, BarcodeFormat barcodeFormat)
          : this()
        {
            this.content = data;
            this.barcodeFormat = barcodeFormat;
        }

        public string Content
        {
            get => this.content;
            set => this.content = value;
        }

        private string EncodedValue => this.encodedValue;

        public string CountryAssigningManufacturerCode => this.countryAssigningManufacturerCode;

        public BarcodeFormat EncodedBarcodeFormat
        {
            set => this.barcodeFormat = value;
            get => this.barcodeFormat;
        }

        public WriteableBitmap EncodedImage => this.encodedImage;
        public System.Windows.Controls.Canvas EncodedCanvas => this.encodedCanvas;

        public BitMatrix BitMatrix { get; private set; }

        public System.Windows.Media.Color ForeColor
        {
            get => this.foreColor;
            set => this.foreColor = value;
        }

        public System.Windows.Media.Color BackColor
        {
            get => this.backColor;
            set => this.backColor = value;
        }

        public Dictionary<EncodeOptions, object> EncodingOptions { get; internal set; }

        public void AddOption(EncodeOptions key, object value)
        {
            if (!this.EncodingOptions.ContainsKey(key))
                this.EncodingOptions.Add(key, value);
            else
                this.EncodingOptions[key] = value;
        }

        public void RemoveOption(EncodeOptions key)
        {
            if (this.EncodingOptions.ContainsKey(key))
                return;
            this.EncodingOptions.Remove(key);
        }

        public void ClearAllOptions() => this.EncodingOptions.Clear();

        public Font LabelFont
        {
            get => this.labelFont;
            set => this.labelFont = value;
        }

        public LabelPositions LabelPosition
        {
            get => this.labelPosition;
            set => this.labelPosition = value;
        }

        public RotateFlipType RotateFlipType
        {
            get => this.rotateFlipType;
            set => this.rotateFlipType = value;
        }

        public int Width
        {
            get => this.width;
            set => this.width = value;
        }

        public int Height
        {
            get => this.height;
            set => this.height = value;
        }

        public bool IncludeLabel
        {
            set => this.includeLabel = value;
            get => this.includeLabel;
        }

        public string CustomLabel
        {
            get => this.customeLabel;
            set => this.customeLabel = value;
        }

        public double EncodingTime
        {
            get => this.encodingTime;
            set => this.encodingTime = value;
        }

        public ImageFormat ImageFormat
        {
            get => this.imageFormat;
            set => this.imageFormat = value;
        }

        public List<string> Errors => this.iOneDEncoder.Errors;

        public AlignmentPositions Alignment
        {
            get => this.alignment;
            set => this.alignment = value;
        }

        public ErrorCorrectionLevel ErrorCorrectionLevel
        {
            get => this.errorCorrectionLevel;
            set => this.errorCorrectionLevel = value;
        }

        public int Margin
        {
            get => this.margin;
            set => this.margin = value;
        }

        public string CharacterSet
        {
            get => this.characterSet;
            set => this.characterSet = value;
        }

        public int ModuleSize
        {
            get => this.moduleSize;
            set => this.moduleSize = value;
        }

        public int MarginSize
        {
            get => this.marginSize;
            set => this.marginSize = value;
        }

        public DataMatrixScheme DataMatrixScheme
        {
            get => this.dataMatrixScheme;
            set => this.dataMatrixScheme = value;
        }

        public DataMatrixSymbolSize DataMatrixSymbolSize
        {
            get => this.dataMatrixSymbolSize;
            set => this.dataMatrixSymbolSize = value;
        }

        public Compaction Pdf417Compaction
        {
            get => this.compaction;
            set => this.compaction = value;
        }

        public System.Version Version
        {
            get
            {
                string empty = string.Empty;
                Match match = new Regex("Version=(?<version>[0-9.]*)").Match(this.GetType().AssemblyQualifiedName);
                if (match.Success)
                    empty = match.Groups["version"].Value;
                return new System.Version(empty);
            }
        }

        public T Generate<T>(
          BarcodeFormat format,
          string content,
          Dictionary<EncodeOptions, object> encodingOptions,
          IOutputProvider<T> provider)
        {
            try
            {
                this.content = content;
                this.barcodeFormat = format;
                if (encodingOptions != null)
                    this.EncodingOptions = encodingOptions;
                IEncoder encoder1 = (IEncoder)new DefaultEncoder();
                DateTime now = DateTime.Now;
                if (string.IsNullOrEmpty(content))
                    throw new Exception("Input data not allowed to be blank.");
                if (this.EncodedBarcodeFormat == BarcodeFormat.Unknown)
                    throw new Exception("Barcode type is not specified.");
                this.encodedValue = string.Empty;
                this.countryAssigningManufacturerCode = "N/A";
                IEncoder encoder2;
                switch (this.barcodeFormat)
                {
                    case BarcodeFormat.QRCode:
                        encoder2 = (IEncoder)new QRCodeEncoder();
                        break;
                    case BarcodeFormat.DataMatrix:
                        encoder2 = (IEncoder)new DataMatrixNewEncoder();
                        break;
                    case BarcodeFormat.PDF417:
                        encoder2 = (IEncoder)new Pdf417Encoder();
                        break;
                    case BarcodeFormat.UPCA:
                    case BarcodeFormat.UCC12:
                        encoder2 = (IEncoder)new UPCAEncoder();
                        break;
                    case BarcodeFormat.EAN13:
                    case BarcodeFormat.UCC13:
                        encoder2 = (IEncoder)new EAN13Encoder();
                        break;
                    case BarcodeFormat.EAN8:
                        encoder2 = (IEncoder)new EAN8Encoder();
                        break;
                    case BarcodeFormat.Code39:
                    case BarcodeFormat.LOGMARS:
                        encoder2 = (IEncoder)new Code39Encoder();
                        break;
                    case BarcodeFormat.Code39Extended:
                        encoder2 = (IEncoder)new Code39Encoder();
                        break;
                    case BarcodeFormat.Codabar:
                        encoder2 = (IEncoder)new CodaBarEncoder();
                        break;
                    case BarcodeFormat.Code128:
                        encoder2 = (IEncoder)new Code128Encoder();
                        break;
                    case BarcodeFormat.Code128A:
                        encoder2 = (IEncoder)new Code128Encoder();
                        break;
                    case BarcodeFormat.Code128B:
                        encoder2 = (IEncoder)new Code128Encoder();
                        break;
                    case BarcodeFormat.Code128C:
                        encoder2 = (IEncoder)new Code128Encoder();
                        break;
                    case BarcodeFormat.ITF14:
                        encoder2 = (IEncoder)new ITFEncoder();
                        break;
                    case BarcodeFormat.Aztec:
                        encoder2 = (IEncoder)new AztecEncoder();
                        break;
                    default:
                        throw new Exception("Unsupported encoding type specified.");
                }
                Dictionary<EncodeOptions, object> encodingOptions1 = new Dictionary<EncodeOptions, object>((IDictionary<EncodeOptions, object>)this.EncodingOptions);
                if (!encodingOptions1.ContainsKey(EncodeOptions.CharacterSet) && !string.IsNullOrEmpty(this.CharacterSet))
                    encodingOptions1.Add(EncodeOptions.CharacterSet, (object)this.CharacterSet);
                if (!encodingOptions1.ContainsKey(EncodeOptions.Margin) && this.Margin >= 0)
                    encodingOptions1.Add(EncodeOptions.Margin, (object)this.Margin);
                if (this.barcodeFormat == BarcodeFormat.QRCode)
                {
                    if (!encodingOptions1.ContainsKey(EncodeOptions.ErrorCorrection))
                        encodingOptions1.Add(EncodeOptions.ErrorCorrection, (object)this.ErrorCorrectionLevel);
                    this.BitMatrix = encoder2.Encode(this.content, this.barcodeFormat, this.Width, this.Height, encodingOptions1);
                }
                else if (this.barcodeFormat == BarcodeFormat.DataMatrix)
                    this.BitMatrix = encoder2.Encode(this.content, this.barcodeFormat, this.Width, this.Height, encodingOptions1);
                else if (this.barcodeFormat == BarcodeFormat.PDF417)
                {
                    if (!encodingOptions1.ContainsKey(EncodeOptions.Pdf417Compaction))
                        encodingOptions1.Add(EncodeOptions.Pdf417Compaction, (object)this.Pdf417Compaction);
                    this.BitMatrix = encoder2.Encode(this.content, this.barcodeFormat, this.Width, this.Height, encodingOptions1);
                }
                else
                    this.BitMatrix = this.barcodeFormat != BarcodeFormat.Aztec ? encoder2.Encode(this.content, this.barcodeFormat, this.Width, this.Height, encodingOptions1) : encoder2.Encode(this.content, this.barcodeFormat, this.Width, this.Height, encodingOptions1);
                this.encodedValue = this.BitMatrix.ToString();
                this.encodingTime = (DateTime.Now - now).TotalMilliseconds;
                return provider.Generate(this.BitMatrix, this.barcodeFormat, content, this.EncodingOptions);
            }
            finally
            {
                this.content = string.Empty;
                this.ClearAllOptions();
            }
        }

        public T Generate<T>(BarcodeFormat format, string content, IOutputProvider<T> provider) => this.Generate<T>(format, content, (Dictionary<EncodeOptions, object>)null, provider);

        public WriteableBitmap Encode(BarcodeFormat format, string content)
        {
            this.content = content;
            return this.Encode(format);
        }

        public System.Windows.Controls.Canvas EncodeVector(BarcodeFormat format, string content)
        {
            this.content = content;
            return this.EncodeCanvas(format);
        }

        void CreateLine(System.Windows.Controls.Canvas canvas, System.Drawing.Pen pen, System.Drawing.Point pt1, System.Drawing.Point pt2)
        {
            System.Windows.Shapes.Line line = new System.Windows.Shapes.Line();
            line.X1 = pt1.X;
            line.Y1 = pt1.Y;
            line.X2 = pt2.X;
            line.Y2 = pt2.Y;
            line.Stroke = new SolidColorBrush(Colors.Black);
            line.StrokeThickness = pen.Width + 0.1;
            line.SnapsToDevicePixels = true;
            canvas.Children.Add(line);
        }

        public WriteableBitmap Encode(
          BarcodeFormat format,
          string content,
          Dictionary<EncodeOptions, object> encodingOptions)
        {
            try
            {
                this.content = content;
                if (encodingOptions != null)
                    this.EncodingOptions = encodingOptions;
                return this.Encode(format);
            }
            finally
            {
                this.content = string.Empty;
                this.ClearAllOptions();
            }
        }

        internal WriteableBitmap Encode(BarcodeFormat barcodeFormat)
        {
            this.barcodeFormat = barcodeFormat;
            return this.Encode();
        }

        internal System.Windows.Controls.Canvas EncodeCanvas(BarcodeFormat barcodeFormat)
        {
            this.barcodeFormat = barcodeFormat;
            return this.EncodeCanvas();
        }

        internal System.Windows.Controls.Canvas EncodeCanvas()
        {
            this.iOneDEncoder = (IBarcode)new Blank();
            this.iOneDEncoder.Errors.Clear();
            this.iQRCodeEncoder = (IEncoder)new DefaultEncoder();
            this.iDataMatrixEncoder = (IEncoder)new DefaultEncoder();
            this.iAztecEncoder = (IEncoder)new DefaultEncoder();
            DateTime now = DateTime.Now;
            if (string.IsNullOrEmpty(this.content))
                throw new Exception("Input data not allowed to be blank.");
            if (this.EncodedBarcodeFormat == BarcodeFormat.Unknown)
                throw new Exception("Symbology type not allowed to be unspecified.");
            this.encodedValue = string.Empty;
            this.countryAssigningManufacturerCode = "N/A";
            switch (this.barcodeFormat)
            {
                case BarcodeFormat.QRCode:
                    this.iQRCodeEncoder = (IEncoder)new QRCodeEncoder();
                    break;
                case BarcodeFormat.DataMatrix:
                    this.iDataMatrixEncoder = (IEncoder)new DataMatrixEncoder();
                    break;
                case BarcodeFormat.PDF417:
                    this.iPdf417Encoder = (IEncoder)new Pdf417Encoder();
                    break;
                case BarcodeFormat.UPCA:
                case BarcodeFormat.UCC12:
                    this.iOneDEncoder = (IBarcode)new UPCA(this.content);
                    break;
                case BarcodeFormat.UPCE:
                    this.iOneDEncoder = (IBarcode)new UPCE(this.content);
                    break;
                case BarcodeFormat.UPCSupplemental2Digit:
                    this.iOneDEncoder = (IBarcode)new UPCSupplement2(this.content);
                    break;
                case BarcodeFormat.UPCSupplemental5Digit:
                    this.iOneDEncoder = (IBarcode)new UPCSupplement5(this.content);
                    break;
                case BarcodeFormat.EAN13:
                case BarcodeFormat.UCC13:
                    this.iOneDEncoder = (IBarcode)new EAN13(this.content);
                    break;
                case BarcodeFormat.EAN8:
                    this.iOneDEncoder = (IBarcode)new EAN8(this.content);
                    break;
                case BarcodeFormat.Interleaved2of5:
                    this.iOneDEncoder = (IBarcode)new Interleaved2of5(this.content);
                    break;
                case BarcodeFormat.Standard2of5:
                case BarcodeFormat.Industrial2of5:
                    this.iOneDEncoder = (IBarcode)new Standard2of5(this.content);
                    break;
                case BarcodeFormat.Code39:
                case BarcodeFormat.LOGMARS:
                    this.iOneDEncoder = (IBarcode)new Code39(this.content);
                    break;
                case BarcodeFormat.Code39Extended:
                    this.iOneDEncoder = (IBarcode)new Code39(this.content, true);
                    break;
                case BarcodeFormat.Codabar:
                    this.iOneDEncoder = (IBarcode)new Codabar(this.content);
                    break;
                case BarcodeFormat.PostNet:
                    this.iOneDEncoder = (IBarcode)new Postnet(this.content);
                    break;
                case BarcodeFormat.Bookland:
                case BarcodeFormat.ISBN:
                    this.iOneDEncoder = (IBarcode)new ISBN(this.content);
                    break;
                case BarcodeFormat.JAN13:
                    this.iOneDEncoder = (IBarcode)new JAN13(this.content);
                    break;
                case BarcodeFormat.MSIMod10:
                case BarcodeFormat.MSI2Mod10:
                case BarcodeFormat.MSIMod11:
                case BarcodeFormat.MSIMod11Mod10:
                case BarcodeFormat.ModifiedPlessey:
                    this.iOneDEncoder = (IBarcode)new MSI(this.content, this.barcodeFormat);
                    break;
                case BarcodeFormat.Code11:
                case BarcodeFormat.USD8:
                    this.iOneDEncoder = (IBarcode)new Code11(this.content);
                    break;
                case BarcodeFormat.Code128:
                    this.iOneDEncoder = (IBarcode)new Code128(this.content);
                    break;
                case BarcodeFormat.Code128A:
                    this.iOneDEncoder = (IBarcode)new Code128(this.content, Code128.Types.A);
                    break;
                case BarcodeFormat.Code128B:
                    this.iOneDEncoder = (IBarcode)new Code128(this.content, Code128.Types.B);
                    break;
                case BarcodeFormat.Code128C:
                    this.iOneDEncoder = (IBarcode)new Code128(this.content, Code128.Types.C);
                    break;
                case BarcodeFormat.ITF14:
                    this.iOneDEncoder = (IBarcode)new ITF14(this.content);
                    break;
                case BarcodeFormat.Code93:
                    this.iOneDEncoder = (IBarcode)new Code93(this.content);
                    break;
                case BarcodeFormat.Telepen:
                    this.iOneDEncoder = (IBarcode)new Telepen(this.content);
                    break;
                case BarcodeFormat.FIM:
                    this.iOneDEncoder = (IBarcode)new FIM(this.content);
                    break;
                case BarcodeFormat.Aztec:
                    this.iAztecEncoder = (IEncoder)new AztecEncoder();
                    break;
                default:
                    throw new Exception("Unsupported encoding type specified.");
            }
            Dictionary<EncodeOptions, object> dictionary = new Dictionary<EncodeOptions, object>((IDictionary<EncodeOptions, object>)this.EncodingOptions);
            if (!dictionary.ContainsKey(EncodeOptions.CharacterSet) && !string.IsNullOrEmpty(this.CharacterSet))
                dictionary.Add(EncodeOptions.CharacterSet, (object)this.CharacterSet);
            if (!dictionary.ContainsKey(EncodeOptions.Margin) && this.Margin >= 0)
                dictionary.Add(EncodeOptions.Margin, (object)this.Margin);
            if (this.iOneDEncoder.GetType() != typeof(Blank))
            {
                this.encodedValue = this.iOneDEncoder.EncodedValue;
                this.content = this.iOneDEncoder.RawData;
                //this.encodedImage = new WriteableBitmap(BarcodeHelper.ToBitmapSource(this.GenerateImage()));
                this.encodedCanvas = this.GenerateCanvas();
            }
            else if (this.iQRCodeEncoder.GetType() != typeof(DefaultEncoder))
            {
                if (!dictionary.ContainsKey(EncodeOptions.ErrorCorrection))
                    dictionary.Add(EncodeOptions.ErrorCorrection, (object)this.ErrorCorrectionLevel);
                this.BitMatrix = this.iQRCodeEncoder.Encode(this.content, this.barcodeFormat, this.Width, this.Height, dictionary);
                this.encodedImage = MatrixToImageHelper.ToBitmap(this.BitMatrix, this.ForeColor, this.BackColor);
                object encodeOptionType = BarcodeHelper.GetEncodeOptionType(dictionary, EncodeOptions.QRCodeLogo);
                if (encodeOptionType != null)
                {
                    try
                    {
                        WriteableBitmap source = (WriteableBitmap)encodeOptionType;
                        this.encodedImage.Blit(new Rect((double)(this.encodedImage.PixelWidth / 2 - source.PixelWidth / 2), (double)(this.encodedImage.PixelHeight / 2 - source.PixelHeight / 2), (double)source.PixelWidth, (double)source.PixelHeight), BitmapFactory.ConvertToPbgra32Format((BitmapSource)source), new Rect(0.0, 0.0, (double)source.PixelWidth, (double)source.PixelHeight));
                    }
                    catch (Exception ex)
                    {
                    }
                }
                this.encodedValue = this.BitMatrix.ToString();
            }
            else if (this.iDataMatrixEncoder.GetType() != typeof(DefaultEncoder))
            {
                if (this.iDataMatrixEncoder.GetType() == typeof(DataMatrixEncoder))
                {
                    DataMatrixEncoder dataMatrixEncoder = this.iDataMatrixEncoder as DataMatrixEncoder;
                    DataMatrixImageEncoderOptions options = new DataMatrixImageEncoderOptions();
                    options.BackColor = this.BackColor;
                    options.ForeColor = this.ForeColor;
                    options.MarginSize = this.MarginSize;
                    options.ModuleSize = this.ModuleSize;
                    options.Scheme = this.DataMatrixScheme;
                    options.SizeIdx = this.DataMatrixSymbolSize;
                    if (!string.IsNullOrEmpty(this.CharacterSet))
                        options.CharacterSet = this.CharacterSet;
                    this.encodedImage = dataMatrixEncoder.EncodeImage(this.content, options);
                }
                else
                {
                    this.BitMatrix = (this.iDataMatrixEncoder as DataMatrixNewEncoder).Encode(this.content, this.barcodeFormat, this.Width, this.Height, dictionary);
                    this.encodedImage = MatrixToImageHelper.ToBitmap(this.BitMatrix, this.ForeColor, this.BackColor);
                    this.encodedValue = this.BitMatrix.ToString();
                }
            }
            else if (this.iAztecEncoder.GetType() != typeof(DefaultEncoder))
            {
                this.BitMatrix = (this.iAztecEncoder as AztecEncoder).Encode(this.content, this.barcodeFormat, this.Width, this.Height, dictionary);
                this.encodedImage = MatrixToImageHelper.ToBitmap(this.BitMatrix, this.ForeColor, this.BackColor);
                this.encodedValue = this.BitMatrix.ToString();
            }
            else if (this.iPdf417Encoder.GetType() != typeof(DefaultEncoder))
            {
                Pdf417Encoder iPdf417Encoder = this.iPdf417Encoder as Pdf417Encoder;
                if (!dictionary.ContainsKey(EncodeOptions.Pdf417Compaction))
                    dictionary.Add(EncodeOptions.Pdf417Compaction, (object)this.Pdf417Compaction);
                this.BitMatrix = iPdf417Encoder.Encode(this.content, this.barcodeFormat, this.Width, this.Height, dictionary);
                this.encodedImage = MatrixToImageHelper.ToBitmap(this.BitMatrix, this.ForeColor, this.BackColor);
                this.encodedValue = this.BitMatrix.ToString();
            }
            this.encodingTime = (DateTime.Now - now).TotalMilliseconds;
            return this.EncodedCanvas;
        }


        internal WriteableBitmap Encode()
        {
            this.iOneDEncoder = (IBarcode)new Blank();
            this.iOneDEncoder.Errors.Clear();
            this.iQRCodeEncoder = (IEncoder)new DefaultEncoder();
            this.iDataMatrixEncoder = (IEncoder)new DefaultEncoder();
            this.iAztecEncoder = (IEncoder)new DefaultEncoder();
            DateTime now = DateTime.Now;
            if (string.IsNullOrEmpty(this.content))
                throw new Exception("Input data not allowed to be blank.");
            if (this.EncodedBarcodeFormat == BarcodeFormat.Unknown)
                throw new Exception("Symbology type not allowed to be unspecified.");
            this.encodedValue = string.Empty;
            this.countryAssigningManufacturerCode = "N/A";
            switch (this.barcodeFormat)
            {
                case BarcodeFormat.QRCode:
                    this.iQRCodeEncoder = (IEncoder)new QRCodeEncoder();
                    break;
                case BarcodeFormat.DataMatrix:
                    this.iDataMatrixEncoder = (IEncoder)new DataMatrixEncoder();
                    break;
                case BarcodeFormat.PDF417:
                    this.iPdf417Encoder = (IEncoder)new Pdf417Encoder();
                    break;
                case BarcodeFormat.UPCA:
                case BarcodeFormat.UCC12:
                    this.iOneDEncoder = (IBarcode)new UPCA(this.content);
                    break;
                case BarcodeFormat.UPCE:
                    this.iOneDEncoder = (IBarcode)new UPCE(this.content);
                    break;
                case BarcodeFormat.UPCSupplemental2Digit:
                    this.iOneDEncoder = (IBarcode)new UPCSupplement2(this.content);
                    break;
                case BarcodeFormat.UPCSupplemental5Digit:
                    this.iOneDEncoder = (IBarcode)new UPCSupplement5(this.content);
                    break;
                case BarcodeFormat.EAN13:
                case BarcodeFormat.UCC13:
                    this.iOneDEncoder = (IBarcode)new EAN13(this.content);
                    break;
                case BarcodeFormat.EAN8:
                    this.iOneDEncoder = (IBarcode)new EAN8(this.content);
                    break;
                case BarcodeFormat.Interleaved2of5:
                    this.iOneDEncoder = (IBarcode)new Interleaved2of5(this.content);
                    break;
                case BarcodeFormat.Standard2of5:
                case BarcodeFormat.Industrial2of5:
                    this.iOneDEncoder = (IBarcode)new Standard2of5(this.content);
                    break;
                case BarcodeFormat.Code39:
                case BarcodeFormat.LOGMARS:
                    this.iOneDEncoder = (IBarcode)new Code39(this.content);
                    break;
                case BarcodeFormat.Code39Extended:
                    this.iOneDEncoder = (IBarcode)new Code39(this.content, true);
                    break;
                case BarcodeFormat.Codabar:
                    this.iOneDEncoder = (IBarcode)new Codabar(this.content);
                    break;
                case BarcodeFormat.PostNet:
                    this.iOneDEncoder = (IBarcode)new Postnet(this.content);
                    break;
                case BarcodeFormat.Bookland:
                case BarcodeFormat.ISBN:
                    this.iOneDEncoder = (IBarcode)new ISBN(this.content);
                    break;
                case BarcodeFormat.JAN13:
                    this.iOneDEncoder = (IBarcode)new JAN13(this.content);
                    break;
                case BarcodeFormat.MSIMod10:
                case BarcodeFormat.MSI2Mod10:
                case BarcodeFormat.MSIMod11:
                case BarcodeFormat.MSIMod11Mod10:
                case BarcodeFormat.ModifiedPlessey:
                    this.iOneDEncoder = (IBarcode)new MSI(this.content, this.barcodeFormat);
                    break;
                case BarcodeFormat.Code11:
                case BarcodeFormat.USD8:
                    this.iOneDEncoder = (IBarcode)new Code11(this.content);
                    break;
                case BarcodeFormat.Code128:
                    this.iOneDEncoder = (IBarcode)new Code128(this.content);
                    break;
                case BarcodeFormat.Code128A:
                    this.iOneDEncoder = (IBarcode)new Code128(this.content, Code128.Types.A);
                    break;
                case BarcodeFormat.Code128B:
                    this.iOneDEncoder = (IBarcode)new Code128(this.content, Code128.Types.B);
                    break;
                case BarcodeFormat.Code128C:
                    this.iOneDEncoder = (IBarcode)new Code128(this.content, Code128.Types.C);
                    break;
                case BarcodeFormat.ITF14:
                    this.iOneDEncoder = (IBarcode)new ITF14(this.content);
                    break;
                case BarcodeFormat.Code93:
                    this.iOneDEncoder = (IBarcode)new Code93(this.content);
                    break;
                case BarcodeFormat.Telepen:
                    this.iOneDEncoder = (IBarcode)new Telepen(this.content);
                    break;
                case BarcodeFormat.FIM:
                    this.iOneDEncoder = (IBarcode)new FIM(this.content);
                    break;
                case BarcodeFormat.Aztec:
                    this.iAztecEncoder = (IEncoder)new AztecEncoder();
                    break;
                default:
                    throw new Exception("Unsupported encoding type specified.");
            }
            Dictionary<EncodeOptions, object> dictionary = new Dictionary<EncodeOptions, object>((IDictionary<EncodeOptions, object>)this.EncodingOptions);
            if (!dictionary.ContainsKey(EncodeOptions.CharacterSet) && !string.IsNullOrEmpty(this.CharacterSet))
                dictionary.Add(EncodeOptions.CharacterSet, (object)this.CharacterSet);
            if (!dictionary.ContainsKey(EncodeOptions.Margin) && this.Margin >= 0)
                dictionary.Add(EncodeOptions.Margin, (object)this.Margin);
            if (this.iOneDEncoder.GetType() != typeof(Blank))
            {
                this.encodedValue = this.iOneDEncoder.EncodedValue;
                this.content = this.iOneDEncoder.RawData;
                this.encodedImage = new WriteableBitmap(BarcodeHelper.ToBitmapSource(this.GenerateImage()));
            }
            else if (this.iQRCodeEncoder.GetType() != typeof(DefaultEncoder))
            {
                if (!dictionary.ContainsKey(EncodeOptions.ErrorCorrection))
                    dictionary.Add(EncodeOptions.ErrorCorrection, (object)this.ErrorCorrectionLevel);
                this.BitMatrix = this.iQRCodeEncoder.Encode(this.content, this.barcodeFormat, this.Width, this.Height, dictionary);
                this.encodedImage = MatrixToImageHelper.ToBitmap(this.BitMatrix, this.ForeColor, this.BackColor);
                object encodeOptionType = BarcodeHelper.GetEncodeOptionType(dictionary, EncodeOptions.QRCodeLogo);
                if (encodeOptionType != null)
                {
                    try
                    {
                        WriteableBitmap source = (WriteableBitmap)encodeOptionType;
                        this.encodedImage.Blit(new Rect((double)(this.encodedImage.PixelWidth / 2 - source.PixelWidth / 2), (double)(this.encodedImage.PixelHeight / 2 - source.PixelHeight / 2), (double)source.PixelWidth, (double)source.PixelHeight), BitmapFactory.ConvertToPbgra32Format((BitmapSource)source), new Rect(0.0, 0.0, (double)source.PixelWidth, (double)source.PixelHeight));
                    }
                    catch (Exception ex)
                    {
                    }
                }
                this.encodedValue = this.BitMatrix.ToString();
            }
            else if (this.iDataMatrixEncoder.GetType() != typeof(DefaultEncoder))
            {
                if (this.iDataMatrixEncoder.GetType() == typeof(DataMatrixEncoder))
                {
                    DataMatrixEncoder dataMatrixEncoder = this.iDataMatrixEncoder as DataMatrixEncoder;
                    DataMatrixImageEncoderOptions options = new DataMatrixImageEncoderOptions();
                    options.BackColor = this.BackColor;
                    options.ForeColor = this.ForeColor;
                    options.MarginSize = this.MarginSize;
                    options.ModuleSize = this.ModuleSize;
                    options.Scheme = this.DataMatrixScheme;
                    options.SizeIdx = this.DataMatrixSymbolSize;
                    if (!string.IsNullOrEmpty(this.CharacterSet))
                        options.CharacterSet = this.CharacterSet;
                    this.encodedImage = dataMatrixEncoder.EncodeImage(this.content, options);
                }
                else
                {
                    this.BitMatrix = (this.iDataMatrixEncoder as DataMatrixNewEncoder).Encode(this.content, this.barcodeFormat, this.Width, this.Height, dictionary);
                    this.encodedImage = MatrixToImageHelper.ToBitmap(this.BitMatrix, this.ForeColor, this.BackColor);
                    this.encodedValue = this.BitMatrix.ToString();
                }
            }
            else if (this.iAztecEncoder.GetType() != typeof(DefaultEncoder))
            {
                this.BitMatrix = (this.iAztecEncoder as AztecEncoder).Encode(this.content, this.barcodeFormat, this.Width, this.Height, dictionary);
                this.encodedImage = MatrixToImageHelper.ToBitmap(this.BitMatrix, this.ForeColor, this.BackColor);
                this.encodedValue = this.BitMatrix.ToString();
            }
            else if (this.iPdf417Encoder.GetType() != typeof(DefaultEncoder))
            {
                Pdf417Encoder iPdf417Encoder = this.iPdf417Encoder as Pdf417Encoder;
                if (!dictionary.ContainsKey(EncodeOptions.Pdf417Compaction))
                    dictionary.Add(EncodeOptions.Pdf417Compaction, (object)this.Pdf417Compaction);
                this.BitMatrix = iPdf417Encoder.Encode(this.content, this.barcodeFormat, this.Width, this.Height, dictionary);
                this.encodedImage = MatrixToImageHelper.ToBitmap(this.BitMatrix, this.ForeColor, this.BackColor);
                this.encodedValue = this.BitMatrix.ToString();
            }
            this.encodingTime = (DateTime.Now - now).TotalMilliseconds;
            return this.EncodedImage;
        }

        private System.Windows.Controls.Canvas GenerateCanvas()
        {
            if (this.encodedValue == "")
                throw new Exception("Must be encoded first.");
            DateTime now = DateTime.Now;
            Bitmap image;
            System.Windows.Controls.Canvas canvas;
            canvas = new System.Windows.Controls.Canvas();

            if (this.barcodeFormat == BarcodeFormat.ITF14)
            {
                image = new Bitmap(this.Width, this.Height);
                int num1 = (int)((double)image.Width / 12.05);
                int num2 = Convert.ToInt32((double)image.Width * 0.05);
                if (this.margin >= 0)
                    num2 = this.margin;
                int width = (image.Width - num1 * 2 - num2 * 2) / this.encodedValue.Length;
                int num3 = (image.Width - num1 * 2 - num2 * 2) % this.encodedValue.Length / 2;
                if (width <= 0 || num2 <= 0)
                    throw new Exception("Image size specified not large enough to draw image. (Bar size determined to be less than 1 pixel or quiet zone determined to be less than 1 pixel)");
                int index = 0;
                using (Graphics graphics = Graphics.FromImage((Image)image))
                {
                    graphics.Clear(BarcodeHelper.ToWinFormsColor(this.BackColor));
                    using (System.Drawing.Pen pen = new System.Drawing.Pen(BarcodeHelper.ToWinFormsColor(this.ForeColor), (float)width))
                    {
                        pen.Alignment = PenAlignment.Right;
                        for (; index < this.encodedValue.Length; ++index)
                        {
                            if (this.encodedValue[index] == '1')
                            {
                                //graphics.DrawLine(pen, new System.Drawing.Point(index * width + num3 + num1 + num2, 0), new System.Drawing.Point(index * width + num3 + num1 + num2, this.Height));
                                CreateLine(canvas, pen, new System.Drawing.Point(index * width + num3 + num1 + num2, 0), new System.Drawing.Point(index * width + num3 + num1 + num2, this.Height));
                            }
                        }
                        pen.Width = (float)image.Height / 8f;
                        pen.Color = BarcodeHelper.ToWinFormsColor(this.ForeColor);
                        pen.Alignment = PenAlignment.Center;
                        //graphics.DrawLine(pen, new System.Drawing.Point(0, 0), new System.Drawing.Point(image.Width, 0));
                        //graphics.DrawLine(pen, new System.Drawing.Point(0, image.Height), new System.Drawing.Point(image.Width, image.Height));
                        //graphics.DrawLine(pen, new System.Drawing.Point(0, 0), new System.Drawing.Point(0, image.Height));
                        //graphics.DrawLine(pen, new System.Drawing.Point(image.Width, 0), new System.Drawing.Point(image.Width, image.Height));

                        CreateLine(canvas, pen, new System.Drawing.Point(0, 0), new System.Drawing.Point(image.Width, 0));
                        CreateLine(canvas, pen, new System.Drawing.Point(0, image.Height), new System.Drawing.Point(image.Width, image.Height));
                        CreateLine(canvas, pen, new System.Drawing.Point(0, 0), new System.Drawing.Point(0, image.Height));
                        CreateLine(canvas, pen, new System.Drawing.Point(image.Width, 0), new System.Drawing.Point(image.Width, image.Height));
                    }
                }
                if (this.IncludeLabel)
                    this.LabelITF14((Image)image);
            }
            else
            {
                image = new Bitmap(this.Width, this.Height);
                int num4 = 1;
                int num5 = Convert.ToInt32((double)image.Width * 0.05);
                if (this.margin >= 0)
                    num5 = this.margin;
                int num6 = (image.Width - num5 * 2) / this.encodedValue.Length;
                if (num6 == 0)
                    num6 = 1;
                if (this.barcodeFormat == BarcodeFormat.PostNet)
                    num4 = 2;
                int num7;
                switch (this.Alignment)
                {
                    case AlignmentPositions.Center:
                        num7 = (this.Width - num5 * 2) % this.encodedValue.Length / 2;
                        break;
                    case AlignmentPositions.Left:
                        num7 = 0;
                        break;
                    case AlignmentPositions.Right:
                        num7 = (this.Width - num5 * 2) % this.encodedValue.Length;
                        break;
                    default:
                        num7 = (this.Width - num5 * 2) % this.encodedValue.Length / 2;
                        break;
                }
                if (num6 <= 0)
                    throw new Exception("Image size specified not large enough to draw image. (Bar size determined to be less than 1 pixel)");
                int index = 0;
                using (Graphics graphics = Graphics.FromImage((Image)image))
                {
                    graphics.Clear(BarcodeHelper.ToWinFormsColor(this.BackColor));
                    using (System.Drawing.Pen pen1 = new System.Drawing.Pen(BarcodeHelper.ToWinFormsColor(this.BackColor), (float)(num6 / num4)))
                    {
                        using (System.Drawing.Pen pen2 = new System.Drawing.Pen(BarcodeHelper.ToWinFormsColor(this.ForeColor), (float)(num6 / num4)))
                        {
                            for (; index < this.encodedValue.Length; ++index)
                            {
                                if (this.barcodeFormat == BarcodeFormat.PostNet)
                                {
                                    if (this.encodedValue[index] != '1')
                                    {
                                        //graphics.DrawLine(pen2, new System.Drawing.Point(index * num6 + num7 + 1, this.Height), new System.Drawing.Point(index * num6 + num7 + 1, this.Height / 2));
                                        CreateLine(canvas, pen2, new System.Drawing.Point(index * num6 + num7 + 1, this.Height), new System.Drawing.Point(index * num6 + num7 + 1, this.Height / 2));
                                    }
                                    //graphics.DrawLine(pen1, new System.Drawing.Point(index * (num6 * num4) + num7 + num6 + 1, 0), new System.Drawing.Point(index * (num6 * num4) + num7 + num6 + 1, this.Height));
                                    CreateLine(canvas, pen1, new System.Drawing.Point(index * (num6 * num4) + num7 + num6 + 1, 0), new System.Drawing.Point(index * (num6 * num4) + num7 + num6 + 1, this.Height));
                                }
                                if (this.encodedValue[index] == '1')
                                {
                                    //graphics.DrawLine(pen2, new System.Drawing.Point(index * num6 + num7 + num5, num5), new System.Drawing.Point(index * num6 + num7 + num5, this.Height - num5));
                                    CreateLine(canvas, pen2, new System.Drawing.Point(index * num6 + num7 + num5, num5), new System.Drawing.Point(index * num6 + num7 + num5, this.Height - num5));
                                }
                            }
                        }
                    }
                }
                if (this.IncludeLabel)
                    this.LabelGeneric((Image)image, num5);
            }
            this.encodedImage = new WriteableBitmap(BarcodeHelper.ToBitmapSource(image));
            this.encodingTime += (DateTime.Now - now).TotalMilliseconds;

            return canvas;
        }


        private Bitmap GenerateImage()
        {
            if (this.encodedValue == "")
                throw new Exception("Must be encoded first.");
            DateTime now = DateTime.Now;
            Bitmap image;
            if (this.barcodeFormat == BarcodeFormat.ITF14)
            {
                image = new Bitmap(this.Width, this.Height);
                int num1 = (int)((double)image.Width / 12.05);
                int num2 = Convert.ToInt32((double)image.Width * 0.05);
                if (this.margin >= 0)
                    num2 = this.margin;
                int width = (image.Width - num1 * 2 - num2 * 2) / this.encodedValue.Length;
                int num3 = (image.Width - num1 * 2 - num2 * 2) % this.encodedValue.Length / 2;
                if (width <= 0 || num2 <= 0)
                    throw new Exception("Image size specified not large enough to draw image. (Bar size determined to be less than 1 pixel or quiet zone determined to be less than 1 pixel)");
                int index = 0;
                using (Graphics graphics = Graphics.FromImage((Image)image))
                {
                    graphics.Clear(BarcodeHelper.ToWinFormsColor(this.BackColor));
                    using (System.Drawing.Pen pen = new System.Drawing.Pen(BarcodeHelper.ToWinFormsColor(this.ForeColor), (float)width))
                    {
                        pen.Alignment = PenAlignment.Right;
                        for (; index < this.encodedValue.Length; ++index)
                        {
                            if (this.encodedValue[index] == '1')
                                graphics.DrawLine(pen, new System.Drawing.Point(index * width + num3 + num1 + num2, 0), new System.Drawing.Point(index * width + num3 + num1 + num2, this.Height));
                        }
                        pen.Width = (float)image.Height / 8f;
                        pen.Color = BarcodeHelper.ToWinFormsColor(this.ForeColor);
                        pen.Alignment = PenAlignment.Center;
                        graphics.DrawLine(pen, new System.Drawing.Point(0, 0), new System.Drawing.Point(image.Width, 0));
                        graphics.DrawLine(pen, new System.Drawing.Point(0, image.Height), new System.Drawing.Point(image.Width, image.Height));
                        graphics.DrawLine(pen, new System.Drawing.Point(0, 0), new System.Drawing.Point(0, image.Height));
                        graphics.DrawLine(pen, new System.Drawing.Point(image.Width, 0), new System.Drawing.Point(image.Width, image.Height));
                    }
                }
                if (this.IncludeLabel)
                    this.LabelITF14((Image)image);
            }
            else
            {
                image = new Bitmap(this.Width, this.Height);
                int num4 = 1;
                int num5 = Convert.ToInt32((double)image.Width * 0.05);
                if (this.margin >= 0)
                    num5 = this.margin;
                int num6 = (image.Width - num5 * 2) / this.encodedValue.Length;
                if (num6 == 0)
                    num6 = 1;
                if (this.barcodeFormat == BarcodeFormat.PostNet)
                    num4 = 2;
                int num7;
                switch (this.Alignment)
                {
                    case AlignmentPositions.Center:
                        num7 = (this.Width - num5 * 2) % this.encodedValue.Length / 2;
                        break;
                    case AlignmentPositions.Left:
                        num7 = 0;
                        break;
                    case AlignmentPositions.Right:
                        num7 = (this.Width - num5 * 2) % this.encodedValue.Length;
                        break;
                    default:
                        num7 = (this.Width - num5 * 2) % this.encodedValue.Length / 2;
                        break;
                }
                if (num6 <= 0)
                    throw new Exception("Image size specified not large enough to draw image. (Bar size determined to be less than 1 pixel)");
                int index = 0;
                using (Graphics graphics = Graphics.FromImage((Image)image))
                {
                    graphics.Clear(BarcodeHelper.ToWinFormsColor(this.BackColor));
                    using (System.Drawing.Pen pen1 = new System.Drawing.Pen(BarcodeHelper.ToWinFormsColor(this.BackColor), (float)(num6 / num4)))
                    {
                        using (System.Drawing.Pen pen2 = new System.Drawing.Pen(BarcodeHelper.ToWinFormsColor(this.ForeColor), (float)(num6 / num4)))
                        {
                            for (; index < this.encodedValue.Length; ++index)
                            {
                                if (this.barcodeFormat == BarcodeFormat.PostNet)
                                {
                                    if (this.encodedValue[index] != '1')
                                        graphics.DrawLine(pen2, new System.Drawing.Point(index * num6 + num7 + 1, this.Height), new System.Drawing.Point(index * num6 + num7 + 1, this.Height / 2));
                                    graphics.DrawLine(pen1, new System.Drawing.Point(index * (num6 * num4) + num7 + num6 + 1, 0), new System.Drawing.Point(index * (num6 * num4) + num7 + num6 + 1, this.Height));
                                }
                                if (this.encodedValue[index] == '1')
                                    graphics.DrawLine(pen2, new System.Drawing.Point(index * num6 + num7 + num5, num5), new System.Drawing.Point(index * num6 + num7 + num5, this.Height - num5));
                            }
                        }
                    }
                }
                if (this.IncludeLabel)
                    this.LabelGeneric((Image)image, num5);
            }
            this.encodedImage = new WriteableBitmap(BarcodeHelper.ToBitmapSource(image));
            this.encodingTime += (DateTime.Now - now).TotalMilliseconds;
            return image;
        }

        public byte[] GetImageData(SaveOptions savetype)
        {
            byte[] imageData = (byte[])null;
            try
            {
                if (this.encodedImage != null)
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        this.SaveImage((Stream)memoryStream, savetype);
                        imageData = memoryStream.ToArray();
                        memoryStream.Flush();
                        memoryStream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve image data. " + ex.Message);
            }
            return imageData;
        }

        public void SaveImage(string fileName, SaveOptions fileType)
        {
            try
            {
                if (this.encodedImage == null)
                    return;
                ImageFormat format;
                switch (fileType)
                {
                    case SaveOptions.Jpg:
                        format = ImageFormat.Jpeg;
                        break;
                    case SaveOptions.Bmp:
                        format = ImageFormat.Bmp;
                        break;
                    case SaveOptions.Png:
                        format = ImageFormat.Png;
                        break;
                    case SaveOptions.Gif:
                        format = ImageFormat.Gif;
                        break;
                    case SaveOptions.Tiff:
                        format = ImageFormat.Tiff;
                        break;
                    default:
                        format = this.ImageFormat;
                        break;
                }
                BarcodeHelper.ToWinFormsBitmap((BitmapSource)this.encodedImage).Save(fileName, format);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not save image.\n\n=======================\n\n" + ex.Message);
            }
        }

        public void SaveImage(Stream stream, SaveOptions FileType)
        {
            try
            {
                if (this.encodedImage == null)
                    return;
                ImageFormat format;
                switch (FileType)
                {
                    case SaveOptions.Jpg:
                        format = ImageFormat.Jpeg;
                        break;
                    case SaveOptions.Bmp:
                        format = ImageFormat.Bmp;
                        break;
                    case SaveOptions.Png:
                        format = ImageFormat.Png;
                        break;
                    case SaveOptions.Gif:
                        format = ImageFormat.Gif;
                        break;
                    case SaveOptions.Tiff:
                        format = ImageFormat.Tiff;
                        break;
                    default:
                        format = this.ImageFormat;
                        break;
                }
                BarcodeHelper.ToWinFormsBitmap((BitmapSource)this.encodedImage).Save(stream, format);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not save image.\n\n=======================\n\n" + ex.Message);
            }
        }

        public void GetSizeOfImage(ref double width, ref double height, bool metric)
        {
            width = 0.0;
            height = 0.0;
            if (this.EncodedImage == null || this.EncodedImage.Width <= 0.0 || this.EncodedImage.Height <= 0.0)
                return;
            double num = 25.4;
            using (Graphics graphics = Graphics.FromImage((Image)BarcodeHelper.ToWinFormsBitmap((BitmapSource)this.EncodedImage)))
            {
                width = this.EncodedImage.Width / (double)graphics.DpiX;
                height = this.EncodedImage.Height / (double)graphics.DpiY;
                if (!metric)
                    return;
                width *= num;
                height *= num;
            }
        }

        private Image LabelITF14(Image img)
        {
            try
            {
                Font labelFont = this.LabelFont;
                using (Graphics graphics = Graphics.FromImage(img))
                {
                    graphics.DrawImage(img, 0.0f, 0.0f);
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.FillRectangle((System.Drawing.Brush)new SolidBrush(BarcodeHelper.ToWinFormsColor(this.BackColor)), new Rectangle(0, img.Height - (labelFont.Height - 2), img.Width, labelFont.Height));
                    StringFormat format = new StringFormat();
                    format.Alignment = StringAlignment.Center;
                    if (string.IsNullOrEmpty(this.CustomLabel))
                        graphics.DrawString(this.Content, labelFont, (System.Drawing.Brush)new SolidBrush(BarcodeHelper.ToWinFormsColor(this.ForeColor)), (float)(img.Width / 2), (float)(img.Height - labelFont.Height + 1), format);
                    else
                        graphics.DrawString(this.CustomLabel, labelFont, (System.Drawing.Brush)new SolidBrush(BarcodeHelper.ToWinFormsColor(this.ForeColor)), (float)(img.Width / 2), (float)(img.Height - labelFont.Height + 1), format);
                    graphics.DrawLine(new System.Drawing.Pen(BarcodeHelper.ToWinFormsColor(this.ForeColor), (float)img.Height / 16f)
                    {
                        Alignment = PenAlignment.Inset
                    }, new System.Drawing.Point(0, img.Height - labelFont.Height - 2), new System.Drawing.Point(img.Width, img.Height - labelFont.Height - 2));
                    graphics.Save();
                }
                return img;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private Image LabelGeneric(Image img, int iquietzone)
        {
            try
            {
                Font labelFont = this.LabelFont;
                using (Graphics graphics = Graphics.FromImage(img))
                {
                    graphics.DrawImage(img, 0.0f, 0.0f);
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    StringFormat format = new StringFormat();
                    format.Alignment = StringAlignment.Near;
                    format.LineAlignment = StringAlignment.Near;
                    int y = 0;
                    switch (this.LabelPosition)
                    {
                        case LabelPositions.TopLeft:
                            int width1 = img.Width;
                            y = 0;
                            format.Alignment = StringAlignment.Near;
                            break;
                        case LabelPositions.TopCenter:
                            int num1 = img.Width / 2;
                            y = 0;
                            format.Alignment = StringAlignment.Center;
                            break;
                        case LabelPositions.TopRight:
                            int width2 = img.Width;
                            y = 0;
                            format.Alignment = StringAlignment.Far;
                            break;
                        case LabelPositions.BottomLeft:
                            y = img.Height - labelFont.Height;
                            format.Alignment = StringAlignment.Near;
                            break;
                        case LabelPositions.BottomCenter:
                            int num2 = img.Width / 2;
                            y = img.Height - labelFont.Height;
                            format.Alignment = StringAlignment.Center;
                            break;
                        case LabelPositions.BottomRight:
                            int width3 = img.Width;
                            y = img.Height - labelFont.Height;
                            format.Alignment = StringAlignment.Far;
                            break;
                    }
                    graphics.FillRectangle((System.Drawing.Brush)new SolidBrush(BarcodeHelper.ToWinFormsColor(this.BackColor)), new RectangleF(0.0f, (float)y, (float)img.Width, (float)labelFont.Height));
                    if (string.IsNullOrEmpty(this.CustomLabel))
                        graphics.DrawString(this.Content, labelFont, (System.Drawing.Brush)new SolidBrush(BarcodeHelper.ToWinFormsColor(this.ForeColor)), new RectangleF(0.0f, (float)y, (float)(img.Width - iquietzone), (float)labelFont.Height), format);
                    else
                        graphics.DrawString(this.CustomLabel, labelFont, (System.Drawing.Brush)new SolidBrush(BarcodeHelper.ToWinFormsColor(this.ForeColor)), new RectangleF(0.0f, (float)y, (float)(img.Width - iquietzone), (float)labelFont.Height), format);
                    graphics.Save();
                }
                return img;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private Image LabelUPCA(Image img)
        {
            try
            {
                int num1 = this.Width / this.encodedValue.Length;
                int num2;
                switch (this.Alignment)
                {
                    case AlignmentPositions.Center:
                        num2 = this.Width % this.encodedValue.Length / 2;
                        break;
                    case AlignmentPositions.Left:
                        num2 = 0;
                        break;
                    case AlignmentPositions.Right:
                        num2 = this.Width % this.encodedValue.Length;
                        break;
                    default:
                        num2 = this.Width % this.encodedValue.Length / 2;
                        break;
                }
                Font font = new Font("OCR A Extended", 12f, System.Drawing.FontStyle.Bold, GraphicsUnit.Point, (byte)0);
                using (Graphics graphics = Graphics.FromImage(img))
                {
                    graphics.DrawImage(img, 0.0f, 0.0f);
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    RectangleF rectangleF = new RectangleF((float)(num1 * 3 + num2), (float)(this.Height - (int)((double)this.Height * 0.1)), (float)(num1 * 43), (float)(int)((double)this.Height * 0.1));
                    graphics.FillRectangle((System.Drawing.Brush)new SolidBrush(System.Drawing.Color.Yellow), rectangleF.X, rectangleF.Y, rectangleF.Width, rectangleF.Height);
                    graphics.DrawString(this.Content.Substring(1, 5), font, (System.Drawing.Brush)new SolidBrush(BarcodeHelper.ToWinFormsColor(this.ForeColor)), rectangleF.X, rectangleF.Y);
                    graphics.Save();
                }
                return img;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal static bool CheckNumericOnly(string data)
        {
            int num1 = 18;
            string str = data;
            string[] strArray = new string[data.Length / num1 + (data.Length % num1 == 0 ? 0 : 1)];
            int num2 = 0;
            while (num2 < strArray.Length)
            {
                if (str.Length >= num1)
                {
                    strArray[num2++] = str.Substring(0, num1);
                    str = str.Substring(num1);
                }
                else
                    strArray[num2++] = str.Substring(0);
            }
            foreach (string s in strArray)
            {
                long result = 0;
                if (!long.TryParse(s, out result))
                    return false;
            }
            return true;
        }

        public static WriteableBitmap DoEncode(BarcodeFormat format, string data)
        {
            using (BarcodeEncoder barcodeEncoder = new BarcodeEncoder())
                return barcodeEncoder.Encode(format, data);
        }

        public static WriteableBitmap DoEncode(
          BarcodeFormat format,
          string data,
          bool includeLabel)
        {
            using (BarcodeEncoder barcodeEncoder = new BarcodeEncoder())
            {
                barcodeEncoder.IncludeLabel = includeLabel;
                return barcodeEncoder.Encode(format, data);
            }
        }

        public static WriteableBitmap DoEncode(
          BarcodeFormat format,
          string data,
          bool includeLabel,
          int width,
          int height)
        {
            using (BarcodeEncoder barcodeEncoder = new BarcodeEncoder())
            {
                barcodeEncoder.IncludeLabel = includeLabel;
                barcodeEncoder.Width = width;
                barcodeEncoder.Height = height;
                return barcodeEncoder.Encode(format, data);
            }
        }

        public static WriteableBitmap DoEncode(
          BarcodeFormat format,
          string data,
          bool includeLabel,
          System.Windows.Media.Color foreColor,
          System.Windows.Media.Color backColor)
        {
            using (BarcodeEncoder barcodeEncoder = new BarcodeEncoder())
            {
                barcodeEncoder.IncludeLabel = includeLabel;
                barcodeEncoder.ForeColor = foreColor;
                barcodeEncoder.BackColor = backColor;
                return barcodeEncoder.Encode(format, data);
            }
        }

        public static WriteableBitmap DoEncode(
          BarcodeFormat format,
          string data,
          bool includeLabel,
          System.Windows.Media.Color foreColor,
          System.Windows.Media.Color backColor,
          int width,
          int height)
        {
            using (BarcodeEncoder barcodeEncoder = new BarcodeEncoder())
            {
                barcodeEncoder.IncludeLabel = includeLabel;
                barcodeEncoder.Width = width;
                barcodeEncoder.Height = height;
                barcodeEncoder.ForeColor = foreColor;
                barcodeEncoder.BackColor = backColor;
                return barcodeEncoder.Encode(format, data);
            }
        }

        public void Dispose()
        {
        }
    }
}
