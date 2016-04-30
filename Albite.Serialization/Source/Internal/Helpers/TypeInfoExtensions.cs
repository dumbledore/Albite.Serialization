using Albite.Core.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Albite.Serialization.Internal.Helpers
{
    internal static class TypeInfoExtensions
    {
        public static bool IsSerialized(this TypeInfo info)
        {
            return hasAttribute(info.CustomAttributes);
        }

        public static MemberValue[] GetSerializedMembers(this TypeInfo info)
        {
            return info.GetMembers((memberType, memberInfo) =>
            {
                return hasAttribute(memberInfo.CustomAttributes);
            });
        }

        private static bool hasAttribute(IEnumerable<CustomAttributeData> attribs)
        {
            return attribs.Any(a => SerializedAttribute.IsAssignableFrom(a.AttributeType));
        }
    }
}
