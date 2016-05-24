using System;
using System.IO;

namespace Albite.Serialization.Internal.Readers
{
    internal interface IContext : IContextBase
    {
#if DEBUG
        /// <summary>
        /// Reads a serializer from the stream in BinaryReader.
        ///
        /// Serializers are cached to save space, so it would
        /// return the same serializer for the same type.
        /// </summary>
        /// <returns>The read serializer</returns>
#endif
        ISerializer CreateSerializer();

#if DEBUG
        /// <summary>
        /// Reads a Type object.
        /// 
        /// Type objects are cached to save space.
        /// </summary>
        /// <returns>The type read</returns>
#endif
        Type ReadType();

#if DEBUG
        /// <summary>
        /// Used by the serializers for caching objects
        /// and obtaining them from their unique IDs.
        /// </summary>
#endif
        ObjectCache<object> ObjectCache { get; }

#if DEBUG
        /// <summary>
        /// The reader used for serialization
        /// </summary>
#endif
        BinaryReader Reader { get; }
    }
}
