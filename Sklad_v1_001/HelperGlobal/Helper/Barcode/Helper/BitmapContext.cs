// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Helper.BitmapContext
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MessagingToolkit.Barcode.Helper
{
  public struct BitmapContext : IDisposable
  {
    private readonly WriteableBitmap writeableBitmap;
    private readonly ReadWriteMode mode;
    private static readonly IDictionary<WriteableBitmap, int> UpdateCountByBmp = (IDictionary<WriteableBitmap, int>) new Dictionary<WriteableBitmap, int>();
    private readonly unsafe int* backBuffer;

    public WriteableBitmap WriteableBitmap => this.writeableBitmap;

    public int Width => this.writeableBitmap.PixelWidth;

    public int Height => this.writeableBitmap.PixelHeight;

    public unsafe BitmapContext(WriteableBitmap writeableBitmap)
    {
      this.writeableBitmap = writeableBitmap;
      if (writeableBitmap.Format != PixelFormats.Pbgra32)
        throw new ArgumentException("The input WriteableBitmap needs to have the Pbgra32 pixel format. Use the BitmapFactory.ConvertToPbgra32Format method to automatically convert any input BitmapSource to the right format accepted by this class.", nameof (writeableBitmap));
      this.mode = ReadWriteMode.ReadWrite;
      if (!BitmapContext.UpdateCountByBmp.ContainsKey(writeableBitmap))
      {
        BitmapContext.UpdateCountByBmp.Add(writeableBitmap, 1);
        writeableBitmap.Lock();
      }
      else
        BitmapContext.IncrementRefCount(writeableBitmap);
      this.backBuffer = (int*) (void*) writeableBitmap.BackBuffer;
    }

    public BitmapContext(WriteableBitmap writeableBitmap, ReadWriteMode mode)
      : this(writeableBitmap)
    {
      this.mode = mode;
    }

    public unsafe int* Pixels => this.backBuffer;

    public int Length => (int) ((double) (this.writeableBitmap.BackBufferStride / 4) * (double) this.writeableBitmap.PixelHeight);

    public static unsafe void BlockCopy(
      BitmapContext src,
      int srcOffset,
      BitmapContext dest,
      int destOffset,
      int count)
    {
      NativeMethods.CopyUnmanagedMemory((byte*) src.Pixels, srcOffset, (byte*) dest.Pixels, destOffset, count);
    }

    public static unsafe void BlockCopy(
      int[] src,
      int srcOffset,
      BitmapContext dest,
      int destOffset,
      int count)
    {
      fixed (int* srcPtr = src)
        NativeMethods.CopyUnmanagedMemory((byte*) srcPtr, srcOffset, (byte*) dest.Pixels, destOffset, count);
    }

    public static unsafe void BlockCopy(
      byte[] src,
      int srcOffset,
      BitmapContext dest,
      int destOffset,
      int count)
    {
      fixed (byte* srcPtr = src)
        NativeMethods.CopyUnmanagedMemory(srcPtr, srcOffset, (byte*) dest.Pixels, destOffset, count);
    }

    public static unsafe void BlockCopy(
      BitmapContext src,
      int srcOffset,
      byte[] dest,
      int destOffset,
      int count)
    {
      fixed (byte* dstPtr = dest)
        NativeMethods.CopyUnmanagedMemory((byte*) src.Pixels, srcOffset, dstPtr, destOffset, count);
    }

    public static unsafe void BlockCopy(
      BitmapContext src,
      int srcOffset,
      int[] dest,
      int destOffset,
      int count)
    {
      fixed (int* dstPtr = dest)
        NativeMethods.CopyUnmanagedMemory((byte*) src.Pixels, srcOffset, (byte*) dstPtr, destOffset, count);
    }

    public void Clear() => NativeMethods.SetUnmanagedMemory(this.writeableBitmap.BackBuffer, 0, this.writeableBitmap.BackBufferStride * this.writeableBitmap.PixelHeight);

    public void Dispose()
    {
      if (BitmapContext.DecrementRefCount(this.writeableBitmap) != 0)
        return;
      BitmapContext.UpdateCountByBmp.Remove(this.writeableBitmap);
      if (this.mode == ReadWriteMode.ReadWrite)
        this.writeableBitmap.AddDirtyRect(new Int32Rect(0, 0, this.writeableBitmap.PixelWidth, this.writeableBitmap.PixelHeight));
      this.writeableBitmap.Unlock();
    }

    private static void IncrementRefCount(WriteableBitmap target)
    {
      IDictionary<WriteableBitmap, int> updateCountByBmp;
      WriteableBitmap key;
      (updateCountByBmp = BitmapContext.UpdateCountByBmp)[key = target] = updateCountByBmp[key] + 1;
    }

    private static int DecrementRefCount(WriteableBitmap target)
    {
      int num = BitmapContext.UpdateCountByBmp[target] - 1;
      BitmapContext.UpdateCountByBmp[target] = num;
      return num;
    }
  }
}
