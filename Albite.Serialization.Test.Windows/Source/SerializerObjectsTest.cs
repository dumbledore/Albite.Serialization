using Albite.Test;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Albite.Serialization.Test
{
    [TestClass]
    public class SerializerObjectsTestW : UnitTest
    {
        private readonly SerializerObjectsTest _test = new SerializerObjectsTest();

        [TestMethod]
        public void SerializeObjects()
        {
            _test.SerializeObjects();
        }

        [TestMethod]
        public void SerializeRecursiveObjects()
        {
            _test.SerializeRecursiveObjects();
        }

        [TestMethod]
        public void SerializeCustomAttributes()
        {
            _test.SerializeCustomAttributes();
        }

        [TestMethod]
        public void SerializeStruct()
        {
            _test.SerializeStruct();
        }

        [TestMethod]
        public void SerializeArrayOfInterfaces()
        {
            _test.SerializeArrayOfInterfaces();
        }

        [TestMethod]
        public void GenericsTest()
        {
            _test.GenericsTest();
        }

        [TestMethod]
        public void SemiSerializedTest()
        {
            _test.SemiSerializedTest();
        }

        [TestMethod]
        public void CustomListTest()
        {
            _test.CustomListTest();
        }

        [TestMethod]
        public void NullObjectTest()
        {
            _test.NullObjectTest();
        }

        [TestMethod]
        public void AbstractClassTest()
        {
            _test.AbstractClassTest();
        }

        [TestMethod]
        public void ClassWithIndexerTest()
        {
            _test.ClassWithIndexerTest();
        }
    }
}
