// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Rss.Expanded.Decoders.AbstractExpandedDecoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System;

namespace MessagingToolkit.Barcode.OneD.Rss.Expanded.Decoders
{
  public abstract class AbstractExpandedDecoder
  {
    internal readonly BitArray information;
    internal readonly GeneralAppIdDecoder generalDecoder;

    internal AbstractExpandedDecoder(BitArray information)
    {
      this.information = information;
      this.generalDecoder = new GeneralAppIdDecoder(information);
    }

    protected BitArray Information => this.information;

    protected GeneralAppIdDecoder GeneralDecoder => this.generalDecoder;

    public abstract string ParseInformation();

    public static AbstractExpandedDecoder CreateDecoder(BitArray information)
    {
      if (information.Get(1))
        return (AbstractExpandedDecoder) new AI01AndOtherAIs(information);
      if (!information.Get(2))
        return (AbstractExpandedDecoder) new AnyAIDecoder(information);
      switch (GeneralAppIdDecoder.ExtractNumericValueFromBitArray(information, 1, 4))
      {
        case 4:
          return (AbstractExpandedDecoder) new AI013103decoder(information);
        case 5:
          return (AbstractExpandedDecoder) new AI01320xDecoder(information);
        default:
          switch (GeneralAppIdDecoder.ExtractNumericValueFromBitArray(information, 1, 5))
          {
            case 12:
              return (AbstractExpandedDecoder) new AI01392xDecoder(information);
            case 13:
              return (AbstractExpandedDecoder) new AI01393xDecoder(information);
            default:
              switch (GeneralAppIdDecoder.ExtractNumericValueFromBitArray(information, 1, 7))
              {
                case 56:
                  return (AbstractExpandedDecoder) new AI013x0x1xDecoder(information, "310", "11");
                case 57:
                  return (AbstractExpandedDecoder) new AI013x0x1xDecoder(information, "320", "11");
                case 58:
                  return (AbstractExpandedDecoder) new AI013x0x1xDecoder(information, "310", "13");
                case 59:
                  return (AbstractExpandedDecoder) new AI013x0x1xDecoder(information, "320", "13");
                case 60:
                  return (AbstractExpandedDecoder) new AI013x0x1xDecoder(information, "310", "15");
                case 61:
                  return (AbstractExpandedDecoder) new AI013x0x1xDecoder(information, "320", "15");
                case 62:
                  return (AbstractExpandedDecoder) new AI013x0x1xDecoder(information, "310", "17");
                case 63:
                  return (AbstractExpandedDecoder) new AI013x0x1xDecoder(information, "320", "17");
                default:
                  throw new InvalidOperationException("unknown decoder: " + (object) information);
              }
          }
      }
    }
  }
}
