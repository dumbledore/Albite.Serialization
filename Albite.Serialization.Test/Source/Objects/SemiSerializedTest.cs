using Albite.Test;
namespace Albite.Serialization.Test.Objects
{
    public class SemiSerializedTest
    {
        private interface I
        {
            void AssertAsExpected(I expected);
        }

        private class K : I
        {
            [Serialized]
            private int _n1;

            public int N1 { get { return _n1; } }

            protected K() { }

            public K(int n1)
            {
                _n1 = n1;
            }

            public override string ToString()
            {
                return "K: " + _n1;
            }

            public virtual void AssertAsExpected(I expected)
            {
                K k = (K)expected;
                Assert.AreEqual(k.N1, N1);
            }
        }

        private class L : K
        {
            [Serialized]
            private int _n2;

            public int N2 { get { return _n2; } }

            protected L() { }

            public L(int n1, int n2) : base(n1)
            {
                _n2 = n2;
            }

            public override string ToString()
            {
                return "L: " + _n2 + " " + base.ToString();
            }

            public override void AssertAsExpected(I expected)
            {
                L l = (L)expected;
                Assert.AreEqual(l.N2, N2);
                base.AssertAsExpected(expected);
            }
        }

        private class M : L
        {
            private int _n3;

            public int N3 { get { return _n3; } }

            protected M() { }

            public M(int n1, int n2, int n4) : base(n1, n2)
            {
                _n3 = n4;
            }

            public override string ToString()
            {
                return "M: " + _n3 + " " + base.ToString();
            }

            public override void AssertAsExpected(I expected)
            {
                // Should not have been serialized
                Assert.AreEqual(default(int), N3);
                base.AssertAsExpected(expected);
            }
        }

        private class N : M
        {
            [Serialized]
            private int _n4;

            public int N4 { get { return _n4; } }

            protected N() { }

            public N(int n1, int n2, int n3, int n4) : base(n1, n2, n3)
            {
                _n4 = n4;
            }

            public override string ToString()
            {
                return "N: " + _n4 + " " + base.ToString();
            }

            public override void AssertAsExpected(I expected)
            {
                N n = (N)expected;
                Assert.AreEqual(n.N4, N4);
                base.AssertAsExpected(expected);
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
            I[] values = { k, l, m, n, };

            // First check individually
            foreach (var value in values)
            {
                I valueRead = (I)Helper.Test(value);
                valueRead.AssertAsExpected(value);
            }

            // Now as an array
            I[] valuesRead = (I[])Helper.Test(values);
            for (int i = 0; i < values.Length; i++)
            {
                valuesRead[i].AssertAsExpected(values[i]);
            }

            I[] arr = { n, n, new N(1, 2, 3, 4), };
            I[] arrRead = (I[])Helper.Test(arr);
            for (int i = 0; i < arr.Length; i++)
            {
                arrRead[i].AssertAsExpected(arr[i]);
            }
        }
    }
}
