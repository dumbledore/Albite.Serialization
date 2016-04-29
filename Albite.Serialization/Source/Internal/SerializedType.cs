namespace Albite.Serialization.Internal
{
    enum SerializedType
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
