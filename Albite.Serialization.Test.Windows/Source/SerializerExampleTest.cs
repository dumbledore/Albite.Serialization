using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.IO;

namespace Albite.Serialization.Test.Windows
{
    [TestClass]
    public class SerializerExampleTest
    {
        private enum MyEnum
        {
            X = 100,
        }

        [TestMethod]
        public void SimpleExample()
        {
            byte[] buffer;

            using (MemoryStream stream = new MemoryStream())
            {
                using (ObjectWriter writer = new ObjectWriter(stream))
                {
                    writer.WriteObject(10);
                    writer.WriteObject(MyEnum.X);
                    writer.WriteObject("hello");
                    writer.WriteObject(new int[] { 10, 20 });
                    writer.WriteObject(new Stack<int>(new int[] { 100, 200 }));
                    writer.WriteObject(null);
                    writer.WriteObject(typeof(string));
                }

                buffer = stream.ToArray();
            }

            int i;
            MyEnum e;
            string s;
            int[] arr;
            Stack<int> st;
            object o;
            Type t;

            using (MemoryStream stream = new MemoryStream(buffer))
            {
                using (ObjectReader reader = new ObjectReader(stream))
                {
                    i = (int)reader.ReadObject();
                    e = (MyEnum)reader.ReadObject();
                    s = (string)reader.ReadObject();
                    arr = (int[])reader.ReadObject();
                    st = (Stack<int>)reader.ReadObject();
                    o = reader.ReadObject();
                    t = (Type)reader.ReadObject();
                }
            }

            Assert.AreEqual(10, i);
            Assert.AreEqual(MyEnum.X, e);
            Assert.AreEqual("hello", s);
            CollectionAssert.AreEqual(new int[] { 10, 20 }, arr);
            CollectionAssert.AreEqual(new Stack<int>(new int[] { 100, 200 }), st);
            Assert.IsNull(o);
            Assert.AreEqual(typeof(string), t);
        }

        [Serialized]
        private class MyClass
        {
            private byte _b;

            [Serialized]
            private int _i;

            [Serialized]
            public string S { get; private set; }

            public byte B
            {
                get { return _b; }
            }

            public int I
            {
                get { return _i; }
            }

            private MyClass() { }

            public MyClass(byte b, int i, string s)
            {
                _b = b;
                _i = i;
                S = s;
            }
        }

        [TestMethod]
        public void SimpleClassExampleTest()
        {
            byte[] buffer;

            using (MemoryStream stream = new MemoryStream())
            {
                using (ObjectWriter writer = new ObjectWriter(stream))
                {
                    writer.WriteObject(new MyClass(10, 1000, "hello"));
                }

                buffer = stream.ToArray();
            }

            MyClass c;

            using (MemoryStream stream = new MemoryStream(buffer))
            {
                using (ObjectReader reader = new ObjectReader(stream))
                {
                    c = (MyClass)reader.ReadObject();
                }
            }

            Assert.AreEqual(default(byte), c.B); // not serialized
            Assert.AreEqual(1000, c.I);
            Assert.AreEqual("hello", c.S);
        }
    }
}
