// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.Encoder.DataMatrixMatrix3
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;

namespace MessagingToolkit.Barcode.DataMatrix.Encoder
{
  internal class DataMatrixMatrix3
  {
    private double[,] _data;

    private DataMatrixMatrix3()
    {
    }

    internal DataMatrixMatrix3(DataMatrixMatrix3 src) => this._data = new double[3, 3]
    {
      {
        src[0, 0],
        src[0, 1],
        src[0, 2]
      },
      {
        src[1, 0],
        src[1, 1],
        src[1, 2]
      },
      {
        src[2, 0],
        src[2, 1],
        src[2, 2]
      }
    };

    internal static DataMatrixMatrix3 Identity() => DataMatrixMatrix3.Translate(0.0, 0.0);

    internal static DataMatrixMatrix3 Translate(double tx, double ty) => new DataMatrixMatrix3()
    {
      _data = new double[3, 3]
      {
        {
          1.0,
          0.0,
          0.0
        },
        {
          0.0,
          1.0,
          0.0
        },
        {
          tx,
          ty,
          1.0
        }
      }
    };

    internal static DataMatrixMatrix3 Rotate(double angle) => new DataMatrixMatrix3()
    {
      _data = new double[3, 3]
      {
        {
          Math.Cos(angle),
          Math.Sin(angle),
          0.0
        },
        {
          -Math.Sin(angle),
          Math.Cos(angle),
          0.0
        },
        {
          0.0,
          0.0,
          1.0
        }
      }
    };

    internal static DataMatrixMatrix3 Scale(double sx, double sy)
    {
      DataMatrixMatrix3 dataMatrixMatrix3_1 = new DataMatrixMatrix3();
      DataMatrixMatrix3 dataMatrixMatrix3_2 = dataMatrixMatrix3_1;
      double[,] numArray1 = new double[3, 3];
      numArray1[0, 0] = sx;
      numArray1[1, 1] = sy;
      numArray1[2, 2] = 1.0;
      double[,] numArray2 = numArray1;
      dataMatrixMatrix3_2._data = numArray2;
      return dataMatrixMatrix3_1;
    }

    internal static DataMatrixMatrix3 Shear(double shx, double shy) => new DataMatrixMatrix3()
    {
      _data = new double[3, 3]
      {
        {
          1.0,
          shy,
          0.0
        },
        {
          shx,
          1.0,
          0.0
        },
        {
          0.0,
          0.0,
          1.0
        }
      }
    };

    internal static DataMatrixMatrix3 LineSkewTop(double b0, double b1, double sz)
    {
      if (b0 < DataMatrixConstants.DataMatrixAlmostZero)
        throw new ArgumentException("b0 must be larger than zero in top line skew transformation");
      DataMatrixMatrix3 dataMatrixMatrix3_1 = new DataMatrixMatrix3();
      DataMatrixMatrix3 dataMatrixMatrix3_2 = dataMatrixMatrix3_1;
      double[,] numArray1 = new double[3, 3];
      numArray1[0, 0] = b1 / b0;
      numArray1[0, 2] = (b1 - b0) / (sz * b0);
      numArray1[1, 1] = sz / b0;
      numArray1[2, 2] = 1.0;
      double[,] numArray2 = numArray1;
      dataMatrixMatrix3_2._data = numArray2;
      return dataMatrixMatrix3_1;
    }

    internal static DataMatrixMatrix3 LineSkewTopInv(
      double b0,
      double b1,
      double sz)
    {
      if (b1 < DataMatrixConstants.DataMatrixAlmostZero)
        throw new ArgumentException("b1 must be larger than zero in top line skew transformation (Inverse)");
      DataMatrixMatrix3 dataMatrixMatrix3_1 = new DataMatrixMatrix3();
      DataMatrixMatrix3 dataMatrixMatrix3_2 = dataMatrixMatrix3_1;
      double[,] numArray1 = new double[3, 3];
      numArray1[0, 0] = b0 / b1;
      numArray1[0, 2] = (b0 - b1) / (sz * b1);
      numArray1[1, 1] = b0 / sz;
      numArray1[2, 2] = 1.0;
      double[,] numArray2 = numArray1;
      dataMatrixMatrix3_2._data = numArray2;
      return dataMatrixMatrix3_1;
    }

