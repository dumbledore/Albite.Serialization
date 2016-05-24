namespace Albite.Serialization.Internal.Readers
{
    interface IDescriptor
    {
        SerializedType SerializedType { get; }
        IInitiailzableSerializer Create();
    }
}
