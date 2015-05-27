using Albite.Serialization.Internal.Readers.Collections;
using Albite.Serialization.Internal.Readers.Primitives;
using System.Collections.Generic;

namespace Albite.Serialization.Internal.Readers
{
    internal static class Descriptors
    {
        public static readonly IDescriptor[] Values;
        public static readonly IDictionary<SerializedType, IDescriptor> Associations;

        static Descriptors()
        {
            Values = new IDescriptor[] {
                PrimitiveSerializer.Descriptor,
                EnumSerializer.Descriptor,
                TypeSerializer.Descriptor,
                ArraySerializer.Descriptor,
                CollectionSerializer.ListDescriptor,
                CollectionSerializer.LinkedListDescriptor,
                CollectionSerializer.HashSetDescriptor,
                CollectionSerializer.SortedSetDescriptor,
                CollectionSerializer.StackDescriptor,
                CollectionSerializer.QueueDescriptor,
                DictionarySerializer.DictionaryDescriptor,
                DictionarySerializer.SortedDictionaryDescriptor,
                ClassSerializer.Descriptor,
            };

            Associations = new Dictionary<SerializedType, IDescriptor>(Values.Length);
            foreach (var v in Values)
            {
                Associations[v.SerializedType] = v;
            }
        }
    }
}
