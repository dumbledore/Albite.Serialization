using Albite.Serialization.Internal.Writers.Collections;
using Albite.Serialization.Internal.Writers.Primitives;

namespace Albite.Serialization.Internal.Writers
{
    static class Descriptors
    {
        // It's capital that the serializers are traversed in this order
        public static readonly IDescriptor[] Values =
        {
            PrimitiveSerializer.Descriptor,
            EnumSerializer.Descriptor,
            TypeSerializer.Descriptor,
            ArraySerializer.Descriptor,
            CollectionSerializer.LinkedListDescriptor,
            CollectionSerializer.ListDescriptor,
            CollectionSerializer.QueueDescriptor,
            CollectionSerializer.StackDescriptor,
            CollectionSerializer.HashSetDescriptor,
            CollectionSerializer.SortedSetDescriptor,
            DictionarySerializer.DictionaryDescriptor,
            DictionarySerializer.SortedDictionaryDescriptor,
            ClassSerializer.Descriptor,
        };
    }
}
