using System;

namespace Albite.Serialization.Internal.Writers.Collections
{
    class ArraySerializer : CollectionSerializer
    {
        private ArraySerializer() { }

        public override void LateInitialize(IContext context, Type type)
        {
            if (type.GetArrayRank() != 1)
            {
                throw new NotSupportedException("Only one-dimensional arrays are supported");
            }

            Type elementType = type.GetElementType();
            context.WriteType(elementType);
            _nested = context.CreateProxy(elementType);
        }

        #region Descriptor
        private class AD : IDescriptor
        {
            public bool IsTypeSupported(Type type)
            {
                return type.IsArray;
            }

            public SerializedType SerializedType
            {
                get { return SerializedType.Array; }
            }

            public IInitiailzableSerializer Create()
            {
                return new ArraySerializer();
            }
        }

        public static readonly IDescriptor Descriptor = new AD();
        #endregion
    }
}
