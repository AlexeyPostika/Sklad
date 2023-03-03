// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Pdf417.Decoder.Ec.ErrorCorrection
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.Pdf417.Decoder.Ec
{
  public sealed class ErrorCorrection
  {
    private readonly ModulusGF field;

    public ErrorCorrection() => this.field = ModulusGF.PDF417_GF;

    public void Decode(int[] received, int numECCodewords, int[] erasures)
    {
      ModulusPoly modulusPoly1 = new ModulusPoly(this.field, received);
      int[] coefficients = new int[numECCodewords];
      bool flag = false;
      for (int a = numECCodewords; a > 0; --a)
      {
        int at = modulusPoly1.EvaluateAt(this.field.Exp(a));
        coefficients[numECCodewords - a] = at;
        if (at != 0)
          flag = true;
      }
      if (!flag)
        return;
      ModulusPoly modulusPoly2 = this.field.GetOne();
      foreach (int erasure in erasures)
      {
        ModulusPoly other = new ModulusPoly(this.field, new int[2]
        {
          this.field.Subtract(0, this.field.Exp(received.Length - 1 - erasure)),
          1
        });
        modulusPoly2 = modulusPoly2.Multiply(other);
      }
      ModulusPoly b = new ModulusPoly(this.field, coefficients);
      ModulusPoly[] modulusPolyArray = this.RunEuclideanAlgorithm(this.field.BuildMonomial(numECCodewords, 1), b, numECCodewords);
      ModulusPoly errorLocator = modulusPolyArray[0];
      ModulusPoly errorEvaluator = modulusPolyArray[1];
      int[] errorLocations = this.FindErrorLocations(errorLocator);
      int[] errorMagnitudes = this.FindErrorMagnitudes(errorEvaluator, errorLocator, errorLocations);
      for (int index1 = 0; index1 < errorLocations.Length; ++index1)
      {
        int index2 = received.Length - 1 - this.field.Log(errorLocations[index1]);
        if (index2 < 0)
          throw ChecksumException.Instance;
        received[index2] = this.field.Subtract(received[index2], errorMagnitudes[index1]);
      }
    }

    private ModulusPoly[] RunEuclideanAlgorithm(ModulusPoly a, ModulusPoly b, int R)
    {
      if (a.Degree < b.Degree)
      {
        ModulusPoly modulusPoly = a;
        a = b;
        b = modulusPoly;
      }
      ModulusPoly modulusPoly1 = a;
      ModulusPoly modulusPoly2 = b;
      ModulusPoly other1 = this.field.GetZero();
      ModulusPoly modulusPoly3 = this.field.GetOne();
      while (modulusPoly2.Degree >= R / 2)
      {
        ModulusPoly modulusPoly4 = modulusPoly1;
        ModulusPoly other2 = other1;
        modulusPoly1 = modulusPoly2;
        other1 = modulusPoly3;
        if (modulusPoly1.IsZero)
          throw ChecksumException.Instance;
        modulusPoly2 = modulusPoly4;
        ModulusPoly modulusPoly5 = this.field.GetZero();
        int b1 = this.field.Inverse(modulusPoly1.GetCoefficient(modulusPoly1.Degree));
        int degree;
        int coefficient;
        for (; modulusPoly2.Degree >= modulusPoly1.Degree && !modulusPoly2.IsZero; modulusPoly2 = modulusPoly2.Subtract(modulusPoly1.MultiplyByMonomial(degree, coefficient)))
        {
          degree = modulusPoly2.Degree - modulusPoly1.Degree;
          coefficient = this.field.Multiply(modulusPoly2.GetCoefficient(modulusPoly2.Degree), b1);
          modulusPoly5 = modulusPoly5.Add(this.field.BuildMonomial(degree, coefficient));
        }
        modulusPoly3 = modulusPoly5.Multiply(other1).Subtract(other2).Negative();
      }
      int coefficient1 = modulusPoly3.GetCoefficient(0);
      int scalar = coefficient1 != 0 ? this.field.Inverse(coefficient1) : throw ChecksumException.Instance;
      return new ModulusPoly[2]
      {
        modulusPoly3.Multiply(scalar),
        modulusPoly2.Multiply(scalar)
      };
    }

    private int[] FindErrorLocations(ModulusPoly errorLocator)
    {
      int degree = errorLocator.Degree;
      int[] errorLocations = new int[degree];
      int index = 0;
      for (int a = 1; a < this.field.Size && index < degree; ++a)
      {
        if (errorLocator.EvaluateAt(a) == 0)
        {
          errorLocations[index] = this.field.Inverse(a);
          ++index;
        }
      }
      if (index != degree)
        throw ChecksumException.Instance;
      return errorLocations;
    }

    private int[] FindErrorMagnitudes(
      ModulusPoly errorEvaluator,
      ModulusPoly errorLocator,
      int[] errorLocations)
    {
      int degree = errorLocator.Degree;
      int[] coefficients = new int[degree];
      for (int index = 1; index <= degree; ++index)
        coefficients[degree - index] = this.field.Multiply(index, errorLocator.GetCoefficient(index));
      ModulusPoly modulusPoly = new ModulusPoly(this.field, coefficients);
      int length = errorLocations.Length;
      int[] errorMagnitudes = new int[length];
      for (int index = 0; index < length; ++index)
      {
        int a1 = this.field.Inverse(errorLocations[index]);
        int a2 = this.field.Subtract(0, errorEvaluator.EvaluateAt(a1));
        int b = this.field.Inverse(modulusPoly.EvaluateAt(a1));
        errorMagnitudes[index] = this.field.Multiply(a2, b);
      }
      return errorMagnitudes;
    }
  }
}
