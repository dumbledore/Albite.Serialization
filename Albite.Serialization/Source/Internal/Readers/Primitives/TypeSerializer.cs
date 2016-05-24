namespace Albite.Serialization.Internal.Readers.Primitives
{
    class TypeSerializer : IInitiailzableSerializer
    {
        private TypeSerializer() { }

        public void LateInitialize(IContext context) { }

        public object Read(IContext context)
        {
            return context.ReadType();
        }

        #region Descriptor
        private class D : IDescriptor
        {
            public SerializedType SerializedType
            {
                get { return SerializedType.Type; }
            }

            public IInitiailzableSerializer Create()
            {
                return new TypeSerializer();
            }
        }

        public static readonly IDescriptor Descriptor = new D();
        #endregion
    }
}
