using Albite.Test;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Albite.Serialization.Test.Windows
{
    [TestClass]
    public class ArrayTest : UnitTest
    {
        private readonly Test.ArrayTest _test = new Test.ArrayTest();

        [TestMethod]
        public void ByteArray()
        {
            _test.ByteArray();
        }
    }
}
