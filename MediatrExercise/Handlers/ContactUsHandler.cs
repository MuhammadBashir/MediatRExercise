using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MediatrExercise.Handlers
{
    public class ContactUsHandler : IRequestHandler<ContactUsRequest, ContactUsReponse>
    {
        public async Task<ContactUsReponse> Handle(ContactUsRequest request, CancellationToken cancellationToken)
        {
            request.ContactUsRequestId = 2500;
            var response = new ContactUsReponse
            {
                ContactUsResponseId = "Contact Us ResponseMessage"
            };
            return await Task.FromResult(response);
        }
    }
}