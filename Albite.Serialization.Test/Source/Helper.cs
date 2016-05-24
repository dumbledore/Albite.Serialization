using Albite.Diagnostics;
using System;
using System.IO;
using System.Text;

namespace Albite.Serialization.Test
{
    static class Helper
    {
        public static object Test(object value, Type attributeType = null)
        {
            return Test(new object[] { value }, attributeType)[0];
        }

        public static object[] Test(object[] values, Type attributeType = null)
        {
            if (attributeType == null)
            {
                attributeType = typeof(SerializedAttribute);
            }

            byte[] bytes = write(values, attributeType);
            object[] valuesRead = read(bytes, values.Length, attributeType);

            EqualityVerifier verifier = new EqualityVerifier(attributeType);
            for (int i = 0; i < values.Length; i++)
            {
                verifier.Verify(values[i], valuesRead[i]);
            }

            return valuesRead;
        }

        private static byte[] write(object[] values, Type attributeType)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (ObjectWriter writer = new ObjectWriter(stream, Encoding.UTF8, true, attributeType))
                {
                    foreach (var value in values)
                    {
                        string typeName = (value == null) ? null : value.GetType().FullName;
                        long l = stream.Length;
                        Logger.LogMessage("[Test] Writing {0}:{1}", typeName, value);
                        writer.WriteObject(value);
                        l = stream.Length - l;
                        Logger.LogMessage("[Test] Finished writing {0}:{1} Payload: {2} bytes\n",
                            typeName, value, l);
                    }
                }

                return stream.ToArray();
            }
        }

        private static object[] read(byte[] data, int count, Type attributeType)
        {
            object[] values = new object[count];

            using (MemoryStream stream = new MemoryStream(data))
            {
                using (ObjectReader reader = new ObjectReader(stream, Encoding.UTF8, true, attributeType))
                {
                    for (int i = 0; i < count; i++)
                    {
                        Logger.LogMessage("[Test] Reading next value...");
                        object valueRead = reader.ReadObject();
                        values[i] = valueRead;
                        Logger.LogMessage("[Test] Read {0}:{1}\n", valueRead == null ? null : valueRead.GetType().FullName, valueRead);
                    }
                }
            }

            return values;
        }
    }
}
