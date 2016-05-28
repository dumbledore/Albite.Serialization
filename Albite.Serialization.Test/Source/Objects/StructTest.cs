using Albite.Test;
using System;

namespace Albite.Serialization.Test.Objects
{
    public class StructTest
    {
        private struct S { }

        public void Test()
        {
            Assert.ThrowsException<NotSupportedException>(() =>
            {
                S s;
                Helper.Test(s);
            });
        }
    }
}
