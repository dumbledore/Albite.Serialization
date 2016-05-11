using Albite.Test;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Albite.Serialization.Test
{
    [TestClass]
    public class SerializerEnumTestW : UnitTest
    {
        private readonly SerializerEnumTest _test = new SerializerEnumTest();

        [TestMethod]
        public void TestEnum()
        {
            _test.TestEnum();
        }
    }
}
