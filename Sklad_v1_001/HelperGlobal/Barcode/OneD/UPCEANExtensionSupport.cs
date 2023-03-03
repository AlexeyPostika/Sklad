// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.UPCEANExtensionSupport
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;

namespace MessagingToolkit.Barcode.OneD
{
  internal sealed class UPCEANExtensionSupport
  {
    private static readonly int[] EXTENSION_START_PATTERN = new int[3]
    {
      1,
      1,
      2
    };
    private readonly UPCEANExtension2Support twoSupport;
    private readonly UPCEANExtension5Support fiveSupport;

    public UPCEANExtensionSupport()
    {
      this.twoSupport = new UPCEANExtension2Support();
      this.fiveSupport = new UPCEANExtension5Support();
    }

    internal Result DecodeRow(int rowNumber, BitArray row, int rowOffset)
    {
      int[] guardPattern = UPCEANDecoder.FindGuardPattern(row, rowOffset, false, UPCEANExtensionSupport.EXTENSION_START_PATTERN);
      try
      {
        return this.fiveSupport.DecodeRow(rowNumber, row, guardPattern);
      }
      catch (BarcodeDecoderException ex)
      {
        return this.twoSupport.DecodeRow(rowNumber, row, guardPattern);
      }
    }
  }
}
