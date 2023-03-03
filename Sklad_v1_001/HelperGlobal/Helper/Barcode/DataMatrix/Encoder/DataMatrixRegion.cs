// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.Encoder.DataMatrixRegion
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.DataMatrix.Encoder
{
  internal class DataMatrixRegion
  {
    internal DataMatrixRegion()
    {
    }

    internal DataMatrixRegion(DataMatrixRegion src)
    {
      this.BottomAngle = src.BottomAngle;
      this.BottomKnown = src.BottomKnown;
      this.BottomLine = src.BottomLine;
      this.BottomLoc = src.BottomLoc;
      this.BoundMax = src.BoundMax;
      this.BoundMin = src.BoundMin;
      this.FinalNeg = src.FinalNeg;
      this.FinalPos = src.FinalPos;
      this.Fit2Raw = new DataMatrixMatrix3(src.Fit2Raw);
      this.FlowBegin = src.FlowBegin;
      this.JumpToNeg = src.JumpToNeg;
      this.JumpToPos = src.JumpToPos;
      this.LeftAngle = src.LeftAngle;
      this.LeftKnown = src.LeftKnown;
      this.LeftLine = src.LeftLine;
      this.LeftLoc = src.LeftLoc;
      this.LocR = src.LocR;
      this.LocT = src.LocT;
      this.MappingCols = src.MappingCols;
      this.MappingRows = src.MappingRows;
      this.OffColor = src.OffColor;
      this.OnColor = src.OnColor;
      this.Polarity = src.Polarity;
      this.Raw2Fit = new DataMatrixMatrix3(src.Raw2Fit);
      this.RightAngle = src.RightAngle;
      this.RightKnown = src.RightKnown;
      this.RightLoc = src.RightLoc;
      this.SizeIdx = src.SizeIdx;
      this.StepR = src.StepR;
      this.StepsTotal = src.StepsTotal;
      this.StepT = src.StepT;
      this.SymbolCols = src.SymbolCols;
      this.SymbolRows = src.SymbolRows;
      this.TopAngle = src.TopAngle;
      this.TopKnown = src.TopKnown;
      this.TopLoc = src.TopLoc;
    }

    internal int JumpToPos { get; set; }

    internal int JumpToNeg { get; set; }

    internal int StepsTotal { get; set; }

    internal DataMatrixPixelLoc FinalPos { get; set; }

    internal DataMatrixPixelLoc FinalNeg { get; set; }

    internal DataMatrixPixelLoc BoundMin { get; set; }

    internal DataMatrixPixelLoc BoundMax { get; set; }

    internal DataMatrixPointFlow FlowBegin { get; set; }

    internal int Polarity { get; set; }

    internal int StepR { get; set; }

    internal int StepT { get; set; }

    internal DataMatrixPixelLoc LocR { get; set; }

    internal DataMatrixPixelLoc LocT { get; set; }

    internal int LeftKnown { get; set; }

    internal int LeftAngle { get; set; }

    internal DataMatrixPixelLoc LeftLoc { get; set; }

    internal DataMatrixBestLine LeftLine { get; set; }

    internal int BottomKnown { get; set; }

    internal int BottomAngle { get; set; }

    internal DataMatrixPixelLoc BottomLoc { get; set; }

    internal DataMatrixBestLine BottomLine { get; set; }

    internal int TopKnown { get; set; }

    internal int TopAngle { get; set; }

    internal DataMatrixPixelLoc TopLoc { get; set; }

    internal int RightKnown { get; set; }

    internal int RightAngle { get; set; }

    internal DataMatrixPixelLoc RightLoc { get; set; }

    internal int OnColor { get; set; }

    internal int OffColor { get; set; }

    internal DataMatrixSymbolSize SizeIdx { get; set; }

    internal int SymbolRows { get; set; }

    internal int SymbolCols { get; set; }

    internal int MappingRows { get; set; }

    internal int MappingCols { get; set; }

    internal DataMatrixMatrix3 Raw2Fit { get; set; }

    internal DataMatrixMatrix3 Fit2Raw { get; set; }
  }
}
