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
                get
                {
                    return _arr[index];
                }

                set
                {
                    _arr[index] = value;
                }
            }
        }

        public void Test()
        {
            ClassWithIndexer c = new ClassWithIndexer(10);
            c[0] = "Hello";
            c[1] = "there!";

            // this should pass as the indexer would not have been serialized
            // even though the serialized attribute was mistakenly put
            Helper.Test(c);
        }
    }
}
