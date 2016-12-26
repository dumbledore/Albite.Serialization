using Albite.Test;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Albite.Serialization.Test.Windows
{
    [TestClass]
    public class CollectionTest : UnitTest
    {
        private readonly Test.CollectionTest _test = new Test.CollectionTest();

        [TestMethod]
        public void Lists()
        {
            _test.Lists();
        }

        [TestMethod]
        public void LinkedLists()
        {
            _test.LinkedLists();
        }

        [TestMethod]
        public void Stacks()
        {
            _test.Stacks();
        }

        [TestMethod]
        public void Queues()
        {
            _test.Queues();
        }

        [TestMethod]
        public void HashSets()
        {
            _test.HashSets();
        }

        [TestMethod]
        public void SortedSets()
        {
            _test.SortedSets();
        }

        [TestMethod]
        public void CircularBufferTest()
        {
            _test.CircularBufferTest();
        }

        [TestMethod]
        public void TreeTest()
        {
            _test.TreeTest();
        }
    }
}
