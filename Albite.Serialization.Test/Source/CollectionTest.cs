using Albite.Collections;
using Albite.Test;
using System.Collections.Generic;

namespace Albite.Serialization.Test
{
    public class CollectionTest
    {
        private enum MyEnum
        {
            Zero,
            One,
            Ten = 10,
        }

        private static readonly MyEnum[] Sample = { MyEnum.Zero, MyEnum.Zero, MyEnum.Ten, MyEnum.One, };

        public void Lists()
        {
            List<MyEnum> list = new List<MyEnum>();
            foreach (var e in Sample) list.Add(e);
            testCollection(list);
        }

        public void LinkedLists()
        {
            LinkedList<MyEnum> list = new LinkedList<MyEnum>();
            foreach (var e in Sample) list.AddLast(e);
            testCollection(list);
        }

        public void Stacks()
        {
            Stack<MyEnum> stack = new Stack<MyEnum>();
            foreach (var e in Sample) stack.Push(e);
            testCollection(stack);
        }

        public void Queues()
        {
            List<MyEnum> queue = new List<MyEnum>();
            foreach (var e in Sample) queue.Add(e);
            testCollection(queue);
        }

        public void HashSets()
        {
            HashSet<MyEnum> set = new HashSet<MyEnum>();
            foreach (var e in Sample) set.Add(e);
            testCollection(set);
        }

        public void SortedSets()
        {
            SortedSet<MyEnum> set = new SortedSet<MyEnum>();
            foreach (var e in Sample) set.Add(e);
            testCollection(set);
        }

        private void testCollection<E>(IEnumerable<E> collection)
        {
            List<E> values = new List<E>(collection);
            List<E> valuesRead = new List<E>((IEnumerable<E>)Helper.Test(collection));
            CollectionAssert.AreEqual(values, valuesRead);
        }

        public void TreeTest()
        {
            // This one is used so we can add the children to it
            Node<string> dummy = new Node<string>();

            dummy.AppendChild(new Node<string>("1"));
            dummy.LastChild.AppendChild(new Node<string>("1.1"));
            dummy.LastChild.AppendChild(new Node<string>("1.2"));

            dummy.AppendChild(new Node<string>("2"));
            dummy.LastChild.AppendChild(new Node<string>("2.1"));
            dummy.LastChild.LastChild.AppendChild(new Node<string>("2.1.1"));
            dummy.LastChild.LastChild.AppendChild(new Node<string>("2.1.2"));

            dummy.AppendChild(new Node<string>("3"));
            dummy.LastChild.AppendChild(new Node<string>("3.1"));

            testCollection(new Tree<string>(dummy.FirstChild));
        }

        public void CircularBufferTest()
        {
            CircularBufferQueue<string> q = new CircularBufferQueue<string>(4);
            q.Enqueue("a");
            q.Enqueue("b");
            q.Enqueue("c");
            q.Enqueue("d");
            q.Enqueue("e");
            testCollection(q);

            CircularBufferStack<int> s = new CircularBufferStack<int>(10);
            s.Push(1);
            s.Push(2);
            testCollection(s);
        }
    }
}
