using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MediatrExercise.Handlers
{
    public class HomeRequestHandler : IRequestHandler<HomeRequest, HomeResponse>
    {
        public Task<HomeResponse> Handle(HomeRequest request, CancellationToken cancellationToken)
        {
            var response = new HomeResponse
            {
                ResponseId = "test response"
            };
            return Task.FromResult(response);
        }
    }
}