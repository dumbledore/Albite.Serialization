using Albite.Test;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Albite.Serialization.Test
{
    [TestClass]
    public class SerializerTypeTestWP : UnitTest
    {
        private readonly SerializerTypeTest _test = new SerializerTypeTest();

        [TestMethod]
        public void TestTypes()
        {
            _test.TestTypes();
        }
    }
}
