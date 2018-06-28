using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MediatrExercise.Postprocessors
{
    public class TestOnePostProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    {
        private readonly ILogger<TestOnePostProcessor<TRequest, TResponse>> _logger;

        public TestOnePostProcessor(ILogger<TestOnePostProcessor<TRequest,TResponse>> logger)
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