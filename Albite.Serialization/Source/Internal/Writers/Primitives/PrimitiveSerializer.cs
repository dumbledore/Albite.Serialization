using Albite.IO;
using System;
using System.IO;

namespace Albite.Serialization.Internal.Writers.Primitives
{
    class PrimitiveSerializer : IInitiailzableSerializer
    {
        private TypeCode _code;

        private PrimitiveSerializer() { }

        public void LateInitialize(IContext context, Type type)
        {
            _code = type.GetTypeCode();
            context.Writer.WriteSmallEnum(_code);
        }

        public void Write(IContext context, object value)
        {
            BinaryWriter writer = context.Writer;

            switch (_code)
            {
                case TypeCode.Boolean:
                    writer.Write((Boolean)value);
                    break;
                case TypeCode.Char:
                    writer.Write((Char)value);
                    break;
                case TypeCode.SByte:
                    writer.Write((SByte)value);
                    break;
                case TypeCode.Byte:
                    writer.Write((Byte)value);
                    break;
                case TypeCode.Int16:
                    writer.Write((Int16)value);
                    break;
                case TypeCode.UInt16:
                    writer.Write((UInt16)value);
                    break;
                case TypeCode.Int32:
                    writer.Write((Int32)value);
                    break;
                case TypeCode.UInt32:
                    writer.Write((UInt32)value);
                    break;
                case TypeCode.Int64:
                    writer.Write((Int64)value);
                    break;
                case TypeCode.UInt64:
                    writer.Write((UInt64)value);
                    break;
                case TypeCode.Single:
                    writer.Write((Single)value);
                    break;
                case TypeCode.Double:
                    writer.Write((Double)value);
                    break;
                case TypeCode.Decimal:
                    writer.Write((Decimal)value);
                    break;

                // The following are not strictly value types
                // See: https://msdn.microsoft.com/en-us/library/bfft1t3c.aspx
                // However, they are immutable and therefore behave like
                // value types, so it should be OK to serialize
                // two different references with the same data
                // as the same object.
                case TypeCode.DateTime:
                    writer.Write((DateTime)value);
                    break;
                case TypeCode.String:
                    writer.Write((String)value);
                    break;
                case TypeCode.DateTimeOffset:
                    writer.Write((DateTimeOffset)value);
                    break;
                case TypeCode.TimeSpan:
                    writer.Write((TimeSpan)value);
                    break;
                case TypeCode.Guid:
                    writer.Write((Guid)value);
                    break;

                default:
                    throw new InvalidOperationException(
                        String.Format("Unknown typecode: {0}", _code));
            }
        }

        #region Descriptor
        private class D : IDescriptor
        {
            public bool IsTypeSupported(Type type)
            {
                TypeCode code = type.GetTypeCode();
                return code != TypeCode.Object && code != TypeCode.Empty;
            }

            public SerializedType SerializedType
            {
                get { return SerializedType.Primitive; }
            }

            public IInitiailzableSerializer Create()
            {
                return new PrimitiveSerializer();
            }
        }

        public static readonly IDescriptor Descriptor = new D();
        #endregion
    }
}
