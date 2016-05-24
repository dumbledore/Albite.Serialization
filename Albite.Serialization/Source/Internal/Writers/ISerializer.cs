namespace Albite.Serialization.Internal.Writers
{
    interface ISerializer
    {
        void Write(IContext context, object value);
    }
}
