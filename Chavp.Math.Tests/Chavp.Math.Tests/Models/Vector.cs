using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chavp.Math.Tests.Models
{
    public struct Vector
    {
        List<double?> X;
        public Vector(params double?[] values)
        {
            X = new List<double?>(values);
        }

        public int Dim
        {
            get
            {
                return X.Count;
            }
        }

        public static Vector operator +(Vector c1, Vector c2)
        {
            var list = new double?[c1.Dim];
            for (int i = 0; i < c1.Dim; i++)
            {
                list[i] = (c1[i] + c2[i]);
            }
            return new Vector(list);
        }

        public static Vector operator -(Vector c1, Vector c2)
        {
            var list = new double?[c1.Dim];
            for (int i = 0; i < c1.Dim; i++)
            {
                list[i] = (c1[i] - c2[i]);
            }
            return new Vector(list);
        }

        public static Vector operator /(Vector c1, Vector c2)
        {
            var list = new double?[c1.Dim];
            for (int i = 0; i < c1.Dim; i++)
            {
                list[i] = (c1.X[i] / c2.X[i]);
            }
            return new Vector(list);
        }

        public static Vector operator *(Vector c1, double c)
        {
            var list = new double?[c1.Dim];
            for (int i = 0; i < c1.Dim; i++)
            {
                list[i] = (c1.X[i] * c);
            }
            return new Vector(list);
        }

        public static Vector operator ^(Vector c1, double c)
        {
            var list = new double?[c1.Dim];
            for (int i = 0; i < c1.Dim; i++)
            {
                list[i] = System.Math.Pow(c1.X[i].GetValueOrDefault(), c);
            }
            return new Vector(list);
        }

        public static Vector operator *(double c, Vector c1)
        {
            return c1 * c;
        }

        public static bool operator !=(Vector c1, Vector c2)
        {
            return !(c1 == c2);
        }

        public static bool operator ==(Vector c1, Vector c2)
        {
            for (int i = 0; i < c1.Dim; i++)
            {
                if (c1[i] != c2[i]) return false;
            }
            return true;
        }

        public override string ToString()
        {
            return string.Join(",", X);
        }

        public double Magnitude()
        {
            var pow_2 = this ^ 2;
            return System.Math.Pow(pow_2.Sum(), 0.5);
        }

        public Vector Cross(Vector c)
        {
            if (c.Dim != 3) throw new Exception("Not 3D");

            var m1 = new Matrix();
            m1.Elements.Add(new Vector(1, 1, 1));
            m1.Elements.Add(this);
            m1.Elements.Add(c);

            var x1 = m1.Submatrix(0, 0).Determinant();
            var x2 = m1.Submatrix(0, 1).Determinant();
            var x3 = m1.Submatrix(0, 2).Determinant();

            return new Vector(x1, (-1) * x2, x3);
        }

        public double Dot(Vector c)
        {
            double dot = 0;
            for (int i = 0; i < c.X.Count; i++)
            {
                dot += this[i].GetValueOrDefault() * c[i].GetValueOrDefault();
            }
            return dot;
        }

        public void Add(double? val)
        {
            X.Add(val);
        }

        public double Sum()
        {
            return X.Sum().GetValueOrDefault();
        }

        public void RemoveAt(int index)
        {
            X.RemoveAt(index);
        }

        public double?[] Values
        {
            get
            {
                return X.ToArray();
            }
        }

        public double? this[int i]
        {
            get
            {
                return X[i];
            }
            set
            {
                X[i] = value;
            }
        }
    }

}
