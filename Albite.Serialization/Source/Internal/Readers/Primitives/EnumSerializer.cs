using Albite.IO;
using System;

namespace Albite.Serialization.Internal.Readers.Primitives
{
    internal class EnumSerializer : IInitiailzableSerializer
    {
        private Type _enumType;

        private EnumSerializer() { }

        public void LateInitialize(IContext context)
        {
            this._enumType = context.ReadType();
        }

        public object Read(Readers.IContext context)
        {
            return context.Reader.ReadEnum(_enumType);
        }

        #region Descriptor
        private class D : IDescriptor
        {
            public SerializedType SerializedType
            {
                get { return SerializedType.Enum; }
            }

            public IInitiailzableSerializer Create()
            {
                return new EnumSerializer();
            }
        }

        public static readonly IDescriptor Descriptor = new D();
        #endregion
    }
}
