namespace Albite.Serialization.Internal.Readers
{
    internal abstract class ObjectSerializer : IInitiailzableSerializer
    {
        public abstract void LateInitialize(IContext context);

        public object Read(IContext context)
        {
            bool alreadySerialized = context.Reader.ReadBoolean();
            if (alreadySerialized)
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
