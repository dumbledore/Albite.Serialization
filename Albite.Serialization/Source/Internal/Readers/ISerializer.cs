namespace Albite.Serialization.Internal.Readers
{
    internal interface ISerializer
    {
        object Read(IContext context);
    }
}
