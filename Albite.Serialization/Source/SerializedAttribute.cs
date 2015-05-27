using System;
using System.Reflection;

namespace Albite.Serialization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class SerializedAttribute : Attribute
    {
        private static readonly TypeInfo TypeInfo = typeof(SerializedAttribute).GetTypeInfo();

        internal static bool IsAssignableFrom(Type type)
        {
            return IsAssignableFrom(type.GetTypeInfo());
        }

        internal static bool IsAssignableFrom(TypeInfo info)
        {
            return TypeInfo.IsAssignableFrom(info);
        }
    }
}
