namespace Albite.Serialization.Test.Objects
{
    public class GenericsTest
    {
        private class G<T>
        {
            [Serialized]
            public T Custom { get; private set; }

            [Serialized]
            public int Data { get; private set; }

            private G() { }

            public G(T custom, int data)
            {
                this.Custom = custom;
                this.Data = data;
            }
        }

        private class H
        {
            [Serialized]
            public string Data { get; private set; }

            protected H() { }

            public H(string data)
            {
                this.Data = data;
            }
        }

        private class J : H
        {
            [Serialized]
            public int Data2 { get; private set; }

            private J() { }

            public J(string data, int data2)
                : base(data)
            {
                this.Data2 = data2;
            }
        }

        public void Test()
        {
            H h1 = new H("Hello");
            J j1 = new J("There", 128);
            G<H> g1 = new G<H>(h1, 10);
            G<H> g2 = new G<H>(j1, 20);
            G<J> g3 = new G<J>(j1, 30);

            Helper.Test(g1);
            Helper.Test(g2);
            Helper.Test(g3);
        }
    }
}
