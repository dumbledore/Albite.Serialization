namespace Albite.Serialization.Test
{
    public class SerializerArrayTest : SerializerTest
    {
        public void SerializeByteArray()
        {
            byte[] arr1 = new byte[] { 1, 4, 10 };

            test(new object[] {
                arr1,
                arr1,
                (byte) 111,
                new byte[] {},
                new byte[] {10},
                new byte[] {1, 1, 3},
                new byte[][] { null, new byte[] { 13 }, arr1, null, new byte[] { 9, 9 }, arr1 },
            });
        }
    }
}
