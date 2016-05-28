using Albite.Test;

namespace Albite.Serialization.Test.Objects
{
    public class ClassWithReadOnlyWriteOnlyPropertiesTest
    {
        private class MyClass
        {
            [Serialized]
            public int ReadOnlyProperty { get { return -1; } }

            [Serialized]
            public int WriteOnlyProperty { set { } }

            [Serialized]
            public int NormalProperty { get; private set; }

            private MyClass() { }

            public MyClass(int x)
            {
                NormalProperty = x;
            }

            public override bool Equals(object obj)
            {
                MyClass other = obj as MyClass;
                return (other == null) ? false : (NormalProperty == other.NormalProperty);
            }

            public override int GetHashCode()
            {
                return NormalProperty;
            }
        }

        public void Test()
        {
            MyClass c = new MyClass(10);

            // This should pass as only the normal property
            // should have been serialized.
            MyClass cRead = (MyClass)Helper.Test(c);
            Assert.AreEqual(c, cRead);
        }
    }
}
