﻿using Albite.Collections;
using Albite.Diagnostics;
using Albite.IO;
using System;
using System.IO;
using System.Reflection;

namespace Albite.Serialization.Internal.Writers
{
    class Context : ContextBase, IContext
    {
        public BinaryWriter Writer { get; private set; }
        public override Version Version { get { return _version; } }

        private readonly SerializerCache _serializers = new SerializerCache();
        private readonly SerializerCache _proxies = new SerializerCache();
        private readonly ObjectCache<object> _objects = new ObjectCache<object>(new IdentityEqualityComparer<object>());
        private readonly ObjectCache<Type> _types = new ObjectCache<Type>();
        private readonly Version _version;

        public Context(BinaryWriter writer)
            : this(writer, typeof(SerializedAttribute))
        {
        }

        public Context(BinaryWriter writer, Type attributeType)
            : base(attributeType)
        {
            this.Writer = writer;
            this._version = GetType().GetTypeInfo().Assembly.GetName().Version;
#if DEBUG
            Logger.LogMessage("New Writers.Context. Version={0}", Version);
#endif
            writer.Write(Version.ToString());
        }

        public ISerializer CreateSerializer(Type type)
        {
#if DEBUG
            Logger.LogMessage("CreateSerializer for {0}", type.FullName);
#endif

            ISerializer serializer;
            uint id;
            if (_serializers.TryGet(type, out serializer, out id))
            {
#if DEBUG
                Logger.LogMessage("Serializer for {0} cached with id {1}", type.FullName, id);
#endif
                Writer.WriteSmallEnum(SerializedType.Cached);
                Writer.Write(id);
                return serializer;
            }
            else
            {
                foreach (var descriptor in Descriptors.Values)
                {
                    if (descriptor.IsTypeSupported(type))
                    {
#if DEBUG
                        Logger.LogMessage("Creating serializer of type {0} for type {1}",
                            descriptor.SerializedType, type.FullName);
#endif
                        // Serialize the type of the serialier
                        Writer.WriteSmallEnum(descriptor.SerializedType);

                        // Create the serializer
                        IInitiailzableSerializer s = descriptor.Create();

                        // Cache it
                        _serializers.Add(type, s);

                        // And now initialize it
                        s.LateInitialize(this, type);

                        return s;
                    }
                }

                throw new NotSupportedException(String.Format(
                    "Type {0} is not supported", type.Name));
            }
        }

        public ISerializer CreateProxy(Type type)
        {
            ISerializer serializer;

            // Check if the proxy has already been created
            uint id;
            if (_proxies.TryGet(type, out serializer, out id))
            {
#if DEBUG
                Logger.LogMessage("Proxy serializer for {0} cached with id {1}", type.FullName, id);
#endif
                // Already cached, write only the id
                Writer.WriteSmallEnum(SerializedType.CachedProxy);
                Writer.Write(id);
            }
            else
            {
#if DEBUG
                Logger.LogMessage("Creating proxy serializer for type {0}", type.FullName);
#endif
                Writer.WriteSmallEnum(SerializedType.Proxy);
                // No need to write the ID
                IInitiailzableSerializer s = new ProxySerializer();
                _proxies.Add(type, s);
                s.LateInitialize(this, type);
                serializer = s;
            }

            return serializer;
        }

        // We cannot serialize types like objects.
        // That is because we cannot rely on two Type objects being the same instance
        // even though it is usually true. This means that the IdentityEqualityComparer
        // might not be useful and therefore we might end up serializing more than
        // what we have, i.e. the same Type might be serialized multiple times,
        // meaning there would be no gain in serializing it.
        public void WriteType(Type type)
        {
            uint id;
            if (_types.GetId(type, out id))
            {
#if DEBUG
                Logger.LogMessage("Type {0} cached with id {1}", type.FullName, id);
#endif
                Writer.Write(true);
                Writer.Write(id);
            }
            else
            {
#if DEBUG
                Logger.LogMessage("Writing type {0}", type.FullName);
#endif
                Writer.Write(false);
                Writer.Write(type);
            }
        }

        public ObjectCache<object> ObjectCache
        {
            get { return _objects; }
        }
    }
}
