// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.Encoder.DataMatrixEncode
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace MessagingToolkit.Barcode.DataMatrix.Encoder
{
  internal class DataMatrixEncode
  {
    private int _method;
    private DataMatrixScheme _scheme;
    private DataMatrixSymbolSize _sizeIdxRequest;
    private int _marginSize;
    private int _moduleSize;
    private DataMatrixPackOrder _pixelPacking;
    private DataMatrixFlip _imageFlip;
    private int _rowPadBytes;
    private DataMatrixMessage _message;
    private DataMatrixImage _image;
    private DataMatrixRegion _region;
    private bool[,] _rawData;
    private int _width;
    private int _height;
    private int _quietZone;

    internal DataMatrixEncode()
    {
      this._scheme = DataMatrixScheme.SchemeAscii;
      this._sizeIdxRequest = DataMatrixSymbolSize.SymbolSquareAuto;
      this._marginSize = 10;
      this._moduleSize = 5;
      this._width = 250;
      this._height = 250;
      this._quietZone = 1;
      this._pixelPacking = DataMatrixPackOrder.Pack24bppRGB;
      this._imageFlip = DataMatrixFlip.FlipNone;
      this._rowPadBytes = 0;
    }

    private DataMatrixEncode(DataMatrixEncode src)
    {
      this._scheme = src._scheme;
      this._sizeIdxRequest = src._sizeIdxRequest;
      this._marginSize = src._marginSize;
      this._moduleSize = src._moduleSize;
      this._pixelPacking = src._pixelPacking;
      this._imageFlip = src._imageFlip;
      this._rowPadBytes = src._rowPadBytes;
      this._image = src._image;
      this._message = src._message;
      this._method = src._method;
      this._region = src._region;
    }

    internal bool EncodeDataMatrixRaw(byte[] inputString) => this.EncodeDataMatrix(new Color?(), new Color?(), inputString, true);

    internal bool EncodeDataMatrix(Color? foreColor, Color? backColor, byte[] inputString) => this.EncodeDataMatrix(foreColor, backColor, inputString, false);

    internal bool EncodeDataMatrix(
      Color? foreColor,
      Color? backColor,
      byte[] inputString,
      bool encodeRaw)
    {
      byte[] buf = new byte[4096];
      DataMatrixSymbolSize sizeIdxRequest = this._sizeIdxRequest;
      int dataWordCount = this.EncodeDataCodewords(buf, inputString, ref sizeIdxRequest);
      if (dataWordCount <= 0)
        return false;
      if (sizeIdxRequest == DataMatrixSymbolSize.SymbolSquareAuto || sizeIdxRequest == DataMatrixSymbolSize.SymbolRectAuto)
        throw new Exception("Invalid symbol size for encoding!");
      int num = this.AddPadChars(buf, ref dataWordCount, DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribSymbolDataWords, sizeIdxRequest));
      this._region = new DataMatrixRegion();
      this._region.SizeIdx = sizeIdxRequest;
      this._region.SymbolRows = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribSymbolRows, sizeIdxRequest);
      this._region.SymbolCols = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribSymbolCols, sizeIdxRequest);
      this._region.MappingRows = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribMappingMatrixRows, sizeIdxRequest);
      this._region.MappingCols = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribMappingMatrixCols, sizeIdxRequest);
      this._message = new DataMatrixMessage(sizeIdxRequest, DataMatrixFormat.Matrix)
      {
        PadCount = num
      };
      for (int index = 0; index < dataWordCount; ++index)
        this._message.Code[index] = buf[index];
      DataMatrixCommon.GenReedSolEcc(this._message, this._region.SizeIdx);
      DataMatrixDecode.ModulePlacementEcc200(this._message.Array, this._message.Code, this._region.SizeIdx, DataMatrixConstants.DataMatrixModuleOnRGB);
      int width = 2 * this._marginSize + this._region.SymbolCols * this._moduleSize;
      int height = 2 * this._marginSize + this._region.SymbolRows * this._moduleSize;
      int bitsPerPixel = DataMatrixCommon.GetBitsPerPixel(this._pixelPacking);
      if (bitsPerPixel == DataMatrixConstants.DataMatrixUndefined)
        return false;
      if (bitsPerPixel % 8 != 0)
        throw new Exception("Invalid color depth for encoding!");
      this._image = new DataMatrixImage(new byte[width * height * (bitsPerPixel / 8) + this._rowPadBytes], width, height, this._pixelPacking)
      {
        ImageFlip = this._imageFlip,
        RowPadBytes = this._rowPadBytes
      };
      if (encodeRaw)
        this.PrintPatternRaw();
      else
        this.PrintPattern(foreColor, backColor);
      return true;
    }

    internal bool EncodeDataMosaic(byte[] inputString)
    {
      int[] numArray = new int[3];
      List<byte[]> numArrayList = new List<byte[]>(3);
      for (int index = 0; index < 3; ++index)
        numArrayList.Add(new byte[4096]);
      DataMatrixSymbolSize sizeIdxRequest;
      DataMatrixSymbolSize sizeIdx1 = sizeIdxRequest = this._sizeIdxRequest;
      if (this.EncodeDataCodewords(numArrayList[0], inputString, ref sizeIdx1) <= 0)
        return false;
      int dataWords = (inputString.Length + 2) / 3;
      numArray[0] = dataWords;
      numArray[1] = dataWords;
      numArray[2] = inputString.Length - (numArray[0] + numArray[1]);
      DataMatrixSymbolSize correctSymbolSize = this.FindCorrectSymbolSize(dataWords, sizeIdxRequest);
      if (correctSymbolSize == DataMatrixSymbolSize.SymbolShapeAuto)
        return false;
      DataMatrixSymbolSize matrixSymbolSize;
      switch (sizeIdxRequest)
      {
        case DataMatrixSymbolSize.SymbolRectAuto:
          matrixSymbolSize = DataMatrixSymbolSize.Symbol16x48;
          break;
        case DataMatrixSymbolSize.SymbolSquareAuto:
          matrixSymbolSize = DataMatrixSymbolSize.Symbol144x144;
          break;
        default:
          matrixSymbolSize = correctSymbolSize;
          break;
      }
      byte[] inputString1 = new byte[numArray[0]];
      for (int index = 0; index < numArray[0]; ++index)
        inputString1[index] = inputString[index];
      byte[] inputString2 = new byte[numArray[1]];
      for (int index = numArray[0]; index < numArray[0] + numArray[1]; ++index)
        inputString2[index - numArray[0]] = inputString[index];
      byte[] inputString3 = new byte[numArray[2]];
      for (int index = numArray[0] + numArray[1]; index < inputString.Length; ++index)
        inputString3[index - numArray[0] - numArray[1]] = inputString[index];
      DataMatrixSymbolSize sizeIdx2;
      for (sizeIdx2 = correctSymbolSize; sizeIdx2 <= matrixSymbolSize; ++sizeIdx2)
      {
        DataMatrixSymbolSize sizeIdx3 = sizeIdx2;
        this.EncodeDataCodewords(numArrayList[0], inputString1, ref sizeIdx3);
        if (sizeIdx3 == sizeIdx2)
        {
          sizeIdx3 = sizeIdx2;
          this.EncodeDataCodewords(numArrayList[1], inputString2, ref sizeIdx3);
          if (sizeIdx3 == sizeIdx2)
          {
            sizeIdx3 = sizeIdx2;
            this.EncodeDataCodewords(numArrayList[2], inputString3, ref sizeIdx3);
            if (sizeIdx3 == sizeIdx2)
              break;
          }
        }
      }
      this._sizeIdxRequest = sizeIdx2;
      DataMatrixEncode dataMatrixEncode1 = new DataMatrixEncode(this);
      DataMatrixEncode dataMatrixEncode2 = new DataMatrixEncode(this);
      this.EncodeDataMatrix(new Color?(), new Color?(), inputString1);
      dataMatrixEncode1.EncodeDataMatrix(new Color?(), new Color?(), inputString2);
      dataMatrixEncode2.EncodeDataMatrix(new Color?(), new Color?(), inputString3);
      int symbolAttribute1 = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribMappingMatrixRows, sizeIdx2);
      int symbolAttribute2 = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribMappingMatrixCols, sizeIdx2);
      for (int index = 0; index < this._region.MappingCols * this._region.MappingRows; ++index)
        this._message.Array[index] = (byte) 0;
      DataMatrixDecode.ModulePlacementEcc200(this._message.Array, this._message.Code, this._region.SizeIdx, DataMatrixConstants.DataMatrixModuleOnRed);
      for (int index1 = 0; index1 < symbolAttribute1; ++index1)
      {
        for (int index2 = 0; index2 < symbolAttribute2; ++index2)
          this._message.Array[index1 * symbolAttribute2 + index2] &= (byte) ((int) byte.MaxValue ^ (DataMatrixConstants.DataMatrixModuleAssigned | DataMatrixConstants.DataMatrixModuleVisited));
      }
      DataMatrixDecode.ModulePlacementEcc200(this._message.Array, dataMatrixEncode1.Message.Code, this._region.SizeIdx, DataMatrixConstants.DataMatrixModuleOnGreen);
      for (int index3 = 0; index3 < symbolAttribute1; ++index3)
      {
        for (int index4 = 0; index4 < symbolAttribute2; ++index4)
          this._message.Array[index3 * symbolAttribute2 + index4] &= (byte) ((int) byte.MaxValue ^ (DataMatrixConstants.DataMatrixModuleAssigned | DataMatrixConstants.DataMatrixModuleVisited));
      }
      DataMatrixDecode.ModulePlacementEcc200(this._message.Array, dataMatrixEncode2.Message.Code, this._region.SizeIdx, DataMatrixConstants.DataMatrixModuleOnBlue);
      this.PrintPattern(new Color?(), new Color?());
      return true;
    }

    private void PrintPatternRaw()
    {
      this._rawData = new bool[this._region.SymbolCols, this._region.SymbolRows];
      for (int symbolRow = 0; symbolRow < this._region.SymbolRows; ++symbolRow)
      {
        for (int symbolCol = 0; symbolCol < this._region.SymbolCols; ++symbolCol)
        {
          int num = this._message.SymbolModuleStatus(this._region.SizeIdx, symbolRow, symbolCol);
          this._rawData[symbolCol, this._region.SymbolRows - symbolRow - 1] = (num & DataMatrixConstants.DataMatrixModuleOnBlue) != 0;
        }
      }
    }

    private void PrintPattern(Color? foreColor, Color? backColor)
    {
      int[] numArray = new int[3];
      double marginSize = (double) this._marginSize;
      DataMatrixMatrix3 dataMatrixMatrix3_1 = DataMatrixMatrix3.Translate(marginSize, marginSize);
      DataMatrixMatrix3 dataMatrixMatrix3_2 = DataMatrixMatrix3.Scale((double) this._moduleSize, (double) this._moduleSize) * dataMatrixMatrix3_1;
      int rowSizeBytes = this._image.RowSizeBytes;
      int height = this._image.Height;
      for (int index = 0; index < rowSizeBytes * height; ++index)
        this._image.Pxl[index] = byte.MaxValue;
      for (int index1 = 0; index1 < this._region.SymbolRows; ++index1)
      {
        for (int index2 = 0; index2 < this._region.SymbolCols; ++index2)
        {
          DataMatrixVector2 dataMatrixVector2 = new DataMatrixVector2((double) index2, (double) index1) * dataMatrixMatrix3_2;
          int x1 = (int) dataMatrixVector2.X;
          int y1 = (int) dataMatrixVector2.Y;
          int num = this._message.SymbolModuleStatus(this._region.SizeIdx, index1, index2);
          for (int y2 = y1; y2 < y1 + this._moduleSize; ++y2)
          {
            for (int x2 = x1; x2 < x1 + this._moduleSize; ++x2)
            {
              if (foreColor.HasValue && backColor.HasValue)
              {
                numArray[0] = (num & DataMatrixConstants.DataMatrixModuleOnRed) != 0 ? (int) foreColor.Value.B : (int) backColor.Value.B;
                numArray[1] = (num & DataMatrixConstants.DataMatrixModuleOnGreen) != 0 ? (int) foreColor.Value.G : (int) backColor.Value.G;
                numArray[2] = (num & DataMatrixConstants.DataMatrixModuleOnBlue) != 0 ? (int) foreColor.Value.R : (int) backColor.Value.R;
              }
              else
              {
                numArray[0] = (num & DataMatrixConstants.DataMatrixModuleOnBlue) != 0 ? 0 : (int) byte.MaxValue;
                numArray[1] = (num & DataMatrixConstants.DataMatrixModuleOnGreen) != 0 ? 0 : (int) byte.MaxValue;
                numArray[2] = (num & DataMatrixConstants.DataMatrixModuleOnRed) != 0 ? 0 : (int) byte.MaxValue;
              }
              this._image.SetPixelValue(x2, y2, 0, (byte) numArray[0]);
              this._image.SetPixelValue(x2, y2, 1, (byte) numArray[1]);
              this._image.SetPixelValue(x2, y2, 2, (byte) numArray[2]);
            }
          }
        }
      }
    }

    private int AddPadChars(byte[] buf, ref int dataWordCount, int paddedSize)
    {
      int num = 0;
      if (dataWordCount < paddedSize)
      {
        ++num;
        buf[dataWordCount++] = DataMatrixConstants.DataMatrixCharAsciiPad;
      }
      while (dataWordCount < paddedSize)
      {
        ++num;
        buf[dataWordCount] = this.Randomize253State(DataMatrixConstants.DataMatrixCharAsciiPad, dataWordCount + 1);
        ++dataWordCount;
      }
      return num;
    }

    private byte Randomize253State(byte codewordValue, int codewordPosition)
    {
      int num1 = 149 * codewordPosition % 253 + 1;
      int num2 = (int) codewordValue + num1;
      if (num2 > 254)
        num2 -= 254;
      return num2 >= 0 && num2 <= (int) byte.MaxValue ? (byte) num2 : throw new Exception("Error randomizing 253 state!");
    }

    private int EncodeDataCodewords(
      byte[] buf,
      byte[] inputString,
      ref DataMatrixSymbolSize sizeIdx)
    {
      int dataWords;
      switch (this._scheme)
      {
        case DataMatrixScheme.SchemeAutoFast:
          dataWords = 0;
          break;
        case DataMatrixScheme.SchemeAutoBest:
          dataWords = this.EncodeAutoBest(buf, inputString);
          break;
        default:
          dataWords = this.EncodeSingleScheme(buf, inputString, this._scheme);
          break;
      }
      sizeIdx = this.FindCorrectSymbolSize(dataWords, sizeIdx);
      return sizeIdx == DataMatrixSymbolSize.SymbolShapeAuto ? 0 : dataWords;
    }

    private DataMatrixSymbolSize FindCorrectSymbolSize(
      int dataWords,
      DataMatrixSymbolSize sizeIdxRequest)
    {
      if (dataWords <= 0)
        return DataMatrixSymbolSize.SymbolShapeAuto;
      DataMatrixSymbolSize sizeIdx;
      if (sizeIdxRequest == DataMatrixSymbolSize.SymbolSquareAuto || sizeIdxRequest == DataMatrixSymbolSize.SymbolRectAuto)
      {
        int num1;
        int num2;
        if (sizeIdxRequest == DataMatrixSymbolSize.SymbolSquareAuto)
        {
          num1 = 0;
          num2 = DataMatrixConstants.DataMatrixSymbolSquareCount;
        }
        else
        {
          num1 = DataMatrixConstants.DataMatrixSymbolSquareCount;
          num2 = DataMatrixConstants.DataMatrixSymbolSquareCount + DataMatrixConstants.DataMatrixSymbolRectCount;
        }
        sizeIdx = (DataMatrixSymbolSize) num1;
        while (sizeIdx < (DataMatrixSymbolSize) num2 && DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribSymbolDataWords, sizeIdx) < dataWords)
          ++sizeIdx;
        if (sizeIdx == (DataMatrixSymbolSize) num2)
          return DataMatrixSymbolSize.SymbolShapeAuto;
      }
      else
        sizeIdx = sizeIdxRequest;
      return dataWords > DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribSymbolDataWords, sizeIdx) ? DataMatrixSymbolSize.SymbolShapeAuto : sizeIdx;
    }

    private int EncodeSingleScheme(byte[] buf, byte[] codewords, DataMatrixScheme scheme)
    {
      DataMatrixChannel channel = new DataMatrixChannel();
      DataMatrixEncode.InitChannel(channel, codewords);
      while (channel.InputIndex < channel.Input.Length)
      {
        if (!this.EncodeNextWord(channel, scheme) || channel.Invalid != DataMatrixChannelStatus.ChannelValid)
          return 0;
      }
      int num = channel.EncodedLength / 12;
      for (int index = 0; index < num; ++index)
        buf[index] = channel.EncodedWords[index];
      return num;
    }

    private int EncodeAutoBest(byte[] buf, byte[] codewords)
    {
      DmtxChannelGroup group = new DmtxChannelGroup();
      DmtxChannelGroup dmtxChannelGroup = new DmtxChannelGroup();
      for (DataMatrixScheme targetScheme = DataMatrixScheme.SchemeAscii; targetScheme <= DataMatrixScheme.SchemeBase256; ++targetScheme)
      {
        DataMatrixChannel channel = group.Channels[(int) targetScheme];
        DataMatrixEncode.InitChannel(channel, codewords);
        if (this.EncodeNextWord(channel, targetScheme))
          return 0;
      }
      for (; group.Channels[0].InputIndex < group.Channels[0].Input.Length; group = dmtxChannelGroup)
      {
        for (DataMatrixScheme targetScheme = DataMatrixScheme.SchemeAscii; targetScheme <= DataMatrixScheme.SchemeBase256; ++targetScheme)
          dmtxChannelGroup.Channels[(int) targetScheme] = this.FindBestChannel(group, targetScheme);
      }
      DataMatrixChannel channel1 = group.Channels[0];
      for (DataMatrixScheme index = DataMatrixScheme.SchemeC40; index <= DataMatrixScheme.SchemeBase256; ++index)
      {
        if (group.Channels[(int) index].Invalid == DataMatrixChannelStatus.ChannelValid && group.Channels[(int) index].EncodedLength < channel1.EncodedLength)
          channel1 = group.Channels[(int) index];
      }
      int num = channel1.EncodedLength / 12;
      for (int index = 0; index < num; ++index)
        buf[index] = channel1.EncodedWords[index];
      return num;
    }

    private DataMatrixChannel FindBestChannel(
      DmtxChannelGroup group,
      DataMatrixScheme targetScheme)
    {
      DataMatrixChannel bestChannel = (DataMatrixChannel) null;
      for (DataMatrixScheme index = DataMatrixScheme.SchemeAscii; index <= DataMatrixScheme.SchemeBase256; ++index)
      {
        DataMatrixChannel channel = group.Channels[(int) index];
        if (channel.Invalid == DataMatrixChannelStatus.ChannelValid && channel.InputIndex != channel.Input.Length)
        {
          this.EncodeNextWord(channel, targetScheme);
          if ((channel.Invalid & DataMatrixChannelStatus.ChannelUnsupportedChar) != DataMatrixChannelStatus.ChannelValid)
          {
            bestChannel = channel;
            break;
          }
          if ((channel.Invalid & DataMatrixChannelStatus.ChannelCannotUnlatch) == DataMatrixChannelStatus.ChannelValid && (bestChannel == null || channel.CurrentLength < bestChannel.CurrentLength))
            bestChannel = channel;
        }
      }
      return bestChannel;
    }

    private bool EncodeNextWord(DataMatrixChannel channel, DataMatrixScheme targetScheme)
    {
      if (channel.EncScheme != targetScheme)
      {
        this.ChangeEncScheme(channel, targetScheme, DmtxUnlatch.Explicit);
        if (channel.Invalid != DataMatrixChannelStatus.ChannelValid)
          return false;
      }
      if (channel.EncScheme != targetScheme)
        throw new Exception("For encoding, channel scheme must equal target scheme!");
      switch (channel.EncScheme)
      {
        case DataMatrixScheme.SchemeAscii:
          return this.EncodeAsciiCodeword(channel);
        case DataMatrixScheme.SchemeC40:
          return this.EncodeTripletCodeword(channel);
        case DataMatrixScheme.SchemeText:
          return this.EncodeTripletCodeword(channel);
        case DataMatrixScheme.SchemeX12:
          return this.EncodeTripletCodeword(channel);
        case DataMatrixScheme.SchemeEdifact:
          return this.EncodeEdifactCodeword(channel);
        case DataMatrixScheme.SchemeBase256:
          return this.EncodeBase256Codeword(channel);
        default:
          return false;
      }
    }

    private bool EncodeBase256Codeword(DataMatrixChannel channel)
    {
      byte[] numArray = new byte[2];
      if (channel.EncScheme != DataMatrixScheme.SchemeBase256)
        throw new Exception("Invalid encoding scheme selected!");
      int index1 = channel.FirstCodeWord / 12;
      numArray[0] = DataMatrixMessage.UnRandomize255State(channel.EncodedWords[index1], channel.FirstCodeWord / 12 + 1);
      int num1 = (numArray[0] > (byte) 249 ? 250 * ((int) numArray[0] - 249) + (int) DataMatrixMessage.UnRandomize255State(channel.EncodedWords[index1 + 1], channel.FirstCodeWord / 12 + 2) : (int) numArray[0]) + 1;
      int num2;
      if (num1 <= 249)
      {
        num2 = 1;
        numArray[0] = (byte) num1;
        numArray[1] = (byte) 0;
      }
      else
      {
        num2 = 2;
        numArray[0] = (byte) (num1 / 250 + 249);
        numArray[1] = (byte) (num1 % 250);
      }
      if (num1 <= 0 || num1 > 1555)
        throw new Exception("Encoding failed, data length out of range!");
      if (num1 == 250)
      {
        for (int index2 = channel.CurrentLength / 12 - 1; index2 > channel.FirstCodeWord / 12; --index2)
        {
          byte codewordValue = DataMatrixMessage.UnRandomize255State(channel.EncodedWords[index2], index2 + 1);
          channel.EncodedWords[index2 + 1] = this.Randomize255State(codewordValue, index2 + 2);
        }
        this.IncrementProgress(channel, 12);
        channel.EncodedLength += 12;
      }
      for (int index3 = 0; index3 < num2; ++index3)
        channel.EncodedWords[index1 + index3] = this.Randomize255State(numArray[index3], channel.FirstCodeWord / 12 + index3 + 1);
      this.PushInputWord(channel, this.Randomize255State(channel.Input[channel.InputIndex], channel.CurrentLength / 12 + 1));
      this.IncrementProgress(channel, 12);
      ++channel.InputIndex;
      return true;
    }

    private bool EncodeEdifactCodeword(DataMatrixChannel channel)
    {
      if (channel.EncScheme != DataMatrixScheme.SchemeEdifact)
        throw new Exception("Invalid encoding scheme selected!");
      byte num = channel.Input[channel.InputIndex];
      if (num < (byte) 32 || num > (byte) 94)
      {
        channel.Invalid = DataMatrixChannelStatus.ChannelUnsupportedChar;
        return false;
      }
      this.PushInputWord(channel, (byte) ((uint) num & 63U));
      this.IncrementProgress(channel, 9);
      ++channel.InputIndex;
      this.CheckForEndOfSymbolEdifact(channel);
      return true;
    }

    private void CheckForEndOfSymbolEdifact(DataMatrixChannel channel)
    {
      if (channel.InputIndex > channel.Input.Length)
        throw new Exception("Input index out of range while encoding!");
      int num1 = channel.Input.Length - channel.InputIndex;
      if (num1 > 4)
        return;
      int dataWords = channel.CurrentLength / 12;
      int num2 = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribSymbolDataWords, this.FindCorrectSymbolSize(dataWords, DataMatrixSymbolSize.SymbolSquareAuto)) - dataWords;
      if (channel.CurrentLength % 12 == 0 && (num2 == 1 || num2 == 2))
      {
        if (num1 > num2)
          return;
        this.ChangeEncScheme(channel, DataMatrixScheme.SchemeAscii, DmtxUnlatch.Implicit);
        for (int index = 0; index < num1 && this.EncodeNextWord(channel, DataMatrixScheme.SchemeAscii); ++index)
        {
          if (channel.Invalid != DataMatrixChannelStatus.ChannelValid)
            throw new Exception("Error checking for end of symbol edifact");
        }
      }
      else
      {
        if (num1 != 0)
          return;
        this.ChangeEncScheme(channel, DataMatrixScheme.SchemeAscii, DmtxUnlatch.Explicit);
      }
    }

    private void PushInputWord(DataMatrixChannel channel, byte codeword)
    {
      if (channel.EncodedLength / 12 > 4674)
        throw new Exception("Can't push input word, encoded length exceeds limits!");
      switch (channel.EncScheme)
      {
        case DataMatrixScheme.SchemeAscii:
          channel.EncodedWords[channel.CurrentLength / 12] = codeword;
          channel.EncodedLength += 12;
          break;
        case DataMatrixScheme.SchemeC40:
          channel.EncodedWords[channel.EncodedLength / 12] = codeword;
          channel.EncodedLength += 12;
          break;
        case DataMatrixScheme.SchemeText:
          channel.EncodedWords[channel.EncodedLength / 12] = codeword;
          channel.EncodedLength += 12;
          break;
        case DataMatrixScheme.SchemeX12:
          channel.EncodedWords[channel.EncodedLength / 12] = codeword;
          channel.EncodedLength += 12;
          break;
        case DataMatrixScheme.SchemeEdifact:
          int index1 = channel.CurrentLength % 4;
          int index2 = (channel.CurrentLength + 9) / 12 - index1;
          DataMatrixQuadruplet quadrupletValues = this.GetQuadrupletValues(channel.EncodedWords[index2], channel.EncodedWords[index2 + 1], channel.EncodedWords[index2 + 2]);
          quadrupletValues.Value[index1] = codeword;
          for (int index3 = index1 + 1; index3 < 4; ++index3)
            quadrupletValues.Value[index3] = (byte) 0;
          switch (index1)
          {
            case 0:
              channel.EncodedWords[index2] = (byte) ((int) quadrupletValues.Value[0] << 2 | (int) quadrupletValues.Value[1] >> 4);
              break;
            case 1:
              channel.EncodedWords[index2 + 1] = (byte) (((int) quadrupletValues.Value[1] & 15) << 4 | (int) quadrupletValues.Value[2] >> 2);
              break;
            case 2:
              channel.EncodedWords[index2 + 2] = (byte) ((uint) (((int) quadrupletValues.Value[2] & 3) << 6) | (uint) quadrupletValues.Value[3]);
              break;
            case 3:
              channel.EncodedWords[index2 + 2] = (byte) ((uint) (((int) quadrupletValues.Value[2] & 3) << 6) | (uint) quadrupletValues.Value[3]);
              break;
          }
          channel.EncodedLength += 9;
          break;
        case DataMatrixScheme.SchemeBase256:
          channel.EncodedWords[channel.CurrentLength / 12] = codeword;
          channel.EncodedLength += 12;
          break;
      }
    }

    private bool EncodeTripletCodeword(DataMatrixChannel channel)
    {
      int[] outputWords = new int[4];
      byte[] numArray = new byte[6];
      DataMatrixTriplet triplet = new DataMatrixTriplet();
      if (channel.EncScheme != DataMatrixScheme.SchemeX12 && channel.EncScheme != DataMatrixScheme.SchemeText && channel.EncScheme != DataMatrixScheme.SchemeC40)
        throw new Exception("Invalid encoding scheme selected!");
      if (channel.CurrentLength > channel.EncodedLength)
        throw new Exception("Encoding length out of range!");
      if (channel.CurrentLength == channel.EncodedLength)
      {
        int num = channel.CurrentLength % 12 == 0 ? channel.InputIndex : throw new Exception("Invalid encoding length!");
        int tripletCount = 0;
label_13:
        while (tripletCount >= 3 || num >= channel.Input.Length)
        {
          triplet.Value[0] = numArray[0];
          triplet.Value[1] = numArray[1];
          triplet.Value[2] = numArray[2];
          if (tripletCount >= 3)
          {
            this.PushTriplet(channel, triplet);
            numArray[0] = numArray[3];
            numArray[1] = numArray[4];
            numArray[2] = numArray[5];
            tripletCount -= 3;
          }
          if (num == channel.Input.Length)
          {
            while (channel.CurrentLength < channel.EncodedLength)
            {
              this.IncrementProgress(channel, 8);
              ++channel.InputIndex;
            }
            if (channel.CurrentLength == channel.EncodedLength + 8)
            {
              channel.CurrentLength = channel.EncodedLength;
              --channel.InputIndex;
            }
            if (channel.Input.Length < channel.InputIndex)
              throw new Exception("Channel input index exceeds range!");
            int inputCount = channel.Input.Length - channel.InputIndex;
            if (!this.ProcessEndOfSymbolTriplet(channel, triplet, tripletCount, inputCount))
              return false;
            goto label_26;
          }
          else if (tripletCount == 0)
            goto label_26;
        }
        byte inputWord = channel.Input[num++];
        int c40TextX12Words = this.GetC40TextX12Words(outputWords, inputWord, channel.EncScheme);
        if (c40TextX12Words == 0)
        {
          channel.Invalid = DataMatrixChannelStatus.ChannelUnsupportedChar;
          return false;
        }
        for (int index = 0; index < c40TextX12Words; ++index)
          numArray[tripletCount++] = (byte) outputWords[index];
        goto label_13;
      }
label_26:
      if (channel.CurrentLength < channel.EncodedLength)
      {
        this.IncrementProgress(channel, 8);
        ++channel.InputIndex;
      }
      return true;
    }

    private int GetC40TextX12Words(int[] outputWords, byte inputWord, DataMatrixScheme encScheme)
    {
      if (encScheme != DataMatrixScheme.SchemeX12 && encScheme != DataMatrixScheme.SchemeText && encScheme != DataMatrixScheme.SchemeC40)
        throw new Exception("Invalid encoding scheme selected!");
      int c40TextX12Words = 0;
      if (inputWord > (byte) 127)
      {
        if (encScheme == DataMatrixScheme.SchemeX12)
          return 0;
        int[] numArray1 = outputWords;
        int index1 = c40TextX12Words;
        int num = index1 + 1;
        int charTripletShift2 = (int) DataMatrixConstants.DataMatrixCharTripletShift2;
        numArray1[index1] = charTripletShift2;
        int[] numArray2 = outputWords;
        int index2 = num;
        c40TextX12Words = index2 + 1;
        numArray2[index2] = 30;
        inputWord -= (byte) 128;
      }
      if (encScheme == DataMatrixScheme.SchemeX12)
      {
        switch (inputWord)
        {
          case 13:
            outputWords[c40TextX12Words++] = 0;
            break;
          case 32:
            outputWords[c40TextX12Words++] = 3;
            break;
          case 42:
            outputWords[c40TextX12Words++] = 1;
            break;
          case 62:
            outputWords[c40TextX12Words++] = 2;
            break;
          default:
            if (inputWord >= (byte) 48 && inputWord <= (byte) 57)
            {
              outputWords[c40TextX12Words++] = (int) inputWord - 44;
              break;
            }
            if (inputWord >= (byte) 65 && inputWord <= (byte) 90)
            {
              outputWords[c40TextX12Words++] = (int) inputWord - 51;
              break;
            }
            break;
        }
      }
      else if (inputWord <= (byte) 31)
      {
        int[] numArray3 = outputWords;
        int index3 = c40TextX12Words;
        int num1 = index3 + 1;
        int charTripletShift1 = (int) DataMatrixConstants.DataMatrixCharTripletShift1;
        numArray3[index3] = charTripletShift1;
        int[] numArray4 = outputWords;
        int index4 = num1;
        c40TextX12Words = index4 + 1;
        int num2 = (int) inputWord;
        numArray4[index4] = num2;
      }
      else if (inputWord == (byte) 32)
        outputWords[c40TextX12Words++] = 3;
      else if (inputWord <= (byte) 47)
      {
        int[] numArray5 = outputWords;
        int index5 = c40TextX12Words;
        int num3 = index5 + 1;
        int charTripletShift2 = (int) DataMatrixConstants.DataMatrixCharTripletShift2;
        numArray5[index5] = charTripletShift2;
        int[] numArray6 = outputWords;
        int index6 = num3;
        c40TextX12Words = index6 + 1;
        int num4 = (int) inputWord - 33;
        numArray6[index6] = num4;
      }
      else if (inputWord <= (byte) 57)
        outputWords[c40TextX12Words++] = (int) inputWord - 44;
      else if (inputWord <= (byte) 64)
      {
        int[] numArray7 = outputWords;
        int index7 = c40TextX12Words;
        int num5 = index7 + 1;
        int charTripletShift2 = (int) DataMatrixConstants.DataMatrixCharTripletShift2;
        numArray7[index7] = charTripletShift2;
        int[] numArray8 = outputWords;
        int index8 = num5;
        c40TextX12Words = index8 + 1;
        int num6 = (int) inputWord - 43;
        numArray8[index8] = num6;
      }
      else if (inputWord <= (byte) 90 && encScheme == DataMatrixScheme.SchemeC40)
        outputWords[c40TextX12Words++] = (int) inputWord - 51;
      else if (inputWord <= (byte) 90 && encScheme == DataMatrixScheme.SchemeText)
      {
        int[] numArray9 = outputWords;
        int index9 = c40TextX12Words;
        int num7 = index9 + 1;
        int charTripletShift3 = (int) DataMatrixConstants.DataMatrixCharTripletShift3;
        numArray9[index9] = charTripletShift3;
        int[] numArray10 = outputWords;
        int index10 = num7;
        c40TextX12Words = index10 + 1;
        int num8 = (int) inputWord - 64;
        numArray10[index10] = num8;
      }
      else if (inputWord <= (byte) 95)
      {
        int[] numArray11 = outputWords;
        int index11 = c40TextX12Words;
        int num9 = index11 + 1;
        int charTripletShift2 = (int) DataMatrixConstants.DataMatrixCharTripletShift2;
        numArray11[index11] = charTripletShift2;
        int[] numArray12 = outputWords;
        int index12 = num9;
        c40TextX12Words = index12 + 1;
        int num10 = (int) inputWord - 69;
        numArray12[index12] = num10;
      }
      else if (inputWord == (byte) 96 && encScheme == DataMatrixScheme.SchemeText)
      {
        int[] numArray13 = outputWords;
        int index13 = c40TextX12Words;
        int num = index13 + 1;
        int charTripletShift3 = (int) DataMatrixConstants.DataMatrixCharTripletShift3;
        numArray13[index13] = charTripletShift3;
        int[] numArray14 = outputWords;
        int index14 = num;
        c40TextX12Words = index14 + 1;
        numArray14[index14] = 0;
      }
      else if (inputWord <= (byte) 122 && encScheme == DataMatrixScheme.SchemeText)
        outputWords[c40TextX12Words++] = (int) inputWord - 83;
      else if (inputWord <= (byte) 127)
      {
        int[] numArray15 = outputWords;
        int index15 = c40TextX12Words;
        int num11 = index15 + 1;
        int charTripletShift3 = (int) DataMatrixConstants.DataMatrixCharTripletShift3;
        numArray15[index15] = charTripletShift3;
        int[] numArray16 = outputWords;
        int index16 = num11;
        c40TextX12Words = index16 + 1;
        int num12 = (int) inputWord - 96;
        numArray16[index16] = num12;
      }
      return c40TextX12Words;
    }

    private bool ProcessEndOfSymbolTriplet(
      DataMatrixChannel channel,
      DataMatrixTriplet triplet,
      int tripletCount,
      int inputCount)
    {
      if (channel.CurrentLength % 12 != 0)
        throw new Exception("Invalid current length for encoding!");
      int num1 = tripletCount - inputCount;
      int num2 = channel.CurrentLength / 12;
      DataMatrixSymbolSize correctSymbolSize = this.FindCorrectSymbolSize(num2 + (inputCount == 3 ? 2 : inputCount), this._sizeIdxRequest);
      if (correctSymbolSize == DataMatrixSymbolSize.SymbolShapeAuto)
        return false;
      int num3 = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribSymbolDataWords, correctSymbolSize) - num2;
      if (inputCount == 1 && num3 == 1)
      {
        this.ChangeEncScheme(channel, DataMatrixScheme.SchemeAscii, DmtxUnlatch.Implicit);
        if (!this.EncodeNextWord(channel, DataMatrixScheme.SchemeAscii))
          return false;
        if (channel.Invalid != DataMatrixChannelStatus.ChannelValid || channel.InputIndex != channel.Input.Length)
          throw new Exception("Error processing end of symbol triplet!");
      }
      else if (num3 == 2)
      {
        switch (tripletCount)
        {
          case 1:
            this.ChangeEncScheme(channel, DataMatrixScheme.SchemeAscii, DmtxUnlatch.Explicit);
            if (!this.EncodeNextWord(channel, DataMatrixScheme.SchemeAscii))
              return false;
            if (channel.Invalid != DataMatrixChannelStatus.ChannelValid)
              throw new Exception("Error processing end of symbol triplet!");
            break;
          case 2:
            triplet.Value[2] = (byte) 0;
            this.PushTriplet(channel, triplet);
            this.IncrementProgress(channel, 24);
            channel.EncScheme = DataMatrixScheme.SchemeAscii;
            channel.InputIndex += 2;
            channel.InputIndex -= num1;
            break;
          case 3:
            this.PushTriplet(channel, triplet);
            this.IncrementProgress(channel, 24);
            channel.EncScheme = DataMatrixScheme.SchemeAscii;
            channel.InputIndex += 3;
            channel.InputIndex -= num1;
            break;
        }
      }
      else
      {
        int num4 = channel.CurrentLength / 12;
        if (DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribSymbolDataWords, correctSymbolSize) - num4 > 0)
        {
          this.ChangeEncScheme(channel, DataMatrixScheme.SchemeAscii, DmtxUnlatch.Explicit);
          while (channel.InputIndex < channel.Input.Length)
          {
            if (!this.EncodeNextWord(channel, DataMatrixScheme.SchemeAscii))
              return false;
            if (channel.Invalid != DataMatrixChannelStatus.ChannelValid)
              throw new Exception("Error processing end of symbol triplet!");
          }
        }
      }
      if (channel.InputIndex != channel.Input.Length)
        throw new Exception("Could not fully process end of symbol triplet!");
      return true;
    }

    private void PushTriplet(DataMatrixChannel channel, DataMatrixTriplet triplet)
    {
      int num = 1600 * (int) triplet.Value[0] + 40 * (int) triplet.Value[1] + (int) triplet.Value[2] + 1;
      this.PushInputWord(channel, (byte) (num / 256));
      this.PushInputWord(channel, (byte) (num % 256));
    }

    private bool EncodeAsciiCodeword(DataMatrixChannel channel)
    {
      if (channel.EncScheme != DataMatrixScheme.SchemeAscii)
        throw new Exception("Invalid encoding scheme selected!");
      byte inputValue1 = channel.Input[channel.InputIndex];
      if (this.IsDigit(inputValue1) && channel.CurrentLength >= channel.FirstCodeWord + 12)
      {
        int index = (channel.CurrentLength - 12) / 12;
        byte inputValue2 = (byte) ((uint) channel.EncodedWords[index] - 1U);
        if ((index > channel.FirstCodeWord / 12 ? (int) channel.EncodedWords[index - 1] : 0) != 235 && this.IsDigit(inputValue2))
        {
          channel.EncodedWords[index] = (byte) (10 * ((int) inputValue2 - 48) + ((int) inputValue1 - 48) + 130);
          ++channel.InputIndex;
          return true;
        }
      }
      if ((int) inputValue1 == (int) DataMatrixConstants.DataMatrixCharFNC1)
      {
        this.PushInputWord(channel, DataMatrixConstants.DataMatrixCharFNC1);
        this.IncrementProgress(channel, 12);
        ++channel.InputIndex;
        return true;
      }
      if (inputValue1 >= (byte) 128)
      {
        this.PushInputWord(channel, DataMatrixConstants.DataMatrixCharAsciiUpperShift);
        this.IncrementProgress(channel, 12);
        inputValue1 -= (byte) 128;
      }
      this.PushInputWord(channel, (byte) ((uint) inputValue1 + 1U));
      this.IncrementProgress(channel, 12);
      ++channel.InputIndex;
      return true;
    }

    private bool IsDigit(byte inputValue) => inputValue >= (byte) 48 && inputValue <= (byte) 57;

    private void ChangeEncScheme(
      DataMatrixChannel channel,
      DataMatrixScheme targetScheme,
      DmtxUnlatch unlatchType)
    {
      if (channel.EncScheme == targetScheme)
        throw new Exception("Target scheme already equals channel scheme, cannot be changed!");
      switch (channel.EncScheme)
      {
        case DataMatrixScheme.SchemeAscii:
          if (channel.CurrentLength % 12 != 0)
            throw new Exception("Invalid current length detected encoding ascii code");
          break;
        case DataMatrixScheme.SchemeC40:
        case DataMatrixScheme.SchemeText:
        case DataMatrixScheme.SchemeX12:
          if (channel.CurrentLength % 12 != 0)
          {
            channel.Invalid = DataMatrixChannelStatus.ChannelCannotUnlatch;
            return;
          }
          if (channel.CurrentLength != channel.EncodedLength)
          {
            channel.Invalid = DataMatrixChannelStatus.ChannelCannotUnlatch;
            return;
          }
          if (unlatchType == DmtxUnlatch.Explicit)
          {
            this.PushInputWord(channel, (byte) DataMatrixConstants.DataMatrixCharTripletUnlatch);
            this.IncrementProgress(channel, 12);
            break;
          }
          break;
        case DataMatrixScheme.SchemeEdifact:
          if (channel.CurrentLength % 3 != 0)
            throw new Exception("Error changing encryption scheme, current length is invalid!");
          if (unlatchType == DmtxUnlatch.Explicit)
          {
            this.PushInputWord(channel, (byte) DataMatrixConstants.DataMatrixCharEdifactUnlatch);
            this.IncrementProgress(channel, 9);
          }
          int num = channel.CurrentLength % 4 * 3;
          channel.CurrentLength += num;
          channel.EncodedLength += num;
          break;
      }
      channel.EncScheme = DataMatrixScheme.SchemeAscii;
      switch (targetScheme)
      {
        case DataMatrixScheme.SchemeC40:
          this.PushInputWord(channel, DataMatrixConstants.DataMatrixCharC40Latch);
          this.IncrementProgress(channel, 12);
          break;
        case DataMatrixScheme.SchemeText:
          this.PushInputWord(channel, DataMatrixConstants.DataMatrixCharTextLatch);
          this.IncrementProgress(channel, 12);
          break;
        case DataMatrixScheme.SchemeX12:
          this.PushInputWord(channel, DataMatrixConstants.DataMatrixCharX12Latch);
          this.IncrementProgress(channel, 12);
          break;
        case DataMatrixScheme.SchemeEdifact:
          this.PushInputWord(channel, DataMatrixConstants.DataMatrixCharEdifactLatch);
          this.IncrementProgress(channel, 12);
          break;
        case DataMatrixScheme.SchemeBase256:
          this.PushInputWord(channel, DataMatrixConstants.DataMatrixCharBase256Latch);
          this.IncrementProgress(channel, 12);
          this.PushInputWord(channel, this.Randomize255State((byte) 0, 2));
          this.IncrementProgress(channel, 12);
          break;
      }
      channel.EncScheme = targetScheme;
      channel.FirstCodeWord = channel.CurrentLength - 12;
      if (channel.FirstCodeWord % 12 != 0)
        throw new Exception("Error while changin encoding scheme, invalid first code word!");
    }

    private byte Randomize255State(byte codewordValue, int codewordPosition)
    {
      int num1 = 149 * codewordPosition % (int) byte.MaxValue + 1;
      int num2 = (int) codewordValue + num1;
      return num2 <= (int) byte.MaxValue ? (byte) num2 : (byte) (num2 - 256);
    }

    private void IncrementProgress(DataMatrixChannel channel, int encodedUnits)
    {
      if (channel.EncScheme == DataMatrixScheme.SchemeC40 || channel.EncScheme == DataMatrixScheme.SchemeText)
      {
        int index1 = channel.CurrentLength % 6 / 2;
        int index2 = channel.CurrentLength / 12 - (index1 >> 1);
        if (DataMatrixEncode.GetTripletValues(channel.EncodedWords[index2], channel.EncodedWords[index2 + 1]).Value[index1] <= (byte) 2)
          channel.CurrentLength += 8;
      }
      channel.CurrentLength += encodedUnits;
    }

    private static DataMatrixTriplet GetTripletValues(byte cw1, byte cw2)
    {
      DataMatrixTriplet tripletValues = new DataMatrixTriplet();
      int num = (int) cw1 << 8 | (int) cw2;
      tripletValues.Value[0] = (byte) ((num - 1) / 1600);
      tripletValues.Value[1] = (byte) ((num - 1) / 40 % 40);
      tripletValues.Value[2] = (byte) ((num - 1) % 40);
      return tripletValues;
    }

    private DataMatrixQuadruplet GetQuadrupletValues(
      byte cw1,
      byte cw2,
      byte cw3)
    {
      DataMatrixQuadruplet quadrupletValues = new DataMatrixQuadruplet();
      quadrupletValues.Value[0] = (byte) ((uint) cw1 >> 2);
      quadrupletValues.Value[1] = (byte) (((int) cw1 & 3) << 4 | ((int) cw2 & 240) >> 4);
      quadrupletValues.Value[2] = (byte) (((int) cw2 & 15) << 2 | ((int) cw3 & 192) >> 6);
      quadrupletValues.Value[3] = (byte) ((uint) cw3 & 63U);
      return quadrupletValues;
    }

    private static void InitChannel(DataMatrixChannel channel, byte[] codewords)
    {
      channel.EncScheme = DataMatrixScheme.SchemeAscii;
      channel.Invalid = DataMatrixChannelStatus.ChannelValid;
      channel.InputIndex = 0;
      channel.Input = codewords;
    }

    internal int Method
    {
      get => this._method;
      set => this._method = value;
    }

    internal DataMatrixScheme Scheme
    {
      get => this._scheme;
      set => this._scheme = value;
    }

    internal DataMatrixSymbolSize SizeIdxRequest
    {
      get => this._sizeIdxRequest;
      set => this._sizeIdxRequest = value;
    }

    internal int MarginSize
    {
      get => this._marginSize;
      set => this._marginSize = value;
    }

    internal int Width
    {
      get => this._width;
      set => this._width = value;
    }

    internal int Height
    {
      get => this._height;
      set => this._height = value;
    }

    internal int QuietZone
    {
      get => this._quietZone;
      set => this._quietZone = value;
    }

    internal int ModuleSize
    {
      get => this._moduleSize;
      set => this._moduleSize = value;
    }

    internal DataMatrixPackOrder PixelPacking
    {
      get => this._pixelPacking;
      set => this._pixelPacking = value;
    }

    internal DataMatrixFlip ImageFlip
    {
      get => this._imageFlip;
      set => this._imageFlip = value;
    }

    internal int RowPadBytes
    {
      get => this._rowPadBytes;
      set => this._rowPadBytes = value;
    }

    internal DataMatrixMessage Message
    {
      get => this._message;
      set => this._message = value;
    }

    internal DataMatrixImage Image
    {
      get => this._image;
      set => this._image = value;
    }

    internal DataMatrixRegion Region
    {
      get => this._region;
      set => this._region = value;
    }

    public bool[,] RawData
    {
      get => this._rawData;
      set => this._rawData = value;
    }
  }
}
