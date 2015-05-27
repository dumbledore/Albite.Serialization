using System;

namespace Albite.Serialization.Internal.Readers
{
    internal interface IInitiailzableSerializer : ISerializer
    {
        // We don't want to export this out of IContext
        // The idea of LateInitailize is to call it after
        // the Serializer has been cached, because
        // this method might call IContext.CreateInitializer
        // recursively for the same type.
        void LateInitialize(IContext context);
    }
}
