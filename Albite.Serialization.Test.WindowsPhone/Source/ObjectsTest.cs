using Albite.Serialization.Test.Objects;
using Albite.Test;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Albite.Serialization.Test.WindowsPhone
{
    [TestClass]
    public class SerializerObjectsTest : UnitTest
    {
        [TestMethod]
        public void VariousObjects()
        {
            new VariousObjectsTest().Test();
        }

        [TestMethod]
        public void RecursiveObjects()
        {
            new RecursiveObjectsTest().Test();
        }

        [TestMethod]
        public void CustomAttributes()
        {
            new CustomAttributesTest().Test();
        }

        [TestMethod]
        public void Structs()
        {
            new StructTest().Test();
        }

        [TestMethod]
        public void ArrayOfInterfaces()
        {
            new ArrayOfInterfacesTest().Test();
        }

        [TestMethod]
        public void Generics()
        {
            new GenericsTest().Test();
        }

        [TestMethod]
        public void SemiSerialized()
        {
            new SemiSerializedTest().Test();
        }

        [TestMethod]
        public void CustomList()
        {
            new CustomListTest().Test();
        }

        [TestMethod]
        public void NullObjects()
        {
            new NullObjectTest().Test();
        }

        [TestMethod]
        public void AbstractClasses()
        {
            new AbstractClassTest().Test();
        }

        [TestMethod]
        public void ClassWithIndexer()
        {
            new ClassWithIndexerTest().Test();
        }

        [TestMethod]
        public void ClassWithReadOnlyWriteOnlyProperties()
        {
            new ClassWithReadOnlyWriteOnlyPropertiesTest().Test();
        }
    }
}
