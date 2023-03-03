// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.Encoder.DataMatrixCommon
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;

namespace MessagingToolkit.Barcode.DataMatrix.Encoder
{
  internal static class DataMatrixCommon
  {
    internal static void GenReedSolEcc(DataMatrixMessage message, DataMatrixSymbolSize sizeIdx)
    {
      byte[] numArray1 = new byte[69];
      byte[] numArray2 = new byte[68];
      int symbolAttribute1 = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribSymbolDataWords, sizeIdx);
      int symbolAttribute2 = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribSymbolErrorWords, sizeIdx);
      int num1 = symbolAttribute1 + symbolAttribute2;
      int symbolAttribute3 = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribBlockErrorWords, sizeIdx);
      int symbolAttribute4 = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribInterleavedBlocks, sizeIdx);
      if (symbolAttribute3 != symbolAttribute2 / symbolAttribute4)
        throw new Exception("Error generation reed solomon error correction");
      for (int index = 0; index < numArray1.Length; ++index)
        numArray1[index] = (byte) 1;
      for (int b = 1; b <= symbolAttribute3; ++b)
      {
        for (int index = b - 1; index >= 0; --index)
        {
          numArray1[index] = DataMatrixCommon.GfDoublify(numArray1[index], b);
          if (index > 0)
            numArray1[index] = DataMatrixCommon.GfSum(numArray1[index], numArray1[index - 1]);
        }
      }
      for (int blockIdx = 0; blockIdx < symbolAttribute4; ++blockIdx)
      {
        for (int index = 0; index < numArray2.Length; ++index)
          numArray2[index] = (byte) 0;
        for (int index1 = blockIdx; index1 < symbolAttribute1; index1 += symbolAttribute4)
        {
          int b = (int) DataMatrixCommon.GfSum(numArray2[symbolAttribute3 - 1], message.Code[index1]);
          for (int index2 = symbolAttribute3 - 1; index2 > 0; --index2)
            numArray2[index2] = DataMatrixCommon.GfSum(numArray2[index2 - 1], DataMatrixCommon.GfProduct(numArray1[index2], b));
          numArray2[0] = DataMatrixCommon.GfProduct(numArray1[0], b);
        }
        int blockDataSize = DataMatrixCommon.GetBlockDataSize(sizeIdx, blockIdx);
        int num2 = symbolAttribute3;
        for (int index = blockIdx + symbolAttribute4 * blockDataSize; index < num1; index += symbolAttribute4)
          message.Code[index] = numArray2[--num2];
        if (num2 != 0)
          throw new Exception("Error generation error correction code!");
      }
    }

    private static byte GfProduct(byte a, int b) => a == (byte) 0 || b == 0 ? (byte) 0 : (byte) DataMatrixConstants.aLogVal[(DataMatrixConstants.logVal[(int) a] + DataMatrixConstants.logVal[b]) % (int) byte.MaxValue];

    private static byte GfSum(byte a, byte b) => (byte) ((uint) a ^ (uint) b);

    private static byte GfDoublify(byte a, int b)
    {
      if (a == (byte) 0)
        return 0;
      return b == 0 ? a : (byte) DataMatrixConstants.aLogVal[(DataMatrixConstants.logVal[(int) a] + b) % (int) byte.MaxValue];
    }

    internal static int GetSymbolAttribute(
      DataMatrixSymAttribute attribute,
      DataMatrixSymbolSize sizeIdx)
    {
      if (sizeIdx < DataMatrixSymbolSize.Symbol10x10 || sizeIdx >= (DataMatrixSymbolSize) (DataMatrixConstants.DataMatrixSymbolSquareCount + DataMatrixConstants.DataMatrixSymbolRectCount))
        return DataMatrixConstants.DataMatrixUndefined;
      switch (attribute)
      {
        case DataMatrixSymAttribute.SymAttribSymbolRows:
          return DataMatrixConstants.SymbolRows[(int) sizeIdx];
        case DataMatrixSymAttribute.SymAttribSymbolCols:
          return DataMatrixConstants.SymbolCols[(int) sizeIdx];
        case DataMatrixSymAttribute.SymAttribDataRegionRows:
          return DataMatrixConstants.DataRegionRows[(int) sizeIdx];
        case DataMatrixSymAttribute.SymAttribDataRegionCols:
          return DataMatrixConstants.DataRegionCols[(int) sizeIdx];
        case DataMatrixSymAttribute.SymAttribHorizDataRegions:
          return DataMatrixConstants.HorizDataRegions[(int) sizeIdx];
        case DataMatrixSymAttribute.SymAttribVertDataRegions:
          return sizeIdx >= (DataMatrixSymbolSize) DataMatrixConstants.DataMatrixSymbolSquareCount ? 1 : DataMatrixConstants.HorizDataRegions[(int) sizeIdx];
        case DataMatrixSymAttribute.SymAttribMappingMatrixRows:
          return DataMatrixConstants.DataRegionRows[(int) sizeIdx] * DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribVertDataRegions, sizeIdx);
        case DataMatrixSymAttribute.SymAttribMappingMatrixCols:
          return DataMatrixConstants.DataRegionCols[(int) sizeIdx] * DataMatrixConstants.HorizDataRegions[(int) sizeIdx];
        case DataMatrixSymAttribute.SymAttribInterleavedBlocks:
          return DataMatrixConstants.InterleavedBlocks[(int) sizeIdx];
        case DataMatrixSymAttribute.SymAttribBlockErrorWords:
          return DataMatrixConstants.BlockErrorWords[(int) sizeIdx];
        case DataMatrixSymAttribute.SymAttribBlockMaxCorrectable:
          return DataMatrixConstants.BlockMaxCorrectable[(int) sizeIdx];
        case DataMatrixSymAttribute.SymAttribSymbolDataWords:
          return DataMatrixConstants.SymbolDataWords[(int) sizeIdx];
        case DataMatrixSymAttribute.SymAttribSymbolErrorWords:
          return DataMatrixConstants.BlockErrorWords[(int) sizeIdx] * DataMatrixConstants.InterleavedBlocks[(int) sizeIdx];
        case DataMatrixSymAttribute.SymAttribSymbolMaxCorrectable:
          return DataMatrixConstants.BlockMaxCorrectable[(int) sizeIdx] * DataMatrixConstants.InterleavedBlocks[(int) sizeIdx];
        default:
          return DataMatrixConstants.DataMatrixUndefined;
      }
    }

    internal static int GetBlockDataSize(DataMatrixSymbolSize sizeIdx, int blockIdx)
    {
      int symbolAttribute1 = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribSymbolDataWords, sizeIdx);
      int symbolAttribute2 = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribInterleavedBlocks, sizeIdx);
      int num = symbolAttribute1 / symbolAttribute2;
      if (symbolAttribute1 < 1 || symbolAttribute2 < 1)
        return DataMatrixConstants.DataMatrixUndefined;
      return sizeIdx != DataMatrixSymbolSize.Symbol144x144 || blockIdx >= 8 ? num : num + 1;
    }

    internal static DataMatrixSymbolSize FindCorrectSymbolSize(
      int dataWords,
      DataMatrixSymbolSize sizeIdxRequest)
    {
      if (dataWords <= 0)
        return DataMatrixSymbolSize.SymbolShapeAuto;
      DataMatrixSymbolSize sizeIdx;
      if (sizeIdxRequest == DataMatrixSymbolSize.SymbolSquareAuto || sizeIdxRequest == DataMatrixSymbolSize.SymbolRectAuto)
      {
        DataMatrixSymbolSize matrixSymbolSize1;
        DataMatrixSymbolSize matrixSymbolSize2;
        if (sizeIdxRequest == DataMatrixSymbolSize.SymbolSquareAuto)
        {
          matrixSymbolSize1 = DataMatrixSymbolSize.Symbol10x10;
          matrixSymbolSize2 = (DataMatrixSymbolSize) DataMatrixConstants.DataMatrixSymbolSquareCount;
        }
        else
        {
          matrixSymbolSize1 = (DataMatrixSymbolSize) DataMatrixConstants.DataMatrixSymbolSquareCount;
          matrixSymbolSize2 = (DataMatrixSymbolSize) (DataMatrixConstants.DataMatrixSymbolSquareCount + DataMatrixConstants.DataMatrixSymbolRectCount);
        }
        sizeIdx = matrixSymbolSize1;
        while (sizeIdx < matrixSymbolSize2 && DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribSymbolDataWords, sizeIdx) < dataWords)
          ++sizeIdx;
        if (sizeIdx == matrixSymbolSize2)
          return DataMatrixSymbolSize.SymbolShapeAuto;
      }
      else
        sizeIdx = sizeIdxRequest;
      return dataWords > DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribSymbolDataWords, sizeIdx) ? DataMatrixSymbolSize.SymbolShapeAuto : sizeIdx;
    }

    internal static int GetBitsPerPixel(DataMatrixPackOrder pack)
    {
      switch (pack)
      {
        case DataMatrixPackOrder.Pack1bppK:
          return 1;
        case DataMatrixPackOrder.Pack8bppK:
          return 8;
        case DataMatrixPackOrder.Pack16bppRGB:
        case DataMatrixPackOrder.Pack16bppRGBX:
        case DataMatrixPackOrder.Pack16bppXRGB:
        case DataMatrixPackOrder.Pack16bppBGR:
        case DataMatrixPackOrder.Pack16bppBGRX:
        case DataMatrixPackOrder.Pack16bppXBGR:
        case DataMatrixPackOrder.Pack16bppYCbCr:
          return 16;
        case DataMatrixPackOrder.Pack24bppRGB:
        case DataMatrixPackOrder.Pack24bppBGR:
        case DataMatrixPackOrder.Pack24bppYCbCr:
          return 24;
        case DataMatrixPackOrder.Pack32bppRGBX:
        case DataMatrixPackOrder.Pack32bppXRGB:
        case DataMatrixPackOrder.Pack32bppBGRX:
        case DataMatrixPackOrder.Pack32bppXBGR:
        case DataMatrixPackOrder.Pack32bppCMYK:
          return 32;
        default:
          return DataMatrixConstants.DataMatrixUndefined;
      }
    }

    internal static T Min<T>(T x, T y) where T : IComparable<T> => x.CompareTo(y) >= 0 ? y : x;

    internal static T Max<T>(T x, T y) where T : IComparable<T> => x.CompareTo(y) >= 0 ? x : y;

    internal static bool DecodeCheckErrors(
      byte[] code,
      int codeIndex,
      DataMatrixSymbolSize sizeIdx,
      int fix)
    {
      byte[] numArray = new byte[(int) byte.MaxValue];
      int symbolAttribute1 = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribInterleavedBlocks, sizeIdx);
      int symbolAttribute2 = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribBlockErrorWords, sizeIdx);
      int num1 = 0;
      for (int blockIdx = 0; blockIdx < symbolAttribute1; ++blockIdx)
      {
        int num2 = symbolAttribute2 + DataMatrixCommon.GetBlockDataSize(sizeIdx, blockIdx);
        for (int index = 0; index < num2; ++index)
          numArray[index] = code[index * symbolAttribute1 + blockIdx];
        num1 = num1;
        for (int index = 0; index < num2; ++index)
          code[index * symbolAttribute1 + blockIdx] = numArray[index];
      }
      return fix == DataMatrixConstants.DataMatrixUndefined || fix < 0 || fix >= num1;
    }

    internal static double RightAngleTrueness(
      DataMatrixVector2 c0,
      DataMatrixVector2 c1,
      DataMatrixVector2 c2,
      double angle)
    {
      DataMatrixVector2 dataMatrixVector2_1 = c0 - c1;
      DataMatrixVector2 dataMatrixVector2_2 = c2 - c1;
      dataMatrixVector2_1.Norm();
      dataMatrixVector2_2.Norm();
      DataMatrixMatrix3 dataMatrixMatrix3 = DataMatrixMatrix3.Rotate(angle);
      DataMatrixVector2 v2 = dataMatrixVector2_2 * dataMatrixMatrix3;
      return dataMatrixVector2_1.Dot(v2);
    }
  }
}
