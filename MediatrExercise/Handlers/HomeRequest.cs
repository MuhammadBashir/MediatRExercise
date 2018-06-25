using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediatrExercise.Handlers
{
    public class HomeRequest : IRequest<HomeResponse>
    {
        public int RequestId { get; set; }
    }
}