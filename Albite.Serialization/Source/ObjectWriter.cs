using Albite.Serialization.Internal.Writers;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Albite.Serialization
{
    public class ObjectWriter : BinaryWriter
    {
        private readonly IContext _context;

        public ObjectWriter(Stream output)
            : base(output)
        {
            this._context = new Context(this);
        }

        public ObjectWriter(Stream output, Encoding encoding)
            : base(output, encoding)
        {
            this._context = new Context(this);
        }

        public ObjectWriter(Stream output, Encoding encoding, bool leaveOpen)
            : base(output, encoding, leaveOpen)
        {
            this._context = new Context(this);
        }

        public void WriteObject(object value)
        {
            if (value == null)
            {
                // The value can't be null, because one wouldn't be able
                // to get the type - yes, on Read() one can return null,
                // what should its type be?
                throw new NullReferenceException("Value can't be null");
            }

            var s = _context.CreateSerializer(value.GetType());
            s.Write(_context, value);
        }

#if DEBUG
        public override void Write(bool value)
        {
            Debug.WriteLine("Write bool: {0}", value);
            base.Write(value);
        }

        public override void Write(byte value)
        {
            Debug.WriteLine("Write byte: {0}", value);
            base.Write(value);
        }

        public override void Write(int value)
        {
            Debug.WriteLine("Write int: {0}", value);
            base.Write(value);
        }

        public override void Write(uint value)
        {
            Debug.WriteLine("Write uint: {0}", value);
            base.Write(value);
        }

        public override void Write(string value)
        {
            base.Write(value);
            Debug.WriteLine("Write string: {0}", value);
        }
#endif
    }
}
