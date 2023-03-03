// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.Encoder.DataMatrixMessage
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;

namespace MessagingToolkit.Barcode.DataMatrix.Encoder
{
  internal class DataMatrixMessage
  {
    private int _outputIdx;

    internal DataMatrixMessage(DataMatrixSymbolSize sizeIdx, DataMatrixFormat symbolFormat)
    {
      if (symbolFormat != DataMatrixFormat.Matrix && symbolFormat != DataMatrixFormat.Mosaic)
        throw new ArgumentException("Only DmtxFormats Matrix and Mosaic are currently supported");
      int symbolAttribute = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribMappingMatrixRows, sizeIdx);
      this.Array = new byte[DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribMappingMatrixCols, sizeIdx) * symbolAttribute];
      int length = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribSymbolDataWords, sizeIdx) + DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribSymbolErrorWords, sizeIdx);
      this.Code = new byte[length];
      this.Output = new byte[10 * length];
    }

    internal void DecodeDataStream(DataMatrixSymbolSize sizeIdx, byte[] outputStart)
    {
      bool flag = false;
      this.Output = outputStart ?? this.Output;
      this._outputIdx = 0;
      byte[] code = this.Code;
      int symbolAttribute = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribSymbolDataWords, sizeIdx);
      if ((int) code[0] == (int) DataMatrixConstants.DataMatrixChar05Macro || (int) code[0] == (int) DataMatrixConstants.DataMatrixChar06Macro)
      {
        this.PushOutputMacroHeader(code[0]);
        flag = true;
      }
      int startIndex = 0;
      while (startIndex < symbolAttribute)
      {
        DataMatrixScheme encodationScheme = DataMatrixMessage.GetEncodationScheme(this.Code[startIndex]);
        if (encodationScheme != DataMatrixScheme.SchemeAscii)
          ++startIndex;
        switch (encodationScheme)
        {
          case DataMatrixScheme.SchemeAscii:
            startIndex = this.DecodeSchemeAscii(startIndex, symbolAttribute);
            continue;
          case DataMatrixScheme.SchemeC40:
          case DataMatrixScheme.SchemeText:
            startIndex = this.DecodeSchemeC40Text(startIndex, symbolAttribute, encodationScheme);
            continue;
          case DataMatrixScheme.SchemeX12:
            startIndex = this.DecodeSchemeX12(startIndex, symbolAttribute);
            continue;
          case DataMatrixScheme.SchemeEdifact:
            startIndex = this.DecodeSchemeEdifact(startIndex, symbolAttribute);
            continue;
          case DataMatrixScheme.SchemeBase256:
            startIndex = this.DecodeSchemeBase256(startIndex, symbolAttribute);
            continue;
          default:
            continue;
        }
      }
      if (!flag)
        return;
      this.PushOutputMacroTrailer();
    }

    private void PushOutputMacroHeader(byte macroType)
    {
      this.PushOutputWord((byte) 91);
      this.PushOutputWord((byte) 41);
      this.PushOutputWord((byte) 62);
      this.PushOutputWord((byte) 30);
      this.PushOutputWord((byte) 48);
      if ((int) macroType == (int) DataMatrixConstants.DataMatrixChar05Macro)
      {
        this.PushOutputWord((byte) 53);
      }
      else
      {
        if ((int) macroType != (int) DataMatrixConstants.DataMatrixChar06Macro)
          throw new ArgumentException("Macro Header only supported for char05 and char06");
        this.PushOutputWord((byte) 54);
      }
      this.PushOutputWord((byte) 29);
    }

    private void PushOutputMacroTrailer()
    {
      this.PushOutputWord((byte) 30);
      this.PushOutputWord((byte) 4);
    }

    private void PushOutputWord(byte value) => this.Output[this._outputIdx++] = value;

    private static DataMatrixScheme GetEncodationScheme(byte val)
    {
      if ((int) val == (int) DataMatrixConstants.DataMatrixCharC40Latch)
        return DataMatrixScheme.SchemeC40;
      if ((int) val == (int) DataMatrixConstants.DataMatrixCharBase256Latch)
        return DataMatrixScheme.SchemeBase256;
      if ((int) val == (int) DataMatrixConstants.DataMatrixCharEdifactLatch)
        return DataMatrixScheme.SchemeEdifact;
      if ((int) val == (int) DataMatrixConstants.DataMatrixCharTextLatch)
        return DataMatrixScheme.SchemeText;
      return (int) val == (int) DataMatrixConstants.DataMatrixCharX12Latch ? DataMatrixScheme.SchemeX12 : DataMatrixScheme.SchemeAscii;
    }

    private int DecodeSchemeAscii(int startIndex, int endIndex)
    {
      bool flag = false;
      while (startIndex < endIndex)
      {
        byte num1 = this.Code[startIndex];
        if (DataMatrixMessage.GetEncodationScheme(this.Code[startIndex]) != DataMatrixScheme.SchemeAscii)
          return startIndex;
        ++startIndex;
        if (flag)
        {
          this.PushOutputWord((byte) ((uint) num1 + (uint) sbyte.MaxValue));
          flag = false;
        }
        else if ((int) num1 == (int) DataMatrixConstants.DataMatrixCharAsciiUpperShift)
        {
          flag = true;
        }
        else
        {
          if ((int) num1 == (int) DataMatrixConstants.DataMatrixCharAsciiPad)
          {
            this.PadCount = endIndex - startIndex;
            return endIndex;
          }
          if (num1 <= (byte) 128)
            this.PushOutputWord((byte) ((uint) num1 - 1U));
          else if (num1 <= (byte) 229)
          {
            byte num2 = (byte) ((uint) num1 - 130U);
            this.PushOutputWord((byte) ((int) num2 / 10 + 48));
            this.PushOutputWord((byte) ((int) num2 - (int) num2 / 10 * 10 + 48));
          }
        }
      }
      return startIndex;
    }

    private int DecodeSchemeC40Text(int startIndex, int endIndex, DataMatrixScheme encScheme)
    {
      int[] numArray = new int[3];
      C40TextState state = new C40TextState()
      {
        Shift = DataMatrixConstants.DataMatrixC40TextBasicSet,
        UpperShift = false
      };
      if (encScheme != DataMatrixScheme.SchemeC40 && encScheme != DataMatrixScheme.SchemeText)
        throw new ArgumentException("Invalid scheme selected for decodind!");
      while (startIndex < endIndex)
      {
        int num = (int) this.Code[startIndex] << 8 | (int) this.Code[startIndex + 1];
        numArray[0] = (num - 1) / 1600;
        numArray[1] = (num - 1) / 40 % 40;
        numArray[2] = (num - 1) % 40;
        startIndex += 2;
        for (int index = 0; index < 3; ++index)
        {
          if (state.Shift == DataMatrixConstants.DataMatrixC40TextBasicSet)
          {
            if (numArray[index] <= 2)
              state.Shift = numArray[index] + 1;
            else if (numArray[index] == 3)
              this.PushOutputC40TextWord(ref state, 32);
            else if (numArray[index] <= 13)
              this.PushOutputC40TextWord(ref state, numArray[index] - 13 + 57);
            else if (numArray[index] <= 39)
            {
              switch (encScheme)
              {
                case DataMatrixScheme.SchemeC40:
                  this.PushOutputC40TextWord(ref state, numArray[index] - 39 + 90);
                  continue;
                case DataMatrixScheme.SchemeText:
                  this.PushOutputC40TextWord(ref state, numArray[index] - 39 + 122);
                  continue;
                default:
                  continue;
              }
            }
          }
          else if (state.Shift == DataMatrixConstants.DataMatrixC40TextShift1)
            this.PushOutputC40TextWord(ref state, numArray[index]);
          else if (state.Shift == DataMatrixConstants.DataMatrixC40TextShift2)
          {
            if (numArray[index] <= 14)
              this.PushOutputC40TextWord(ref state, numArray[index] + 33);
            else if (numArray[index] <= 21)
              this.PushOutputC40TextWord(ref state, numArray[index] + 43);
            else if (numArray[index] <= 26)
              this.PushOutputC40TextWord(ref state, numArray[index] + 69);
            else if (numArray[index] == 27)
              this.PushOutputC40TextWord(ref state, 29);
            else if (numArray[index] == 30)
            {
              state.UpperShift = true;
              state.Shift = DataMatrixConstants.DataMatrixC40TextBasicSet;
            }
          }
          else if (state.Shift == DataMatrixConstants.DataMatrixC40TextShift3)
          {
            switch (encScheme)
            {
              case DataMatrixScheme.SchemeC40:
                this.PushOutputC40TextWord(ref state, numArray[index] + 96);
                continue;
              case DataMatrixScheme.SchemeText:
                if (numArray[index] == 0)
                {
                  this.PushOutputC40TextWord(ref state, numArray[index] + 96);
                  continue;
                }
                if (numArray[index] <= 26)
                {
                  this.PushOutputC40TextWord(ref state, numArray[index] - 26 + 90);
                  continue;
                }
                this.PushOutputC40TextWord(ref state, numArray[index] - 31 + (int) sbyte.MaxValue);
                continue;
              default:
                continue;
            }
          }
        }
        if ((int) this.Code[startIndex] == DataMatrixConstants.DataMatrixCharTripletUnlatch)
          return startIndex + 1;
        if (endIndex - startIndex == 1)
          return startIndex;
      }
      return startIndex;
    }

    private void PushOutputC40TextWord(ref C40TextState state, int value)
    {
      this.Output[this._outputIdx] = value >= 0 && value < 256 ? (byte) value : throw new ArgumentException("Invalid value: Exceeds range for conversion to byte");
      if (state.UpperShift)
      {
        if (value < 0 || value >= 256)
          throw new ArgumentException("Invalid value: Exceeds range for conversion to upper case character");
        this.Output[this._outputIdx] += (byte) 128;
      }
      ++this._outputIdx;
      state.Shift = DataMatrixConstants.DataMatrixC40TextBasicSet;
      state.UpperShift = false;
    }

    private int DecodeSchemeX12(int startIndex, int endIndex)
    {
      int[] numArray = new int[3];
      while (startIndex < endIndex)
      {
        int num = (int) this.Code[startIndex] << 8 | (int) this.Code[startIndex + 1];
        numArray[0] = (num - 1) / 1600;
        numArray[1] = (num - 1) / 40 % 40;
        numArray[2] = (num - 1) % 40;
        startIndex += 2;
        for (int index = 0; index < 3; ++index)
        {
          if (numArray[index] == 0)
            this.PushOutputWord((byte) 13);
          else if (numArray[index] == 1)
            this.PushOutputWord((byte) 42);
          else if (numArray[index] == 2)
            this.PushOutputWord((byte) 62);
          else if (numArray[index] == 3)
            this.PushOutputWord((byte) 32);
          else if (numArray[index] <= 13)
            this.PushOutputWord((byte) (numArray[index] + 44));
          else if (numArray[index] <= 90)
            this.PushOutputWord((byte) (numArray[index] + 51));
        }
        if ((int) this.Code[startIndex] == DataMatrixConstants.DataMatrixCharTripletUnlatch)
          return startIndex + 1;
        if (endIndex - startIndex == 1)
          return startIndex;
      }
      return startIndex;
    }

    private int DecodeSchemeEdifact(int startIndex, int endIndex)
    {
      byte[] numArray = new byte[4];
      while (startIndex < endIndex)
      {
        numArray[0] = (byte) (((int) this.Code[startIndex] & 252) >> 2);
        numArray[1] = (byte) (((int) this.Code[startIndex] & 3) << 4 | ((int) this.Code[startIndex + 1] & 240) >> 4);
        numArray[2] = (byte) (((int) this.Code[startIndex + 1] & 15) << 2 | ((int) this.Code[startIndex + 2] & 192) >> 6);
        numArray[3] = (byte) ((uint) this.Code[startIndex + 2] & 63U);
        for (int index = 0; index < 4; ++index)
        {
          if (index < 3)
            ++startIndex;
          if ((int) numArray[index] == DataMatrixConstants.DataMatrixCharEdifactUnlatch)
          {
            if (this.Output[this._outputIdx] != (byte) 0)
              throw new Exception("Error decoding edifact scheme");
            return startIndex;
          }
          this.PushOutputWord((byte) ((uint) numArray[index] ^ (uint) (((int) numArray[index] & 32 ^ 32) << 1)));
        }
        if (endIndex - startIndex < 3)
          return startIndex;
      }
      return startIndex;
    }

    private int DecodeSchemeBase256(int startIndex, int endIndex)
    {
      int num1 = startIndex + 1;
      int num2 = (int) this.Code[startIndex++];
      int idx = num1;
      int num3 = idx + 1;
      int num4 = (int) DataMatrixMessage.UnRandomize255State((byte) num2, idx);
      int num5;
      if (num4 == 0)
        num5 = endIndex;
      else if (num4 <= 249)
      {
        num5 = startIndex + num4;
      }
      else
      {
        int num6 = (int) DataMatrixMessage.UnRandomize255State(this.Code[startIndex++], num3++);
        num5 = startIndex + (num4 - 249) * 250 + num6;
      }
      if (num5 > endIndex)
        throw new Exception("Error decoding scheme base 256");
      while (startIndex < num5)
        this.PushOutputWord(DataMatrixMessage.UnRandomize255State(this.Code[startIndex++], num3++));
      return startIndex;
    }

    internal static byte UnRandomize255State(byte value, int idx)
    {
      int num1 = 149 * idx % (int) byte.MaxValue + 1;
      int num2 = (int) value - num1;
      if (num2 < 0)
        num2 += 256;
      return num2 >= 0 && num2 < 256 ? (byte) num2 : throw new Exception("Error unrandomizing 255 state");
    }

    internal int SymbolModuleStatus(DataMatrixSymbolSize sizeIdx, int symbolRow, int symbolCol)
    {
      int symbolAttribute1 = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribDataRegionRows, sizeIdx);
      int symbolAttribute2 = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribDataRegionCols, sizeIdx);
      int symbolAttribute3 = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribSymbolRows, sizeIdx);
      int symbolAttribute4 = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribMappingMatrixCols, sizeIdx);
      int num1 = symbolAttribute3 - symbolRow - 1;
      int num2 = num1 - 1 - 2 * (num1 / (symbolAttribute1 + 2));
      int num3 = symbolCol - 1 - 2 * (symbolCol / (symbolAttribute2 + 2));
      if (symbolRow % (symbolAttribute1 + 2) == 0 || symbolCol % (symbolAttribute2 + 2) == 0)
        return DataMatrixConstants.DataMatrixModuleOnRGB | (DataMatrixConstants.DmtxModuleData == 0 ? 1 : 0);
      if ((symbolRow + 1) % (symbolAttribute1 + 2) == 0)
        return ((symbolCol & 1) != 0 ? 0 : DataMatrixConstants.DataMatrixModuleOnRGB) | (DataMatrixConstants.DmtxModuleData == 0 ? 1 : 0);
      return (symbolCol + 1) % (symbolAttribute2 + 2) == 0 ? ((symbolRow & 1) != 0 ? 0 : DataMatrixConstants.DataMatrixModuleOnRGB) | (DataMatrixConstants.DmtxModuleData == 0 ? 1 : 0) : (int) this.Array[num2 * symbolAttribute4 + num3] | DataMatrixConstants.DmtxModuleData;
    }

    internal int PadCount { get; set; }

    internal byte[] Array { get; set; }

    internal byte[] Code { get; set; }

    internal byte[] Output { get; set; }

    internal int ArraySize => this.Array.Length;

    internal int CodeSize => this.Code.Length;

    internal int OutputSize => this.Output.Length;
  }
}
