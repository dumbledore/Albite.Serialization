using System.Collections.Generic;

namespace Albite.Serialization.Test.Objects
{
    public class CustomListTest
    {
        private class LA<T> : List<T>
        {
            [Serialized]
            private int x;

            protected LA() { }

            public LA(int x)
            {
                this.x = x;
            }
        }

        private class LB<T> : LA<T>
        {
            [Serialized]
            private int y;

            protected LB() { }

            public LB(int x, int y)
                : base(x)
            {
                this.y = y;
            }
        }

        public void Test()
        {
            LB<string> x = new LB<string>(1, 2);
            x.AddRange(new string[] { "One", "Two", "Ten" });

            ICollection<string>[] arr = { x, x, new LB<string>(10, 20) };
            Helper.Test(x);
            Helper.Test((object)arr);
        }
    }
}
