using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MediatrExercise.Controllers
{
    public class LogsApiController : ApiController
    {
        private readonly IMediator mediator;
        public LogsApiController(IMediator mediator) => this.mediator = mediator;
        [HttpGet]
        public async Task<IHttpActionResult> GetAllLogs()
        {
            List<Logs> logs = new List<Logs>
            {
                new Logs {LogId = 1, LogMessage = "Log 1"},
                new Logs {LogId = 2, LogMessage = "Log 2"},
                new Logs {LogId = 3, LogMessage = "Log 3"},
                new Logs {LogId = 4, LogMessage = "Log 4"}
            };

            return await Task.Run(()=> Json(logs));
        }
    }

    public class Logs
    {
        public int LogId { get; set; }
        public string LogMessage { get; set; }
    }
}
