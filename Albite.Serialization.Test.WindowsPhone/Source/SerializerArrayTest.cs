using Albite.Test;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Albite.Serialization.Test.WindowsPhone
{
    [TestClass]
    public class SerializerArrayTest : UnitTest
    {
        private readonly Test.SerializerArrayTest _test = new Test.SerializerArrayTest();

        [TestMethod]
        public void SerializeByteArray()
        {
            _test.SerializeByteArray();
        }
    }
}
