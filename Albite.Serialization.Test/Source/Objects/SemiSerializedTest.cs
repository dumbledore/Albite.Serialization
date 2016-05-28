namespace Albite.Serialization.Test.Objects
{
    public class SemiSerializedTest
    {
        private interface I { }

        private class K : I
        {
            [Serialized]
            int n1;

            protected K() { }

            public K(int n1)
            {
                this.n1 = n1;
            }

            public override string ToString()
            {
                return "K: " + n1;
            }
        }

        private class L : K
        {
            [Serialized]
            int n2;

            protected L() { }

            public L(int n1, int n2)
                : base(n1)
            {
                this.n2 = n2;
            }

            public override string ToString()
            {
                return "L: " + n2 + " " + base.ToString();
            }
        }

        private class M : L
        {
            int n3;

            protected M() { }

            public M(int n1, int n2, int n4)
                : base(n1, n2)
            {
                this.n3 = n4;
            }

            public override string ToString()
            {
                return "M: " + n3 + " " + base.ToString();
            }
        }

        private class N : M
        {
            [Serialized]
            int n4;

            protected N() { }

            public N(int n1, int n2, int n3, int n4)
                : base(n1, n2, n3)
            {
                this.n4 = n4;
            }

            public override string ToString()
            {
                return "N: " + n4 + " " + base.ToString();
            }
        }

        // DO NOT FORGET,
        // that members HAVE to have the serialized attribute
        // or they will NOT be serialized
        public void Test()
        {
            K k = new K(10);
            L l = new L(100, 200);
            M m = new M(1000, 2000, 3000);
            N n = new N(10000, 20000, 30000, 40000);

            I[] arr1 = { k, l, m, n };
            K[] arr2 = { n, n, new N(1, 2, 3, 4), };

            Helper.Test(k);
            Helper.Test(l);
            Helper.Test(m);
            Helper.Test(n);
            Helper.Test((object)arr1);
            Helper.Test((object)arr2);
        }
    }
}
