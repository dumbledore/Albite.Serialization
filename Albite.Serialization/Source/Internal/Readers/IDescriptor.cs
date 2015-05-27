namespace Albite.Serialization.Internal.Readers
{
    internal interface IDescriptor
    {
        SerializedType SerializedType { get; }
        IInitiailzableSerializer Create();
    }
}
