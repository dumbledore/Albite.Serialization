using Albite.Test;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Albite.Serialization.Test.Windows
{
    [TestClass]
    public class SerializerTypeTest : UnitTest
    {
        private readonly Test.TypeTest _test = new Test.TypeTest();

        [TestMethod]
        public void Types()
        {
            _test.Types();
        }
    }
}
