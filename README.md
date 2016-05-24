# Albite.Serialization
This is a simple serialization library written entirely in C#. There are no native dependencies, so that it can run on any architecture.

The main reason for existence is the inconsistent APIs on Windows Phone.

### What can it serialiaze?
* Primitives
* Arrays
* Standard collections
* Classes

### How do I use it?
Simply throw something at `ObjectWriter.WriteObject()` and than get it back using `ObjectReader.ReadObject()`.

For example, serializing:

    using (ObjectWriter writer = new ObjectWriter(stream))
    {
        writer.WriteObject(10);
        writer.WriteObject(MyEnum.X);
        writer.WriteObject("hello");
        writer.WriteObject(new int[] { 10, 20 });
        writer.WriteObject(new Stack<int>(new int[] { 100, 200 }));
        writer.WriteObject(null);
        writer.WriteObject(typeof(string));
    }

Then, reading it back:

    using (ObjectReader reader = new ObjectReader(stream))
    {
        int i = (int)reader.ReadObject();
        MyEnum e = (MyEnum)reader.ReadObject();
        string s = (string)reader.ReadObject();
        int[] arr = (int[])reader.ReadObject();
        Stack<int> st = (Stack<int>)reader.ReadObject();
        object o = reader.ReadObject();
        Type t = (Type)reader.ReadObject();
    }

### Serializing Custom Classes

If one needs to serialize a class, one needs to add the `Serialized` attribute to members of that class that need to be serialized.

For example, this is a class that is ready to be serialized:

    private class MyClass
    {
        private byte _b;

        [Serialized]
        private int _i;

        [Serialized]
        public string S { get; private set; }

        public byte B
        {
            get { return _b; }
        }

        public int I
        {
            get { return _i; }
        }

        private MyClass() { }

        public MyClass(byte b, int i, string s)
        {
            _b = b;
            _i = i;
            S = s;
        }
    }

Note that `MyClass` needs to have a _default constructor_ in order to be deserialized. Non-public default constructors are fine.

Note also, that `MyClass._b` does not have the `Serialized` attribute and therefore is not serialized. So if one was to serialize:

    writer.WriteObject(new MyClass(10, 1000, "hello"));

And then one was to read it back:

    MyClass c = (MyClass)reader.ReadObject();

One would get:

    Assert.AreEqual(default(byte), c.B);
    Assert.AreEqual(1000, c.I);
    Assert.AreEqual("hello", c.S);

Finally, instead of the `Serialized` attribute, one can use a custom one. One needs to pass it as the last argument of the full constructor.
This may not look apparently useful, but it may be needed for more advanced cases, e.g. an attribute for a column in a database that is backed up by the serializer.

### Full list of serialized types

* Primitives (And primitive-likes)
 * `Boolean`
 * `Char`
 * `SByte`
 * `Byte`
 * `Int16`
 * `UInt16`
 * `Int32`
 * `UInt32`
 * `Int64`
 * `UInt64`
 * `Single`
 * `Double`
 * `Decimal`
 * `DateTime`
 * `String`
 * `DateTimeOffset`
 * `TimeSpan`
 * `Guid`
* `Enum`
* `Type`
* `Array`
* Collections
 * `LinkedList<>`
 * `List<>`
 * `Queue<>`
 * `Stack<>`
 * `HashSet<>`
 * `SortedSet<>`
 * `Dictionary<,>`
 * `SortedDictionary<,>`
* `Class`

