using Albite.Core.Reflection;
using System.Linq;
using System.Reflection;

namespace Albite.Serialization.Internal.Helpers
{
    internal static class TypeInfoExtensions
    {
        public static IMemberValue[] GetSerializedMembers(this TypeInfo info)
        {
            return info.GetMembers((memberType, memberInfo) =>
            {
                return memberInfo.CustomAttributes.Any(
                    a => SerializedAttribute.IsAssignableFrom(a.AttributeType));
            });
        }
    }
}
