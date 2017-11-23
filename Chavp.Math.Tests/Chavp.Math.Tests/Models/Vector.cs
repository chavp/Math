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

        public static Vector operator *(double c, Vector c1)
        {
            return c1 * c;
        }

        public override string ToString()
        {
            return string.Join(",", X);
        }

        public void Add(double? val)
        {
            X.Add(val);
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
