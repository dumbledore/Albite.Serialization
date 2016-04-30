using Albite.Core.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Albite.Serialization.Internal.Writers.Collections
{
    internal class CollectionSerializer : ObjectSerializer
    {
        private readonly bool _reverse;

        protected CollectionSerializer() : this(false) { }

        protected CollectionSerializer(bool reverse)
        {
            _reverse = reverse;
        }

        protected ISerializer _nested;

        public override void LateInitialize(IContext context, Type type)
        {
            Type elementType = type.GenericTypeArguments[0];
            context.WriteType(elementType);
            _nested = context.CreateProxy(elementType);
        }

        protected override void WriteObject(IContext context, object value)
        {
            IEnumerable collection = (IEnumerable)value;

            // Now write the size
            context.Writer.Write(collection.Count());

            // Needed for Stack.
            if (_reverse)
            {
                collection = collection.Reverse();
            }

            // And finally write all the elements
            foreach (var element in collection)
            {
                _nested.Write(context, element);
            }
        }

        #region Descriptor
        protected class D : IDescriptor
        {
            private readonly SerializedType _stype;
            private readonly Type _type;
            private readonly bool _reverse;

            public D(SerializedType stype, Type type, bool reverse = false)
            {
                _stype = stype;
                _type = type;
                _reverse = reverse;
            }

            public bool IsTypeSupported(Type type)
            {
                TypeInfo info = type.GetTypeInfo();
                return info.IsGenericType && (info.GetGenericTypeDefinition() == _type);
            }

            public SerializedType SerializedType
            {
                get { return _stype; }
            }

            public virtual IInitiailzableSerializer Create()
            {
                return new CollectionSerializer(_reverse);
            }
        }

        public static readonly IDescriptor LinkedListDescriptor = new D(SerializedType.LinkedList, typeof(LinkedList<>));
        public static readonly IDescriptor ListDescriptor = new D(SerializedType.List, typeof(List<>));
        public static readonly IDescriptor QueueDescriptor = new D(SerializedType.Queue, typeof(Queue<>));
        public static readonly IDescriptor StackDescriptor = new D(SerializedType.Stack, typeof(Stack<>), true);
        public static readonly IDescriptor HashSetDescriptor = new D(SerializedType.HashSet, typeof(HashSet<>));
        public static readonly IDescriptor SortedSetDescriptor = new D(SerializedType.SortedSet, typeof(SortedSet<>));
        #endregion
    }
}
