using Albite.Reflection;
using Albite.Serialization.Internal.Helpers;
using System;
using System.Reflection;

namespace Albite.Serialization.Internal.Writers
{
    internal class ClassSerializer : ObjectSerializer
    {
        private MemberSerializer[] _members;
        private ObjectSerializer _parent;

        private ClassSerializer() { }

        public override void LateInitialize(IContext context, Type type)
        {
            context.WriteType(type);

            TypeInfo info = type.GetTypeInfo();

            _members = createMembers(context, info);
            _parent = createParent(context, info);
        }

        protected override void WriteObject(IContext context, object value)
        {
            foreach (var m in _members)
            {
                m.Write(context, value);
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
            IMemberValue[] members = info.GetSerializedMembers();

            // Now Create the member serializers
            MemberSerializer[] serializers = new MemberSerializer[members.Length];

            // And write how many members we have
            context.Writer.Write(members.Length);

            for (int i = 0; i < serializers.Length; i++)
            {
                IMemberValue m = members[i];

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
            Type parent = info.BaseType;

            if (parent == null || typeof(Object).Equals(parent))
            {
                // reached the root
                return null;
            }
            else
            {
                return (ObjectSerializer)context.CreateSerializer(parent);
            }
        }

        private class MemberSerializer
        {
            private readonly IMemberValue _member;
            private readonly ISerializer _serializer;

            public MemberSerializer(ISerializer serializer, IMemberValue member)
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
