using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albite.Serialization.Test.Objects
{
    public class CustomAttributesTest
    {
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

        public void Test()
        {
            CA s = new CA(13);
            Helper.Test(s, typeof(CustomSerializedAttribute));
        }
    }
}
