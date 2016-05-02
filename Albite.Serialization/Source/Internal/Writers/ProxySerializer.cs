using Albite.Core.Diagnostics;
using System;
using System.Reflection;

namespace Albite.Serialization.Internal.Writers
{
    // Note that, because this is a proxy, the info can actually be
    // an interface, e.g. we are serializing an array of interfaces.
    // The type for ProxySerializer will be an interface,
    // but when writing to it, we'll have a concrete type
    internal class ProxySerializer : IInitiailzableSerializer
    {
#if DEBUG
        private TypeInfo _info;
#endif

        public void LateInitialize(IContext context, Type type)
        {
#if DEBUG
            _info = type.GetTypeInfo();
#endif
            context.WriteType(type);
        }

        public void Write(IContext context, object value)
        {
            if (value == null)
            {
#if DEBUG
                Logger.LogMessage("Proxy: Writing a null value");
#endif
                context.Writer.Write(false);
            }
            else
            {
#if DEBUG
                Logger.LogMessage("Proxy: Writing a value: {0}", value);
#endif
                context.Writer.Write(true);
                Type type = value.GetType();
#if DEBUG
                TypeInfo info = type.GetTypeInfo();
                if (!_info.IsAssignableFrom(info))
                {
                    throw new InvalidCastException(String.Format(
                        "Written object is of type {0} when expected {1}",
                        type.Name, _info.AsType().Name));
                }
#endif
                ISerializer s = context.CreateSerializer(type);

                // And finally write the actual contents
                s.Write(context, value);
            }
        }
    }
}
