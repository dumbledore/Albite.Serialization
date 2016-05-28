using Albite.Test;

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

            public override bool Equals(object obj)
            {
                X other = obj as X;
                return (other == null) ? false : (Data == other.Data);
            }

            public override int GetHashCode()
            {
                return Data;
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

            public override bool Equals(object obj)
            {
                Y other = obj as Y;
                return (other == null) ? false : (Data == other.Data);
            }

            public override int GetHashCode()
            {
                return Data.GetHashCode();
            }
        }

        private class Z : I
        {
            public override bool Equals(object obj)
            {
                return (obj as Z) != null;
            }

            public override int GetHashCode()
            {
                return 0;
            }
        }

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

            object[] values = { i1, i2, i3, i4, i5, };
            foreach (var value in values)
            {
                object valueRead = Helper.Test(value);
                CollectionAssert.AreEqual((object[])value, (object[])valueRead);
            }
        }
    }
}
