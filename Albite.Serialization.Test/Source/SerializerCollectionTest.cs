﻿using Albite.Test;
using System.Collections.Generic;

namespace Albite.Serialization.Test
{
    public class SerializerCollectionTest
    {
        private enum E
        {
            Zero,
            One,
            Ten = 10,
        }

        private static readonly E[] Sample = { E.Zero, E.Zero, E.Ten, E.One, };

        public void ListTest()
        {
            List<E> list = new List<E>();
            foreach (var e in Sample) list.Add(e);
            testCollection(list);
        }

        public void LinkedListTest()
        {
            LinkedList<E> list = new LinkedList<E>();
            foreach (var e in Sample) list.AddLast(e);
            testCollection(list);
        }

        public void StackTest()
        {
            Stack<E> stack = new Stack<E>();
            foreach (var e in Sample) stack.Push(e);
            testCollection(stack);
        }

        public void QueueTest()
        {
            List<E> queue = new List<E>();
            foreach (var e in Sample) queue.Add(e);
            testCollection(queue);
        }

        public void HashSetTest()
        {
            HashSet<E> set = new HashSet<E>();
            foreach (var e in Sample) set.Add(e);
            testCollection(set);
        }

        public void SortedSetTest()
        {
            SortedSet<E> set = new SortedSet<E>();
            foreach (var e in Sample) set.Add(e);
            testCollection(set);
        }

        private void testCollection(IEnumerable<E> collection)
        {
            List<E> values = new List<E>(collection);
            List<E> valuesRead = new List<E>((IEnumerable<E>)Helper.Test(collection));
            CollectionAssert.AreEqual(values, valuesRead);
        }
    }
}
