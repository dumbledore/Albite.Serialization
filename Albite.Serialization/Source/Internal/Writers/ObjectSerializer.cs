using Albite.Core.IO;
using System;

namespace Albite.Serialization.Internal.Writers
{
    internal abstract class ObjectSerializer : IInitiailzableSerializer
    {
        public abstract void LateInitialize(IContext context, Type type);

        public void Write(IContext context, object value)
        {
            if (value == null)
            {
                // Can't be null, this is handled in ClassProxySerializer
                throw new System.InvalidOperationException("value can't be null");
            }

            // Was it serialized already?
            uint id;
            if (context.ObjectCache.GetId(value, out id))
            {
                // Already seriaized
                context.Writer.Write(true);
                context.Writer.Write(id);
            }
            else
            {
                context.Writer.Write(false);
                // Now serialize it, no need to write the ID
                WriteObject(context, value);
            }
        }

        protected abstract void WriteObject(IContext context, object value);

        // This is here so that child classes can call WriteObject(IContext, object) on a reference through here.
        protected static void WriteObject(ObjectSerializer s, IContext context, object value)
        {
            s.WriteObject(context, value);
        }
    }
}
