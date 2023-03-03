// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.BarcodeDecoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Aztec;
using MessagingToolkit.Barcode.Common;
using MessagingToolkit.Barcode.DataMatrix;
using MessagingToolkit.Barcode.Helper;
using MessagingToolkit.Barcode.MaxiCode;
using MessagingToolkit.Barcode.Multi;
using MessagingToolkit.Barcode.Multi.QRCode;
using MessagingToolkit.Barcode.OneD;
using MessagingToolkit.Barcode.Pdf417;
using MessagingToolkit.Barcode.QRCode;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Media.Imaging;

namespace MessagingToolkit.Barcode
{
  public class BarcodeDecoder : IBarcodeDecoder, IDecoder
  {
    private Dictionary<DecodeOptions, object> decodingOptions;
    private List<IDecoder> decoders = new List<IDecoder>();
    private LuminanceSource luminanceSource;
    private static readonly Func<WriteableBitmap, LuminanceSource> defaultCreateLuminanceSource = (Func<WriteableBitmap, LuminanceSource>) (bitmap => (LuminanceSource) new BitmapSourceLuminanceSource((BitmapSource) bitmap));
    private readonly Func<WriteableBitmap, LuminanceSource> createLuminanceSource;
    private readonly Func<LuminanceSource, Binarizer> createBinarizer;
    private static readonly Func<LuminanceSource, Binarizer> defaultCreateBinarizer = (Func<LuminanceSource, Binarizer>) (luminanceSource => (Binarizer) new HybridBinarizer(luminanceSource));
    private readonly Func<byte[], int, int, RGBLuminanceSource.BitmapFormat, LuminanceSource> createRGBLuminanceSource;
    private static readonly Func<byte[], int, int, RGBLuminanceSource.BitmapFormat, LuminanceSource> defaultCreateRGBLuminanceSource = (Func<byte[], int, int, RGBLuminanceSource.BitmapFormat, LuminanceSource>) ((rawBytes, width, height, format) => (LuminanceSource) new RGBLuminanceSource(rawBytes, width, height, format));

    public BarcodeDecoder()
      : this((Func<WriteableBitmap, LuminanceSource>) null, (Func<LuminanceSource, Binarizer>) null)
    {
    }

    protected bool AutoRotate { get; set; }

    protected bool TryInverted { get; set; }

    public BarcodeDecoder(
      Func<WriteableBitmap, LuminanceSource> createLuminanceSource,
      Func<LuminanceSource, Binarizer> createBinarizer)
      : this(createLuminanceSource, createBinarizer, (Func<byte[], int, int, RGBLuminanceSource.BitmapFormat, LuminanceSource>) null)
    {
    }

    public BarcodeDecoder(
      Func<WriteableBitmap, LuminanceSource> createLuminanceSource,
      Func<LuminanceSource, Binarizer> createBinarizer,
      Func<byte[], int, int, RGBLuminanceSource.BitmapFormat, LuminanceSource> createRGBLuminanceSource)
    {
      this.createLuminanceSource = createLuminanceSource ?? BarcodeDecoder.defaultCreateLuminanceSource;
      this.createBinarizer = createBinarizer ?? BarcodeDecoder.defaultCreateBinarizer;
      this.createRGBLuminanceSource = createRGBLuminanceSource ?? BarcodeDecoder.defaultCreateRGBLuminanceSource;
      this.decodingOptions = new Dictionary<DecodeOptions, object>(1);
    }

    protected Func<WriteableBitmap, LuminanceSource> CreateLuminanceSource => this.createLuminanceSource ?? BarcodeDecoder.defaultCreateLuminanceSource;

    protected Func<LuminanceSource, Binarizer> CreateBinarizer => this.createBinarizer ?? BarcodeDecoder.defaultCreateBinarizer;

    public Result Decode(BinaryBitmap binaryBitmap)
    {
      this.SetOptions((Dictionary<DecodeOptions, object>) null);
      return this.DecodeInternal(binaryBitmap);
    }

    public Result Decode(WriteableBitmap image)
    {
      if (image == null)
        throw NotFoundException.Instance;
      this.luminanceSource = this.CreateLuminanceSource(image);
      return this.Decode(new BinaryBitmap(this.CreateBinarizer(this.luminanceSource)));
    }

    public Result Decode(
      BinaryBitmap image,
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      this.SetOptions(decodingOptions);
      return this.DecodeInternal(image);
    }

    public Result Decode(
      WriteableBitmap image,
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      if (image == null)
        throw NotFoundException.Instance;
      this.luminanceSource = this.CreateLuminanceSource(image);
      return this.Decode(new BinaryBitmap(this.CreateBinarizer(this.luminanceSource)), decodingOptions);
    }

