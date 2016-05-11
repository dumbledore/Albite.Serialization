using Albite.Test;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Albite.Serialization.Test.Windows
{
    [TestClass]
    public class SerializerCollectionTest : UnitTest
    {
        private readonly Test.SerializerCollectionTest _test = new Test.SerializerCollectionTest();

        [TestMethod]
        public void ListTest()
        {
            _test.ListTest();
        }

        [TestMethod]
        public void LinkedListTest()
        {
            _test.LinkedListTest();
        }

        [TestMethod]
        public void StackTest()
        {
            _test.StackTest();
        }

        [TestMethod]
        public void QueueTest()
        {
            _test.QueueTest();
        }

        [TestMethod]
        public void HashSetTest()
        {
            _test.HashSetTest();
        }

        [TestMethod]
        public void SortedSetTest()
        {
            _test.SortedSetTest();
        }
    }
}
