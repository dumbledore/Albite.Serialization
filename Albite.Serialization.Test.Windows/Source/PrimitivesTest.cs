using Albite.Test;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Albite.Serialization.Test.Windows
{
    [TestClass]
    public class SerializerPrimitivesTest : UnitTest
    {
        private readonly Test.PrimitivesTest _test = new Test.PrimitivesTest();

        [TestMethod]
        public void Primitives()
        {
            _test.Primitives();
        }
    }
}
