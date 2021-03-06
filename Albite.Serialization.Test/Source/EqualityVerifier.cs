﻿using Albite.Collections;
using Albite.Diagnostics;
using Albite.Reflection;
using Albite.Serialization.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Albite.Serialization.Test
{
    class EqualityVerifier
    {
        private class Exception : System.Exception
        {
            public Exception() { }
            public Exception(string message) : base(message) { }
        }

        // Objects Cache
        private readonly Dictionary<object, object> _cache
            = new Dictionary<object, object>(new IdentityEqualityComparer<object>());

        private Type _attributeType;

        public EqualityVerifier(Type attributeType)
        {
            _attributeType = attributeType;
        }

        public void Verify(object value, object valueRead)
        {
            if (value == null)
            {
                if (valueRead != null)
                {
                    throw new Exception("Read value must is not null, when the written value is null");
                }
            }
            else
            {
                Type type = value.GetType();
                TypeInfo info = type.GetTypeInfo();
                TypeCode code = type.GetTypeCode();
                if (info.IsType() || info.IsEnum || (code != TypeCode.Object && code != TypeCode.Empty))
                {
                    // Compare them directly for value(-like) types
                    Logger.LogMessage("Comparing values `{0}` and `{1}`", value, valueRead);
                    if (!value.Equals(valueRead))
                    {
                        throw new Exception("Values don't match");
                    }
                }
                else if (info.IsClass || info.IsArray)
                {
                    // Has the pair already been compared or being compared now?
                    object o2Cached;
                    if (_cache.TryGetValue(value, out o2Cached))
                    {
                        // Already compared or being compared now.
                        // Make sure RHS is still the same, i.e. o2 is o2Cached
                        Logger.LogMessage("Object `{0}` already (being) serialized", value);
                        if (!object.ReferenceEquals(valueRead, o2Cached))
                        {
                            throw new Exception("References do not match");
                        }
                    }
                    else
                    {
                        // Cache the pair
                        _cache[value] = valueRead;

                        if (type.IsArray)
                        {
                            verifyEnumerable((IEnumerable)value, (IEnumerable)valueRead, true);
                        }
                        else if (info.IsGenericType)
                        {
                            // Collections
                            Type genericType = type.GetGenericTypeDefinition();
                            if (
                                typeof(List<>).Equals(genericType) ||
                                typeof(LinkedList<>).Equals(genericType) ||
                                typeof(Stack<>).Equals(genericType) ||
                                typeof(Queue<>).Equals(genericType) ||
                                typeof(SortedSet<>).Equals(genericType))
                            {
                                verifyEnumerable((IEnumerable)value, (IEnumerable)valueRead, true);
                            }
                            else if (typeof(HashSet<>).Equals(genericType))
                            {
                                verifyEnumerable((IEnumerable)value, (IEnumerable)valueRead, false);

                            }
                            else if (typeof(Dictionary<,>).Equals(genericType))
                            {
                                verifyDictionary((IDictionary)value, (IDictionary)valueRead, true);
                            }
                            else if (typeof(SortedDictionary<,>).Equals(genericType))
                            {
                                verifyDictionary((IDictionary)value, (IDictionary)valueRead, false);
                            }
                        }
                        else
                        {
                            verifyClass(value, valueRead);
                        }
                    }
                }
                else
                {
                    throw new InvalidOperationException("Can't get here");
                }
            }
        }

        private void verifyEnumerable(IEnumerable e1, IEnumerable e2, bool sorted)
        {
            Logger.LogMessage("Comparing {0} enumerables", sorted ? "sorted" : "unsorted");

            List<object> l1 = new List<object>();
            foreach (var e in e1) l1.Add(e);

            List<object> l2 = new List<object>();
            foreach (var e in e2) l2.Add(e);

            if (l1.Count != l2.Count)
            {
                throw new Exception("Enumerations have different lengths");
            }

            if (sorted)
            {
                for (int i = 0; i < l1.Count; i++)
                {
                    Verify(l1[i], l2[i]);
                }
            }
            else
            {
                foreach (var e in l1)
                {
                    int index = l2.IndexOf(e);
                    if (index == -1)
                    {
                        throw new Exception("Value {0} missing in read enumeration" + e);
                    }

                    Verify(e, l2[index]);
                }
            }
        }

        private void verifyDictionary(IDictionary d1, IDictionary d2, bool sorted)
        {
            Logger.LogMessage("Comparing {0} dictionaries", sorted ? "sorted" : "unsorted");

            if (d1.Count != d2.Count)
            {
                throw new Exception("Enumerations have different lengths");
            }

            if (sorted)
            {
                IDictionaryEnumerator e1 = d1.GetEnumerator();
                IDictionaryEnumerator e2 = d2.GetEnumerator();

                while (e1.MoveNext() && e2.MoveNext())
                {
                    Verify(e1.Current, e2.Current);
                }
            }
            else
            {
                foreach (var e in d1)
                {
                    IDictionaryEnumerator e1 = d1.GetEnumerator();

                    while (e1.MoveNext())
                    {
                        object v2 = d2[e1.Key];
                        Verify(e1.Value, v2);
                    }
                }
            }
        }

        private void verifyClass(object o1, object o2)
        {
            if (!o1.GetType().Equals(o2.GetType()))
            {
                throw new Exception("Read value has a different type");
            }

            verifyClass(o1.GetType(), o1, o2);
        }

        // The type parameter here is used
        // to serialize the objects up the hierarchy, i.e.
        // starting from the actual type of the object
        // and then going up to the root, checking the members
        // at each level.
        private void verifyClass(Type type, object o1, object o2)
        {
            if (typeof(Object).Equals(type))
            {
                // Nothing to verify on the root type
                return;
            }

            TypeInfo info = type.GetTypeInfo();

            // We already know that both objects have the same type
            List<IMemberValue> members =
                ContextBase.GetSerializedMembers(info, _attributeType).ToList();

            Logger.LogMessage("Comparing objects of class `{0}`", type.Name);

            foreach (var m in members)
            {
                Logger.LogMessage("Comparing member `{0}` of type `{1}` for class `{2}`",
                    m.Info.Name, m.MemberType.Name, type.Name);

                object value1 = m.GetValue(o1);
                object value2 = m.GetValue(o2);
                Verify(value1, value2);
            }

            Type parent = type.GetTypeInfo().BaseType;
            if (parent != null)
            {
                verifyClass(parent, o1, o2);
            }
        }
    }
}
