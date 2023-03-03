// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Rss.Expanded.Decoders.GeneralAppIdDecoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System;
using System.Text;

namespace MessagingToolkit.Barcode.OneD.Rss.Expanded.Decoders
{
  public sealed class GeneralAppIdDecoder
  {
    private readonly BitArray information;
    private readonly CurrentParsingState current;
    private readonly StringBuilder buffer;

    public GeneralAppIdDecoder(BitArray information)
    {
      this.current = new CurrentParsingState();
      this.buffer = new StringBuilder();
      this.information = information;
    }

    internal string DecodeAllCodes(StringBuilder buff, int initialPosition)
    {
      int pos = initialPosition;
      string remaining = (string) null;
      while (true)
      {
        DecodedInformation decodedInformation = this.DecodeGeneralPurposeField(pos, remaining);
        string inGeneralPurpose = FieldParser.ParseFieldsInGeneralPurpose(decodedInformation.NewString);
        buff.Append(inGeneralPurpose);
        remaining = !decodedInformation.IsRemaining ? (string) null : decodedInformation.RemainingValue.ToString();
        if (pos != decodedInformation.NewPosition)
          pos = decodedInformation.NewPosition;
        else
          break;
      }
      return buff.ToString();
    }

    private bool IsStillNumeric(int pos)
    {
      if (pos + 7 > this.information.Size)
        return pos + 4 <= this.information.Size;
      for (int i = pos; i < pos + 3; ++i)
      {
        if (this.information.Get(i))
          return true;
      }
      return this.information.Get(pos + 3);
    }

    private DecodedNumeric DecodeNumeric(int pos)
    {
      if (pos + 7 > this.information.Size)
      {
        int valueFromBitArray = this.ExtractNumericValueFromBitArray(pos, 4);
        return valueFromBitArray == 0 ? new DecodedNumeric(this.information.Size, 10, 10) : new DecodedNumeric(this.information.Size, valueFromBitArray - 1, 10);
      }
      int valueFromBitArray1 = this.ExtractNumericValueFromBitArray(pos, 7);
      int firstDigit = (valueFromBitArray1 - 8) / 11;
      int secondDigit = (valueFromBitArray1 - 8) % 11;
      return new DecodedNumeric(pos + 7, firstDigit, secondDigit);
    }

    internal int ExtractNumericValueFromBitArray(int pos, int bits) => GeneralAppIdDecoder.ExtractNumericValueFromBitArray(this.information, pos, bits);

    internal static int ExtractNumericValueFromBitArray(BitArray information_0, int pos, int bits)
    {
      if (bits > 32)
        throw new ArgumentException("extractNumberValueFromBitArray can't handle more than 32 bits");
      int valueFromBitArray = 0;
      for (int index = 0; index < bits; ++index)
      {
        if (information_0.Get(pos + index))
          valueFromBitArray |= 1 << bits - index - 1;
      }
      return valueFromBitArray;
    }

    internal DecodedInformation DecodeGeneralPurposeField(
      int pos,
      string remaining)
    {
      this.buffer.Length = 0;
      if (remaining != null)
        this.buffer.Append(remaining);
      this.current.position = pos;
      DecodedInformation blocks = this.ParseBlocks();
      return blocks != null && blocks.IsRemaining ? new DecodedInformation(this.current.position, this.buffer.ToString(), blocks.RemainingValue) : new DecodedInformation(this.current.position, this.buffer.ToString());
    }

    private DecodedInformation ParseBlocks()
    {
      int position;
      BlockParsedResult blockParsedResult;
      bool isFinished;
      do
      {
        position = this.current.position;
        if (this.current.IsAlpha)
        {
          blockParsedResult = this.ParseAlphaBlock();
          isFinished = blockParsedResult.IsFinished;
        }
        else if (this.current.IsIsoIec646)
        {
          blockParsedResult = this.ParseIsoIec646Block();
          isFinished = blockParsedResult.IsFinished;
        }
        else
        {
          blockParsedResult = this.ParseNumericBlock();
          isFinished = blockParsedResult.IsFinished;
        }
      }
      while ((position != this.current.position || isFinished) && !isFinished);
      return blockParsedResult.DecodedInformation;
    }

