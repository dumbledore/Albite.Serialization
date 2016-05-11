using Albite.Test;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Albite.Serialization.Test.Windows
{
    [TestClass]
    public class SerializerTypeTest : UnitTest
    {
        private readonly Test.SerializerTypeTest _test = new Test.SerializerTypeTest();

        [TestMethod]
        public void TestTypes()
        {
            _test.TestTypes();
        }
    }
}
