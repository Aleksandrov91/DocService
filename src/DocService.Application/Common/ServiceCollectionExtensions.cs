namespace DocService.Application.Common
{
    using DocService.Application.Common.Behaviors;
    using DocService.Application.Common.Factories;
    using DocService.Application.Common.Services;
    using DocService.Domain;
    using FluentValidation;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;
    using System.Threading.Channels;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            serviceCollection
                .AddMediatR(cfg =>
                {
                    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
                    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
                });

            serviceCollection.AddTransient<IDocumentFactory, DocumentFactory>();

            serviceCollection.AddSingleton(Channel.CreateUnbounded<XmlDocumentItem>());
            serviceCollection.AddSingleton(svc => svc.GetRequiredService<Channel<XmlDocumentItem>>().Reader);
            serviceCollection.AddSingleton(svc => svc.GetRequiredService<Channel<XmlDocumentItem>>().Writer);

            serviceCollection.AddHostedService<DocumentProcessor>();

            return serviceCollection;
        }
    }
}
