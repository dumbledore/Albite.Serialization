using Albite.Serialization.Internal.Readers;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Albite.Serialization
{
    /// <summary>
    /// Used for reading objects from an input stream.
    /// </summary>
    public class ObjectReader : BinaryReader
    {
        private readonly IContext _context;

        /// <summary>
        /// Initializes a new instance of the <c>Albite.Serialization.ObjectWriter</c>
        /// class based on the specified stream and using UTF-8 encoding.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <exception cref="System.ArgumentException">
        /// The stream does not support reading, is <c>null</c>, or is already closed.
        /// </exception>
        public ObjectReader(Stream input)
            : base(input)
        {
            this._context = new Context(this);
        }

        /// <summary>
        /// Initializes a new instance of the <c>Albite.Serialization.ObjectWriter</c>
        /// class based on the specified stream and character encoding.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <exception cref="System.ArgumentException">
        /// The stream does not support reading, is <c>null</c>, or is already closed.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// <c>encoding</c> is <c>null</c>.
        /// </exception>
        public ObjectReader(Stream input, Encoding encoding)
            : base(input, encoding)
        {
            this._context = new Context(this);
        }

        /// <summary>
        /// Initializes a new instance of the <c>Albite.Serialization.ObjectWriter</c>
        /// class based on the specified stream and character encoding, and optionally
        /// leaves the stream open.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="leaveOpen">
        /// <c>true</c> to leave the stream open after the
        /// <c>Albite.Serialization.ObjectWriter</c> object is disposed;
        /// otherwise, <c>false</c>.
        /// </param>
        /// <exception cref="System.ArgumentException">
        /// The stream does not support reading, is <c>null</c>, or is already closed.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// <c>encoding</c> or <c>input</c> is <c>null</c>.
        /// </exception>
        public ObjectReader(Stream input, Encoding encoding, bool leaveOpen)
            : base(input, encoding, leaveOpen)
        {
            this._context = new Context(this);
        }

        /// <summary>
        /// Read an object from an input stream.
        /// </summary>
        /// <returns>The read object.</returns>
        public object ReadObject()
        {
            var r = _context.CreateSerializer();
            return r.Read(_context);
        }

#if DEBUG
        public override bool ReadBoolean()
        {
            bool value = base.ReadBoolean();
            Debug.WriteLine("Read bool: {0}", value);
            return value;
        }

        public override byte ReadByte()
        {
            byte value = base.ReadByte();
            Debug.WriteLine("Read byte: {0}", value);
            return value;
        }

        public override int ReadInt32()
        {
            int value = base.ReadInt32();
            Debug.WriteLine("Read int: {0}", value);
            return value;
        }

        public override uint ReadUInt32()
        {
            uint value = base.ReadUInt32();
            Debug.WriteLine("Read uint: {0}", value);
            return value;
        }

        public override string ReadString()
        {
            string value = base.ReadString();
            Debug.WriteLine("Read string: {0}", value);
            return value;
        }
#endif
    }
}