    private BlockParsedResult ParseNumericBlock()
    {
      while (this.IsStillNumeric(this.current.position))
      {
        DecodedNumeric decodedNumeric = this.DecodeNumeric(this.current.position);
        this.current.position = decodedNumeric.NewPosition;
        if (decodedNumeric.IsFirstDigitFnc1())
          return new BlockParsedResult(!decodedNumeric.IsSecondDigitFnc1() ? new DecodedInformation(this.current.position, this.buffer.ToString(), decodedNumeric.SecondDigit) : new DecodedInformation(this.current.position, this.buffer.ToString()), true);
        this.buffer.Append(decodedNumeric.FirstDigit);
        if (decodedNumeric.IsSecondDigitFnc1())
          return new BlockParsedResult(new DecodedInformation(this.current.position, this.buffer.ToString()), true);
        this.buffer.Append(decodedNumeric.SecondDigit);
      }
      if (this.IsNumericToAlphaNumericLatch(this.current.position))
      {
        this.current.SetAlpha();
        this.current.position += 4;
      }
      return new BlockParsedResult(false);
    }

    private BlockParsedResult ParseIsoIec646Block()
    {
      while (this.IsStillIsoIec646(this.current.position))
      {
        DecodedChar decodedChar = this.DecodeIsoIec646(this.current.position);
        this.current.position = decodedChar.NewPosition;
        if (decodedChar.IsFnc1)
          return new BlockParsedResult(new DecodedInformation(this.current.position, this.buffer.ToString()), true);
        this.buffer.Append(decodedChar.Value);
      }
      if (this.IsAlphaOr646ToNumericLatch(this.current.position))
      {
        this.current.position += 3;
        this.current.SetNumeric();
      }
      else if (this.IsAlphaTo646ToAlphaLatch(this.current.position))
      {
        if (this.current.position + 5 < this.information.Size)
          this.current.position += 5;
        else
          this.current.position = this.information.Size;
        this.current.SetAlpha();
      }
      return new BlockParsedResult(false);
    }

    private BlockParsedResult ParseAlphaBlock()
    {
      while (this.IsStillAlpha(this.current.position))
      {
        DecodedChar decodedChar = this.DecodeAlphanumeric(this.current.position);
        this.current.position = decodedChar.NewPosition;
        if (decodedChar.IsFnc1)
          return new BlockParsedResult(new DecodedInformation(this.current.position, this.buffer.ToString()), true);
        this.buffer.Append(decodedChar.Value);
      }
      if (this.IsAlphaOr646ToNumericLatch(this.current.position))
      {
        this.current.position += 3;
        this.current.SetNumeric();
      }
      else if (this.IsAlphaTo646ToAlphaLatch(this.current.position))
      {
        if (this.current.position + 5 < this.information.Size)
          this.current.position += 5;
        else
          this.current.position = this.information.Size;
        this.current.SetIsoIec646();
      }
      return new BlockParsedResult(false);
    }

    private bool IsStillIsoIec646(int pos)
    {
      if (pos + 5 > this.information.Size)
        return false;
      int valueFromBitArray1 = this.ExtractNumericValueFromBitArray(pos, 5);
      if (valueFromBitArray1 >= 5 && valueFromBitArray1 < 16)
        return true;
      if (pos + 7 > this.information.Size)
        return false;
      int valueFromBitArray2 = this.ExtractNumericValueFromBitArray(pos, 7);
      if (valueFromBitArray2 >= 64 && valueFromBitArray2 < 116)
        return true;
      if (pos + 8 > this.information.Size)
        return false;
      int valueFromBitArray3 = this.ExtractNumericValueFromBitArray(pos, 8);
      return valueFromBitArray3 >= 232 && valueFromBitArray3 < 253;
    }

