using System;
using System.Reflection;

namespace Albite.Serialization
{
    /// <summary>
    /// Use this attribute or one that inherits from it
    /// on classes or members that are to be serialized.
    ///
    /// If a class does not have this attribute,
    /// it will not be serialized, meaning any instances of it
    /// will be deserialized as null, and in cases when a class
    /// that inherits from it is serialized, none of its members
    /// will be serialized either.
    ///
    /// Note that the fact that a class has the attribute is
    /// not enough to serialize its members. One needs to put the
    /// attribute on every member that needs to be serialized as well.
    /// </summary>
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
