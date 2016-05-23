using Albite.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Albite.Serialization.Test")]

namespace Albite.Serialization.Internal
{
    internal static class TypeInfoExtensions
    {
        private static readonly TypeInfo TypeInfo = typeof(SerializedAttribute).GetTypeInfo();

        public static IEnumerable<IMemberValue> GetSerializedMembers(this TypeInfo info)
        {
            return info.GetMembers().Where((m) =>
            {
                return m.CanRead
                    && m.CanWrite
                    && m.Info.CustomAttributes.Any(
                            a => TypeInfo.IsAssignableFrom(a.AttributeType.GetTypeInfo()));
            });
        }
    }
}
