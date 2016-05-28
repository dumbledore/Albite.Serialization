namespace Albite.Serialization.Test.Objects
{
    public class VariousObjectsTest
    {
        private interface I { }

        private class A : I
        {
            [Serialized]
            public A Other;

            [Serialized]
            private byte[][] privateData;

            [Serialized]
            public string Text { get; private set; }

            protected A() { }

            public A(string text, byte[][] priv)
            {
                this.Text = text;
                this.privateData = priv;
            }
        }

        private class B : A
        {
            [Serialized]
            private int otherData;

            private B() { }

            public B(string text, int otherData)
                : base(text, null)
            {
                this.otherData = otherData;
            }
        }

        private class C
        {
            [Serialized]
            public A Other { get; private set; }

            [Serialized]
            public string Text { get; private set; }

            private C() { }

            public C(string text, A other)
            {
                this.Text = text;
                this.Other = other;
            }
        }

        public void Test()
        {
            A a1 = new A("A1", new byte[][] { new byte[] { 1 }, new byte[] { 3, 4 } });
            A a2 = new A("A2", null);
            A a3 = new A("A3", null);
            a1.Other = a1;
            a2.Other = a3;
            a3.Other = a2;

            B b1 = new B("B1", 7);
            b1.Other = a2;

            C c1 = new C("C1", a1);
            C c2 = new C("C2", a2);
            C c3 = new C("C3", a3);
            C c4 = new C("C4", b1);

            A[] ar1 = new A[] { a1, a2, a3 };
            B[] ar2 = new B[] { b1 };
            A[] ar3 = ar2; // Arrays are classes after all, so this is possible
            A[] ar4 = new A[] { a1, b1 };
            C[] ar5 = new C[] { c1, c2, c3, c4 };
            A[][] ar6 = new A[][] { ar2, ar3, };
            I[] ar7 = new I[] { a1, a2, a3 };

            Helper.Test(a1);
            Helper.Test(a2);
            Helper.Test(a3);
            Helper.Test(b1);
            Helper.Test(c1);
            Helper.Test(c2);
            Helper.Test(c3);
            Helper.Test(c4);
            Helper.Test(ar1);
            Helper.Test(ar2);
            Helper.Test(ar3);
            Helper.Test(ar4);
            Helper.Test(ar5);
            Helper.Test(ar6);
            Helper.Test(ar7);
        }
    }
}
