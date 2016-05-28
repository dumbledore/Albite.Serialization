namespace Albite.Serialization.Test.Objects
{
    public class ArrayOfInterfacesTest
    {
        private interface I { }

        private class X : I
        {
            [Serialized]
            public int Data { get; private set; }

            private X() { }

            public X(int data)
            {
                this.Data = data;
            }
        }

        private class Y : I
        {
            [Serialized]
            public string Data { get; private set; }

            private Y() { }

            public Y(string data)
            {
                this.Data = data;
            }
        }

        private class Z : I { }

        public void Test()
        {
            I[] i1 = { };
            I[] i2 = { null };
            I[] i3 = { null, new X(1), new X(2), null, new X(4), };

            X x = new X(1);
            Y y = new Y("hello");
            I[] i4 = { x, x, new X(1), y, y, new Y("there"), };

            Z z = new Z();
            I[] i5 = { z, z, new Z(), };

            Helper.Test((object)i1);
            Helper.Test((object)i2);
            Helper.Test((object)i3);
            Helper.Test((object)i4);
            Helper.Test((object)i5);
        }
    }
}
