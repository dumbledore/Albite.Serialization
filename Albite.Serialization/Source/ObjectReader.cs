using Albite.Serialization.Internal.Readers;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Albite.Serialization
{
    public class ObjectReader : BinaryReader
    {
        private readonly IContext _context;

        public ObjectReader(Stream output)
            : base(output)
        {
            this._context = new Context(this);
        }

        public ObjectReader(Stream output, Encoding encoding)
            : base(output, encoding)
        {
            this._context = new Context(this);
        }

        public ObjectReader(Stream output, Encoding encoding, bool leaveOpen)
            : base(output, encoding, leaveOpen)
        {
            this._context = new Context(this);
        }

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
