using Albite.IO;
using System;
using System.IO;

namespace Albite.Serialization.Internal.Readers.Primitives
{
    class PrimitiveSerializer : IInitiailzableSerializer
    {
        private TypeCode _code;

        private PrimitiveSerializer() { }

        public void LateInitialize(IContext context)
        {
            _code = context.Reader.ReadSmallEnum<TypeCode>();
        }

        public object Read(IContext context)
        {
            BinaryReader reader = context.Reader;

            switch (_code)
            {
                case TypeCode.Boolean:
                    return reader.ReadBoolean();
                case TypeCode.Char:
                    return reader.ReadChar();
                case TypeCode.SByte:
                    return reader.ReadSByte();
                case TypeCode.Byte:
                    return reader.ReadByte();
                case TypeCode.Int16:
                    return reader.ReadInt16();
                case TypeCode.UInt16:
                    return reader.ReadUInt16();
                case TypeCode.Int32:
                    return reader.ReadInt32();
                case TypeCode.UInt32:
                    return reader.ReadUInt32();
                case TypeCode.Int64:
                    return reader.ReadInt64();
                case TypeCode.UInt64:
                    return reader.ReadUInt64();
                case TypeCode.Single:
                    return reader.ReadSingle();
                case TypeCode.Double:
                    return reader.ReadDouble();
                case TypeCode.Decimal:
                    return reader.ReadDecimal();
                case TypeCode.DateTime:
                    return reader.ReadDateTime();
                case TypeCode.String:
                    return reader.ReadString();
                case TypeCode.DateTimeOffset:
                    return reader.ReadDateTimeOffset();
                case TypeCode.TimeSpan:
                    return reader.ReadTimeSpan();
                case TypeCode.Guid:
                    return reader.ReadGuid();

                default:
                    throw new InvalidOperationException(
                        String.Format("Unknown typecode: {0}", _code));
            }
        }

        #region Descriptor
        private class D : IDescriptor
        {
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