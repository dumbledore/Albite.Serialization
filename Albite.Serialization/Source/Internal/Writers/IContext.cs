using System;
using System.IO;

namespace Albite.Serialization.Internal.Writers
{
    internal interface IContext
    {
        /// <summary>
        /// Returns a serilizer for the specified type.
        /// 
        /// Serializers are cached to save space, so it would
        /// return the same serializer for the same type.
        /// </summary>
        /// <param name="type">The type being serialized</param>
        /// <returns>The serializer for the type</returns>
        ISerializer CreateSerializer(Type type);

        /// <summary>
        /// Returns a proxy serializer for the specified type.
        /// 
        /// Proxies are used to serialize objects from a varying
        /// type, but one that is assignable from the initial type.
        /// 
        /// Serializers are cached to save space, so it would
        /// return the same serializer for the same type.
        /// </summary>
        /// <param name="type">The type being serialized</param>
        /// <returns>The proxy serializer for the type</returns>
        ISerializer CreateProxy(Type type);

        /// <summary>
        /// Serializes a Type object.
        /// 
        /// Type objects are cached to save space.
        /// </summary>
        /// <param name="type">The type to be serialized</param>
        void WriteType(Type type);

        /// <summary>
        /// Used for caching objects and obtaining their unique IDs.
        /// </summary>
        ObjectCache<object> ObjectCache { get; }

        /// <summary>
        /// The BinaryWriter used for serialization.
        /// </summary>
        BinaryWriter Writer { get; }
    }
}
