using System;
using System.Collections;
using System.Collections.Generic;

namespace Albite.Serialization.Internal.Readers
{
    internal class DictionarySerializer : ObjectSerializer
    {
        private readonly Type _collectionType;

        private Type _type;
        private ISerializer _keys;
        private ISerializer _values;

        private DictionarySerializer(Type collectionType)
        {
            _collectionType = collectionType;
        }

        public override void LateInitialize(IContext context)
        {
            // Read the types first
            Type keyType = context.ReadType();
            Type valueType = context.ReadType();

            // Construct the whole type
            _type = _collectionType.MakeGenericType(keyType, valueType);

            // Get the nested serializers
            _keys = context.CreateSerializer();
            _values = context.CreateSerializer();
        }

        protected override object CreateObject(IContext context)
        {
            return Activator.CreateInstance(_type);
        }

        protected override void ReadObject(IContext context, object o)
        {
            IDictionary dictionary = (IDictionary)o;

            int size = context.Reader.ReadInt32();

            for (int i = 0; i < size; i++)
            {
                object key = _keys.Read(context);
                object value = _values.Read(context);
                dictionary.Add(key, value);
            }
        }

        #region Descriptor
        private class D : IDescriptor
        {
            private readonly SerializedType _stype;
            private readonly Type _collectionType;

            public D(SerializedType stype, Type collectionType)
            {
                _stype = stype;
                _collectionType = collectionType;
            }

            public SerializedType SerializedType
            {
                get { return _stype; }
            }

            public virtual IInitiailzableSerializer Create()
            {
                return new DictionarySerializer(_collectionType);
            }
        }

        public static readonly IDescriptor DictionaryDescriptor = new D(SerializedType.Dictionary, typeof(Dictionary<,>));
        public static readonly IDescriptor SortedDictionaryDescriptor = new D(SerializedType.SortedDictionary, typeof(SortedDictionary<,>));
        #endregion
    }
}
