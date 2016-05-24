using System.Collections.Generic;

namespace Albite.Serialization.Internal.Writers
{
#if DEBUG
    /// <summary>
    /// Used to track object IDs.
    /// </summary>
    /// <typeparam name="T">The type being cached</typeparam>
#endif
    class ObjectCache<T>
    {
        private readonly Dictionary<T, uint> _cache;

        // ID 0 is not valid
        private uint _currentId = 1;

        public ObjectCache()
        {
            _cache = new Dictionary<T, uint>();
        }

        public ObjectCache(IEqualityComparer<T> comparer)
        {
            _cache = new Dictionary<T, uint>(comparer);
        }

#if DEBUG
        /// <summary>
        /// Gets a unique id associated with the specified object.
        /// </summary>
        /// <param name="obj">The object for which the id is being queried</param>
        /// <param name="id">A unique id for this object</param>
        /// <returns>true if the object was already serialized</returns>
#endif
        public bool GetId(T obj, out uint id)
        {
            if (_cache.TryGetValue(obj, out id))
            {
                return true;
            }
            else
            {
                _cache[obj] = _currentId++;
                return false;
            }
        }
    }
}
