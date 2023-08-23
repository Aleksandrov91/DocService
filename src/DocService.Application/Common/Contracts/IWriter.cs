using System.Threading.Tasks;

namespace DocService.Application.Common.Contracts
{
    public interface IWriter
    {
        Task Write(string filename, string content);
    }
}
