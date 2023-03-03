// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Rss.AbstractRssDecoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.OneD.Rss
{
  public abstract class AbstractRssDecoder : OneDDecoder
  {
    private const float MinFinderPatternRatio = 0.7916667f;
    private const float MaxFinderPatternRatio = 0.8928571f;
    private static int MaxAvgVariance = 51;
    private static int MaxIndividualVariance = 115;
    protected internal readonly int[] decodeFinderCounters;
    protected internal readonly int[] dataCharacterCounters;
    protected internal readonly float[] oddRoundingErrors;
    protected internal readonly float[] evenRoundingErrors;
    protected internal readonly int[] oddCounts;
    protected internal readonly int[] evenCounts;

    protected internal AbstractRssDecoder()
    {
      this.decodeFinderCounters = new int[4];
      this.dataCharacterCounters = new int[8];
      this.oddRoundingErrors = new float[4];
      this.evenRoundingErrors = new float[4];
      this.oddCounts = new int[this.dataCharacterCounters.Length / 2];
      this.evenCounts = new int[this.dataCharacterCounters.Length / 2];
    }

    protected int[] DecodeFinderCounters => this.decodeFinderCounters;

    protected int[] DataCharacterCounters => this.dataCharacterCounters;

    protected float[] OddRoundingErrors => this.oddRoundingErrors;

    protected float[] EvenRoundingErrors => this.evenRoundingErrors;

    protected int[] OddCounts => this.oddCounts;

    protected int[] EvenCounts => this.evenCounts;

    protected internal static int ParseFinderValue(int[] counters, int[][] finderPatterns)
    {
      for (int finderValue = 0; finderValue < finderPatterns.Length; ++finderValue)
      {
        if (OneDDecoder.PatternMatchVariance(counters, finderPatterns[finderValue], AbstractRssDecoder.MaxIndividualVariance) < AbstractRssDecoder.MaxAvgVariance)
          return finderValue;
      }
      throw NotFoundException.Instance;
    }

    protected internal static int Count(int[] array)
    {
      int num = 0;
      for (int index = 0; index < array.Length; ++index)
        num += array[index];
      return num;
    }

    protected internal static void Increment(int[] array, float[] errors)
    {
      int index1 = 0;
      float error = errors[0];
      for (int index2 = 1; index2 < array.Length; ++index2)
      {
        if ((double) errors[index2] > (double) error)
        {
          error = errors[index2];
          index1 = index2;
        }
      }
      ++array[index1];
    }

    protected internal static void Decrement(int[] array, float[] errors)
    {
      int index1 = 0;
      float error = errors[0];
      for (int index2 = 1; index2 < array.Length; ++index2)
      {
        if ((double) errors[index2] < (double) error)
        {
          error = errors[index2];
          index1 = index2;
        }
      }
      --array[index1];
    }

    protected internal static bool IsFinderPattern(int[] counters)
    {
      int num1 = counters[0] + counters[1];
      int num2 = num1 + counters[2] + counters[3];
      float num3 = (float) num1 / (float) num2;
      if ((double) num3 < 0.791666686534882 || (double) num3 > 0.892857134342194)
        return false;
      int num4 = int.MaxValue;
      int num5 = int.MinValue;
      for (int index = 0; index < counters.Length; ++index)
      {
        int counter = counters[index];
        if (counter > num5)
          num5 = counter;
        if (counter < num4)
          num4 = counter;
      }
      return num5 < 10 * num4;
    }
  }
}
