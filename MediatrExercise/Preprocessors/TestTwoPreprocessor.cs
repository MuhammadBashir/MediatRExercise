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
    public class TestTwoPreprocessor<TRequest> : IRequestPreProcessor<TRequest>
    {
        public TestTwoPreprocessor(ILogger<TestTwoPreprocessor<TRequest>> logger)
        {
            _logger = logger;
        }

        public ILogger<TestTwoPreprocessor<TRequest>> _logger { get; }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Running Preprocessor {0}", this.GetType().Name);
            var req = request.GetType();
            return Task.CompletedTask;
        }
    }
}