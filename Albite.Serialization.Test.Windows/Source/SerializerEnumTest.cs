using Albite.Test;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Albite.Serialization.Test.Windows
{
    [TestClass]
    public class SerializerEnumTest : UnitTest
    {
        private readonly Test.SerializerEnumTest _test = new Test.SerializerEnumTest();

        [TestMethod]
        public void TestEnum()
        {
            _test.TestEnum();
        }
    }
}
