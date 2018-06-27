using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MediatrExercise.Postprocessors
{
    public class TestTwoPostProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    {
        private readonly ILogger<TestTwoPostProcessor<TRequest, TResponse>> _logger;

        public TestTwoPostProcessor(ILogger<TestTwoPostProcessor<TRequest,TResponse>> logger)
        {
            this._logger = logger;
        }
        public Task Process(TRequest request, TResponse response)
        {
            _logger.LogDebug("Running post processor {0}", this.GetType().Name);
            var req = request.GetType();
            return Task.CompletedTask;
        }
    }
}