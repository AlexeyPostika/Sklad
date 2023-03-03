// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Code39Encoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode.OneD
{
  public sealed class Code39Encoder : OneDEncoder
  {
    public override BitMatrix Encode(
      string contents,
      BarcodeFormat format,
      int width,
      int height,
      Dictionary<EncodeOptions, object> encodingOptions)
    {
      if (format != BarcodeFormat.Code39)
        throw new ArgumentException("Can only encode CODE_39, but got " + (object) format);
      return base.Encode(contents, format, width, height, encodingOptions);
    }

    public override bool[] Encode(string contents)
    {
      int length1 = contents.Length;
      if (length1 > 80)
        throw new ArgumentException("Requested contents should be less than 80 digits long, but got " + (object) length1);
      int[] numArray = new int[9];
      int length2 = 25 + length1;
      for (int index1 = 0; index1 < length1; ++index1)
      {
        int index2 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. *$/+%".IndexOf(contents[index1]);
        Code39Encoder.ToIntArray(Code39Decoder.CharacterEncodings[index2], numArray);
        foreach (int num in numArray)
          length2 += num;
      }
      bool[] target = new bool[length2];
      Code39Encoder.ToIntArray(Code39Decoder.CharacterEncodings[39], numArray);
      int pos1 = OneDEncoder.AppendPattern(target, 0, numArray, true);
      int[] pattern = new int[1]{ 1 };
      int pos2 = pos1 + OneDEncoder.AppendPattern(target, pos1, pattern, false);
      for (int index3 = length1 - 1; index3 >= 0; --index3)
      {
        int index4 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. *$/+%".IndexOf(contents[index3]);
        Code39Encoder.ToIntArray(Code39Decoder.CharacterEncodings[index4], numArray);
        int pos3 = pos2 + OneDEncoder.AppendPattern(target, pos2, numArray, true);
        pos2 = pos3 + OneDEncoder.AppendPattern(target, pos3, pattern, false);
      }
      Code39Encoder.ToIntArray(Code39Decoder.CharacterEncodings[39], numArray);
      int num1 = pos2 + OneDEncoder.AppendPattern(target, pos2, numArray, true);
      return target;
    }

    private static void ToIntArray(int a, int[] toReturn)
    {
      for (int index = 0; index < 9; ++index)
      {
        int num = a & 1 << index;
        toReturn[index] = num == 0 ? 1 : 2;
      }
    }
  }
}
