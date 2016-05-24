using System;

namespace Albite.Serialization.Internal.Writers
{
    interface IDescriptor
    {
        bool IsTypeSupported(Type type);
        SerializedType SerializedType { get; }
        IInitiailzableSerializer Create();
    }
}
