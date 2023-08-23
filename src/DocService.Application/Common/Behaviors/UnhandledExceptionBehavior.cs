namespace DocService.Application.Common.Behaviors
{
    using MediatR;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> logger;

        public UnhandledExceptionBehavior(ILogger<TRequest> logger)
        {
            this.logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
			try
			{
				return await next();
			}
			catch (Exception ex)
			{
                this.logger.LogError(ex, "Unhandled exception while processing request of type {REQUEST} with paramenters {PARAMETERS}", typeof(TRequest).Name, request);
				throw;
			}
        }
    }
}
