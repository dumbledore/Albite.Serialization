using Albite.Test;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Albite.Serialization.Test.Windows
{
    [TestClass]
    public class SerializerEnumTest : UnitTest
    {
        private readonly Test.EnumTest _test = new Test.EnumTest();

        [TestMethod]
        public void Enums()
        {
            _test.Enums();
        }
    }
}
