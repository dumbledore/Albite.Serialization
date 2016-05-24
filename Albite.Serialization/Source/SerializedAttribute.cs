using System;
using System.Reflection;

namespace Albite.Serialization
{
    /// <summary>
    /// Use this attribute on members that are to be serialized.
    ///
    /// If a member (i.e. property or field) does not have this attribute,
    /// it will not be serialized.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class SerializedAttribute : Attribute
    {
    }
}
