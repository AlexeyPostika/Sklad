// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.Encoder.DataMatrixImage
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;

namespace MessagingToolkit.Barcode.DataMatrix.Encoder
{
  internal class DataMatrixImage
  {
    private int _rowPadBytes;

    internal DataMatrixImage(byte[] pxl, int width, int height, DataMatrixPackOrder pack)
    {
      this.BitsPerChannel = new int[4];
      this.ChannelStart = new int[4];
      if (pxl == null || width < 1 || height < 1)
        throw new ArgumentException("Cannot create image of size null");
      this.Pxl = pxl;
      this.Width = width;
      this.Height = height;
      this.PixelPacking = pack;
      this.BitsPerPixel = DataMatrixCommon.GetBitsPerPixel(pack);
      this.BytesPerPixel = this.BitsPerPixel / 8;
      this._rowPadBytes = 0;
      this.RowSizeBytes = this.Width * this.BytesPerPixel + this._rowPadBytes;
      this.ImageFlip = DataMatrixFlip.FlipNone;
      this.ChannelCount = 0;
      switch (pack)
      {
        case DataMatrixPackOrder.PackCustom:
          break;
        case DataMatrixPackOrder.Pack1bppK:
          throw new ArgumentException("Cannot create image: not supported pack order!");
        case DataMatrixPackOrder.Pack8bppK:
          this.SetChannel(0, 8);
          break;
        case DataMatrixPackOrder.Pack16bppRGB:
        case DataMatrixPackOrder.Pack16bppBGR:
        case DataMatrixPackOrder.Pack16bppYCbCr:
          this.SetChannel(0, 5);
          this.SetChannel(5, 5);
          this.SetChannel(10, 5);
          break;
        case DataMatrixPackOrder.Pack16bppRGBX:
        case DataMatrixPackOrder.Pack16bppBGRX:
          this.SetChannel(0, 5);
          this.SetChannel(5, 5);
          this.SetChannel(10, 5);
          break;
        case DataMatrixPackOrder.Pack16bppXRGB:
        case DataMatrixPackOrder.Pack16bppXBGR:
          this.SetChannel(1, 5);
          this.SetChannel(6, 5);
          this.SetChannel(11, 5);
          break;
        case DataMatrixPackOrder.Pack24bppRGB:
        case DataMatrixPackOrder.Pack24bppBGR:
        case DataMatrixPackOrder.Pack24bppYCbCr:
        case DataMatrixPackOrder.Pack32bppRGBX:
        case DataMatrixPackOrder.Pack32bppBGRX:
          this.SetChannel(0, 8);
          this.SetChannel(8, 8);
          this.SetChannel(16, 8);
          break;
        case DataMatrixPackOrder.Pack32bppXRGB:
        case DataMatrixPackOrder.Pack32bppXBGR:
          this.SetChannel(8, 8);
          this.SetChannel(16, 8);
          this.SetChannel(24, 8);
          break;
        case DataMatrixPackOrder.Pack32bppCMYK:
          this.SetChannel(0, 8);
          this.SetChannel(8, 8);
          this.SetChannel(16, 8);
          this.SetChannel(24, 8);
          break;
        default:
          throw new ArgumentException("Cannot create image: Invalid Pack Order");
      }
    }

    internal bool SetChannel(int channelStart, int bitsPerChannel)
    {
      if (this.ChannelCount >= 4)
        return false;
      this.BitsPerChannel[this.ChannelCount] = bitsPerChannel;
      this.ChannelStart[this.ChannelCount] = channelStart;
      ++this.ChannelCount;
      return true;
    }

    internal int GetByteOffset(int x, int y)
    {
      if (this.ImageFlip == DataMatrixFlip.FlipX)
        throw new ArgumentException("FlipX is not an option!");
      if (!this.ContainsInt(0, x, y))
        return DataMatrixConstants.DataMatrixUndefined;
      return this.ImageFlip == DataMatrixFlip.FlipY ? y * this.RowSizeBytes + x * this.BytesPerPixel : (this.Height - y - 1) * this.RowSizeBytes + x * this.BytesPerPixel;
    }

    internal bool GetPixelValue(int x, int y, int channel, ref int value)
    {
      if (channel >= this.ChannelCount)
        throw new ArgumentException("Channel greater than channel count!");
      int byteOffset = this.GetByteOffset(x, y);
      if (byteOffset == DataMatrixConstants.DataMatrixUndefined)
        return false;
      switch (this.BitsPerChannel[channel])
      {
        case 8:
          if (this.ChannelStart[channel] % 8 != 0 || this.BitsPerPixel % 8 != 0)
            throw new Exception("Error getting pixel value");
          value = (int) this.Pxl[byteOffset + channel];
          break;
      }
      return true;
    }

    internal bool SetPixelValue(int x, int y, int channel, byte value)
    {
      if (channel >= this.ChannelCount)
        throw new ArgumentException("Channel greater than channel count!");
      int byteOffset = this.GetByteOffset(x, y);
      if (byteOffset == DataMatrixConstants.DataMatrixUndefined)
        return false;
      switch (this.BitsPerChannel[channel])
      {
        case 8:
          if (this.ChannelStart[channel] % 8 != 0 || this.BitsPerPixel % 8 != 0)
            throw new Exception("Error getting pixel value");
          this.Pxl[byteOffset + channel] = value;
          break;
      }
      return true;
    }

    internal bool ContainsInt(int margin, int x, int y) => x - margin >= 0 && x + margin < this.Width && y - margin >= 0 && y + margin < this.Height;

    internal bool ContainsFloat(double x, double y) => x >= 0.0 && x < (double) this.Width && y >= 0.0 && y < (double) this.Height;

    internal int Width { get; set; }

    internal int Height { get; set; }

    internal DataMatrixPackOrder PixelPacking { get; set; }

    internal int BitsPerPixel { get; set; }

    internal int BytesPerPixel { get; set; }

    internal int RowPadBytes
    {
      get => this._rowPadBytes;
      set
      {
        this._rowPadBytes = value;
        this.RowSizeBytes = this.Width * (this.BitsPerPixel / 8) + this._rowPadBytes;
      }
    }

    internal int RowSizeBytes { get; set; }

    internal DataMatrixFlip ImageFlip { get; set; }

    internal int ChannelCount { get; set; }

    internal int[] ChannelStart { get; set; }

    internal int[] BitsPerChannel { get; set; }

    internal byte[] Pxl { get; set; }
  }
}
