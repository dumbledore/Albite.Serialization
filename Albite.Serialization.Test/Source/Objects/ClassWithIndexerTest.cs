using Albite.Test;
using System;
using System.Linq;

namespace Albite.Serialization.Test.Objects
{
    public class ClassWithIndexerTest
    {
        private class ClassWithIndexer
        {
            [Serialized]
            string[] _arr;

            private ClassWithIndexer() { }

            public ClassWithIndexer(int size)
            {
                _arr = new string[size];
            }

            [Serialized]
            public string this[int index]
            {
                get { return _arr[index]; }
                set { _arr[index] = value; }
            }

            public override bool Equals(object obj)
            {
                ClassWithIndexer other = obj as ClassWithIndexer;

                if (other == null || (_arr.Length != other._arr.Length))
                {
                    return false;
                }
                else
                {
                    return _arr.SequenceEqual(other._arr);
                }
            }

            public override int GetHashCode()
            {
                return _arr.Length;
            }
        }

        public void Test()
        {
            ClassWithIndexer c = new ClassWithIndexer(10);
            c[0] = "Hello";
            c[1] = "there!";

            // This should not thorw as the indexer would not have been
            // even though the serialized attribute was mistakenly put
            // because indexers are not supported for IMemberValue.
            ClassWithIndexer cRead = (ClassWithIndexer)Helper.Test(c);
            Assert.AreEqual(c, cRead);
        }
    }
}
