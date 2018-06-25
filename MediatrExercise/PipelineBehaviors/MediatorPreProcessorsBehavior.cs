using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MediatrExercise.PipelineBehaviors
{
    public class MediatorPreProcessorsBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public IEnumerable<IRequestPreProcessor<TRequest>> _preProcessors;
        public MediatorPreProcessorsBehavior(IEnumerable<IRequestPreProcessor<TRequest>> preProcessors)
        {
            _preProcessors = preProcessors;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {

            foreach (var preprocessor in _preProcessors)
            {
                await preprocessor.Process(request, cancellationToken);
            }
            var response = await next().ConfigureAwait(false);
            return response;
        }
    }
}