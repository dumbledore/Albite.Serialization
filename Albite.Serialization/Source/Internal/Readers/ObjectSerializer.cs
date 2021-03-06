﻿namespace Albite.Serialization.Internal.Readers
{
    abstract class ObjectSerializer : IInitiailzableSerializer
    {
        public abstract void LateInitialize(IContext context);

        public object Read(IContext context)
        {
            bool serialized = context.Reader.ReadBoolean();

            if (serialized)
            {
                uint id = context.Reader.ReadUInt32();
                return context.ObjectCache.Get(id);
            }
            else
            {
                // Create the object
                object o = CreateObject(context);

                // Cache it
                context.ObjectCache.Add(o);

                // Go on with reading it
                ReadObject(context, o);

                return o;
            }
        }

        protected abstract object CreateObject(IContext context);

        protected abstract void ReadObject(IContext context, object value);

        // This is here so that child classes can call ReadObject(IContext, object) on a reference through here.
        protected static void ReadObject(ObjectSerializer s, IContext context, object value)
        {
            s.ReadObject(context, value);
        }
    }
}
