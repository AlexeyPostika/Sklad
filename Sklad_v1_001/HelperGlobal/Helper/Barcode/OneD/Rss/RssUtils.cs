// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Rss.RssUtils
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.OneD.Rss
{
  public sealed class RssUtils
  {
    private RssUtils()
    {
    }

    internal static int[] GetRssWidths(int val, int n, int elements, int maxWidth, bool noNarrow)
    {
      int[] rssWidths = new int[elements];
      int num1 = 0;
      int index1;
      for (index1 = 0; index1 < elements - 1; ++index1)
      {
        num1 |= 1 << index1;
        int num2 = 1;
        int num3;
        while (true)
        {
          num3 = RssUtils.Combins(n - num2 - 1, elements - index1 - 2);
          if (noNarrow && num1 == 0 && n - num2 - (elements - index1 - 1) >= elements - index1 - 1)
            num3 -= RssUtils.Combins(n - num2 - (elements - index1), elements - index1 - 2);
          if (elements - index1 - 1 > 1)
          {
            int num4 = 0;
            for (int index2 = n - num2 - (elements - index1 - 2); index2 > maxWidth; --index2)
              num4 += RssUtils.Combins(n - num2 - index2 - 1, elements - index1 - 3);
            num3 -= num4 * (elements - 1 - index1);
          }
          else if (n - num2 > maxWidth)
            --num3;
          val -= num3;
          if (val >= 0)
          {
            ++num2;
            num1 &= ~(1 << index1);
          }
          else
            break;
        }
        val += num3;
        n -= num2;
        rssWidths[index1] = num2;
      }
      rssWidths[index1] = n;
      return rssWidths;
    }

    public static int GetRssValue(int[] widths, int maxWidth, bool noNarrow)
    {
      int length = widths.Length;
      int num1 = 0;
      for (int index = 0; index < length; ++index)
        num1 += widths[index];
      int rssValue = 0;
      int num2 = 0;
      for (int index1 = 0; index1 < length - 1; ++index1)
      {
        int num3 = 1;
        num2 |= 1 << index1;
        while (num3 < widths[index1])
        {
          int num4 = RssUtils.Combins(num1 - num3 - 1, length - index1 - 2);
          if (noNarrow && num2 == 0 && num1 - num3 - (length - index1 - 1) >= length - index1 - 1)
            num4 -= RssUtils.Combins(num1 - num3 - (length - index1), length - index1 - 2);
          if (length - index1 - 1 > 1)
          {
            int num5 = 0;
            for (int index2 = num1 - num3 - (length - index1 - 2); index2 > maxWidth; --index2)
              num5 += RssUtils.Combins(num1 - num3 - index2 - 1, length - index1 - 3);
            num4 -= num5 * (length - 1 - index1);
          }
          else if (num1 - num3 > maxWidth)
            --num4;
          rssValue += num4;
          ++num3;
          num2 &= ~(1 << index1);
        }
        num1 -= num3;
      }
      return rssValue;
    }

    internal static int Combins(int n, int r)
    {
      int num1;
      int num2;
      if (n - r > r)
      {
        num1 = r;
        num2 = n - r;
      }
      else
      {
        num1 = n - r;
        num2 = r;
      }
      int num3 = 1;
      int num4 = 1;
      for (int index = n; index > num2; --index)
      {
        num3 *= index;
        if (num4 <= num1)
        {
          num3 /= num4;
          ++num4;
        }
      }
      for (; num4 <= num1; ++num4)
        num3 /= num4;
      return num3;
    }

    internal static int[] Elements(int[] eDist, int N, int K)
    {
      int[] numArray = new int[eDist.Length + 2];
      int num1 = K << 1;
      numArray[0] = 1;
      int num2 = 10;
      int num3 = 1;
      for (int index = 1; index < num1 - 2; index += 2)
      {
        numArray[index] = eDist[index - 1] - numArray[index - 1];
        numArray[index + 1] = eDist[index] - numArray[index];
        num3 += numArray[index] + numArray[index + 1];
        if (numArray[index] < num2)
          num2 = numArray[index];
      }
      numArray[num1 - 1] = N - num3;
      if (numArray[num1 - 1] < num2)
        num2 = numArray[num1 - 1];
      if (num2 > 1)
      {
        for (int index = 0; index < num1; index += 2)
        {
          numArray[index] += num2 - 1;
          numArray[index + 1] -= num2 - 1;
        }
      }
      return numArray;
    }
  }
}
