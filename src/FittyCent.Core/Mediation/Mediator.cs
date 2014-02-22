using System;
using System.Reflection;

namespace FittyCent.Core.Mediation {
    public class Mediator : IMediator {
        private readonly Func<Type, object> _dependencyResolver;

        public Mediator(Func<Type, object> dependencyResolver) {
            _dependencyResolver = dependencyResolver;
        }

        public TResponse Request<TResponse>(IQuery<TResponse> query) {
            var queryType = query.GetType();

            var mediatorMethodInvoker = new MediatorMethodInvoker<TResponse>(typeof(IQueryHandler<,>), queryType, _dependencyResolver);

            return mediatorMethodInvoker.InvokeQuery(query);
        }

        public CommandResult<TResponse> Send<TResponse>(ICommand<TResponse> command) {
            var queryType = command.GetType();

            var mediatorMethodInvoker = new MediatorMethodInvoker<TResponse>(typeof(ICommandHandler<,>), queryType, _dependencyResolver);

            return mediatorMethodInvoker.InvokeSend(command);
        }


        private class MediatorMethodInvoker<T> {
            private const string HandlerMethodName = "Handle";
            private readonly MethodInfo _handleMethod;
            private readonly object _request;

            public MediatorMethodInvoker(Type handlerTypeTemplate, Type queryType, Func<Type, object> dependencyResolver) {
                var handlerType = CreateGenericType(handlerTypeTemplate, queryType);
                _request = dependencyResolver(handlerType);
                _handleMethod = GetHandlerMethod(handlerType, queryType);
            }


            public T InvokeQuery(object query) {
                return (T) _handleMethod.Invoke(_request, new[] { query });
            }

            public CommandResult<T> InvokeSend(object command) {
                return (CommandResult<T>) _handleMethod.Invoke(_request, new[] { command });
            }

            private static Type CreateGenericType(Type handlerTypeTemplate, Type queryType) {
                return handlerTypeTemplate.MakeGenericType(queryType, typeof(T));
            }

            private static MethodInfo GetHandlerMethod(Type handlerType, Type messageType) {
                return handlerType
                    .GetMethod(HandlerMethodName,
                        BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod,
                        null, CallingConventions.HasThis,
                        new[] { messageType },
                        null);
            }
        }
    }
}