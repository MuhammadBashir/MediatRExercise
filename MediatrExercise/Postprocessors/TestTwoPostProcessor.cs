using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MediatrExercise.Postprocessors
{
    public class TestTwoPostProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    {
        public Task Process(TRequest request, TResponse response)
        {
            var req = request.GetType();
            return Task.CompletedTask;
        }
    }
}