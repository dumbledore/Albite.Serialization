using System;
using System.Reflection;
using Albite.Core.Reflection;

namespace Albite.Serialization.Internal.Writers.Primitives
{
    internal class TypeSerializer : IInitiailzableSerializer
    {
        private TypeSerializer() { }

        public void LateInitialize(IContext context, Type type) { }

        public void Write(IContext context, object value)
        {
            context.WriteType((Type)value);
        }

        #region Descriptor
        private class D : IDescriptor
        {
            public bool IsTypeSupported(Type type)
            {
                return type.GetTypeInfo().IsType();
            }

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
