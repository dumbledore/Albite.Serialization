using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Albite.Core.Collections;

namespace Albite.Serialization.Internal.Writers.Collections
{
    internal class CollectionSerializer : ObjectSerializer
    {
        protected CollectionSerializer() { }

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

            public D(SerializedType stype, Type type)
            {
                _stype = stype;
                _type = type;
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
                return new CollectionSerializer();
            }
        }

        public static readonly IDescriptor LinkedListDescriptor = new D(SerializedType.LinkedList, typeof(LinkedList<>));
        public static readonly IDescriptor ListDescriptor = new D(SerializedType.List, typeof(List<>));
        public static readonly IDescriptor QueueDescriptor = new D(SerializedType.Queue, typeof(Queue<>));
        public static readonly IDescriptor StackDescriptor = new D(SerializedType.Stack, typeof(Stack<>));
        public static readonly IDescriptor HashSetDescriptor = new D(SerializedType.HashSet, typeof(HashSet<>));
        public static readonly IDescriptor SortedSetDescriptor = new D(SerializedType.SortedSet, typeof(SortedSet<>));
        #endregion
    }
}
