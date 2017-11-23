using System;
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

            var m1 = new Matrix();
            m1.Elements.Add(vec1);
            m1.Elements.Add(vec2);

            Console.WriteLine("Size(m1) = {0} x {1}", m1.Size.Item1, m1.Size.Item2);

            var m2 = new Matrix();
            m2.Elements.Add(new Vector(0, 0, 5));
            m2.Elements.Add(new Vector(7, 5, 0));

            Console.WriteLine("Size(m2) = {0} x {1}", m2.Size.Item1, m2.Size.Item2);

            var m3 = m1 + m2;

            Console.WriteLine("{0} + {1} = {2}", m1, m2, m3);

            var m4 = 2 * m3;

            var m5 = new Matrix
            {
                Elements = new List<Vector>
                {
                    new Vector(1, 2, 3),
                    new Vector(0, -6, 7)
                }
            };

            Console.WriteLine("2 * m3 = {0}", m4);
            Console.WriteLine("m5' = {0}", ~m5);

        }

        [TestMethod]
        public void multiplication()
        {
            var A = new Matrix
            {
                Elements = new List<Vector>
                {
                    new Vector(2, 3, 4),
                    new Vector(1, 0, 0)
                }
            };

            var B = new Matrix
            {
                Elements = new List<Vector>
                {
                    new Vector(0, 1000),
                    new Vector(1, 100),
                    new Vector(0, 10)
                }
            };

            var C = A * B;

            Console.WriteLine("A * B = {0}", C);
        }

        [TestMethod]
        public void submatrix()
        {
            var A = new Matrix
            {
                Elements = new List<Vector>
                {
                    new Vector(1, 2, 3, 4),
                    new Vector(5, 6, 7, 8),
                    new Vector(9, 10, 11, 12)
                }
            };

            var B = A.Submatrix(2, 1);

            Console.WriteLine("B = {0}", B);
        }

        [TestMethod]
        public void trace()
        {
            var A = new Matrix
            {
                Elements = new List<Vector>
                {
                    new Vector(-1, 0, 3),
                    new Vector(11, 5, 2),
                    new Vector(6, 12, -6)
                }
            };

            Assert.AreEqual(-2, A.Trace());

            var TA = ~A;
            Assert.AreEqual(TA.Trace(), A.Trace());
        }

        [TestMethod]
        public void determinant()
        {
            var A = new Matrix
            {
                Elements = new List<Vector>
                {
                    new Vector(-1, 3),
                    new Vector(2, -1)
                }
            };

            Assert.AreEqual(-5, A.Determinant());
        }

        [TestMethod]
        public void determinant_3()
        {
            var A = new Matrix
            {
                Elements = new List<Vector>
                {
                    new Vector(-2, 2, -3),
                    new Vector(-1, 1, 3),
                    new Vector(2, 0, -1),
                }
            };

            Assert.AreEqual(18, A.Determinant());
        }

        [TestMethod]
        public void determinant_4()
        {
            var A = new Matrix
            {
                Elements = new List<Vector>
                {
                    new Vector(1, 5, 4, 2),
                    new Vector(-2, 3, 6, 4),
                    new Vector(5, 1, 0, -1),
                    new Vector(2, 3, -4, 0),
                }
            };

            Console.WriteLine(A.Determinant());
        }

        [TestMethod]
        public void adjugate()
        {
            var A = new Matrix
            {
                Elements = new List<Vector>
                {
                    new Vector(-3, 2, -5),
                    new Vector(-1, 0, -2),
                    new Vector(3, -4, 1),
                }
            };

            Console.WriteLine(A.Adjugate());
        }

        [TestMethod]
        public void invert()
        {
            var A = new Matrix
            {
                Elements = new List<Vector>
                {
                    new Vector(-3, 2, -5),
                    new Vector(-1, 0, -2),
                    new Vector(3, -4, 1),
                }
            };

            var vert_A = A.Invert();

            Console.WriteLine(A * vert_A);
        }

        [TestMethod]
        public void sum()
        {
            var A = new Matrix
            {
                Elements = new List<Vector>
                {
                    new Vector(1, 4, 3),
                    new Vector(2, 5, 2),
                    new Vector(1, 6, 4),
                }
            };

            Console.WriteLine("Sum = {0}", A.Sum());
            Console.WriteLine("Mean = {0}", A.Mean());

            var M = Matrix.Init(A.Size.Item1, A.Mean()[0]);

            var DIF_M = A - M;

            Console.WriteLine("DIF_M = {0}", DIF_M);

            Console.WriteLine("DIF_M_SQ = {0}", ~DIF_M * DIF_M);
        }
    }
}
