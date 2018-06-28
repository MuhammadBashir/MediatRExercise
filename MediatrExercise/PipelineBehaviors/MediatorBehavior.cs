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
    public class MediatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest: ContactUsRequest
        where TResponse: ContactUsReponse
    {
        private readonly ILogger<MediatorBehavior<TRequest, TResponse>> _logger;
        public IEnumerable<IRequestPostProcessor<TRequest, TResponse>> _postProcessors;
        public MediatorBehavior(IEnumerable<IRequestPostProcessor<TRequest, TResponse>> postProcessors, ILogger<MediatorBehavior<TRequest, TResponse>> logger)
        {
            _postProcessors = postProcessors;
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogDebug("Going Through {0} Behavior", this.GetType().Name);
            var response = await next().ConfigureAwait(false);
            foreach (var postprocessor in _postProcessors)
            {
                await postprocessor.Process(request, response);
            }
            return response;
        }
    }
}