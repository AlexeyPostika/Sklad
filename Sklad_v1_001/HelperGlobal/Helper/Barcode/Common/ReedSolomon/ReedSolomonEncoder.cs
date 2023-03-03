// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Common.ReedSolomon.ReedSolomonEncoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode.Common.ReedSolomon
{
  public sealed class ReedSolomonEncoder
  {
    private GenericGF field;
    private List<GenericGFPoly> cachedGenerators;

    public ReedSolomonEncoder(GenericGF field)
    {
      this.field = field;
      this.cachedGenerators = new List<GenericGFPoly>(10);
      this.cachedGenerators.Add(new GenericGFPoly(field, new int[1]
      {
        1
      }));
    }

    private GenericGFPoly BuildGenerator(int degree)
    {
      if (degree >= this.cachedGenerators.Count)
      {
        GenericGFPoly genericGfPoly1 = this.cachedGenerators[this.cachedGenerators.Count - 1];
        for (int count = this.cachedGenerators.Count; count <= degree; ++count)
        {
          GenericGFPoly genericGfPoly2 = genericGfPoly1.Multiply(new GenericGFPoly(this.field, new int[2]
          {
            1,
            this.field.Exp(count - 1 + this.field.GetGeneratorBase())
          }));
          this.cachedGenerators.Add(genericGfPoly2);
          genericGfPoly1 = genericGfPoly2;
        }
      }
      return this.cachedGenerators[degree];
    }

    public void Encode(int[] toEncode, int ecBytes)
    {
      if (ecBytes == 0)
        throw new ArgumentException("No error correction bytes");
      int length = toEncode.Length - ecBytes;
      if (length <= 0)
        throw new ArgumentException("No data bytes provided");
      GenericGFPoly other = this.BuildGenerator(ecBytes);
      int[] numArray = new int[length];
      Array.Copy((Array) toEncode, 0, (Array) numArray, 0, length);
      int[] coefficients = new GenericGFPoly(this.field, numArray).MultiplyByMonomial(ecBytes, 1).Divide(other)[1].GetCoefficients();
      int num = ecBytes - coefficients.Length;
      for (int index = 0; index < num; ++index)
        toEncode[length + index] = 0;
      Array.Copy((Array) coefficients, 0, (Array) toEncode, length + num, coefficients.Length);
    }
  }
}
