using Albite.Reflection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Albite.Serialization.Internal
{
    interface IContextBase
    {
        Version Version { get; }
        Type SerializedAttribute { get; }
        IEnumerable<IMemberValue> GetSerializedMembers(TypeInfo info);
    }
}
