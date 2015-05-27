using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Collections.Generic;

namespace Albite.Serialization.Test.Windows
{
    [TestClass]
    public class SerializerCollectionTest : SerializerTest
    {
        private enum E
        {
            Zero,
            One,
            Ten = 10,
        }

        private static readonly E[] Sample = { E.Zero, E.Zero, E.Ten, E.One, };

        [TestMethod]
        public void ListTest()
        {
            List<E> list = new List<E>();
            foreach (var e in Sample) list.Add(e);
            test(list);
        }

        [TestMethod]
        public void LinkedListTest()
        {
            LinkedList<E> list = new LinkedList<E>();
            foreach (var e in Sample) list.AddLast(e);
            test(list);
        }

        [TestMethod]
        public void StackTest()
        {
            Stack<E> stack = new Stack<E>();
            foreach (var e in Sample) stack.Push(e);
            test(stack);
        }

        [TestMethod]
        public void QueueTest()
        {
            List<E> queue = new List<E>();
            foreach (var e in Sample) queue.Add(e);
            test(queue);
        }

        [TestMethod]
        public void HashSetTest()
        {
            HashSet<E> set = new HashSet<E>();
            foreach (var e in Sample) set.Add(e);
            test(set);
        }

        [TestMethod]
        public void SortedSetTest()
        {
            SortedSet<E> set = new SortedSet<E>();
            foreach (var e in Sample) set.Add(e);
            test(set);
        }
    }
}
