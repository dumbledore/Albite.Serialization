namespace Albite.Serialization.Internal
{
    internal enum SerializedType
    {
        Null,
        Cached, // An already serialized definition of a serializer
        Primitive,
        Enum,
        Type,
        Array,
        List,
        LinkedList,
        HashSet,
        SortedSet,
        Stack,
        Queue,
        Dictionary,
        SortedDictionary,
        Class,
        CachedProxy,
        Proxy,
    }
}
