using System;

namespace Albite.Serialization.Test
{
    public class SerializerTypeTest : SerializerTest
    {
        private interface I { }
        private abstract class A : I { }
        private class B : I { }
        private class D : B { }

        public void TestTypes()
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

            test(values);
        }
    }
}
