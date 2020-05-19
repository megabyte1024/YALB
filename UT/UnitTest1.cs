using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using YALB;

namespace UT
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var ben = new Benchmark();
            ben.Setup();
            var parserNew = new FastContactHydrator2();
            var parserRef = new ManualContactHydrator();
            foreach (var dataItem in ben.GetBenchData())
            {
                Contact contactNew = parserNew.HydrateWithLinq2(dataItem);
                Contact contactRef = parserRef.HydrateWithoutLinq(dataItem);
                Assert.AreEqual(contactRef.FullName, contactNew.FullName);
                Assert.AreEqual(contactRef.Age, contactNew.Age);
                Assert.AreEqual(contactRef.Phone, contactNew.Phone);
            }
        }
    }
}
