// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Common.Collections
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System.Collections.Generic;

namespace MessagingToolkit.Barcode.Common
{
  public sealed class Collections
  {
    private Collections()
    {
    }

    public static void InsertionSort<T>(List<T> vector, Comparator comparator)
    {
      int count = vector.Count;
      for (int index1 = 1; index1 < count; ++index1)
      {
        T o2 = vector[index1];
        int index2;
        T obj;
        for (index2 = index1 - 1; index2 >= 0 && comparator.Compare((object) (obj = vector[index2]), (object) o2) > 0; --index2)
          vector[index2 + 1] = obj;
        vector[index2 + 1] = o2;
      }
    }
  }
}