    public Result Decode(
      byte[] rawRGB,
      int width,
      int height,
      RGBLuminanceSource.BitmapFormat format)
    {
      if (rawRGB == null)
        throw new ArgumentNullException(nameof (rawRGB));
      this.luminanceSource = this.createRGBLuminanceSource(rawRGB, width, height, format);
      return this.Decode(new BinaryBitmap(this.CreateBinarizer(this.luminanceSource)));
    }

    public Result Decode(
      byte[] rawRGB,
      int width,
      int height,
      RGBLuminanceSource.BitmapFormat format,
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      if (rawRGB == null)
        throw new ArgumentNullException(nameof (rawRGB));
      this.luminanceSource = this.createRGBLuminanceSource(rawRGB, width, height, format);
      return this.Decode(new BinaryBitmap(this.CreateBinarizer(this.luminanceSource)), decodingOptions);
    }

    public Result DecodeWithState(LuminanceSource luminanceSource, BinaryBitmap image)
    {
      if (this.decoders.Count == 0)
        this.SetOptions((Dictionary<DecodeOptions, object>) null);
      this.luminanceSource = luminanceSource;
      return this.DecodeInternal(image);
    }

    public void SetOptions(Dictionary<DecodeOptions, object> decodingOptions)
    {
      this.decodingOptions = decodingOptions;
      bool flag1 = decodingOptions != null && decodingOptions.ContainsKey(DecodeOptions.TryHarder);
      this.AutoRotate = decodingOptions != null && decodingOptions.ContainsKey(DecodeOptions.AutoRotate) && (bool) BarcodeHelper.GetDecodeOptionType(decodingOptions, DecodeOptions.AutoRotate);
      List<BarcodeFormat> barcodeFormatList = decodingOptions == null ? (List<BarcodeFormat>) null : (List<BarcodeFormat>) BarcodeHelper.GetDecodeOptionType(decodingOptions, DecodeOptions.PossibleFormats);
      this.decoders.Clear();
      if (barcodeFormatList != null)
      {
        bool flag2 = barcodeFormatList.Contains(BarcodeFormat.UPCA) || barcodeFormatList.Contains(BarcodeFormat.UPCE) || barcodeFormatList.Contains(BarcodeFormat.EAN13) || barcodeFormatList.Contains(BarcodeFormat.EAN8) || barcodeFormatList.Contains(BarcodeFormat.Codabar) || barcodeFormatList.Contains(BarcodeFormat.Code39) || barcodeFormatList.Contains(BarcodeFormat.Code93) || barcodeFormatList.Contains(BarcodeFormat.Code128) || barcodeFormatList.Contains(BarcodeFormat.ITF14) || barcodeFormatList.Contains(BarcodeFormat.RSS14) || barcodeFormatList.Contains(BarcodeFormat.RSSExpanded) || barcodeFormatList.Contains(BarcodeFormat.MSIMod10);
        if (flag2 && !flag1)
          this.decoders.Add((IDecoder) new BarcodeOneDDecoder(decodingOptions));
        if (barcodeFormatList.Contains(BarcodeFormat.QRCode) && BarcodeHelper.GetDecodeOptionType(decodingOptions, DecodeOptions.MultipleBarcode) == null)
          this.decoders.Add((IDecoder) new QRCodeDecoder());
        if (barcodeFormatList.Contains(BarcodeFormat.DataMatrix))
          this.decoders.Add((IDecoder) new DataMatrixDecoder());
        if (barcodeFormatList.Contains(BarcodeFormat.Aztec))
          this.decoders.Add((IDecoder) new AztecDecoder());
        if (barcodeFormatList.Contains(BarcodeFormat.PDF417))
          this.decoders.Add((IDecoder) new Pdf417Decoder());
        if (barcodeFormatList.Contains(BarcodeFormat.MaxiCode))
          this.decoders.Add((IDecoder) new MaxiCodeDecoder());
        if (flag2 && flag1)
          this.decoders.Add((IDecoder) new BarcodeOneDDecoder(decodingOptions));
      }
      if (this.decoders.Count != 0)
        return;
      if (BarcodeHelper.GetDecodeOptionType(decodingOptions, DecodeOptions.MultipleBarcode) == null)
        this.decoders.Add((IDecoder) new QRCodeDecoder());
      this.decoders.Add((IDecoder) new DataMatrixDecoder());
      this.decoders.Add((IDecoder) new AztecDecoder());
      this.decoders.Add((IDecoder) new Pdf417Decoder());
      this.decoders.Add((IDecoder) new MaxiCodeDecoder());
      this.decoders.Add((IDecoder) new BarcodeOneDDecoder(decodingOptions));
    }

