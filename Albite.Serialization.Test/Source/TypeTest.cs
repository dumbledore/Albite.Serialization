using Albite.Test;
using System;

namespace Albite.Serialization.Test
{
    public class TypeTest
    {
        private interface I { }
        private abstract class A : I { }
        private class B : I { }
        private class D : B { }

        public void Types()
        {
            Type[] values =
            {
                typeof(Int32),
                typeof(Int32),
                typeof(Int32).GetType(),
                typeof(String),
                typeof(Enum),
                typeof(Object),
                typeof(I),
                typeof(A),
                typeof(B),
                typeof(D),
                typeof(Type),
                1.GetType(),
                1.GetType().GetType(),
                "".GetType(),
            };

            object[] valuesRead = Helper.TestMultiple(values);
            CollectionAssert.AreEqual(values, valuesRead);
        }
    }
}
