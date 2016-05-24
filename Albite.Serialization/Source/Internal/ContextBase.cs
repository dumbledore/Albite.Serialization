using Albite.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Albite.Serialization.Test")]

namespace Albite.Serialization.Internal
{
    abstract class ContextBase : IContextBase
    {
        private readonly Type _type;

        public abstract Version Version { get; }

        protected ContextBase(Type attributeType)
        {
            if (attributeType == null)
            {
                throw new ArgumentNullException("attributeType is null");
            }

            _type = attributeType;
        }

        public Type SerializedAttribute { get { return _type; } }

        public IEnumerable<IMemberValue> GetSerializedMembers(TypeInfo info)
        {
            return GetSerializedMembers(info, _type);
        }

        public static IEnumerable<IMemberValue> GetSerializedMembers(
            TypeInfo info, Type attributeType)
        {
            return info.GetMembers().Where((m) =>
            {
                return m.CanRead
                    && m.CanWrite
                    && m.Info.CustomAttributes.Any(a => (a.AttributeType == attributeType));
            });
        }
    }
}
