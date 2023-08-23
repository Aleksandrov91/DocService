namespace DocService.Infrastructure.Common
{
    using DocService.Application.Common.Contracts;
    using DocService.Infrastructure.Persistence;
    using DocService.Infrastructure.Serializers;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using System.Xml;

    using static DocService.Domain.GlobalConstants;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ISerializer<XmlDocument>, XmlToJsonSerializer>();
            serviceCollection.AddTransient<IWriter, FileWriter>((x) => new FileWriter(DocumentsPath, serviceCollection.BuildServiceProvider().GetService<ILogger<FileWriter>>()));
            serviceCollection.AddTransient<IFileService, FileService>((x) => new FileService(DocumentsPath));

            return serviceCollection;
        }
    }
}
