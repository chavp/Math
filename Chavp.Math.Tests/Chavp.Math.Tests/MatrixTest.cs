using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chavp.Math.Tests.Models;
using System.Collections.Generic;

namespace Chavp.Math.Tests
{
    [TestClass]
    public class MatrixTest
    {
        // https://en.wikipedia.org/wiki/Adjugate_matrix

        [TestMethod]
        public void vector_1()
        {
            var vec1 = new Vector(1, 3, 1);
            var vec2 = new Vector(1, 0, 0);

            var m1 = new Matrix(vec1, vec2);

            Console.WriteLine("Size(m1) = {0} x {1}", m1.Size.Item1, m1.Size.Item2);

            var m2 = new Matrix(new Vector(0, 0, 5), new Vector(7, 5, 0));

            Console.WriteLine("Size(m2) = {0} x {1}", m2.Size.Item1, m2.Size.Item2);

            var m3 = m1 + m2;

            Console.WriteLine("{0} + {1} = {2}", m1, m2, m3);

            var m4 = 2 * m3;

            var m5 = new Matrix(new Vector(1, 2, 3), new Vector(0, -6, 7));

            Console.WriteLine("2 * m3 = {0}", m4);
            Console.WriteLine("m5' = {0}", m5.Transpose());

        }

        [TestMethod]
        public void multiplication()
        {
            var A = new Matrix
            (
                new Vector(1, 1),
                    new Vector(2, 1)
                    );

            var B = new Matrix
            (
                new Vector(2, 1),
                    new Vector(1, 1)
            );

            Assert.AreEqual(new Matrix
            (
                new Vector(3, 2),
                    new Vector(5, 3)
            ), A * B);

            Assert.AreEqual(new Matrix
            (
                new Vector(4, 3),
                    new Vector(3, 2)
            ), B * A);
        }

        [TestMethod]
        public void submatrix()
        {
            var A = new Matrix
            (new Vector(1, 2, 3, 4),
                    new Vector(5, 6, 7, 8),
                    new Vector(9, 10, 11, 12));

            var B = A.Submatrix(2, 1);

            Console.WriteLine("B = {0}", B);
        }

        [TestMethod]
        public void trace()
        {
            var A = new Matrix
            (new Vector(-1, 0, 3),
                    new Vector(11, 5, 2),
                    new Vector(6, 12, -6));

            Assert.AreEqual(-2, A.Trace());

            var TA = A.Transpose();
            Assert.AreEqual(TA.Trace(), A.Trace());
        }

        [TestMethod]
        public void determinant()
        {
            var A = new Matrix
            (new Vector(-1, 3),
                    new Vector(2, -1)
            );

            Assert.AreEqual(-5, A.Determinant());
        }

        [TestMethod]
        public void determinant_3()
        {
            var A = new Matrix
            (new Vector(-2, 2, -3),
                    new Vector(-1, 1, 3),
                    new Vector(2, 0, -1));

            Assert.AreEqual(18, A.Determinant());
        }

        [TestMethod]
        public void determinant_4()
        {
            var A = new Matrix
            (new Vector(1, 5, 4, 2),
                    new Vector(-2, 3, 6, 4),
                    new Vector(5, 1, 0, -1),
                    new Vector(2, 3, -4, 0));

            Console.WriteLine(A.Determinant());
        }

        [TestMethod]
        public void adjugate()
        {
            var A = new Matrix
            (new Vector(-3, 2, -5),
                    new Vector(-1, 0, -2),
                    new Vector(3, -4, 1));

            Console.WriteLine(A.Adjugate());
        }

        [TestMethod]
        public void invert()
        {
            var A = new Matrix
            (new Vector(-3, 2, -5),
                    new Vector(-1, 0, -2),
                    new Vector(3, -4, 1));

            var vert_A = A.Invert();

            Console.WriteLine(A * vert_A);
        }

