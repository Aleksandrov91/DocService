namespace DocService.Application.Common.Contracts
{
    public interface ISerializer<TDocument>
    {
        string Serialize(TDocument doc);
    }
}
