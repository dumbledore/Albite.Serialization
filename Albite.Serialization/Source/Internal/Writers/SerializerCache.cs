using System;
using System.Collections.Generic;

namespace Albite.Serialization.Internal.Writers
{
    /// <summary>
    /// Used for caching serializers of a type
    /// and making sure each one has a unique ID.
    /// </summary>
    internal class SerializerCache
    {
        private readonly Dictionary<Type, KeyValuePair<ISerializer, uint>> _cache
            = new Dictionary<Type, KeyValuePair<ISerializer, uint>>();

        // ID 0 is not valid
        private uint _currentId = 1;

        public bool TryGet(Type type, out ISerializer serializer, out uint id)
        {
            KeyValuePair<ISerializer, uint> s;
            if (_cache.TryGetValue(type, out s))
            {
                serializer = s.Key;
                id = s.Value;
                return true;
            }
            else
            {
                serializer = null;
                id = 0;
                return false;
            }
        }

        public uint Add(Type type, ISerializer serializer)
        {
            uint id = _currentId++;
            _cache[type] = new KeyValuePair<ISerializer, uint>(serializer, id);
            return id;
        }
    }
}
