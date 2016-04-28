using System;
using System.Reflection;

namespace Albite.Serialization.Internal.Readers
{
    internal class ProxySerializer : IInitiailzableSerializer
    {
        private TypeInfo _info;

        public void LateInitialize(IContext context)
        {
            // Cache the type info as it will be used upon every read
            Type type = context.ReadType();
            _info = type.GetTypeInfo();
        }

        public object Read(IContext context)
        {
            bool haveObject = context.Reader.ReadBoolean();

            if (haveObject)
            {
                ISerializer s = context.CreateSerializer();
                object value = s.Read(context);

                if (value != null && !_info.IsAssignableFrom(value.GetType().GetTypeInfo()))
                {
                    throw new InvalidCastException("Read object is not from the right type");
                }

                return value;
            }
            else
            {
                return null;
            }
        }
    }   
}
