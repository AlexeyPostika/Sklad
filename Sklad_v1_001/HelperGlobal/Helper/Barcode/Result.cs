// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Result
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode
{
  public sealed class Result
  {
    private readonly string text;
    private readonly byte[] rawBytes;
    private ResultPoint[] resultPoints;
    private readonly BarcodeFormat format;
    private Dictionary<ResultMetadataType, object> resultMetadata;
    private readonly long timestamp;

    public string Text => this.text;

    public byte[] RawBytes => this.rawBytes;

    public ResultPoint[] ResultPoints => this.resultPoints;

    public BarcodeFormat BarcodeFormat => this.format;

    public Dictionary<ResultMetadataType, object> ResultMetadata => this.resultMetadata;

    public Result(string text, byte[] rawBytes, ResultPoint[] resultPoints, BarcodeFormat format)
      : this(text, rawBytes, resultPoints, format, DateTime.Now.Ticks / 10000L)
    {
    }

    public Result(
      string text,
      byte[] rawBytes,
      ResultPoint[] resultPoints,
      BarcodeFormat format,
      long timestamp)
    {
      this.text = text != null || rawBytes != null ? text : throw new ArgumentException("Text and bytes are null");
      this.rawBytes = rawBytes;
      this.resultPoints = resultPoints;
      this.format = format;
      this.resultMetadata = (Dictionary<ResultMetadataType, object>) null;
      this.timestamp = timestamp;
    }

    public void PutMetadata(ResultMetadataType type, object value)
    {
      if (this.resultMetadata == null)
        this.resultMetadata = new Dictionary<ResultMetadataType, object>(3);
      this.resultMetadata[type] = value;
    }

    public void PutAllMetadata(Dictionary<ResultMetadataType, object> metadata)
    {
      if (metadata == null)
        return;
      if (this.resultMetadata == null)
      {
        this.resultMetadata = metadata;
      }
      else
      {
        IEnumerator<ResultMetadataType> enumerator = (IEnumerator<ResultMetadataType>) metadata.Keys.GetEnumerator();
        while (enumerator.MoveNext())
        {
          ResultMetadataType current = enumerator.Current;
          object obj = metadata[current];
          this.resultMetadata.Add(current, obj);
        }
      }
    }

    public void AddResultPoints(ResultPoint[] newPoints)
    {
      if (this.resultPoints == null)
      {
        this.resultPoints = newPoints;
      }
      else
      {
        if (newPoints == null || newPoints.Length <= 0)
          return;
        ResultPoint[] destinationArray = new ResultPoint[this.resultPoints.Length + newPoints.Length];
        Array.Copy((Array) this.resultPoints, 0, (Array) destinationArray, 0, this.resultPoints.Length);
        Array.Copy((Array) newPoints, 0, (Array) destinationArray, this.resultPoints.Length, newPoints.Length);
        this.resultPoints = destinationArray;
      }
    }

    public long Timestamp => this.timestamp;

    public override string ToString() => this.text == null ? "[" + (object) this.rawBytes.Length + " bytes]" : this.text;
  }
}
