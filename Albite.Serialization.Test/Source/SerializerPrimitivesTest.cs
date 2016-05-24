using Albite.Test;
using System;

namespace Albite.Serialization.Test
{
    public class SerializerPrimitivesTest : SerializerTest
    {
        public void TestPrimitives()
        {
            DateTimeOffset localTime = DateTimeOffset.Now;

            object[] values =
            {
                true,
                '\u0424',
                SByte.MinValue,
                SByte.MaxValue,
                Byte.MinValue,
                Byte.MaxValue,
                Int16.MinValue,
                Int16.MaxValue,
                UInt16.MinValue,
                UInt16.MaxValue,
                Int32.MinValue,
                Int32.MaxValue,
                UInt32.MinValue,
                UInt32.MaxValue,
                Int64.MinValue,
                Int64.MaxValue,
                UInt64.MinValue,
                UInt64.MaxValue,
                Single.NegativeInfinity,
                Single.PositiveInfinity,
                Single.NaN,
                Single.Epsilon,
                Single.MinValue,
                Single.MaxValue,
                Double.NegativeInfinity,
                Double.PositiveInfinity,
                Double.NaN,
                Double.Epsilon,
                Double.MinValue,
                Double.MaxValue,
                Decimal.Zero,
                Decimal.One,
                Decimal.MinusOne,
                Decimal.MinValue,
                Decimal.MaxValue,
                DateTime.Now,
                DateTime.MinValue,
                DateTime.MaxValue,
                DateTime.Now,
                DateTime.UtcNow,
                DateTime.Today,
                new DateTime(DateTime.Now.Ticks,DateTimeKind.Local),
                new DateTime(DateTime.Now.Ticks,DateTimeKind.Utc),
                new DateTime(DateTime.Now.Ticks,DateTimeKind.Unspecified),
                "hello there",
                TimeSpan.Zero,
                new TimeSpan(-2,0,0),
                new TimeSpan(2,0,0),
                TimeSpan.MinValue,
                TimeSpan.MaxValue,
                DateTimeOffset.MinValue,
                DateTimeOffset.MaxValue,
                DateTimeOffset.Now,
                DateTimeOffset.UtcNow,
                new DateTimeOffset(DateTime.Now,localTime.Offset),
                new DateTimeOffset(DateTime.UtcNow,TimeSpan.Zero),
                new DateTimeOffset(new DateTime(DateTime.Now.Ticks,DateTimeKind.Local),localTime.Offset),
                new DateTimeOffset(new DateTime(DateTime.Now.Ticks,DateTimeKind.Utc),TimeSpan.Zero),
                new DateTimeOffset(new DateTime(DateTime.Now.Ticks,DateTimeKind.Unspecified),new TimeSpan(2,0,0)),
                Guid.Empty,
                new Guid(Int32.MaxValue,Int16.MaxValue,Int16.MinValue,1,2,3,4,5,6,Byte.MinValue,Byte.MaxValue),
            };

            object[] valuesRead = test(values);
            CollectionAssert.AreEqual(values, valuesRead);
        }
    }
}