    internal static DataMatrixMatrix3 LineSkewSide(
      double b0,
      double b1,
      double sz)
    {
      if (b0 < DataMatrixConstants.DataMatrixAlmostZero)
        throw new ArgumentException("b0 must be larger than zero in side line skew transformation (Inverse)");
      DataMatrixMatrix3 dataMatrixMatrix3_1 = new DataMatrixMatrix3();
      DataMatrixMatrix3 dataMatrixMatrix3_2 = dataMatrixMatrix3_1;
      double[,] numArray1 = new double[3, 3];
      numArray1[0, 0] = sz / b0;
      numArray1[1, 1] = b1 / b0;
      numArray1[1, 2] = (b1 - b0) / (sz * b0);
      numArray1[2, 2] = 1.0;
      double[,] numArray2 = numArray1;
      dataMatrixMatrix3_2._data = numArray2;
      return dataMatrixMatrix3_1;
    }

    internal static DataMatrixMatrix3 LineSkewSideInv(
      double b0,
      double b1,
      double sz)
    {
      if (b1 < DataMatrixConstants.DataMatrixAlmostZero)
        throw new ArgumentException("b1 must be larger than zero in top line skew transformation (Inverse)");
      DataMatrixMatrix3 dataMatrixMatrix3_1 = new DataMatrixMatrix3();
      DataMatrixMatrix3 dataMatrixMatrix3_2 = dataMatrixMatrix3_1;
      double[,] numArray1 = new double[3, 3];
      numArray1[0, 0] = b0 / sz;
      numArray1[1, 1] = b0 / b1;
      numArray1[1, 2] = (b0 - b1) / (sz * b1);
      numArray1[2, 2] = 1.0;
      double[,] numArray2 = numArray1;
      dataMatrixMatrix3_2._data = numArray2;
      return dataMatrixMatrix3_1;
    }

    public static DataMatrixMatrix3 operator *(
      DataMatrixMatrix3 m1,
      DataMatrixMatrix3 m2)
    {
      DataMatrixMatrix3 dataMatrixMatrix3 = new DataMatrixMatrix3()
      {
        _data = new double[3, 3]
      };
      for (int i = 0; i < 3; ++i)
      {
        for (int j = 0; j < 3; ++j)
        {
          for (int index = 0; index < 3; ++index)
            dataMatrixMatrix3[i, j] += m1[i, index] * m2[index, j];
        }
      }
      return dataMatrixMatrix3;
    }

    public static DataMatrixVector2 operator *(
      DataMatrixVector2 vector,
      DataMatrixMatrix3 matrix)
    {
      double num = Math.Abs(vector.X * matrix[0, 2] + vector.Y * matrix[1, 2] + matrix[2, 2]);
      if (num <= DataMatrixConstants.DataMatrixAlmostZero)
        throw new ArgumentException("Multiplication of vector and matrix resulted in invalid result");
      return new DataMatrixVector2((vector.X * matrix[0, 0] + vector.Y * matrix[1, 0] + matrix[2, 0]) / num, (vector.X * matrix[0, 1] + vector.Y * matrix[1, 1] + matrix[2, 1]) / num);
    }

    public override string ToString() => string.Format("{0}\t{1}\t{2}\n{3}\t{4}\t{5}\n{6}\t{7}\t{8}\n", (object) this._data[0, 0], (object) this._data[0, 1], (object) this._data[0, 2], (object) this._data[1, 0], (object) this._data[1, 1], (object) this._data[1, 2], (object) this._data[2, 0], (object) this._data[2, 1], (object) this._data[2, 2]);

    internal double this[int i, int j]
    {
      get => this._data[i, j];
      set => this._data[i, j] = value;
    }
  }
}