        [TestMethod]
        public void sum()
        {
            var A = new Matrix
            (new Vector(1, 4, 3),
                    new Vector(2, 5, 2),
                    new Vector(1, 6, 4));

            Console.WriteLine("Sum = {0}", A.Sum());
            Console.WriteLine("Mean = {0}", A.Mean());

            var M = Matrix.Create(A.Size.Item1, A.Mean()[0]);

            var DIF_M = A - M;

            Console.WriteLine("DIF_M = {0}", DIF_M);

            Console.WriteLine("DIF_M_SQ = {0}", DIF_M.Transpose() * DIF_M);


        }

        [TestMethod]
        public void are_Equals()
        {
            var A = new Matrix
            (new Vector(-1, 0, 3),
                    new Vector(11, 5, 2),
                    new Vector(6, 12, -6));

            var B = new Matrix
            (new Vector(-1, 1, 3),
                    new Vector(11, 5, 2),
                    new Vector(6, 12, -6));

            var C = new Matrix
            (new Vector(-1, 0, 3),
                    new Vector(11, 5, 2),
                    new Vector(6, 12, -6));

            Assert.IsFalse(A == B);
            Assert.IsTrue(A != B);
            Assert.IsTrue(A == C);
        }

        [TestMethod]
        public void identity()
        {
            var A = new Matrix
            (new Vector(-1, 0, 3),
                    new Vector(11, 5, 2),
                    new Vector(6, 12, -6));

            var I = Matrix.Identity(A.Size.Item1);

            // A * I = A
            Assert.AreEqual(A, A * I);

            // A * inv A = I
            Assert.AreEqual(I, A * A.Invert());
        }

        [TestMethod]
        public void row()
        {
            var A = new Matrix
            (new Vector(-1, 0, 3),
                    new Vector(11, 5, 2),
                    new Vector(6, 12, -6));

            Assert.AreEqual(A[0], Matrix.Row(0, A)[0]);
            Assert.AreEqual(A[1], Matrix.Row(1, A)[0]);
            Assert.AreEqual(A[2], Matrix.Row(2, A)[0]);
        }

        [TestMethod]
        public void col()
        {
            var A = new Matrix
            (new Vector(-1, 0, 3),
                    new Vector(11, 5, 2),
                    new Vector(6, 12, -6));

            Console.WriteLine(Matrix.Col(0, A));
            Console.WriteLine(Matrix.Col(1, A));
            Console.WriteLine(Matrix.Col(2, A));
        }

        [TestMethod]
        public void E()
        {
            var d = 5;
            var list = new List<Matrix>();
            Matrix I = Matrix.Create(d, d, 0);
            for (int i = 0; i < d; i++)
            {
                list.Add(Matrix.E(i, i, d));
            }

            foreach (var item in list)
            {
                I = I + item;
            }

            Assert.AreEqual(Matrix.Identity(d), I);
        }

        [TestMethod]
        public void orthogonal()
        {
            var x = new Matrix(new Vector(3, 0));
            var y = new Matrix(new Vector(0, 4));

            var xy = x * y.Transpose();

            Assert.AreEqual(new Matrix(new Vector(0)), xy);
            Assert.AreEqual(0, xy.Trace());
        }

        [TestMethod]
        public void angle()
        {
            var x = new Matrix(new Vector(-6, 8));
            var y = new Matrix(new Vector(5, 12));

            var xy = x * y.Transpose();
            var xx = x * x.Transpose();
            var yy = y * y.Transpose();

            var xxyy = xx * yy;
            var v = xy[0, 0] / System.Math.Pow(xx[0,0] * yy[0,0], 0.5);

            double mycalcInRadians = System.Math.Acos(v);
            double mycalcInDegrees = System.Math.Round((mycalcInRadians * 180 / System.Math.PI));

            Assert.AreEqual(59, mycalcInDegrees);
        }

        [TestMethod]
        public void reverse_transpose()
        {
            var A = new Matrix(
                    new Vector(1, 2, 3),
                    new Vector(4, 5, 6)
                );

            Assert.AreEqual(new Matrix(
                    new Vector(6, 3),
                    new Vector(5, 2),
                    new Vector(4, 1)
                ), A.ReverseTranspose());
        }

        [TestMethod]
        public void absolute()
        {
            var A = new Matrix
            (new Vector(-1, -1),
                    new Vector(-2, -2));

            Assert.AreEqual(new Matrix
            (new Vector(1, 1),
                    new Vector(2, 2)), A.Absolute());
        }

