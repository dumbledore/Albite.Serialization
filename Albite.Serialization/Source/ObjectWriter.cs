using Albite.Diagnostics;
using Albite.IO;
using Albite.Serialization.Internal.Writers;
using System.IO;
using System.Text;

namespace Albite.Serialization
{
    /// <summary>
    /// Used for writing objects to an output stream.
    /// </summary>
    public class ObjectWriter : BinaryWriter
    {
        private readonly IContext _context;

        /// <summary>
        /// Initializes a new instance of the <c>Albite.Serialization.ObjectWriter</c>
        /// class based on the specified stream and using UTF-8 encoding.
        /// </summary>
        /// <param name="output">The output stream.</param>
        /// <exception cref="System.ArgumentException">
        /// The stream does not support writing or is already closed.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// <c>output</c> is <c>null</c>.
        /// </exception>
        public ObjectWriter(Stream output)
            : base(output)
        {
            this._context = new Context(this);
        }

        /// <summary>
        /// Initializes a new instance of the <c>Albite.Serialization.ObjectWriter</c>
        /// class based on the specified stream and character encoding.
        /// </summary>
        /// <param name="output">The output stream.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <exception cref="System.ArgumentException">
        /// The stream does not support writing or is already closed.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// <c>output</c> or <c>encoding</c> is <c>null</c>.
        /// </exception>
        public ObjectWriter(Stream output, Encoding encoding)
            : base(output, encoding)
        {
            this._context = new Context(this);
        }

        /// <summary>
        /// Initializes a new instance of the <c>Albite.Serialization.ObjectWriter</c>
        /// class based on the specified stream and character encoding, and optionally
        /// leaves the stream open.
        /// </summary>
        /// <param name="output">The output stream.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="leaveOpen">
        /// true to leave the stream open after the
        /// <c>Albite.Serialization.ObjectWriter</c> object is disposed;
        /// otherwise, false.
        /// </param>
        /// <exception cref="System.ArgumentException">
        /// The stream does not support writing or is already closed.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// <c>output</c> or <c>encoding</c> is <c>null</c>.
        /// </exception>
        public ObjectWriter(Stream output, Encoding encoding, bool leaveOpen)
            : base(output, encoding, leaveOpen)
        {
            this._context = new Context(this);
        }

        /// <summary>
        /// Serialize an object to an output stream.
        /// </summary>
        /// <param name="value">The object being serialized.</param>
        public void WriteObject(object value)
        {
            if (value == null)
            {
                this.WriteSmallEnum(Internal.SerializedType.Null);
                return;
            }

            var s = _context.CreateSerializer(value == null ? null : value.GetType());
            s.Write(_context, value);
        }

#if DEBUG
        public override void Write(bool value)
        {
            Logger.LogMessage("Write bool: {0}", value);
            base.Write(value);
        }

        public override void Write(byte value)
        {
            Logger.LogMessage("Write byte: {0}", value);
            base.Write(value);
        }

        public override void Write(int value)
        {
            Logger.LogMessage("Write int: {0}", value);
            base.Write(value);
        }

        public override void Write(uint value)
        {
            Logger.LogMessage("Write uint: {0}", value);
            base.Write(value);
        }

        public override void Write(string value)
        {
            base.Write(value);
            Logger.LogMessage("Write string: {0}", value);
        }
#endif
    }
}
