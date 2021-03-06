﻿using System.Collections.Generic;

namespace Albite.Serialization.Internal.Readers
{
    class ObjectCache<T>
    {
        private readonly Dictionary<uint, T> _cache = new Dictionary<uint, T>();
        private uint _currentId = 1;

#if DEBUG
        /// <summary>
        /// Gets the object, associated with the specified unique id.
        /// </summary>
        /// <param name="id">The object id</param>
        /// <returns>The object for the specified id</returns>
#endif
        public T Get(uint id)
        {
            return _cache[id];
        }

#if DEBUG
        /// <summary>
        /// Caches the object and retrieves its unique id
        /// </summary>
        /// <param name="value">The object to be cached</param>
        /// <returns>The object id</returns>
#endif
        public uint Add(T value)
        {
            uint id = _currentId++;
            _cache[id] = value;
            return id;
        }
    }
}
