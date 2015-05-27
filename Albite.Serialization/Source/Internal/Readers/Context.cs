using Albite.Core.IO;
using System;
using System.Diagnostics;
using System.IO;

namespace Albite.Serialization.Internal.Readers
{
    internal class Context : IContext
    {
        public BinaryReader Reader { get; private set; }
        public int Version { get; private set; }

        private readonly SerializerCache _serializers = new SerializerCache();
        private readonly SerializerCache _proxies = new SerializerCache();
        private readonly ObjectCache<object> _objects = new ObjectCache<object>();
        private readonly ObjectCache<Type> _types = new ObjectCache<Type>();

        public Context(BinaryReader reader)
        {
            int version = reader.ReadInt32();

#if DEBUG
            Debug.WriteLine("New Readers.Context. Version={0}", version);
#endif
            this.Reader = reader;
            this.Version = version;
        }

        public ISerializer CreateSerializer()
        {
            SerializedType type = Reader.ReadSmallEnum<SerializedType>();

#if DEBUG
            Debug.WriteLine("CreateSerializer for type {0}", type);
#endif
            // Handle special types first
            switch (type)
            {
                case SerializedType.Cached:
                    return getCachedSerializer();

                case SerializedType.CachedProxy:
                    return getCachedProxy();

                case SerializedType.Proxy:
                    return createProxy();
            }

            // We must create a new serializer
            IDescriptor desc;
            if (Descriptors.Associations.TryGetValue(type, out desc))
            {
                IInitiailzableSerializer serializer = desc.Create();

                // Cache it
                _serializers.Add(serializer);

                // Now, ready to initialize it
                serializer.LateInitialize(this);

                return serializer;
            }
            else
            {
                throw new InvalidOperationException("Unsupported serializer type: " + type);
            }
        }

        private ISerializer getCachedSerializer()
        {
            uint id = Reader.ReadUInt32();

#if DEBUG
            Debug.WriteLine("Getting cached serializer with id {0}", id);
#endif
            return _serializers.Get(id);
        }

        private ISerializer getCachedProxy()
        {
            uint id = Reader.ReadUInt32();

#if DEBUG
            Debug.WriteLine("Getting cached proxy with id {0}", id);
#endif
            return _proxies.Get(id);
        }

        private ISerializer createProxy()
        {
            IInitiailzableSerializer serializer = new ProxySerializer();
            _proxies.Add(serializer);
            serializer.LateInitialize(this);
            return serializer;
        }

        public Type ReadType()
        {
            Type type;

            bool cached = Reader.ReadBoolean();
            if (cached)
            {
                uint id = Reader.ReadUInt32();
                type = _types.Get(id);

#if DEBUG
                Debug.WriteLine("Read cached type {0} with id {1}", type.FullName, id);
#endif
            }
            else
            {
#if DEBUG
                Debug.WriteLine("Reading a new type");
#endif
                type = Reader.ReadType();
                _types.Add(type);
            }

            return type;
        }

        public ObjectCache<object> ObjectCache
        {
            get { return _objects; }
        }
    }
}
