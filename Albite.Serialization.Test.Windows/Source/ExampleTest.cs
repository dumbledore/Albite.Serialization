using Albite.Test;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Albite.Serialization.Test.Windows
{
    [TestClass]
    public class SerializerExampleTest : UnitTest
    {
        private readonly Test.ExampleTest _test = new Test.ExampleTest();

        [TestMethod]
        public void SimpleExample()
        {
            _test.SimpleExample();
        }

        [TestMethod]
        public void SimpleClassExample()
        {
            _test.SimpleClassExample();
        }
    }
}
