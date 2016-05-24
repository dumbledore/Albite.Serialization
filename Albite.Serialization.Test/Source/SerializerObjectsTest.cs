﻿using Albite.Test;
using System;
using System.Collections.Generic;

namespace Albite.Serialization.Test
{
    public class SerializerObjectsTest
    {
        private interface I
        {
        }

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

        public void SerializeObjects()
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

        private class R
        {
            [Serialized]
            public R Me { get; set; }
        }

        public void SerializeRecursiveObjects()
        {
            R r1 = new R();
            Helper.Test(r1);

            R r2 = new R();
            r2.Me = r1;
            Helper.Test(r2);

            R r3 = new R();
            r3.Me = r3;
            Helper.Test(r3);
        }

        [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
        private class CustomSerializedAttribute : Attribute { }

        private class CA
        {
            private CA() { }

            public CA(int data)
            {
                this.data = data;
            }

            [CustomSerialized]
            private readonly int data;
        }

        public void SerializeCustomAttributes()
        {
            CA s = new CA(13);
            Helper.Test(s, typeof(CustomSerializedAttribute));
        }

        private struct S { }

        public void SerializeStruct()
        {
            Assert.ThrowsException<NotSupportedException>(() =>
            {
                S s;
                Helper.Test(s);
            });
        }

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

        private class W : I { }

        public void SerializeArrayOfInterfaces()
        {
            I[] i1 = { };
            I[] i2 = { null };
            I[] i3 = { null, new X(1), new X(2), null, new X(4), };

            X x = new X(1);
            Y y = new Y("hello");
            I[] i4 = { x, x, new X(1), y, y, new Y("there"), };

            W w = new W();
            I[] i5 = { w, w, new W(), };

            Helper.Test((object)i1);
            Helper.Test((object)i2);
            Helper.Test((object)i3);
            Helper.Test((object)i4);
            Helper.Test((object)i5);
        }

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

        public void GenericsTest()
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
        public void SemiSerializedTest()
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

        public void CustomListTest()
        {
            LB<string> x = new LB<string>(1, 2);
            x.AddRange(new string[] { "One", "Two", "Ten" });

            ICollection<string>[] arr = { x, x, new LB<string>(10, 20) };
            Helper.Test(x);
            Helper.Test((object)arr);
        }

        public void NullObjectTest()
        {
            object o = null;
            I i = null;
            A a = null;

            Helper.Test(o);
            Helper.Test(i);
            Helper.Test(a);
        }

        private abstract class AbstractClass : I
        {
            // not serialized
            private int _privData;

            // In order to serialize this, we need
            // to have a setter!
            [Serialized]
            public abstract int AbstractData { get; protected set; }

            protected AbstractClass() { }

            protected AbstractClass(int data)
            {
                _privData = data;
            }
        }

        private class ConcreteClass : AbstractClass
        {
            [Serialized]
            private int _privData;

            private ConcreteClass() { }

            // This one will not have the serialized attribute
            // as it is not inheritable.
            // This is expected as it will be serialized as part
            // of AbstractClass. Otherwise it would have been
            // serialzed twice (not good at all!).
            public override int AbstractData { get; protected set; }

            public ConcreteClass(int data, int data2, int data3)
                : base(data)
            {
                AbstractData = data2;
                _privData = data3;
            }
        }

        public void AbstractClassTest()
        {
            AbstractClass a = new ConcreteClass(10, 20, 30);
            AbstractClass[] arr = new AbstractClass[] { a, a, new ConcreteClass(100, 200, 300), null };
            Helper.Test(a);
            Helper.Test((object)arr);
        }

        private class ClassWithIndexer
        {
            [Serialized]
            string[] _arr;

            private ClassWithIndexer() { }

            public ClassWithIndexer(int size)
            {
                _arr = new string[size];
            }

            [Serialized]
            public string this[int index]
            {
                get
                {
                    return _arr[index];
                }

                set
                {
                    _arr[index] = value;
                }
            }
        }

        public void ClassWithIndexerTest()
        {
            ClassWithIndexer c = new ClassWithIndexer(10);
            c[0]="Hello";
            c[1]="there!";

            // this should pass as the indexer would not have been serialized
            // even though the serialized attribute was mistakenly put
            Helper.Test(c);
        }

        private class ClassWithROWO
        {
            [Serialized]
            public int ReadOnlyProperty { get { return -1; } }

            [Serialized]
            public int WriteOnlyProperty { set { } }

            [Serialized]
            public int NormalProperty { get; private set; }

            private ClassWithROWO() { }

            public ClassWithROWO(int x)
            {
                NormalProperty = x;
            }
        }

        public void ClassWithReadOnlyWriteOnlyPropertiesTest()
        {
            ClassWithROWO c = new ClassWithROWO(10);

            // This should pass as only the normal property
            // should have been serialized.
            Helper.Test(c);
        }
    }
}