    private DecodedChar DecodeIsoIec646(int pos)
    {
      int valueFromBitArray1 = this.ExtractNumericValueFromBitArray(pos, 5);
      if (valueFromBitArray1 == 15)
        return new DecodedChar(pos + 5, '$');
      if (valueFromBitArray1 >= 5 && valueFromBitArray1 < 15)
        return new DecodedChar(pos + 5, (char) (48 + valueFromBitArray1 - 5));
      int valueFromBitArray2 = this.ExtractNumericValueFromBitArray(pos, 7);
      if (valueFromBitArray2 >= 64 && valueFromBitArray2 < 90)
        return new DecodedChar(pos + 7, (char) (valueFromBitArray2 + 1));
      if (valueFromBitArray2 >= 90 && valueFromBitArray2 < 116)
        return new DecodedChar(pos + 7, (char) (valueFromBitArray2 + 7));
      int valueFromBitArray3 = this.ExtractNumericValueFromBitArray(pos, 8);
      switch (valueFromBitArray3)
      {
        case 232:
          return new DecodedChar(pos + 8, '!');
        case 233:
          return new DecodedChar(pos + 8, '"');
        case 234:
          return new DecodedChar(pos + 8, '%');
        case 235:
          return new DecodedChar(pos + 8, '&');
        case 236:
          return new DecodedChar(pos + 8, '\'');
        case 237:
          return new DecodedChar(pos + 8, '(');
        case 238:
          return new DecodedChar(pos + 8, ')');
        case 239:
          return new DecodedChar(pos + 8, '*');
        case 240:
          return new DecodedChar(pos + 8, '+');
        case 241:
          return new DecodedChar(pos + 8, ',');
        case 242:
          return new DecodedChar(pos + 8, '-');
        case 243:
          return new DecodedChar(pos + 8, '.');
        case 244:
          return new DecodedChar(pos + 8, '/');
        case 245:
          return new DecodedChar(pos + 8, ':');
        case 246:
          return new DecodedChar(pos + 8, ';');
        case 247:
          return new DecodedChar(pos + 8, '<');
        case 248:
          return new DecodedChar(pos + 8, '=');
        case 249:
          return new DecodedChar(pos + 8, '>');
        case 250:
          return new DecodedChar(pos + 8, '?');
        case 251:
          return new DecodedChar(pos + 8, '_');
        case 252:
          return new DecodedChar(pos + 8, ' ');
        default:
          throw new Exception("Decoding invalid ISO/IEC 646 value: " + (object) valueFromBitArray3);
      }
    }

    private bool IsStillAlpha(int pos)
    {
      if (pos + 5 > this.information.Size)
        return false;
      int valueFromBitArray1 = this.ExtractNumericValueFromBitArray(pos, 5);
      if (valueFromBitArray1 >= 5 && valueFromBitArray1 < 16)
        return true;
      if (pos + 6 > this.information.Size)
        return false;
      int valueFromBitArray2 = this.ExtractNumericValueFromBitArray(pos, 6);
      return valueFromBitArray2 >= 16 && valueFromBitArray2 < 63;
    }

    private DecodedChar DecodeAlphanumeric(int pos)
    {
      int valueFromBitArray1 = this.ExtractNumericValueFromBitArray(pos, 5);
      if (valueFromBitArray1 == 15)
        return new DecodedChar(pos + 5, '$');
      if (valueFromBitArray1 >= 5 && valueFromBitArray1 < 15)
        return new DecodedChar(pos + 5, (char) (48 + valueFromBitArray1 - 5));
      int valueFromBitArray2 = this.ExtractNumericValueFromBitArray(pos, 6);
      if (valueFromBitArray2 >= 32 && valueFromBitArray2 < 58)
        return new DecodedChar(pos + 6, (char) (valueFromBitArray2 + 33));
      switch (valueFromBitArray2)
      {
        case 58:
          return new DecodedChar(pos + 6, '*');
        case 59:
          return new DecodedChar(pos + 6, ',');
        case 60:
          return new DecodedChar(pos + 6, '-');
        case 61:
          return new DecodedChar(pos + 6, '.');
        case 62:
          return new DecodedChar(pos + 6, '/');
        default:
          throw new Exception("Decoding invalid alphanumeric value: " + (object) valueFromBitArray2);
      }
    }

    private bool IsAlphaTo646ToAlphaLatch(int pos)
    {
      if (pos + 1 > this.information.Size)
        return false;
      for (int index = 0; index < 5 && index + pos < this.information.Size; ++index)
      {
        if (index == 2)
        {
          if (!this.information.Get(pos + 2))
            return false;
        }
        else if (this.information.Get(pos + index))
          return false;
      }
      return true;
    }

    private bool IsAlphaOr646ToNumericLatch(int pos)
    {
      if (pos + 3 > this.information.Size)
        return false;
      for (int i = pos; i < pos + 3; ++i)
      {
        if (this.information.Get(i))
          return false;
      }
      return true;
    }

    private bool IsNumericToAlphaNumericLatch(int pos)
    {
      if (pos + 1 > this.information.Size)
        return false;
      for (int index = 0; index < 4 && index + pos < this.information.Size; ++index)
      {
        if (this.information.Get(pos + index))
          return false;
      }
      return true;
    }
  }
}
