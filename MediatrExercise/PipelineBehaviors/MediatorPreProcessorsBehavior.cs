using MediatR;
using MediatR.Pipeline;
using MediatrExercise.Handlers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MediatrExercise.PipelineBehaviors
{
    public class MediatorPreProcessorsBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : HomeRequest
        where TResponse : HomeResponse
    {
        public IEnumerable<IRequestPreProcessor<TRequest>> _preProcessors;
        public ILogger<MediatorPreProcessorsBehavior<TRequest, TResponse>> _logger;
        public MediatorPreProcessorsBehavior(IEnumerable<IRequestPreProcessor<TRequest>> preProcessors, 
            ILogger<MediatorPreProcessorsBehavior<TRequest, TResponse>> logger)
        {
            _preProcessors = preProcessors;
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogDebug("Going Through {0} Behavior", this.GetType().Name);

            foreach (var preprocessor in _preProcessors)
            {
                await preprocessor.Process(request, cancellationToken);
            }
            var response = await next().ConfigureAwait(false);
            return response;
        }
    }
}