namespace Albite.Serialization.Internal.Readers
{
    interface ISerializer
    {
        object Read(IContext context);
    }
}
