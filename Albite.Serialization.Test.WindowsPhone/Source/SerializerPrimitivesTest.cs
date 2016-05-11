using Albite.Test;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Albite.Serialization.Test.WindowsPhone
{
    [TestClass]
    public class SerializerPrimitivesTest : UnitTest
    {
        private readonly Test.SerializerPrimitivesTest _test = new Test.SerializerPrimitivesTest();

        [TestMethod]
        public void TestPrimitives()
        {
            _test.TestPrimitives();
        }
    }
}
