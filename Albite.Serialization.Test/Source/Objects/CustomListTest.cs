using Albite.Test;
using System.Collections.Generic;
using System.Linq;

namespace Albite.Serialization.Test.Objects
{
    public class CustomListTest
    {
        private class CustomList<T> : List<T>
        {
            [Serialized]
            private int _x;

            protected CustomList() { }

            public CustomList(int x)
            {
                _x = x;
            }

            public override bool Equals(object obj)
            {
                CustomList<T> other = obj as CustomList<T>;
                return (other == null) ? false : (_x == other._x && this.SequenceEqual(other));
            }

            public override int GetHashCode()
            {
                return _x;
            }
        }

        private class MoreCustomList<T> : CustomList<T>
        {
            [Serialized]
            private int _y;

            protected MoreCustomList() { }

            public MoreCustomList(int x, int y) : base(x)
            {
                _y = y;
            }

            public override bool Equals(object obj)
            {
                MoreCustomList<T> other = obj as MoreCustomList<T>;
                return (other == null) ? false : (_y == other._y && base.Equals(other));
            }

            public override int GetHashCode()
            {
                return _y;
            }
        }

        public void Test()
        {
            CustomList<string> x = new CustomList<string>(1);
            x.AddRange(new string[] { "One", });
            CustomList<string> xRead = (CustomList<string>)Helper.Test(x);
            CollectionAssert.AreEqual(x, xRead);

            MoreCustomList<string> y = new MoreCustomList<string>(10, 20);
            y.AddRange(new string[] { "Ten", "Twenty," });
            IList<string>[] arr = { x, x, y, };
            IList<string>[] arrRead = (IList<string>[])Helper.Test(arr);
            Assert.AreEqual(arr[0], arrRead[0]);
            CollectionAssert.AreEqual(arr, arrRead);
            Assert.AreSame(arrRead[0], arrRead[1]);
        }
    }
}
