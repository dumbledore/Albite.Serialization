using Albite.Core.Reflection;
using Albite.Serialization.Internal.Helpers;
using System;
using System.Reflection;

namespace Albite.Serialization.Internal.Writers
{
    internal class ClassSerializer : ObjectSerializer
    {
        private MemberSerializer[] _members;
        private ObjectSerializer _parent;
        private Boolean _serialized;

        private ClassSerializer() { }

        public override void LateInitialize(IContext context, Type type)
        {
            context.WriteType(type);

            TypeInfo info = type.GetTypeInfo();
            _serialized = info.IsSerialized();

            // Create the member serializers if the class
            // has the Serialized attribute
            if (_serialized)
            {
                context.Writer.Write(true);
                _members = createMembers(context, info);
            }
            else
            {
                context.Writer.Write(false);
                _members = null;
            }

            // Create the parent
            _parent = createParent(context, info);
        }

        protected override bool IsSerialized
        {
            get { return _serialized; }
        }

        protected override void WriteObject(IContext context, object value)
        {
            if (_members != null)
            {
                // This class has the Serialized attribute
                foreach (var m in _members)
                {
                    m.Write(context, value);
                }
            }

            if (_parent != null)
            {
                // Parent can be non-null on non-serialized classes
                ObjectSerializer.WriteObject(_parent, context, value);
            }
        }

        private static MemberSerializer[] createMembers(IContext context, TypeInfo info)
        {
            // Get the serializers for the type's members
            MemberValue[] members = info.GetSerializedMembers();

            // Now Create the member serializers
            MemberSerializer[] serializers = new MemberSerializer[members.Length];

            // And write how many members we have
            context.Writer.Write(members.Length);

            for (int i = 0; i < serializers.Length; i++)
            {
                MemberValue m = members[i];

                // Write the name of the member
                context.Writer.Write(m.Name);

                // Now this will create the serializer and
                // write its signature
                ISerializer s = context.CreateProxy(m.MemberType);

                serializers[i] = new MemberSerializer(s, m);
            }

            return serializers;
        }

        private static ObjectSerializer createParent(IContext context, TypeInfo info)
        {
            // We'll need to serialize the parent as well in all cases
            Type parent = info.BaseType;

            if (parent != null && !typeof(Object).Equals(parent))
            {
                context.Writer.Write(true);
                return (ObjectSerializer)context.CreateSerializer(parent);
            }
            else
            {
                // Not serializing the parent
                context.Writer.Write(false);
                return null;
            }
        }

        private class MemberSerializer
        {
            private readonly MemberValue _member;
            private readonly ISerializer _serializer;

            public MemberSerializer(ISerializer serializer, MemberValue member)
            {
                this._serializer = serializer;
                this._member = member;
            }

            public void Write(IContext context, object entity)
            {
                object value = _member.GetValue(entity);
                _serializer.Write(context, value);
            }
        }

        #region Descriptor
        private class D : IDescriptor
        {
            public bool IsTypeSupported(Type type)
            {
                return type.GetTypeInfo().IsClass;
            }

            public SerializedType SerializedType
            {
                get { return SerializedType.Class; }
            }

            public IInitiailzableSerializer Create()
            {
                return new ClassSerializer();
            }
        }

        public static readonly IDescriptor Descriptor = new D();
        #endregion
    }
}
