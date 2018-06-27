using Autofac;
using MediatR;
using MediatR.Pipeline;
using MediatrExercise.Extensions;
using MediatrExercise.Handlers;
using MediatrExercise.PipelineBehaviors;
using MediatrExercise.Postprocessors;
using MediatrExercise.Preprocessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediatrExercise.AutofacModules
{
    public class MediatorModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var mediatrAssembly = typeof(IMediator).Assembly;

            builder
                .RegisterAssemblyTypes(mediatrAssembly)
                .AsImplementedInterfaces();

            var handlersAssembly = typeof(HomeRequest).Assembly;
            builder.RegisterAssemblyTypes(handlersAssembly)
                .InNamespaceOf<HomeRequestHandler>()
                .Where(IsAnyMediatorImplementation)
                .AsImplementedInterfaces()
                .AsSelf();
            builder.RegisterModule<LoggingModule>();

            builder.RegisterGenericPipelines(
                typeof(MediatorPreProcessorsBehavior<,>),
                typeof(MediatorBehavior<,>),
                typeof(MediatorGenericPipelineBehavior<,>)
                );
            builder.RegisterGenericPostProcessors(typeof(TestOnePostProcessor<,>),
                typeof(TestTwoPostProcessor<,>));
            builder.RegisterGenericPreProcessors(typeof(TestOnePreprocessor<>),
                typeof(TestTwoPreprocessor<>));
            RegisterFactories(builder);
        }

        private static void RegisterFactories(ContainerBuilder builder)
        {
            builder.RegisterSingleInstanceFactory();
            builder.RegisterMultiInstanceFactory();
        }

        private static bool IsAnyMediatorImplementation(Type x)
        {
            return x.IsClosedTypeOf(typeof(IRequestHandler<>))
                   || x.IsClosedTypeOf(typeof(IRequestHandler<,>))
                   || x.IsClosedTypeOf(typeof(IRequestPreProcessor<>))
                   || x.IsClosedTypeOf(typeof(IRequestPostProcessor<,>))
                   || x.IsClosedTypeOf(typeof(IPipelineBehavior<,>));
        }
    }
}