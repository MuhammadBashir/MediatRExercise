using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MediatrExercise.PipelineBehaviors
{
    public class MediatorGenericPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<MediatorGenericPipelineBehavior<TRequest, TResponse>> _logger;
        public IEnumerable<IRequestPostProcessor<TRequest, TResponse>> _postProcessors;
        public MediatorGenericPipelineBehavior(IEnumerable<IRequestPostProcessor<TRequest, TResponse>> postProcessors, ILogger<MediatorGenericPipelineBehavior<TRequest, TResponse>> logger)
        {
            _postProcessors = postProcessors;
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = await next().ConfigureAwait(false);
            _logger.LogDebug("Going Through {0} Behavior", this.GetType().Name);
            return response;
        }
    }
}