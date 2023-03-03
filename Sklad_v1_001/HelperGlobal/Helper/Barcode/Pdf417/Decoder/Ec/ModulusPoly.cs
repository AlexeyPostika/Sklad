// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Pdf417.Decoder.Ec.ModulusPoly
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;
using System.Text;

namespace MessagingToolkit.Barcode.Pdf417.Decoder.Ec
{
  internal sealed class ModulusPoly
  {
    private readonly ModulusGF field;
    private readonly int[] coefficients;

    public ModulusPoly(ModulusGF field, int[] coefficients)
    {
      if (coefficients.Length == 0)
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

    internal int[] Coefficients => this.coefficients;

    internal int Degree => this.coefficients.Length - 1;

    public bool IsZero => this.coefficients[0] == 0;

    internal int GetCoefficient(int degree) => this.coefficients[this.coefficients.Length - 1 - degree];

    internal int EvaluateAt(int a)
    {
      if (a == 0)
        return this.GetCoefficient(0);
      int length = this.coefficients.Length;
      int a1 = 0;
      if (a == 1)
      {
        foreach (int coefficient in this.coefficients)
          a1 = this.field.Add(a1, coefficient);
        return a1;
      }
      int b = this.coefficients[0];
      for (int index = 1; index < length; ++index)
        b = this.field.Add(this.field.Multiply(a, b), this.coefficients[index]);
      return b;
    }

    internal ModulusPoly Add(ModulusPoly other)
    {
      if (!this.field.Equals((object) other.field))
        throw new ArgumentException("ModulusPolys do not have same ModulusGF field");
      if (this.IsZero)
        return other;
      if (other.IsZero)
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
        numArray3[index] = this.field.Add(numArray1[index - length], sourceArray[index]);
      return new ModulusPoly(this.field, numArray3);
    }

    internal ModulusPoly Subtract(ModulusPoly other)
    {
      if (!this.field.Equals((object) other.field))
        throw new ArgumentException("ModulusPolys do not have same ModulusGF field");
      return other.IsZero ? this : this.Add(other.Negative());
    }

    internal ModulusPoly Multiply(ModulusPoly other)
    {
      if (!this.field.Equals((object) other.field))
        throw new ArgumentException("ModulusPolys do not have same ModulusGF field");
      if (this.IsZero || other.IsZero)
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
          coefficients3[index1 + index2] = this.field.Add(coefficients3[index1 + index2], this.field.Multiply(a, coefficients2[index2]));
      }
      return new ModulusPoly(this.field, coefficients3);
    }

    internal ModulusPoly Negative()
    {
      int length = this.coefficients.Length;
      int[] coefficients = new int[length];
      for (int index = 0; index < length; ++index)
        coefficients[index] = this.field.Subtract(0, this.coefficients[index]);
      return new ModulusPoly(this.field, coefficients);
    }

    internal ModulusPoly Multiply(int scalar)
    {
      if (scalar == 0)
        return this.field.GetZero();
      if (scalar == 1)
        return this;
      int length = this.coefficients.Length;
      int[] coefficients = new int[length];
      for (int index = 0; index < length; ++index)
        coefficients[index] = this.field.Multiply(this.coefficients[index], scalar);
      return new ModulusPoly(this.field, coefficients);
    }

    public ModulusPoly MultiplyByMonomial(int degree, int coefficient)
    {
      if (degree < 0)
        throw new ArgumentException();
      if (coefficient == 0)
        return this.field.GetZero();
      int length = this.coefficients.Length;
      int[] coefficients = new int[length + degree];
      for (int index = 0; index < length; ++index)
        coefficients[index] = this.field.Multiply(this.coefficients[index], coefficient);
      return new ModulusPoly(this.field, coefficients);
    }

    internal ModulusPoly[] Divide(ModulusPoly other)
    {
      if (!this.field.Equals((object) other.field))
        throw new ArgumentException("ModulusPolys do not have same ModulusGF field");
      if (other.IsZero)
        throw new ArgumentException("Divide by 0");
      ModulusPoly modulusPoly1 = this.field.GetZero();
      ModulusPoly modulusPoly2 = this;
      int b = this.field.Inverse(other.GetCoefficient(other.Degree));
      ModulusPoly other1;
      for (; modulusPoly2.Degree >= other.Degree && !modulusPoly2.IsZero; modulusPoly2 = modulusPoly2.Subtract(other1))
      {
        int degree = modulusPoly2.Degree - other.Degree;
        int coefficient = this.field.Multiply(modulusPoly2.GetCoefficient(modulusPoly2.Degree), b);
        other1 = other.MultiplyByMonomial(degree, coefficient);
        ModulusPoly other2 = this.field.BuildMonomial(degree, coefficient);
        modulusPoly1 = modulusPoly1.Add(other2);
      }
      return new ModulusPoly[2]
      {
        modulusPoly1,
        modulusPoly2
      };
    }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder(8 * this.Degree);
      for (int degree = this.Degree; degree >= 0; --degree)
      {
        int num = this.GetCoefficient(degree);
        if (num != 0)
        {
          if (num < 0)
          {
            stringBuilder.Append(" - ");
            num = -num;
          }
          else if (stringBuilder.Length > 0)
            stringBuilder.Append(" + ");
          if (degree == 0 || num != 1)
            stringBuilder.Append(num);
          switch (degree)
          {
            case 0:
              continue;
            case 1:
              stringBuilder.Append('x');
              continue;
            default:
              stringBuilder.Append("x^");
              stringBuilder.Append(degree);
              continue;
          }
        }
      }
      return stringBuilder.ToString();
    }
  }
}
