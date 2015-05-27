using System;

namespace Albite.Serialization.Internal.Writers
{
    internal interface ISerializer
    {
        void Write(IContext context, object value);
    }
}
