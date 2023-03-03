// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Common.ReedSolomon.GenericGFPoly
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;
using System.Text;

namespace MessagingToolkit.Barcode.Common.ReedSolomon
{
  internal sealed class GenericGFPoly
  {
    private readonly GenericGF field;
    private readonly int[] coefficients;

    internal GenericGFPoly(GenericGF field, int[] coefficients)
    {
      if (coefficients == null || coefficients.Length == 0)
        throw new ArgumentException();
      this.field = field;
      int length = coefficients.Length;
      if (length > 1 && coefficients[0] == 0)
      {
        int sourceIndex = 1;
        while (sourceIndex < length && coefficients[sourceIndex] == 0)
          ++sourceIndex;
        if (sourceIndex == length)
        {
          this.coefficients = field.GetZero().coefficients;
        }
        else
        {
          this.coefficients = new int[length - sourceIndex];
          Array.Copy((Array) coefficients, sourceIndex, (Array) this.coefficients, 0, this.coefficients.Length);
        }
      }
      else
        this.coefficients = coefficients;
    }

    internal int[] GetCoefficients() => this.coefficients;

    internal int GetDegree() => this.coefficients.Length - 1;

    internal bool IsZero() => this.coefficients[0] == 0;

    internal int GetCoefficient(int degree) => this.coefficients[this.coefficients.Length - 1 - degree];

    internal int EvaluateAt(int a)
    {
      if (a == 0)
        return this.GetCoefficient(0);
      int length = this.coefficients.Length;
      if (a == 1)
      {
        int a1 = 0;
        for (int index = 0; index < length; ++index)
          a1 = GenericGF.AddOrSubtract(a1, this.coefficients[index]);
        return a1;
      }
      int b = this.coefficients[0];
      for (int index = 1; index < length; ++index)
        b = GenericGF.AddOrSubtract(this.field.Multiply(a, b), this.coefficients[index]);
      return b;
    }

    internal GenericGFPoly AddOrSubtract(GenericGFPoly other)
    {
      if (!this.field.Equals((object) other.field))
        throw new ArgumentException("GenericGFPolys do not have same GenericGF field");
      if (this.IsZero())
        return other;
      if (other.IsZero())
        return this;
      int[] numArray1 = this.coefficients;
      int[] sourceArray = other.coefficients;
      if (numArray1.Length > sourceArray.Length)
      {
        int[] numArray2 = numArray1;
        numArray1 = sourceArray;
        sourceArray = numArray2;
      }
      int[] numArray3 = new int[sourceArray.Length];
      int length = sourceArray.Length - numArray1.Length;
      Array.Copy((Array) sourceArray, 0, (Array) numArray3, 0, length);
      for (int index = length; index < sourceArray.Length; ++index)
        numArray3[index] = GenericGF.AddOrSubtract(numArray1[index - length], sourceArray[index]);
      return new GenericGFPoly(this.field, numArray3);
    }

    internal GenericGFPoly Multiply(GenericGFPoly other)
    {
      if (!this.field.Equals((object) other.field))
        throw new ArgumentException("GenericGFPolys do not have same GenericGF field");
      if (this.IsZero() || other.IsZero())
        return this.field.GetZero();
      int[] coefficients1 = this.coefficients;
      int length1 = coefficients1.Length;
      int[] coefficients2 = other.coefficients;
      int length2 = coefficients2.Length;
      int[] coefficients3 = new int[length1 + length2 - 1];
      for (int index1 = 0; index1 < length1; ++index1)
      {
        int a = coefficients1[index1];
        for (int index2 = 0; index2 < length2; ++index2)
          coefficients3[index1 + index2] = GenericGF.AddOrSubtract(coefficients3[index1 + index2], this.field.Multiply(a, coefficients2[index2]));
      }
      return new GenericGFPoly(this.field, coefficients3);
    }

    internal GenericGFPoly Multiply(int scalar)
    {
      if (scalar == 0)
        return this.field.GetZero();
      if (scalar == 1)
        return this;
      int length = this.coefficients.Length;
      int[] coefficients = new int[length];
      for (int index = 0; index < length; ++index)
        coefficients[index] = this.field.Multiply(this.coefficients[index], scalar);
      return new GenericGFPoly(this.field, coefficients);
    }

    internal GenericGFPoly MultiplyByMonomial(int degree, int coefficient)
    {
      if (degree < 0)
        throw new ArgumentException();
      if (coefficient == 0)
        return this.field.GetZero();
      int length = this.coefficients.Length;
      int[] coefficients = new int[length + degree];
      for (int index = 0; index < length; ++index)
        coefficients[index] = this.field.Multiply(this.coefficients[index], coefficient);
      return new GenericGFPoly(this.field, coefficients);
    }

    internal GenericGFPoly[] Divide(GenericGFPoly other)
    {
      if (!this.field.Equals((object) other.field))
        throw new ArgumentException("GenericGFPolys do not have same GenericGF field");
      if (other.IsZero())
        throw new ArgumentException("Divide by 0");
      GenericGFPoly genericGfPoly1 = this.field.GetZero();
      GenericGFPoly genericGfPoly2 = this;
      int b = this.field.Inverse(other.GetCoefficient(other.GetDegree()));
      GenericGFPoly other1;
      for (; genericGfPoly2.GetDegree() >= other.GetDegree() && !genericGfPoly2.IsZero(); genericGfPoly2 = genericGfPoly2.AddOrSubtract(other1))
      {
        int degree = genericGfPoly2.GetDegree() - other.GetDegree();
        int coefficient = this.field.Multiply(genericGfPoly2.GetCoefficient(genericGfPoly2.GetDegree()), b);
        other1 = other.MultiplyByMonomial(degree, coefficient);
        GenericGFPoly other2 = this.field.BuildMonomial(degree, coefficient);
        genericGfPoly1 = genericGfPoly1.AddOrSubtract(other2);
      }
      return new GenericGFPoly[2]
      {
        genericGfPoly1,
        genericGfPoly2
      };
    }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder(8 * this.GetDegree());
      for (int degree = this.GetDegree(); degree >= 0; --degree)
      {
        int a = this.GetCoefficient(degree);
        if (a != 0)
        {
          if (a < 0)
          {
            stringBuilder.Append(" - ");
            a = -a;
          }
          else if (stringBuilder.Length > 0)
            stringBuilder.Append(" + ");
          if (degree == 0 || a != 1)
          {
            int num = this.field.Log(a);
            switch (num)
            {
              case 0:
                stringBuilder.Append('1');
                break;
              case 1:
                stringBuilder.Append('a');
                break;
              default:
                stringBuilder.Append("a^");
                stringBuilder.Append(num);
                break;
            }
          }
          switch (degree)
          {
            case 0:
              continue;
            case 1:
              stringBuilder.Append('x');
              continue;
            default:
              stringBuilder.Append("X^");
              stringBuilder.Append(degree);
              continue;
          }
        }
      }
      return stringBuilder.ToString();
    }
  }
}
