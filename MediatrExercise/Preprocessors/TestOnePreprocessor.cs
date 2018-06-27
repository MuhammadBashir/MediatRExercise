using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MediatrExercise.Preprocessors
{
    public class TestOnePreprocessor<TRequest> : IRequestPreProcessor<TRequest>
    {
        ILogger<TestOnePreprocessor<TRequest>> _logger;
        public TestOnePreprocessor(ILogger<TestOnePreprocessor<TRequest>> logger)
        {
            _logger = logger;
        }
        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Running Preprocessor {0}", this.GetType().Name);
            var type = request.GetType();
            return Task.CompletedTask;
        }
    }
}