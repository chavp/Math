using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chavp.Math.Tests.Models
{
    // https://en.wikipedia.org/wiki/Matrix_(mathematics)
    public class Matrix
    {
        public Matrix()
        {
            Elements = new List<Vector>();
        }

        public List<Vector> Elements { get; set; }

        public Tuple<int, int> Size
        {
            get
            {

                return new Tuple<int, int>(Elements.Count, Elements.First().Dim);
            }
        }

        public static Matrix operator +(Matrix c1, Matrix c2)
        {
            var m = new Matrix();
            for (int j = 0; j < c1.Elements.Count; j++)
            {
                m.Elements.Add(c1[j] + c2[j]);
            }
            return m;
        }

        public static Matrix operator -(Matrix c1, Matrix c2)
        {
            var m = new Matrix();
            for (int j = 0; j < c1.Elements.Count; j++)
            {
                m.Elements.Add(c1[j] - c2[j]);
            }
            return m;
        }

        public static Matrix operator *(Matrix c1, double c)
        {
            var m = new Matrix();
            for (int j = 0; j < c1.Elements.Count; j++)
            {
                var vec1 = c * c1.Elements[j];

                m.Elements.Add(vec1);
            }
            return m;
        }

        public static Matrix operator *(double c, Matrix c1)
        {
            return c1 * c;
        }

        public static Matrix operator *(Matrix c1, Matrix c2)
        {
            if (c1.Size.Item2 != c2.Size.Item1)
            {
                throw new NotImplementedException("Can not multiply cols != rows");
            }

            var m = Matrix.Init(c1.Size.Item1, c2.Size.Item2);
            for (int r = 0; r < c1.Size.Item1; r++)
            {
                for (int i = 0; i < c2.Size.Item2; i++)
                {
                    double sum = 0;
                    for (int j = 0; j < c2.Size.Item1; j++)
                    {
                        sum = sum + c1[r, j].Value * c2[j, i].Value;
                    }

                    m[r, i] = sum;
                }
            }

            return m;
        }

        public static Matrix Init(int rows, int cols, double? defaultVal = null)
        {
            var m = new Matrix();
            for (int i = 0; i < rows; i++)
            {
                var list = new double?[cols];
                for (int j = 0; j < cols; j++)
                {
                    list[j] = defaultVal;
                }
                m.Elements.Add(new Vector(list));
            }
            return m;
        }

        public static Matrix Init(int rows, Vector vector)
        {
            var m = new Matrix();
            for (int i = 0; i < rows; i++)
            {
                m.Elements.Add(vector);
            }
            return m;
        }

        public static Matrix operator ~(Matrix c1)
        {
            var t = Matrix.Init(c1.Size.Item2, c1.Size.Item1);

            for (int i = 0; i < c1.Size.Item1; i++)
            {
                for (int j = 0; j < c1.Size.Item2; j++)
                {
                    t[j, i] = c1[i, j];
                }
            }
            return t;
        }

        public Vector this[int i]
        {
            get
            {
                return Elements[i];
            }
            set
            {
                Elements[i] = value;
            }
        }

        public double? this[int i, int j]
        {
            get
            {
                return Elements[i][j];
            }
            set
            {
                var v = Elements[i];
                v[j] = value;
            }
        }

        public Matrix Submatrix(int i, int j)
        {
            var sub = this.Clone();
            sub.Elements.RemoveAt(i);
            foreach (var item in sub.Elements)
            {
                item.RemoveAt(j);
            }

            return sub;
        }

        public double Trace()
        {
            if (this.Size.Item1 != this.Size.Item2)
            {
                throw new NotImplementedException("It not square matrix");
            }

            double trace = 0;
            for (int i = 0; i < this.Size.Item1; i++)
            {
                trace += this[i, i].Value;
            }
            return trace;
        }

        public Matrix Cofactor()
        {
            check_square_matrix();

            var co = Init(this.Size.Item2, this.Size.Item2);
            for (int i = 0; i < this.Size.Item2; i++)
            {
                for (int j = 0; j < this.Size.Item2; j++)
                {
                    co[i, j] = this.Submatrix(i, j).Determinant();

                    if ((i + j) % 2 != 0)
                    {
                        co[i, j] = (-1) * co[i, j];
                    }
                }
            }
            return co;
        }

        public Matrix Adjugate()
        {
            return ~Cofactor();
        }

        public Matrix Invert()
        {
            check_square_matrix();

            var det = this.Determinant();

            var adj = this.Adjugate();

            return (1 / det) * adj;
        }

        public Matrix Sum()
        {
            var ONE = Matrix.OneMatrix(this.Size.Item2, 1);

            return ~(~this * ONE);
        }

        public Matrix Mean()
        {
            var SUM = Sum();
            var ONE = Matrix.OneMatrix(Size.Item1, Size.Item2);

            var N = ONE.Sum();
            var M = SUM.Clone();
            M[0] = (SUM[0] / N[0]);
            return M;
        }

        public double Determinant()
        {
            check_square_matrix();

            if (this.Size.Item1 == 1)
            {
                return this[0, 0].Value;
            }

            double determinant = 0;
            for (int i = 0; i < this.Size.Item2; i++)
            {
                var sub = this.Submatrix(0, i);

                if (i % 2 == 0)
                {
                    determinant += this[0, i].Value * sub.Determinant();
                }
                else
                {
                    determinant -= this[0, i].Value * sub.Determinant();
                }
            }

            return determinant;
        }

        public Matrix Clone()
        {
            var copy = new Matrix
            {
                //Elements = this.Elements.ToList()
            };

            foreach (var item in this.Elements)
            {
                copy.Elements.Add(new Vector(item.Values));
            }

            return copy;
        }

        private void check_square_matrix()
        {
            if (this.Size.Item1 != this.Size.Item2)
            {
                throw new NotImplementedException("It not square matrix");
            }
        }

        public static Matrix OneMatrix(int rows, int columns)
        {
            return Init(rows, columns, 1);
        }

        public override string ToString()
        {
            var result = new List<string>();
            foreach (var vec in this.Elements)
            {
                result.Add("|" + vec.ToString() + "|");
            }

            return string.Join("", result);
        }
    }
}
