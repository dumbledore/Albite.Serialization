using System;
using System.Reflection;

namespace Albite.Serialization.Internal.Readers
{
    internal class ProxySerializer : IInitiailzableSerializer
    {
#if DEBUG
        private TypeInfo _info;
#endif

        public void LateInitialize(IContext context)
        {
#if DEBUG
            Type type = context.ReadType();
            _info = type.GetTypeInfo();
#endif
        }

        public object Read(IContext context)
        {
            bool haveObject = context.Reader.ReadBoolean();

            if (haveObject)
            {
                ISerializer s = context.CreateSerializer();
                object value = s.Read(context);
#if DEBUG
                if (value != null && !_info.IsAssignableFrom(value.GetType().GetTypeInfo()))
                {
                    throw new InvalidCastException(String.Format(
                        "Read object is of type {0} when expected {1}",
                        value.GetType().Name, _info.AsType().Name));
                }
#endif
                return value;
            }
            else
            {
                return null;
            }
        }
    }   
}
