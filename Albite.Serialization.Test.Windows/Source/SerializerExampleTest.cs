using Albite.Test;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Albite.Serialization.Test.Windows
{
    [TestClass]
    public class SerializerExampleTest : UnitTest
    {
        private readonly Test.SerializerExampleTest _test = new Test.SerializerExampleTest();

        [TestMethod]
        public void SimpleExample()
        {
            _test.SimpleExample();
        }

        [TestMethod]
        public void SimpleClassExampleTest()
        {
            _test.SimpleClassExampleTest();
        }
    }
}
