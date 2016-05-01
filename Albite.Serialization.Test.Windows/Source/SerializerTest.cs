using Albite.Core;
using Albite.Core.Collections;
using Albite.Core.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Albite.Serialization.Test.Windows
{
    public abstract class SerializerTest
    {
        protected static void test(object value)
        {
            test(new object[] { value });
        }

        protected static void test(object[] values)
        {
            byte[] bytes = write(values);
            object[] valuesRead = read(bytes, values.Length);

            EqualityVerifier verifier = new EqualityVerifier();
            for (int i = 0; i < values.Length; i++)
            {
                verifier.Verify(values[i], valuesRead[i]);
            }
        }

        private static byte[] write(object[] values)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (ObjectWriter writer = new ObjectWriter(stream, Encoding.UTF8, true))
                {
                    foreach (var value in values)
                    {
                        string typeName = (value == null) ? null : value.GetType().FullName;
                        long l = stream.Length;
                        Debug.WriteLine("[Test] Writing {0}:{1}", typeName, value);
                        writer.WriteObject(value);
                        l = stream.Length - l;
                        Debug.WriteLine("[Test] Finished writing {0}:{1} Payload: {2} bytes\n",
                            typeName, value, l);
                    }
                }

                return stream.ToArray();
            }
        }

        private static object[] read(byte[] data, int count)
        {
            object[] values = new object[count];

            using (MemoryStream stream = new MemoryStream(data))
            {
                using (ObjectReader reader = new ObjectReader(stream, Encoding.UTF8, true))
                {
                    for (int i = 0; i < count; i++)
                    {
                        Debug.WriteLine("[Test] Reading next value...");
                        object valueRead = reader.ReadObject();
                        values[i] = valueRead;
                        Debug.WriteLine("[Test] Read {0}:{1}\n", valueRead == null ? null : valueRead.GetType().FullName, valueRead);
                    }
                }
            }

            return values;
        }

        private class EqualityVerifier
        {
            public class Exception : System.Exception
            {
                public Exception() { }
                public Exception(string message) : base(message) { }
            }

            // Cache the info for SerializedAttribute
            private static readonly TypeInfo _info = typeof(SerializedAttribute).GetTypeInfo();

            // Objects Cache
            private readonly Dictionary<object, object> _cache
                = new Dictionary<object, object>(new IdentityEqualityComparer<object>());

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
                        Debug.WriteLine("Comparing values `{0}` and `{1}`", value, valueRead);
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
                            Debug.WriteLine("Object `{0}` already (being) serialized", value);
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
                Debug.WriteLine("Comparing {0} enumerables", sorted ? "sorted" : "unsorted");

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
                Debug.WriteLine("Comparing {0} dictionaries", sorted ? "sorted" : "unsorted");

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
                verifyClass(o1.GetType(), o1, o2);
            }

            private void verifyClass(Type type, object o1, object o2)
            {
                TypeInfo info = type.GetTypeInfo();

                if (!o1.GetType().Equals(o2.GetType()))
                {
                    throw new Exception("Read value has a different type");
                }

                // We already know that both objects have the same type
                IMemberValue[] members = getMembers(info);

                Debug.WriteLine("Comparing objects of class `{0}`", type.Name);

                foreach (var m in members)
                {
                    Debug.WriteLine("Comparing member `{0}` of type `{1}` for class `{2}`",
                        m.Name, m.MemberType.Name, type.Name);

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

            private static IMemberValue[] getMembers(TypeInfo info)
            {
                return info.GetMembers((memberType, memberInfo) =>
                {
                    return memberInfo.CustomAttributes.Any(
                        a => _info.IsAssignableFrom(a.AttributeType.GetTypeInfo()));
                });
            }
        }
    }
}
