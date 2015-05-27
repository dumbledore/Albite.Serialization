using System;

namespace Albite.Serialization.Internal.Readers.Collections
{
    internal class ArraySerializer : ObjectSerializer
    {
        private Type _type;
        private ISerializer _nested;

        private ArraySerializer() { }

        public override void LateInitialize(IContext context)
        {
            _type = context.ReadType();
            _nested = context.CreateSerializer();
        }

        protected override object CreateObject(IContext context)
        {
            // Get the number of elements
            int size = context.Reader.ReadInt32();

            // Now create the array
            return Array.CreateInstance(_type, size);
        }

        protected override void ReadObject(IContext context, object o)
        {
            Array result = (Array)o;
            int size = result.Length;

            // And read through all elements
            for (int i = 0; i < size; i++)
            {
                object value = _nested.Read(context);
                result.SetValue(value, i);
            }
        }

        #region Descriptor
        private class D : IDescriptor
        {
            public SerializedType SerializedType
            {
                get { return SerializedType.Array; }
            }

            public virtual IInitiailzableSerializer Create()
            {
                return new ArraySerializer();
            }
        }

        public static readonly IDescriptor Descriptor = new D();
        #endregion
    }
}