        [TestMethod]
        public void powers()
        {
            var A = new Matrix
            (new Vector(2, 2),
                    new Vector(2, 2));

            Assert.AreEqual(Matrix.Identity(2), A.Powers(0));
            Assert.AreEqual(new Matrix
            (new Vector(8, 8),
                    new Vector(8, 8)), A.Powers(2));
        }

        [TestMethod]
        public void isSymmetric()
        {
            var A = new Matrix
            (
                new Vector(1, 1, 0),
                new Vector(1, 0, 1),
                new Vector(0, 1, 0)
            );

            Assert.IsTrue(A.IsSymmetric);
        }

        [TestMethod]
        public void booleanProduct()
        {
            var A = new Matrix
            (
                new Vector(1, 0),
                new Vector(0, 1),
                new Vector(1, 0)
            );

            var B = new Matrix
            (
                new Vector(1, 1, 0),
                new Vector(0, 1, 1)
            );

            var C = A.BooleanProduct(B);

            Assert.AreEqual(new Matrix
            (
                new Vector(1, 1, 0),
                new Vector(0, 1, 1),
                new Vector(1, 1, 0)
            ), C);
        }

        [TestMethod]
        public void booleanPowers()
        {
            var A = new Matrix
            (
                new Vector(0, 0, 1),
                new Vector(1, 0, 0),
                new Vector(1, 1, 0)
            );

            Assert.AreEqual(new Matrix
            (
                new Vector(1, 1, 0),
                new Vector(0, 0, 1),
                new Vector(1, 0, 1)
            ), A.BooleanPowers(2));

            Assert.AreEqual(new Matrix
            (
                new Vector(1, 0, 1),
                new Vector(1, 1, 0),
                new Vector(1, 1, 1)
            ), A.BooleanPowers(3));

            Assert.AreEqual(new Matrix
            (
                new Vector(1, 1, 1),
                new Vector(1, 0, 1),
                new Vector(1, 1, 1)
            ), A.BooleanPowers(4));

            Assert.AreEqual(new Matrix
            (
                new Vector(1, 1, 1),
                new Vector(1, 1, 1),
                new Vector(1, 1, 1)
            ), A.BooleanPowers(5));
        }

        [TestMethod]
        public void gram_schmidt_process()
        {
            var M = new Matrix(
                new Vector(4, 3),
                new Vector(0, 1),
                new Vector(0, 0),
                new Vector(2, -1)
                );

            List<Matrix> X = new List<Matrix>();

            for (int i = 0; i < M.Size.Item2; i++)
            {
                X.Add(M.Col(i));
            }

            var X1 = X[0];
            var X2 = X[1];

            List<Matrix> U = M.GramSchmidtU();
            
            //U.Add(U1);

            for (int i = 0; i < X.Count; i++)
            {
                var Ui = X[i].Clone();
                for (int j = 0; j < i; j++)
                {
                    Ui = Ui - X[i].GramSchmidtDis(U[i - 1]) * U[i - 1];
                }
                U.Add(Ui);
            }

            var U1 = U[0];
            var U2 = U[1];

            List<Matrix> Z = M.GramSchmidtZ();
            var Z1 = Z[0];
            var Z2 = Z[1];

            var I1 = Z1.Transpose() * Z1;
            var I2 = Z2.Transpose() * Z2;

            var UU = U1.Transpose() * U2;
            var ZZ = Z1.Transpose() * Z2;

            Assert.AreEqual(1 , I1[0, 0]);
            Assert.AreEqual(1, I2[0, 0]);

            Assert.AreEqual(0, UU[0, 0]);
            Assert.AreEqual(0, ZZ[0, 0]);
        }

        [TestMethod]
        public void spectral_decomposition_example()
        {
            var A = new Matrix(
                new Vector(13, -4, 2),
                new Vector(-4, 13, -2),
                new Vector(2, -2, 10)
                );

            for (int i = 0; i < 20; i++)
            {
                var D = (A - i * Matrix.Identity(A.Size.Item1)).Determinant();
                if (D == 0)
                    Console.WriteLine(i);
            }

            
        }
    }
}
