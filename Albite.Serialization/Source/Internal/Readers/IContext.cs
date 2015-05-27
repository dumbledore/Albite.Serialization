using System;
using System.IO;

namespace Albite.Serialization.Internal.Readers
{
    internal interface IContext
    {
        /// <summary>
        /// Reads a serializer from the stream in BinaryReader.
        ///
        /// Serializers are cached to save space, so it would
        /// return the same serializer for the same type.
        /// </summary>
        /// <returns>The read serializer</returns>
        ISerializer CreateSerializer();

        /// <summary>
        /// Reads a Type object.
        /// 
        /// Type objects are cached to save space.
        /// </summary>
        /// <returns>The type read</returns>
        Type ReadType();

        /// <summary>
        /// Used by the serializers for caching objects
        /// and obtaining them from their unique IDs.
        /// </summary>
        ObjectCache<object> ObjectCache { get; }

        /// <summary>
        /// Retrieves the version of the serializer
        /// </summary>
        int Version { get; }

        /// <summary>
        /// The reader used for serialization
        /// </summary>
        BinaryReader Reader { get; }
    }
}
