using Albite.Test;
using System;

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

            public override bool Equals(object obj)
            {
                G<T> other = obj as G<T>;
                return (other == null)
                    ? false
                    : (Data == other.Data && Object.Equals(Custom, other.Custom));
            }

            public override int GetHashCode()
            {
                return Data;
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

            public override bool Equals(object obj)
            {
                H other = obj as H;
                return (other == null) ? false : Object.Equals(Data, other.Data);
            }

            public override int GetHashCode()
            {
                return (Data == null) ? 0 : Data.GetHashCode();
            }
        }

        private class J : H
        {
            [Serialized]
            public int Data2 { get; private set; }

            private J() { }

            public J(string data, int data2): base(data)
            {
                this.Data2 = data2;
            }

            public override bool Equals(object obj)
            {
                J other = obj as J;
                return (other == null) ? false : (Data2 == other.Data2 && base.Equals(other));
            }

            public override int GetHashCode()
            {
                return Data2;
            }
        }

        public void Test()
        {
            H h = new H("Hello");
            J j = new J("There", 128);

            object[] values = { new G<H>(h, 10), new G<H>(j, 20), new G<J>(j, 30), };
            foreach (var value in values)
            {
                object valueRead = Helper.Test(value);
                Assert.AreEqual(value, valueRead);
            }
        }
    }
}
