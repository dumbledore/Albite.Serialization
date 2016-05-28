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
            Helper.Test(r1);

            R r2 = new R();
            r2.Me = r1;
            Helper.Test(r2);

            R r3 = new R();
            r3.Me = r3;
            Helper.Test(r3);
        }
    }
}
