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

        private void testCollection(IEnumerable<MyEnum> collection)
        {
            List<MyEnum> values = new List<MyEnum>(collection);
            List<MyEnum> valuesRead = new List<MyEnum>((IEnumerable<MyEnum>)Helper.Test(collection));
            CollectionAssert.AreEqual(values, valuesRead);
        }
    }
}
