// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Helper.NativeMethods
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;
using System.Runtime.InteropServices;

namespace MessagingToolkit.Barcode.Helper
{
  internal static class NativeMethods
  {
    internal static unsafe void CopyUnmanagedMemory(
      byte* srcPtr,
      int srcOffset,
      byte* dstPtr,
      int dstOffset,
      int count)
    {
      srcPtr += srcOffset;
      dstPtr += dstOffset;
      NativeMethods.memcpy(dstPtr, srcPtr, count);
    }

    internal static void SetUnmanagedMemory(IntPtr dst, int filler, int count) => NativeMethods.memset(dst, filler, count);

    [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern unsafe byte* memcpy(byte* dst, byte* src, int count);

    [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern void memset(IntPtr dst, int filler, int count);

    [DllImport("gdi32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool DeleteObject(IntPtr hObject);
  }
}
