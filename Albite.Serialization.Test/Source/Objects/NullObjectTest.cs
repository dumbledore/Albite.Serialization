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

            Helper.Test(o);
            Helper.Test(i);
            Helper.Test(c);
        }
    }
}
