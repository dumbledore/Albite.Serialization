namespace Albite.Serialization.Test.Objects
{
    public class AbstractClassTest
    {
        private interface I { }

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

        public void Test()
        {
            AbstractClass a = new ConcreteClass(10, 20, 30);
            AbstractClass[] arr = new AbstractClass[] { a, a, new ConcreteClass(100, 200, 300), null };
            Helper.Test(a);
            Helper.Test((object)arr);
        }
    }
}
