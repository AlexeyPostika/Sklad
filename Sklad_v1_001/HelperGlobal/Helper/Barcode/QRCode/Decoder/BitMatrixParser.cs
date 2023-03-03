// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.QRCode.Decoder.BitMatrixParser
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;

namespace MessagingToolkit.Barcode.QRCode.Decoder
{
  internal sealed class BitMatrixParser
  {
    private readonly BitMatrix bitMatrix;
    private Version parsedVersion;
    private FormatInformation parsedFormatInfo;

    internal BitMatrixParser(BitMatrix bitMatrix)
    {
      int height = bitMatrix.Height;
      if (height < 21 || (height & 3) != 1)
        throw FormatException.Instance;
      this.bitMatrix = bitMatrix;
    }

    internal FormatInformation ReadFormatInformation()
    {
      if (this.parsedFormatInfo != null)
        return this.parsedFormatInfo;
      int versionBits = 0;
      for (int i = 0; i < 6; ++i)
        versionBits = this.CopyBit(i, 8, versionBits);
      int num1 = this.CopyBit(8, 7, this.CopyBit(8, 8, this.CopyBit(7, 8, versionBits)));
      for (int j = 5; j >= 0; --j)
        num1 = this.CopyBit(8, j, num1);
      int height = this.bitMatrix.Height;
      int num2 = 0;
      int num3 = height - 7;
      for (int j = height - 1; j >= num3; --j)
        num2 = this.CopyBit(8, j, num2);
      for (int i = height - 8; i < height; ++i)
        num2 = this.CopyBit(i, 8, num2);
      this.parsedFormatInfo = FormatInformation.DecodeFormatInformation(num1, num2);
      return this.parsedFormatInfo != null ? this.parsedFormatInfo : throw FormatException.Instance;
    }

    internal Version ReadVersion()
    {
      if (this.parsedVersion != null)
        return this.parsedVersion;
      int height = this.bitMatrix.Height;
      int versionNumber = height - 17 >> 2;
      if (versionNumber <= 6)
        return Version.GetVersionForNumber(versionNumber);
      int versionBits1 = 0;
      int num = height - 11;
      for (int j = 5; j >= 0; --j)
      {
        for (int i = height - 9; i >= num; --i)
          versionBits1 = this.CopyBit(i, j, versionBits1);
      }
      Version version1 = Version.DecodeVersionInformation(versionBits1);
      if (version1 != null && version1.DimensionForVersion == height)
      {
        this.parsedVersion = version1;
        return version1;
      }
      int versionBits2 = 0;
      for (int i = 5; i >= 0; --i)
      {
        for (int j = height - 9; j >= num; --j)
          versionBits2 = this.CopyBit(i, j, versionBits2);
      }
      Version version2 = Version.DecodeVersionInformation(versionBits2);
      if (version2 == null || version2.DimensionForVersion != height)
        throw FormatException.Instance;
      this.parsedVersion = version2;
      return version2;
    }

    private int CopyBit(int i, int j, int versionBits) => !this.bitMatrix.Get(i, j) ? versionBits << 1 : versionBits << 1 | 1;

    internal byte[] ReadCodewords()
    {
      FormatInformation formatInformation = this.ReadFormatInformation();
      Version version = this.ReadVersion();
      DataMask dataMask = DataMask.ForReference((int) formatInformation.GetDataMask());
      int height = this.bitMatrix.Height;
      dataMask.UnmaskBitMatrix(this.bitMatrix, height);
      BitMatrix bitMatrix = version.BuildFunctionPattern();
      bool flag = true;
      byte[] numArray = new byte[version.TotalCodewords];
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      for (int index1 = height - 1; index1 > 0; index1 -= 2)
      {
        if (index1 == 6)
          --index1;
        for (int index2 = 0; index2 < height; ++index2)
        {
          int y = flag ? height - 1 - index2 : index2;
          for (int index3 = 0; index3 < 2; ++index3)
          {
            if (!bitMatrix.Get(index1 - index3, y))
            {
              ++num3;
              num2 <<= 1;
              if (this.bitMatrix.Get(index1 - index3, y))
                num2 |= 1;
              if (num3 == 8)
              {
                numArray[num1++] = (byte) num2;
                num3 = 0;
                num2 = 0;
              }
            }
          }
        }
        flag = !flag;
      }
      if (num1 != version.TotalCodewords)
        throw FormatException.Instance;
      return numArray;
    }
  }
}
