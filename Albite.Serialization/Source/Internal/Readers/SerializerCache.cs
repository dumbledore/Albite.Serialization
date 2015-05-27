using System.Collections.Generic;

namespace Albite.Serialization.Internal.Readers
{
    internal class SerializerCache
    {
        private readonly Dictionary<uint, ISerializer> _cache;
        private uint _currentId = 1;

        public SerializerCache()
        {
            this._cache = new Dictionary<uint, ISerializer>();
        }

        public ISerializer Get(uint id)
        {
            return _cache[id];
        }

        public void Add(ISerializer s)
        {
            _cache[_currentId++] = s;
        }
    }
}
