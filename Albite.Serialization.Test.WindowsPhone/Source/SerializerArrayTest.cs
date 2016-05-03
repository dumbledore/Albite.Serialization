using Albite.Core.Test;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Albite.Serialization.Test
{
    [TestClass]
    public class SerializerArrayTestWP : UnitTest
    {
        private readonly SerializerArrayTest _test = new SerializerArrayTest();

        [TestMethod]
        public void SerializeByteArray()
        {
            _test.SerializeByteArray();
        }
    }
}
