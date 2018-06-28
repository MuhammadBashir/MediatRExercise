using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediatrExercise.Handlers
{
    public class ContactUsRequest : IRequest<ContactUsReponse>
    {
        public int ContactUsRequestId { get; set; }
    }
}