using Albite.IO;
using System;
using System.Reflection;

namespace Albite.Serialization.Internal.Writers.Primitives
{
    class EnumSerializer : IInitiailzableSerializer
    {
        // Note that enums are value types and behave like integers,
        // and therefore they should not be treated like references,
        // meaning that we cannot use ObjectSerializer (as caching enums
        // would be totally useless, since there references would always
        // be different because of boxing).

        private Type _enumType;

        private EnumSerializer() { }

        public void LateInitialize(IContext context, Type type)
        {
            this._enumType = type;
            context.WriteType(type);
        }

        public void Write(IContext context, object value)
        {
            context.Writer.WriteEnum(_enumType, value);
        }

        #region Descriptor
        private class D : IDescriptor
        {
            public bool IsTypeSupported(Type type)
            {
                return type.GetTypeInfo().IsEnum;
            }

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
