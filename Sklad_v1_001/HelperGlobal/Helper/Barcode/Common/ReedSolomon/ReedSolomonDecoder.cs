// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Common.ReedSolomon.ReedSolomonDecoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.Common.ReedSolomon
{
  public sealed class ReedSolomonDecoder
  {
    private GenericGF field;

    public ReedSolomonDecoder(GenericGF field) => this.field = field;

    public void Decode(int[] received, int twoS)
    {
      GenericGFPoly genericGfPoly = new GenericGFPoly(this.field, received);
      int[] coefficients = new int[twoS];
      bool flag = true;
      for (int index = 0; index < twoS; ++index)
      {
        int at = genericGfPoly.EvaluateAt(this.field.Exp(index + this.field.GetGeneratorBase()));
        coefficients[coefficients.Length - 1 - index] = at;
        if (at != 0)
          flag = false;
      }
      if (flag)
        return;
      GenericGFPoly b = new GenericGFPoly(this.field, coefficients);
      GenericGFPoly[] genericGfPolyArray = this.RunEuclideanAlgorithm(this.field.BuildMonomial(twoS, 1), b, twoS);
      GenericGFPoly errorLocator = genericGfPolyArray[0];
      GenericGFPoly errorEvaluator = genericGfPolyArray[1];
      int[] errorLocations = this.FindErrorLocations(errorLocator);
      int[] errorMagnitudes = this.FindErrorMagnitudes(errorEvaluator, errorLocations);
      for (int index1 = 0; index1 < errorLocations.Length; ++index1)
      {
        int index2 = received.Length - 1 - this.field.Log(errorLocations[index1]);
        if (index2 < 0)
          throw new ReedSolomonException("Bad error location");
        received[index2] = GenericGF.AddOrSubtract(received[index2], errorMagnitudes[index1]);
      }
    }

    private GenericGFPoly[] RunEuclideanAlgorithm(
      GenericGFPoly a,
      GenericGFPoly b,
      int R)
    {
      if (a.GetDegree() < b.GetDegree())
      {
        GenericGFPoly genericGfPoly = a;
        a = b;
        b = genericGfPoly;
      }
      GenericGFPoly genericGfPoly1 = a;
      GenericGFPoly genericGfPoly2 = b;
      GenericGFPoly other1 = this.field.GetZero();
      GenericGFPoly genericGfPoly3 = this.field.GetOne();
      while (genericGfPoly2.GetDegree() >= R / 2)
      {
        GenericGFPoly genericGfPoly4 = genericGfPoly1;
        GenericGFPoly other2 = other1;
        genericGfPoly1 = genericGfPoly2;
        other1 = genericGfPoly3;
        if (genericGfPoly1.IsZero())
          throw new ReedSolomonException("r_{idx-1} was zero");
        genericGfPoly2 = genericGfPoly4;
        GenericGFPoly genericGfPoly5 = this.field.GetZero();
        int b1 = this.field.Inverse(genericGfPoly1.GetCoefficient(genericGfPoly1.GetDegree()));
        int degree;
        int coefficient;
        for (; genericGfPoly2.GetDegree() >= genericGfPoly1.GetDegree() && !genericGfPoly2.IsZero(); genericGfPoly2 = genericGfPoly2.AddOrSubtract(genericGfPoly1.MultiplyByMonomial(degree, coefficient)))
        {
          degree = genericGfPoly2.GetDegree() - genericGfPoly1.GetDegree();
          coefficient = this.field.Multiply(genericGfPoly2.GetCoefficient(genericGfPoly2.GetDegree()), b1);
          genericGfPoly5 = genericGfPoly5.AddOrSubtract(this.field.BuildMonomial(degree, coefficient));
        }
        genericGfPoly3 = genericGfPoly5.Multiply(other1).AddOrSubtract(other2);
      }
      int coefficient1 = genericGfPoly3.GetCoefficient(0);
      int scalar = coefficient1 != 0 ? this.field.Inverse(coefficient1) : throw new ReedSolomonException("sigmaTilde(0) was zero");
      return new GenericGFPoly[2]
      {
        genericGfPoly3.Multiply(scalar),
        genericGfPoly2.Multiply(scalar)
      };
    }

    private int[] FindErrorLocations(GenericGFPoly errorLocator)
    {
      int degree = errorLocator.GetDegree();
      if (degree == 1)
        return new int[1]{ errorLocator.GetCoefficient(1) };
      int[] errorLocations = new int[degree];
      int index = 0;
      for (int a = 1; a < this.field.GetSize() && index < degree; ++a)
      {
        if (errorLocator.EvaluateAt(a) == 0)
        {
          errorLocations[index] = this.field.Inverse(a);
          ++index;
        }
      }
      if (index != degree)
        throw new ReedSolomonException("Error locator degree does not match number of roots");
      return errorLocations;
    }

    private int[] FindErrorMagnitudes(GenericGFPoly errorEvaluator, int[] errorLocations)
    {
      int length = errorLocations.Length;
      int[] errorMagnitudes = new int[length];
      for (int index1 = 0; index1 < length; ++index1)
      {
        int num1 = this.field.Inverse(errorLocations[index1]);
        int a = 1;
        for (int index2 = 0; index2 < length; ++index2)
        {
          if (index1 != index2)
          {
            int num2 = this.field.Multiply(errorLocations[index2], num1);
            int b = (num2 & 1) == 0 ? num2 | 1 : num2 & -2;
            a = this.field.Multiply(a, b);
          }
        }
        errorMagnitudes[index1] = this.field.Multiply(errorEvaluator.EvaluateAt(num1), this.field.Inverse(a));
        if (this.field.GetGeneratorBase() != 0)
          errorMagnitudes[index1] = this.field.Multiply(errorMagnitudes[index1], num1);
      }
      return errorMagnitudes;
    }
  }
}
