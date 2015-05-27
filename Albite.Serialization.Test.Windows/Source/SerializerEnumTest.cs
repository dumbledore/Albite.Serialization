using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Albite.Serialization.Test.Windows
{
    [TestClass]
    public class SerializerEnumTest : SerializerTest
    {
        private enum E
        {
            Zero,
            One,
            Two,
            Ten = 10,
            Eleven,
        };

        private enum MyEnum
        {
            X = -10,
            Y = 16,
            Z,
        }

        [TestMethod]
        public void TestEnum()
        {
            object[] values =
            {
                E.Zero,
                E.One,
                E.Two,
                E.Ten,
                E.Eleven,
                (E)0,
                (E)1,
                (E)2,
                (E)10,
                (E)11,
                MyEnum.X,
                MyEnum.Y,
                MyEnum.Z,
                (MyEnum)(-10),
                (MyEnum)16,
                (MyEnum)17,
            };

            test(values);
        }
    }
}
