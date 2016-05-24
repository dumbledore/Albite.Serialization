using Albite.Test;

namespace Albite.Serialization.Test
{
    public class SerializerArrayTest
    {
        public void SerializeByteArray()
        {
            byte[] arr1 = new byte[] { 1, 4, 10 };

            object[] valuesRead = Helper.Test(new object[] {
                arr1,
                arr1,
                (byte) 111,
                new byte[] {},
                new byte[] {10},
                new byte[] {1, 1, 3},
                new byte[][] { null, new byte[] { 13 }, arr1, null, new byte[] { 9, 9 }, arr1 },
            });

            CollectionAssert.AreEqual(arr1, (byte[])valuesRead[0]);
            Assert.AreSame(valuesRead[0], valuesRead[1]);
            Assert.AreEqual(111, (byte)valuesRead[2]);
            CollectionAssert.AreEqual(new byte[0], (byte[])valuesRead[3]);
            CollectionAssert.AreEqual(new byte[] { 10, }, (byte[])valuesRead[4]);
            CollectionAssert.AreEqual(new byte[] { 1, 1, 3 }, (byte[])valuesRead[5]);
        }
    }
}
