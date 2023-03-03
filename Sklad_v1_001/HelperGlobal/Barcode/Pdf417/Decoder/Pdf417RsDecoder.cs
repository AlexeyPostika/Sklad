// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Pdf417.Decoder.Pdf417RsDecoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.Pdf417.Decoder
{
  public sealed class Pdf417RsDecoder
  {
    private static readonly int PRIM = 1;
    private static readonly int GPRIME = 929;
    private static readonly int A0 = 928;
    private int[] AlphaTo = new int[1024];
    private int[] IndexOf = new int[1024];

    public Pdf417RsDecoder()
    {
      int index1 = 1;
      this.IndexOf[1] = Pdf417RsDecoder.GPRIME - 1;
      for (int index2 = 0; index2 < Pdf417RsDecoder.GPRIME - 1; ++index2)
      {
        this.AlphaTo[index2] = index1;
        if (index1 < Pdf417RsDecoder.GPRIME && index2 != Pdf417RsDecoder.GPRIME - 1)
          this.IndexOf[index1] = index2;
        index1 = index1 * 3 % Pdf417RsDecoder.GPRIME;
      }
      this.IndexOf[0] = Pdf417RsDecoder.GPRIME - 1;
      this.AlphaTo[Pdf417RsDecoder.GPRIME - 1] = 1;
      this.IndexOf[Pdf417RsDecoder.GPRIME] = Pdf417RsDecoder.A0;
    }

    private int Modbase(int x) => x % (Pdf417RsDecoder.GPRIME - 1);

    public int CorrectErrors(int[] data, int[] eras_pos, int no_eras, int data_len, int syndLen)
    {
      int[] numArray1 = new int[2049];
      int[] numArray2 = new int[2049];
      int[] numArray3 = new int[2049];
      int[] numArray4 = new int[2049];
      int[] numArray5 = new int[2049];
      int[] numArray6 = new int[2048];
      int[] numArray7 = new int[2049];
      int[] numArray8 = new int[2048];
      for (int index = 1; index <= syndLen; ++index)
        numArray2[index] = 0;
      for (int index1 = 1; index1 <= data_len; ++index1)
      {
        if (data[data_len - index1] != 0)
        {
          int num = this.IndexOf[data[data_len - index1]];
          for (int index2 = 1; index2 <= syndLen; ++index2)
            numArray2[index2] = (numArray2[index2] + this.AlphaTo[this.Modbase(num + index2 * index1)]) % Pdf417RsDecoder.GPRIME;
        }
      }
      int num1 = 0;
      for (int index = 1; index <= syndLen; ++index)
      {
        num1 |= numArray2[index];
        numArray2[index] = this.IndexOf[numArray2[index]];
      }
      int index3;
      if (num1 == 0)
      {
        index3 = 0;
      }
      else
      {
        for (int index4 = syndLen - 1; index4 >= 0; --index4)
          numArray1[index4 + 1] = 0;
        numArray1[0] = 1;
        if (no_eras > 0)
        {
          numArray1[1] = this.AlphaTo[this.Modbase(Pdf417RsDecoder.PRIM * eras_pos[0])];
          for (int index5 = 1; index5 < no_eras; ++index5)
          {
            int num2 = this.Modbase(Pdf417RsDecoder.PRIM * eras_pos[index5]);
            for (int index6 = index5 + 1; index6 > 0; --index6)
            {
              int num3 = this.IndexOf[numArray1[index6 - 1]];
              if (num3 != Pdf417RsDecoder.A0)
                numArray1[index6] = (numArray1[index6] + this.AlphaTo[this.Modbase(num2 + num3)]) % Pdf417RsDecoder.GPRIME;
            }
          }
        }
        for (int index7 = 0; index7 < syndLen + 1; ++index7)
          numArray3[index7] = this.IndexOf[numArray1[index7]];
        int num4 = no_eras;
        int num5 = no_eras;
        while (++num4 <= syndLen)
        {
          int index8 = 0;
          for (int index9 = 0; index9 < num4; ++index9)
          {
            if (numArray1[index9] != 0 && numArray2[num4 - index9] != Pdf417RsDecoder.A0)
              index8 = index9 % 2 != 1 ? (index8 + Pdf417RsDecoder.GPRIME - this.AlphaTo[this.Modbase(this.IndexOf[numArray1[index9]] + numArray2[num4 - index9])]) % Pdf417RsDecoder.GPRIME : (index8 + this.AlphaTo[this.Modbase(this.IndexOf[numArray1[index9]] + numArray2[num4 - index9])]) % Pdf417RsDecoder.GPRIME;
          }
          int num6 = this.IndexOf[index8];
          if (num6 == Pdf417RsDecoder.A0)
          {
            for (int index10 = syndLen - 1; index10 >= 0; --index10)
              numArray3[index10 + 1] = numArray3[index10];
            numArray3[0] = Pdf417RsDecoder.A0;
          }
          else
          {
            numArray4[0] = numArray1[0];
            for (int index11 = 0; index11 < syndLen; ++index11)
              numArray4[index11 + 1] = numArray3[index11] == Pdf417RsDecoder.A0 ? numArray1[index11 + 1] : (numArray1[index11 + 1] + this.AlphaTo[this.Modbase(num6 + numArray3[index11])]) % Pdf417RsDecoder.GPRIME;
            int num7 = 0;
            if (2 * num7 <= num4 + no_eras - 1)
            {
              num5 = num4 + no_eras - num7;
              for (int index12 = 0; index12 <= syndLen; ++index12)
                numArray3[index12] = numArray1[index12] != 0 ? this.Modbase(this.IndexOf[numArray1[index12]] - num6 + Pdf417RsDecoder.GPRIME - 1) : Pdf417RsDecoder.A0;
            }
            else
            {
              for (int index13 = syndLen - 1; index13 >= 0; --index13)
                numArray3[index13 + 1] = numArray3[index13];
              numArray3[0] = Pdf417RsDecoder.A0;
            }
            for (int index14 = syndLen + 1 - 1; index14 >= 0; --index14)
              numArray1[index14] = numArray4[index14];
          }
        }
        int num8 = 0;
        for (int index15 = 0; index15 < syndLen + 1; ++index15)
        {
          numArray1[index15] = this.IndexOf[numArray1[index15]];
          if (numArray1[index15] != Pdf417RsDecoder.A0)
            num8 = index15;
        }
        for (int index16 = syndLen - 1; index16 >= 0; --index16)
          numArray7[index16 + 1] = numArray1[index16 + 1];
        index3 = 0;
        int num9 = 1;
        int num10 = data_len - 1;
        for (; num9 <= Pdf417RsDecoder.GPRIME; ++num9)
        {
          int num11 = 1;
          for (int index17 = num8; index17 > 0; --index17)
          {
            if (numArray7[index17] != Pdf417RsDecoder.A0)
            {
              numArray7[index17] = this.Modbase(numArray7[index17] + index17);
              if (num8 != 1)
              {
                num11 = index17 % 2 != 0 ? (num11 + Pdf417RsDecoder.GPRIME - this.AlphaTo[numArray7[index17]]) % Pdf417RsDecoder.GPRIME : (num11 + this.AlphaTo[numArray7[index17]]) % Pdf417RsDecoder.GPRIME;
              }
              else
              {
                num11 = this.AlphaTo[numArray7[index17]] % Pdf417RsDecoder.GPRIME;
                if (num11 == 1)
                  --num11;
              }
            }
          }
          if (num11 == 0)
          {
            numArray6[index3] = num9;
            numArray8[index3] = Pdf417RsDecoder.GPRIME - 1 - num9;
            if (index3 < syndLen)
              ++index3;
          }
          if (num10 == 0)
            num10 = data_len - 1;
          else
            --num10;
          if (index3 == num8)
            break;
        }
        if (num8 != index3)
        {
          index3 = -1;
        }
        else
        {
          int num12 = 0;
          for (int index18 = 0; index18 < syndLen; ++index18)
          {
            int index19 = 0;
            for (int index20 = num8 < index18 ? num8 : index18; index20 >= 0; --index20)
            {
              if (numArray2[index18 + 1 - index20] != Pdf417RsDecoder.A0 && numArray1[index20] != Pdf417RsDecoder.A0)
                index19 = index20 % 2 != 1 ? (index19 + this.AlphaTo[this.Modbase(numArray2[index18 + 1 - index20] + numArray1[index20])]) % Pdf417RsDecoder.GPRIME : (index19 + Pdf417RsDecoder.GPRIME - this.AlphaTo[this.Modbase(numArray2[index18 + 1 - index20] + numArray1[index20])]) % Pdf417RsDecoder.GPRIME;
            }
            if (index19 != 0)
              num12 = index18;
            numArray5[index18] = this.IndexOf[index19];
          }
          numArray5[syndLen] = Pdf417RsDecoder.A0;
          for (int index21 = index3 - 1; index21 >= 0; --index21)
          {
            int index22 = 0;
            for (int index23 = num12; index23 >= 0; --index23)
            {
              if (numArray5[index23] != Pdf417RsDecoder.A0)
                index22 = (index22 + this.AlphaTo[this.Modbase(numArray5[index23] + (index23 + 1) * numArray6[index21])]) % Pdf417RsDecoder.GPRIME;
            }
            int index24 = 1;
            int index25 = 1;
            for (int index26 = 0; index26 < index3; ++index26)
            {
              if (index26 != index21)
              {
                int index27 = (1 + Pdf417RsDecoder.GPRIME - this.AlphaTo[this.Modbase(Pdf417RsDecoder.GPRIME - 1 - numArray6[index26] + numArray6[index21])]) % Pdf417RsDecoder.GPRIME;
                index25 = this.AlphaTo[this.Modbase(this.IndexOf[index25] + this.IndexOf[index27])];
              }
            }
            if (index25 == 0)
            {
              index3 = -1;
              break;
            }
            int num13 = this.AlphaTo[this.Modbase(this.IndexOf[index22] + this.IndexOf[index24] + Pdf417RsDecoder.GPRIME - 1 - this.IndexOf[index25])] % Pdf417RsDecoder.GPRIME;
            if (index22 != 0 && numArray8[index21] < data_len + 1)
            {
              int index28 = data_len - numArray8[index21];
              if (index28 < data_len + 1)
                data[index28] = (data[index28] + Pdf417RsDecoder.GPRIME - num13) % Pdf417RsDecoder.GPRIME;
            }
          }
        }
      }
      if (eras_pos != null)
      {
        for (int index29 = 0; index29 < index3; ++index29)
        {
          if (eras_pos != null)
            eras_pos[index29] = numArray8[index29];
        }
      }
      return index3;
    }
  }
}
