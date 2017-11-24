using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chavp.Math.Tests.Models;
using System.Collections.Generic;

namespace Chavp.Math.Tests
{
    [TestClass]
    public class VectorTest
    {
        [TestMethod]
        public void plus()
        {
            var v1 = new Vector(1, 2);
            var v2 = new Vector(3, 4);
            var v4 = new Vector(1, 2);

            var v3 = v1 + v2;

            Console.WriteLine(v3);

            Assert.IsFalse(v1 == v2);
            Assert.IsTrue(v1 != v2);
            Assert.IsTrue(v1 == v4);
        }

        [TestMethod]
        public void magnitude()
        {
            var v1 = new Vector(6, 8);

            Console.WriteLine(v1.Magnitude());
        }

        [TestMethod]
        public void cross_product()
        {
            var v1 = new Vector(1, 0, 0);
            var v2 = new Vector(0, 1, 0);

            var v3 = v1.Cross(v2);

            Console.WriteLine(v3);
        }

        [TestMethod]
        public void dot_product()
        {
            var v1 = new Vector(3, 0);
            var v2 = new Vector(0, 4);

            var dot = v1.Dot(v2);

            Console.WriteLine(dot);
        }

        [TestMethod]
        public void num_product()
        {
            var v1 = new Vector(-6, 8);
            var v2 = new Vector(5, 12);

            var dot = v1.Dot(v2);
            var m1 = v1.Magnitude();
            var m2 = v2.Magnitude();
            Console.WriteLine("COS(x) = {0}", dot / (m1 * m2) );
            double mycalcInRadians = System.Math.Acos(dot / (m1 * m2));
            double mycalcInDegrees = (mycalcInRadians * 180 / System.Math.PI);
            Console.WriteLine("COS(-1)(x) = {0}", mycalcInDegrees);

            
        }
    }
}
