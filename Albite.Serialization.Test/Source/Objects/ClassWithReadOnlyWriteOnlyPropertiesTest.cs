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
        }

        public void Test()
        {
            MyClass c = new MyClass(10);

            // This should pass as only the normal property
            // should have been serialized.
            Helper.Test(c);
        }
    }
}
