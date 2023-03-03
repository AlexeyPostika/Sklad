// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.OneDEncoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using MessagingToolkit.Barcode.Helper;
using System;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode.OneD
{
  public abstract class OneDEncoder : IEncoder
  {
    public BitMatrix Encode(string contents, BarcodeFormat format, int width, int height) => this.Encode(contents, format, width, height, (Dictionary<EncodeOptions, object>) null);

    public virtual BitMatrix Encode(
      string contents,
      BarcodeFormat format,
      int width,
      int height,
      Dictionary<EncodeOptions, object> encodingOptions)
    {
      if (contents.Length == 0)
        throw new ArgumentException("Found empty contents");
      if (width < 0 || height < 0)
        throw new ArgumentException("Negative size is not allowed. Input: " + (object) width + (object) 'x' + (object) height);
      int sidesMargin = this.DefaultMargin;
      if (encodingOptions != null)
      {
        object encodeOptionType = BarcodeHelper.GetEncodeOptionType(encodingOptions, EncodeOptions.Margin);
        if (encodeOptionType != null)
          sidesMargin = Convert.ToInt32(encodeOptionType);
      }
      return OneDEncoder.RenderResult(this.Encode(contents), width, height, sidesMargin);
    }

    private static BitMatrix RenderResult(
      bool[] code,
      int width,
      int height,
      int sidesMargin)
    {
      int length = code.Length;
      int val2 = length + (UPCEANDecoder.StartEndPattern.Length << 1);
      int width1 = Math.Max(width, val2);
      int num1 = Math.Max(1, height);
      int width_0 = width1 / val2;
      int num2 = (width1 - length * width_0) / 2;
      BitMatrix bitMatrix = new BitMatrix(width1, num1);
      int index = 0;
      int left = num2;
      while (index < length)
      {
        if (code[index])
          bitMatrix.SetRegion(left, 0, width_0, num1);
        ++index;
        left += width_0;
      }
      return bitMatrix;
    }

    internal static int AppendPattern(bool[] target, int pos, int[] pattern, bool startColor)
    {
      bool flag = startColor;
      int num1 = 0;
      foreach (int num2 in pattern)
      {
        for (int index = 0; index < num2; ++index)
          target[pos++] = flag;
        num1 += num2;
        flag = !flag;
      }
      return num1;
    }

    public virtual int DefaultMargin => 10;

    public abstract bool[] Encode(string contents);
  }
}
