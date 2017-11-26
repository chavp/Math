using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chavp.Math.Tests.Models
{
    // https://en.wikipedia.org/wiki/Matrix_(mathematics)
    public struct Matrix
    {
        public Matrix(params Vector[] vecs)
        {
            Vectors = new List<Vector>(vecs);
        }

        List<Vector> Vectors;

        public Tuple<int, int> Size
        {
            get
            {

                return new Tuple<int, int>(Vectors.Count, Vectors.FirstOrDefault().Dim);
            }
        }

        public static Matrix operator +(Matrix c1, Matrix c2)
        {
            check_size_matrix(c1, c2);
            var m = Matrix.Create();
            for (int j = 0; j < c1.Vectors.Count; j++)
            {
                m.Vectors.Add(c1[j] + c2[j]);
            }
            return m;
        }

        public static Matrix operator -(Matrix c1, Matrix c2)
        {
            check_size_matrix(c1, c2);
            var m = Matrix.Create();
            for (int j = 0; j < c1.Vectors.Count; j++)
            {
                m.Vectors.Add(c1[j] - c2[j]);
            }
            return m;
        }

        public static Matrix operator *(Matrix c1, double c)
        {
            var m = Matrix.Create();
            for (int j = 0; j < c1.Vectors.Count; j++)
            {
                var vec1 = c * c1.Vectors[j];
                m.Vectors.Add(vec1);
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

            var m = Matrix.Create(c1.Size.Item1, c2.Size.Item2);
            for (int i = 0; i < c1.Size.Item1; i++)
            {
                for (int j = 0; j < c2.Size.Item2; j++)
                {
                    double sum = 0;

                    for (int r = 0; r < c2.Size.Item1; r++)
                    {
                        sum = sum + c1[i, r] * c2[r, j];
                    }
                    m[i, j] = System.Math.Round(sum, 4);
                }
            }

            return m;
        }

        public Matrix BooleanProduct(Matrix c2)
        {
            if (this.Size.Item2 != c2.Size.Item1)
            {
                throw new NotImplementedException("Can not multiply cols != rows");
            }

            var m = Matrix.Create(this.Size.Item1, c2.Size.Item2);
            for (int i = 0; i < this.Size.Item1; i++)
            {
                for (int j = 0; j < c2.Size.Item2; j++)
                {
                    int sum = 0;
                    for (int r = 0; r < c2.Size.Item1; r++)
                    {
                        sum = sum | (((this[i, r] > 0) ? 1 : 0) & ((c2[r, j] > 0) ? 1 : 0));
                    }
                    m[i, j] = sum;
                }
            }

            return m;
        }


        public static Matrix Create(int rows, int cols, double? defaultVal = null)
        {
            var m = Matrix.Create();
            for (int i = 0; i < rows; i++)
            {
                var list = new double?[cols];
                for (int j = 0; j < cols; j++)
                {
                    list[j] = defaultVal;
                }
                m.Vectors.Add(new Vector(list));
            }
            return m;
        }
        public static Matrix Create()
        {
            var m = default(Matrix);
            m.Vectors = new List<Vector>();
            return m;
        }
        public static Matrix Create(int rows, Vector vector)
        {
            var m = Matrix.Create();
            for (int i = 0; i < rows; i++)
            {
                m.Vectors.Add(vector);
            }
            return m;
        }

        public Matrix Transpose()
        {
            var t = Matrix.Create(this.Size.Item2, this.Size.Item1);

            for (int i = 0; i < this.Size.Item1; i++)
            {
                for (int j = 0; j < this.Size.Item2; j++)
                {
                    t[j, i] = this[i, j];
                }
            }
            return t;
        }

        public Matrix ReverseTranspose()
        {
            return Matrix.ReverseIdentity(this.Size.Item2) * this.Transpose() * Matrix.ReverseIdentity(this.Size.Item1);
        }

        public static bool operator !=(Matrix c1, Matrix c2)
        {
            return !(c1 == c2);
        }

        public static bool operator ==(Matrix c1, Matrix c2)
        {
            for (int i = 0; i < c1.Size.Item1; i++)
            {
                if (c1[i] != c2[i]) return false;
            }
            return true;
        }

        public Vector this[int i]
        {
            get
            {
                return Vectors[i];
            }
            set
            {
                Vectors[i] = value;
            }
        }

        public double this[int i, int j]
        {
            get
            {
                return Vectors[i][j];
            }
            set
            {
                var v = Vectors[i];
                v[j] = value;
            }
        }

        public Matrix Submatrix(int i, int j)
        {
            var sub = this.Clone();
            sub.Vectors.RemoveAt(i);
            foreach (var item in sub.Vectors)
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
                trace += this[i, i];
            }
            return trace;
        }

        public Matrix Absolute()
        {
            var m = this.Clone();
            for (int i = 0; i < this.Size.Item1; i++)
            {
                m[i] = this[i].Absolute();
        }
            return m;
        }

        public static Matrix Identity(int n)
        {
            var m = Matrix.Create(n, n, 0);
            for (int i = 0; i < n; i++)
            {
                m[i, i] = 1;
            }
            return m;
        }

        public static Matrix ReverseIdentity(int n)
        {
            var m = Matrix.Create(n, n, 0);
            for (int i = 0; i < n; i++)
            {
                m[i, n - i - 1] = 1;
            }
            return m;
        }

        public Matrix Powers(uint r)
        {
            check_square_matrix();
            if(r == 0)
            {
                return Matrix.Identity(this.Size.Item1);
            }

            return this * this.Powers(r - 1);
        }

        public Matrix BooleanPowers(uint r)
        {
            check_square_matrix();
            var result = Matrix.Identity(this.Size.Item1);
            if (r == 0)
            {
                return result;
            }

            return this.BooleanProduct(this.BooleanPowers(r - 1));
        }

        public Matrix Cofactor()
        {
            check_square_matrix();

            var co = Create(this.Size.Item2, this.Size.Item2);
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
            return Cofactor().Transpose();
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
            var ONE = Matrix.One(this.Size.Item2, 1);

            return (this.Transpose() * ONE).Transpose();
        }

        public Matrix Mean()
        {
            var SUM = Sum();
            var ONE = Matrix.One(Size.Item1, Size.Item2);

            var N = ONE.Sum();
            var M = SUM.Clone();
            M[0] = (SUM[0] / N[0]);
            return M;
        }

        public double GramSchmidtDis(Matrix U)
        {
            var cU1 = U.Transpose() * U;
            var cU2 = this.Transpose() * U;
            return (cU2[0, 0] / cU1[0, 0]);
        }
        public List<Matrix> GramSchmidtU()
        {
            var U = new List<Matrix>();
            var X = this.Cols();
            for (int i = 0; i < X.Count; i++)
            {
                var Ui = X[i].Clone();
                for (int j = 0; j < i; j++)
                {
                    Ui = Ui - X[i].GramSchmidtDis(U[i - 1]) * U[i - 1];
                }
                U.Add(Ui);
            }
            return U;
        }

        public List<Matrix> GramSchmidtZ()
        {
            var U = this.GramSchmidtU();
            var Z = new List<Matrix>();
            for (int i = 0; i < U.Count; i++)
            {
                var z = U[i] * (1 / System.Math.Sqrt((U[i].Transpose() * U[i])[0, 0]));
                Z.Add(z);
            }
            return Z;
        }

        public List<Matrix> Cols()
        {
            var X = new List<Matrix>();

            for (int i = 0; i < this.Size.Item2; i++)
            {
                X.Add(this.Col(i));
            }
            return X;
        }

        public double Determinant()
        {
            check_square_matrix();

            if (this.Size.Item1 == 1)
            {
                return this[0, 0];
            }

            double determinant = 0;
            for (int i = 0; i < this.Size.Item2; i++)
            {
                var sub = this.Submatrix(0, i);

                if (i % 2 == 0)
                {
                    determinant += this[0, i] * sub.Determinant();
                }
                else
                {
                    determinant -= this[0, i] * sub.Determinant();
                }
            }

            return determinant;
        }

        public Matrix Clone()
        {
            var copy = Matrix.Create();
            foreach (var item in this.Vectors)
            {
                copy.Vectors.Add(new Vector(item.Values));
            }

            return copy;
        }

        public static Matrix Row(int i, Matrix m)
        {
            var r = m.e(i).Transpose() * m;
            return r;
        }

        public static Matrix Col(int i, Matrix m)
        {
            var r = m * m.e(i);
            return r;
        }

        public Matrix Col(int i)
        {
            var e = Matrix.Create(this.Size.Item2, 1, 0);
            e[i, 0] = 1;
            var r = this * e;
            return r;
        }

        public bool IsSymmetric
        {
            get
            {
                return this == this.Transpose();
            }
        }

        private Matrix e(int i)
        {
            var e = Matrix.Create(this.Size.Item1, 1, 0);
            e[i, 0] = 1;
            return e;
        }

        public static Matrix E(int i, int j, int n)
        {
            var e = Matrix.Create(n, n, 0);
            e[i, j] = 1;
            return e;
        }

        private void check_square_matrix()
        {
            if (this.Size.Item1 != this.Size.Item2)
            {
                throw new NotImplementedException("It not square matrix");
            }
        }
        private static void check_size_matrix(Matrix c1, Matrix c2)
        {
            if (c1.Size.Item1 != c2.Size.Item1 
                && c1.Size.Item2 != c2.Size.Item2)
            {
                throw new NotImplementedException("It not equals size matrix");
            }
        }

        public static Matrix One(int rows, int columns)
        {
            return Create(rows, columns, 1);
        }

        public override string ToString()
        {
            var result = new List<string>();
            foreach (var vec in this.Vectors)
            {
                result.Add("|" + vec.ToString() + "|");
            }

            return string.Join("", result);
        }

        public override bool Equals(object obj)
        {
            return this == (Matrix)obj;
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
    }
}
