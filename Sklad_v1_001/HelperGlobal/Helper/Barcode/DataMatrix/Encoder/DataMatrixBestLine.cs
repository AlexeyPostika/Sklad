// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.Encoder.DataMatrixBestLine
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.DataMatrix.Encoder
{
  internal struct DataMatrixBestLine
  {
    internal int Angle { get; set; }

    internal int HOffset { get; set; }

    internal int Mag { get; set; }

    internal int StepBeg { get; set; }

    internal int StepPos { get; set; }

    internal int StepNeg { get; set; }

    internal int DistSq { get; set; }

    internal double Devn { get; set; }

    internal DataMatrixPixelLoc LocBeg { get; set; }

    internal DataMatrixPixelLoc LocPos { get; set; }

    internal DataMatrixPixelLoc LocNeg { get; set; }
  }
}
