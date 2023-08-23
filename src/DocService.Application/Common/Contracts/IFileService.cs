namespace DocService.Application.Common.Contracts
{
    public interface IFileService
    {
        bool Exists(string filename);
    }
}
