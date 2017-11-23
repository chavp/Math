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

            var v3 = v1 + v2;

            Console.WriteLine(v3);
        }
    }
}
