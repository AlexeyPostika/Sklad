// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Pdf417.Decoder.Pdf417Symbol
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;

namespace MessagingToolkit.Barcode.Pdf417.Decoder
{
  public sealed class Pdf417Symbol
  {
    public int K;
    public int codeword;

    public Pdf417Symbol()
    {
      this.K = -1;
      this.codeword = -1;
    }

    public Pdf417Symbol(int k, int cw)
    {
      this.K = k;
      this.codeword = cw;
    }

    public static bool GetPatternEdges(int[] pattern, int[] edges)
    {
      int num = 0;
      for (int index = 0; index < 8; ++index)
        num += pattern[index];
      int int32_1 = Convert.ToInt32(Math.Round((double) (pattern[0] + pattern[1]) * (17.0 / (double) num)));
      int int32_2 = Convert.ToInt32(Math.Round((double) (pattern[1] + pattern[2]) * (17.0 / (double) num)));
      int int32_3 = Convert.ToInt32(Math.Round((double) (pattern[2] + pattern[3]) * (17.0 / (double) num)));
      int int32_4 = Convert.ToInt32(Math.Round((double) (pattern[3] + pattern[4]) * (17.0 / (double) num)));
      int int32_5 = Convert.ToInt32(Math.Round((double) (pattern[4] + pattern[5]) * (17.0 / (double) num)));
      int int32_6 = Convert.ToInt32(Math.Round((double) (pattern[5] + pattern[6]) * (17.0 / (double) num)));
      if (int32_1 < 2 || int32_1 > 9 || int32_2 < 2 || int32_2 > 9 || int32_3 < 2 || int32_3 > 9 || int32_4 < 2 || int32_4 > 9 || int32_5 < 2 || int32_5 > 9 || int32_6 < 2 || int32_6 > 9)
        return false;
      edges[0] = int32_1;
      edges[1] = int32_2;
      edges[2] = int32_3;
      edges[3] = int32_4;
      edges[4] = int32_5;
      edges[5] = int32_6;
      return true;
    }

    public static bool CheckPatternIsStartLocator(int[] pattern)
    {
      int[] edges = new int[6];
      return Pdf417Symbol.GetPatternEdges(pattern, edges) && edges[0] == 9 && edges[1] == 2 && edges[2] == 2 && edges[3] == 2 && edges[4] == 2 && edges[5] == 2;
    }

    public static bool CheckPatternIsEndLocator(int[] pattern)
    {
      int[] edges = new int[6];
      return Pdf417Symbol.GetPatternEdges(pattern, edges) && edges[0] == 8 && edges[1] == 2 && edges[2] == 4 && edges[3] == 4 && edges[4] == 2 && edges[5] == 2;
    }

    public static int GetPatternSizeInPixels(int[] pattern)
    {
      int patternSizeInPixels = 0;
      for (int index = 0; index < 8; ++index)
        patternSizeInPixels += pattern[index];
      return patternSizeInPixels;
    }

    public static bool CheckPatternBlockLengths(int[] pattern)
    {
      for (int index = 0; index < 8; ++index)
      {
        if (pattern[index] <= 0)
          return false;
      }
      return true;
    }

    public static int GetCluster(int[] edges)
    {
      int cluster = (edges[0] - edges[1] + edges[4] - edges[5] + 9) % 9;
      switch (cluster)
      {
        case 0:
        case 3:
        case 6:
          return cluster;
        default:
          cluster = -1;
          goto case 0;
      }
    }
  }
}
