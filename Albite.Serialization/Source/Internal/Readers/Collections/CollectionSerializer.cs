using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Albite.Serialization.Internal.Readers.Collections
{
    class CollectionSerializer : ObjectSerializer
    {
        private readonly Type _collectionType;
        private readonly string _adderName;

        private Type _type;
        private MethodInfo _add;
        private ISerializer _nested;

        private CollectionSerializer(Type collectionType, string adderName)
        {
            _collectionType = collectionType;
            _adderName = adderName;
        }

        public override void LateInitialize(IContext context)
        {
            Type elementType = context.ReadType();

            _type = _collectionType.MakeGenericType(elementType);
            _add = _type.GetTypeInfo().DeclaredMethods.First(m => m.Name == _adderName);
            _nested = context.CreateSerializer();
        }

        protected override object CreateObject(IContext context)
        {
            return Activator.CreateInstance(_type);
        }

        protected override void ReadObject(IContext context, object o)
        {
            int size = context.Reader.ReadInt32();

            for (int i = 0; i < size; i++)
            {
                object value = _nested.Read(context);
                _add.Invoke(o, new object[] { value });
            }
        }

        #region Descriptor
        private class D : IDescriptor
        {
            private readonly SerializedType _stype;
            private readonly Type _collectionType;
            private readonly string _adderName;

            private static readonly string GenericAdder = "Add";

            public D(SerializedType stype, Type collectionType, string adderName)
            {
                _stype = stype;
                _collectionType = collectionType;
                _adderName = adderName;
            }

            public D(SerializedType stype, Type collectionType)
                : this(stype, collectionType, GenericAdder)
            {
            }

            public SerializedType SerializedType
            {
                get { return _stype; }
            }

            public virtual IInitiailzableSerializer Create()
            {
                return new CollectionSerializer(_collectionType, _adderName);
            }
        }

        public static readonly IDescriptor ListDescriptor = new D(SerializedType.List, typeof(List<>));
        public static readonly IDescriptor HashSetDescriptor = new D(SerializedType.HashSet, typeof(HashSet<>));
        public static readonly IDescriptor SortedSetDescriptor = new D(SerializedType.SortedSet, typeof(SortedSet<>));

        // This one needs AddLast
        public static readonly IDescriptor LinkedListDescriptor = new D(SerializedType.LinkedList, typeof(LinkedList<>), "AddLast");

        // Non-generic adders that do not implement ICollection<> and therefore might (and do) not have Add()
        public static readonly IDescriptor StackDescriptor = new D(SerializedType.Stack, typeof(Stack<>), "Push");
        public static readonly IDescriptor QueueDescriptor = new D(SerializedType.Queue, typeof(Queue<>), "Enqueue");
        #endregion
    }
}
