using System;

namespace Albite.Serialization.Internal.Writers
{
    internal interface IDescriptor
    {
        bool IsTypeSupported(Type type);
        SerializedType SerializedType { get; }
        IInitiailzableSerializer Create();
    }
}
