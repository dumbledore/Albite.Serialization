using System;
using System.Collections;
using System.Collections.Generic;

namespace Albite.Serialization.Internal.Writers.Collections
{
    class DictionarySerializer : CollectionSerializer
    {
        private DictionarySerializer() { }

        public override void LateInitialize(IContext context, Type type)
        {
            _nested = new Serializer(context, type);
        }

        // Special serializer for dictionary entries
        private class Serializer : ISerializer
        {
            private ISerializer _keys;
            private ISerializer _values;

            public Serializer(IContext context, Type type)
            {
                // Get the element types
                Type keyType = type.GenericTypeArguments[0];
                Type valueType = type.GenericTypeArguments[1];

                // Serializer the types
                context.WriteType(keyType);
                context.WriteType(valueType);

                // Prepare the nested serializer proxies
                _keys = context.CreateProxy(keyType);
                _values = context.CreateProxy(valueType);
            }

            public void Write(IContext context, object value)
            {
                DictionaryEntry entry = (DictionaryEntry)value;
                _keys.Write(context, entry.Key);
                _values.Write(context, entry.Value);
            }
        }

        #region Descriptor
        private class DD : D
        {
            public DD(SerializedType stype, Type type) : base(stype, type) { }

            public override IInitiailzableSerializer Create()
            {
                return new DictionarySerializer();
            }
        }

        public static readonly IDescriptor DictionaryDescriptor = new DD(SerializedType.Dictionary, typeof(Dictionary<,>));
        public static readonly IDescriptor SortedDictionaryDescriptor = new DD(SerializedType.SortedDictionary, typeof(SortedDictionary<,>));
        #endregion
    }
}
