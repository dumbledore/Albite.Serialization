using Albite.Test;
using System.Linq;

namespace Albite.Serialization.Test.Objects
{
    public class AbstractClassTest
    {
        private interface I { }

        private abstract class AbstractClass : I
        {
            // In order to serialize this (even though it is abstract),
            // we need to have a setter.
            [Serialized]
            public abstract int Data { get; protected set; }

            protected AbstractClass() { }

            protected AbstractClass(int data)
            {
                this.Data = data;
            }

            public override bool Equals(object obj)
            {
                AbstractClass other = obj as AbstractClass;
                return (other == null) ? false : (Data == other.Data);
            }

            public override int GetHashCode()
            {
                return Data;
            }
        }

        private class ConcreteClass : AbstractClass
        {
            private ConcreteClass() { }

            // This one will not have the serialized attribute
            // as it is not inheritable.
            // This is expected as it will be serialized as part
            // of AbstractClass. Otherwise it would have been
            // serialzed twice (not good at all!).
            public override int Data { get; protected set; }

            public ConcreteClass(int data) : base(data) { }
        }

        public void Test()
        {
            AbstractClass a = new ConcreteClass(10);
            AbstractClass aRead = (AbstractClass)Helper.Test(a);
            Assert.AreEqual(a, aRead);

            AbstractClass[] arr = new AbstractClass[] { a, a, new ConcreteClass(100), null };
            AbstractClass[] arrRead = (AbstractClass[])Helper.Test(arr);
            Assert.IsTrue(arr.SequenceEqual(arrRead));
            Assert.AreSame(arrRead[0], arrRead[1]);
            Assert.AreEqual(100, arrRead[2].Data);
            Assert.IsNull(arrRead[3]);
        }
    }
}
