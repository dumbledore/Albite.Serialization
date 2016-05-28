using Albite.Test;

namespace Albite.Serialization.Test.Objects
{
    public class RecursiveObjectsTest
    {
        private class R
        {
            [Serialized]
            public R Me { get; set; }
        }

        public void Test()
        {
            R r1 = new R();
            R r1Read = (R)Helper.Test(r1);
            Assert.IsNull(r1Read.Me);

            R r2 = new R();
            r2.Me = r2;
            R r2Read = (R)Helper.Test(r2);
            Assert.AreSame(r2, r2.Me);

            R r3 = new R();
            R r4 = new R();
            r3.Me = r3;
            r4.Me = r2;

            R r3Read = (R)Helper.Test(r3);
            Assert.AreSame(r3, r3.Me.Me);
        }
    }
}
