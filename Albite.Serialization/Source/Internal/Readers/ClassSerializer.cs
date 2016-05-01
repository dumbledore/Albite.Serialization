using Albite.Core.Reflection;
using Albite.Serialization.Internal.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Albite.Serialization.Internal.Readers
{
    internal class ClassSerializer : ObjectSerializer
    {
        private ConstructorInfo _ctr;
        private MemberSerializer[] _members;
        private ObjectSerializer _parent;

        private ClassSerializer() { }

        public override void LateInitialize(IContext context)
        {
            Type type = context.ReadType();
            TypeInfo info = type.GetTypeInfo();

            _ctr = createConstructor(context, info);
            _members = createMembers(context, info);
            _parent = createParent(context, info);
        }

        protected override object CreateObject(IContext context)
        {
            if (_ctr == null)
            {
                throw new InvalidOperationException("Cannot create abstract object");
            }

            return _ctr.Invoke(new object[] { });
        }

        protected override void ReadObject(IContext context, object value)
        {
            foreach (MemberSerializer m in _members)
            {
                m.Read(context, value);
            }

            if (_parent != null)
            {
                ObjectSerializer.ReadObject(_parent, context, value);
            }
        }

        private static ConstructorInfo createConstructor(IContext context, TypeInfo info)
        {
            if (!info.IsAbstract)
            {
                // We can't use Activate() because the constructor
                // might be private, and this should be OK.
                // Because of this, however, we can't use Activator either.
                IEnumerable<ConstructorInfo> ctrs = info.DeclaredConstructors.Where(
                    c => c.GetParameters().Length == 0);

                if (ctrs.Count() == 0)
                {
                    throw new InvalidOperationException(
                        "Serialized classes must have a default constructor");
                }

                return ctrs.First();
            }
            else
            {
                return null;
            }
        }

        private static MemberSerializer[] createMembers(IContext context, TypeInfo info)
        {
            // Obtain all available members
            IMemberValue[] members = info.GetSerializedMembers();

            // Read how many members have been serialized
            int membersCount = context.Reader.ReadInt32();

            // We only need to check the count, because afterwards
            // we'll go over all members and if a name of a serializer
            // doesn't match, there will be an exception from Enumerable.First()
            // Also, if the member type doesn't match the one of the
            // serializer, FieldInfo.SetValue() and PropertyInfo.SetValue()
            // would throw an ArgumentException
            if (members.Length != membersCount)
            {
                throw new InvalidOperationException(
                    "Class members are different than what was serialized");
            }

            MemberSerializer[] serializers = new MemberSerializer[membersCount];

            for (int i = 0; i < membersCount; i++)
            {
                string name = context.Reader.ReadString();
                IMemberValue m = members.First(member => member.Name == name);
                ISerializer s = context.CreateSerializer();
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
                return (ObjectSerializer)context.CreateSerializer();
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

            public void Read(IContext context, object entity)
            {
                object value = _serializer.Read(context);
                _member.SetValue(entity, value);
            }
        }

        #region Descriptor
        private class D : IDescriptor
        {
            public SerializedType SerializedType
            {
                get { return SerializedType.Class; }
            }

            public virtual IInitiailzableSerializer Create()
            {
                return new ClassSerializer();
            }
        }

        public static readonly IDescriptor Descriptor = new D();
        #endregion
    }
}
