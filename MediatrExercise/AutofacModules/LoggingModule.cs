using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediatrExercise.AutofacModules
{
    public class LoggingModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LoggerFactory>()
                .As<ILoggerFactory>()
                .OnActivating(e =>
                {
                    var config = e.Context.Resolve<IConfiguration>();
                    Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(config)
                        .CreateLogger();
                })
                .OnActivated(e =>
                {
                    e.Instance.AddSerilog();
                })
                .SingleInstance();

            builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>)).SingleInstance();
        }
    }
}