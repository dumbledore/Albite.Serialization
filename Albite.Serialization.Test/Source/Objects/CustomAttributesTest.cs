using Albite.Test;
using System;

namespace Albite.Serialization.Test.Objects
{
    public class CustomAttributesTest
    {
        [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
        private class CustomSerializedAttribute : Attribute { }

        private class MyClass
        {
            private MyClass() { }

            public MyClass(int data)
            {
                _data = data;
            }

            [CustomSerialized]
            private readonly int _data;

            public override bool Equals(object obj)
            {
                MyClass other = obj as MyClass;
                return (other == null) ? false : (_data == other._data);
            }

            public override int GetHashCode()
            {
                return _data;
            }
        }

        public void Test()
        {
            MyClass s = new MyClass(13);
            MyClass sRead = (MyClass)Helper.Test(s, typeof(CustomSerializedAttribute));
            Assert.AreEqual(s, sRead);
        }
    }
}