    public void Reset()
    {
      int count = this.decoders.Count;
      foreach (IDecoder decoder in this.decoders)
        decoder.Reset();
    }

    public Version Version
    {
      get
      {
        string empty = string.Empty;
        Match match = new Regex("Version=(?<version>[0-9.]*)").Match(this.GetType().AssemblyQualifiedName);
        if (match.Success)
          empty = match.Groups["version"].Value;
        return new Version(empty);
      }
    }

    private Result DecodeInternal(BinaryBitmap binaryBitmap)
    {
      int num = 0;
      for (int index = this.AutoRotate ? 4 : 1; num < index; ++num)
      {
        foreach (IDecoder decoder in this.decoders)
        {
          try
          {
            Result result = decoder.Decode(binaryBitmap, this.decodingOptions);
            if (result != null)
            {
              if (result.ResultMetadata == null)
                result.PutMetadata(ResultMetadataType.Orientation, (object) (num * 90));
              else
                result.ResultMetadata[ResultMetadataType.Orientation] = result.ResultMetadata.ContainsKey(ResultMetadataType.Orientation) ? (object) (((int) result.ResultMetadata[ResultMetadataType.Orientation] + num * 90) % 360) : (object) (num * 90);
            }
            return result;
          }
          catch
          {
          }
        }
        if (this.luminanceSource != null && this.luminanceSource.RotateSupported && this.AutoRotate)
          binaryBitmap = new BinaryBitmap(this.CreateBinarizer(this.luminanceSource.RotateCounterClockwise()));
        else
          break;
      }
      throw NotFoundException.Instance;
    }

    public Result[] DecodeMultiple(WriteableBitmap image) => this.DecodeMultiple(image, (Dictionary<DecodeOptions, object>) null);

    public Result[] DecodeMultiple(
      WriteableBitmap image,
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      return this.DecodeMultiple(new BinaryBitmap(this.CreateBinarizer(this.CreateLuminanceSource(image))), decodingOptions);
    }

    public Result[] DecodeMultiple(BinaryBitmap image) => this.DecodeMultiple(image, (Dictionary<DecodeOptions, object>) null);

    public Result[] DecodeMultiple(
      BinaryBitmap image,
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      if (decodingOptions == null)
        decodingOptions = new Dictionary<DecodeOptions, object>(1);
      decodingOptions.Add(DecodeOptions.MultipleBarcode, (object) true);
      List<BarcodeFormat> barcodeFormatList = (decodingOptions == null ? (List<BarcodeFormat>) null : (List<BarcodeFormat>) BarcodeHelper.GetDecodeOptionType(decodingOptions, DecodeOptions.PossibleFormats)) ?? new List<BarcodeFormat>();
      List<Result> resultList = new List<Result>(1);
      try
      {
        Result[] collection = new GenericMultipleBarcodeDecoder((IDecoder) this).DecodeMultiple(image, decodingOptions);
        if (collection != null)
          resultList.AddRange((IEnumerable<Result>) collection);
      }
      catch (BarcodeDecoderException ex)
      {
      }
      if (barcodeFormatList.Count != 0)
      {
        if (!barcodeFormatList.Contains(BarcodeFormat.QRCode))
          goto label_11;
      }
      try
      {
        Result[] collection = new QRCodeMultiDecoder().DecodeMultiple(image);
        if (collection != null)
          resultList.AddRange((IEnumerable<Result>) collection);
      }
      catch (BarcodeDecoderException ex)
      {
      }
label_11:
      return resultList.ToArray();
    }

    public Result[] DecodeMultiple(
      byte[] rawRGB,
      int width,
      int height,
      RGBLuminanceSource.BitmapFormat format)
    {
      if (rawRGB == null)
        throw new ArgumentNullException(nameof (rawRGB));
      this.luminanceSource = this.createRGBLuminanceSource(rawRGB, width, height, format);
      return this.DecodeMultiple(new BinaryBitmap(this.CreateBinarizer(this.luminanceSource)));
    }

    public Result[] DecodeMultiple(
      byte[] rawRGB,
      int width,
      int height,
      RGBLuminanceSource.BitmapFormat format,
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      if (rawRGB == null)
        throw new ArgumentNullException(nameof (rawRGB));
      this.luminanceSource = this.createRGBLuminanceSource(rawRGB, width, height, format);
      return this.DecodeMultiple(new BinaryBitmap(this.CreateBinarizer(this.luminanceSource)), decodingOptions);
    }
  }
}
