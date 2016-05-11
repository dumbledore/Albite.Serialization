using Albite.Test;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Albite.Serialization.Test
{
    [TestClass]
    public class SerializerPrimitivesTestWP : UnitTest
    {
        private readonly SerializerPrimitivesTest _test = new SerializerPrimitivesTest();

        [TestMethod]
        public void TestPrimitives()
        {
            _test.TestPrimitives();
        }
    }
}
