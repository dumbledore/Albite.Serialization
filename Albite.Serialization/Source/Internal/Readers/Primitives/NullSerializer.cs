namespace Albite.Serialization.Internal.Readers.Primitives
{
    class NullSerializer : IInitiailzableSerializer
    {
        private NullSerializer() { }

        public void LateInitialize(IContext context) { }

        public object Read(Readers.IContext context)
        {
            return null;
        }

        #region Descriptor
        private class D : IDescriptor
        {
            public SerializedType SerializedType
            {
                get { return SerializedType.Null; }
            }

            public IInitiailzableSerializer Create()
            {
                return new NullSerializer();
            }
        }

        public static readonly IDescriptor Descriptor = new D();
        #endregion
    }
}
