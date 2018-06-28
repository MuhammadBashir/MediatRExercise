using Autofac;
using Autofac.Builder;
using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediatrExercise.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static IRegistrationBuilder<TService, SimpleActivatorData, SingleRegistrationStyle> RegisterDecorator<TService, TImplementation, TDecorator>(this ContainerBuilder builder) where TImplementation : TService where TDecorator : TService
        {
            var serviceName = typeof(TImplementation).Name;
            var decoratorName = typeof(TDecorator).Name;
            builder.RegisterType<TImplementation>().Named<TService>(serviceName);
            builder.RegisterType<TDecorator>().Named<TService>(decoratorName);
            return builder.Register<TService>(c => GetDecoratedService<TService>(c, serviceName, decoratorName));
        }

        private static TService GetDecoratedService<TService>(IComponentContext c, string serviceName, string decoratorName)
        {
            var service = c.ResolveNamed<TService>(serviceName);
            var decorator = c.ResolveNamed<TService>(decoratorName, TypedParameter.From(service));
            return decorator;
        }

        public static void RegisterGenericPreProcessors(this ContainerBuilder builder, params Type[] preProcessorTypes)
        {
            foreach (var preProcessorType in preProcessorTypes)
            {
                builder.RegisterGenericPreProcessor(preProcessorType);
            }
        }

        public static IRegistrationBuilder<object, ReflectionActivatorData, DynamicRegistrationStyle> RegisterGenericPreProcessor(this ContainerBuilder builder, Type preProcessorType)
        {
            return builder.RegisterGeneric(preProcessorType).As(typeof(IRequestPreProcessor<>));
        }

        public static void RegisterGenericPostProcessors(this ContainerBuilder builder, params Type[] postProcessorTypes)
        {
            foreach (var preProcessorType in postProcessorTypes)
            {
                builder.RegisterGenericPostProcessor(preProcessorType);
            }
        }

        public static IRegistrationBuilder<object, ReflectionActivatorData, DynamicRegistrationStyle> RegisterGenericPostProcessor(this ContainerBuilder builder, Type postProcessorType)
        {
            return builder.RegisterGeneric(postProcessorType).As(typeof(IRequestPostProcessor<,>));
        }

        public static IRegistrationBuilder<object, ReflectionActivatorData, DynamicRegistrationStyle> RegisterGenericHandler(this ContainerBuilder builder, Type handlerType)
        {
            return builder.RegisterGeneric(handlerType).As(typeof(IRequestHandler<,>));
        }

        public static IRegistrationBuilder<object, ReflectionActivatorData, DynamicRegistrationStyle> RegisterGenericHandlerWithoutResponse(this ContainerBuilder builder, Type handlerType)
        {
            return builder.RegisterGeneric(handlerType).As(typeof(IRequestHandler<>));
        }
        public static IRegistrationBuilder<MultiInstanceFactory, SimpleActivatorData, SingleRegistrationStyle> RegisterMultiInstanceFactory(this ContainerBuilder builder)
        {
            return builder.Register(MultiInstanceFactoryResolve);
        }

        public static IRegistrationBuilder<SingleInstanceFactory, SimpleActivatorData, SingleRegistrationStyle> RegisterSingleInstanceFactory(this ContainerBuilder builder)
        {
            return builder.Register(SingleInstanceFactoryResolve);
        }

        public static void RegisterGenericPipelines(this ContainerBuilder builder, params Type[] pipelineTypes)
        {
            foreach (var pipelineType in pipelineTypes)
            {
                builder.RegisterGenericPipeline(pipelineType);
            }
        }

        public static IRegistrationBuilder<object, ReflectionActivatorData, DynamicRegistrationStyle> RegisterGenericPipeline(this ContainerBuilder builder, Type pipelineType)
        {
            return builder.RegisterGeneric(pipelineType).As(typeof(IPipelineBehavior<,>));
        }
        private static SingleInstanceFactory SingleInstanceFactoryResolve(IComponentContext ctx)
        {
            var c = ctx.Resolve<IComponentContext>();
            return t => c.Resolve(t);
        }
        private static MultiInstanceFactory MultiInstanceFactoryResolve(IComponentContext ctx)
        {
            var c = ctx.Resolve<IComponentContext>();
            return t => MakeGenericType(c, t);
        }
        private static IEnumerable<object> MakeGenericType(IComponentContext c, Type t)
        {
            return (IEnumerable<object>)c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
        }

    }
}