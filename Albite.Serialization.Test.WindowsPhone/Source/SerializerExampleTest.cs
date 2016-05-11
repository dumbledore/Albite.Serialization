using Albite.Test;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Albite.Serialization.Test
{
    [TestClass]
    public class SerializerExampleTestWP : UnitTest
    {
        private readonly SerializerExampleTest _test = new SerializerExampleTest();

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
