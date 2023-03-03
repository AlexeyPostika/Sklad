// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Multi.QRCode.Detector.MultiDetector
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using MessagingToolkit.Barcode.Helper;
using MessagingToolkit.Barcode.QRCode.Detector;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode.Multi.QRCode.Detector
{
  public sealed class MultiDetector : MessagingToolkit.Barcode.QRCode.Detector.Detector
  {
    private static readonly DetectorResult[] EMPTY_DETECTOR_RESULTS = new DetectorResult[0];

    public MultiDetector(BitMatrix image)
      : base(image)
    {
    }

    public DetectorResult[] DetectMulti(
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      FinderPatternInfo[] multi = new MultiFinderPatternFinder(this.Image, decodingOptions == null ? (ResultPointCallback) null : (ResultPointCallback) BarcodeHelper.GetDecodeOptionType(decodingOptions, DecodeOptions.NeedResultPointCallback)).FindMulti(decodingOptions);
      if (multi.Length == 0)
        throw NotFoundException.Instance;
      IList<DetectorResult> detectorResultList = (IList<DetectorResult>) new List<DetectorResult>();
      foreach (FinderPatternInfo info in multi)
      {
        try
        {
          detectorResultList.Add(this.ProcessFinderPatternInfo(info));
        }
        catch (BarcodeDecoderException ex)
        {
        }
      }
      if (detectorResultList.Count == 0)
        return MultiDetector.EMPTY_DETECTOR_RESULTS;
      DetectorResult[] detectorResultArray = new DetectorResult[detectorResultList.Count];
      for (int index = 0; index < detectorResultList.Count; ++index)
        detectorResultArray[index] = detectorResultList[index];
      return detectorResultArray;
    }
  }
}
