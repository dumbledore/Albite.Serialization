using Albite.Test;

namespace Albite.Serialization.Test.Objects
{
    public class NullObjectTest
    {
        interface I { }

        class C : I { }

        public void Test()
        {
            object o = null;
            I i = null;
            C c = null;

            object[] values = { o, i, c, };
            foreach (var value in values)
            {
                object valueRead = Helper.Test(value);
                Assert.IsNull(valueRead);
            }
        }
    }
}
