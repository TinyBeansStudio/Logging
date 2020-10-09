using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using TinyBeans.Logging.Options;

namespace TinyBeans.Logging.Defaults {

    /// <summary>
    /// The default implementation of <see cref="IAsyncLoggingAspect{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="ILogger{T}"/> logs will be written to.</typeparam>
    public class DefaultAsyncLoggingAspect<T> : IAsyncLoggingAspect<T> {
        private readonly IOptionsMonitor<LoggingAspectOptions> _options;

        /// <summary>
        /// The logger used when writing additional logs.
        /// </summary>
        public ILogger<T> Logger { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">The logger used when writing additional logs.</param>
        /// <param name="options">The <see cref="LoggingAspectOptions"/> to use.</param>
        public DefaultAsyncLoggingAspect(ILogger<T> logger, IOptionsMonitor<LoggingAspectOptions> options) {
            Logger = logger;
            _options = options;
        }

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <param name="method">The method to invoke.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        public async Task InvokeAsync(Func<Task> method) {
            LogExecuting(method);

            using (var _ = GetScope(method)) {
                await method();
            }

            LogExecuted(method);
        }

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <typeparam name="P1">The type of the first parameter to pass to the method.</typeparam>
        /// <param name="method">The method to invoke.</param>
        /// <param name="parameter1">The first parameter to pass to the method.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        public async Task InvokeAsync<P1>(Func<P1, Task> method, P1 parameter1) {
            LogExecuting(method);

            using (var _ = GetScope(method)) {
                await method(parameter1);
            }

            LogExecuted(method);
        }

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <typeparam name="P1">The type of the first parameter to pass to the method.</typeparam>
        /// <typeparam name="P2">The type of the second parameter to pass to the method.</typeparam>
        /// <param name="method">The method to invoke.</param>
        /// <param name="parameter1">The first parameter to pass to the method.</param>
        /// <param name="parameter2">The second parameter to pass to the method.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        public async Task InvokeAsync<P1, P2>(Func<P1, P2, Task> method, P1 parameter1, P2 parameter2) {
            LogExecuting(method);

            using (var _ = GetScope(method)) {
                await method(parameter1, parameter2);
            }

            LogExecuted(method);
        }

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <typeparam name="P1">The type of the first parameter to pass to the method.</typeparam>
        /// <typeparam name="P2">The type of the second parameter to pass to the method.</typeparam>
        /// <typeparam name="P3">The type of the third parameter to pass to the method.</typeparam>
        /// <param name="method">The method to invoke.</param>
        /// <param name="parameter1">The first parameter to pass to the method.</param>
        /// <param name="parameter2">The second parameter to pass to the method.</param>
        /// <param name="parameter3">The third parameter to pass to the method.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        public async Task InvokeAsync<P1, P2, P3>(Func<P1, P2, P3, Task> method, P1 parameter1, P2 parameter2, P3 parameter3) {
            LogExecuting(method);

            using (var _ = GetScope(method)) {
                await method(parameter1, parameter2, parameter3);
            }

            LogExecuted(method);
        }

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <typeparam name="P1">The type of the first parameter to pass to the method.</typeparam>
        /// <typeparam name="P2">The type of the second parameter to pass to the method.</typeparam>
        /// <typeparam name="P3">The type of the third parameter to pass to the method.</typeparam>
        /// <typeparam name="P4">The type of the fourth parameter to pass to the method.</typeparam>
        /// <param name="method">The method to invoke.</param>
        /// <param name="parameter1">The first parameter to pass to the method.</param>
        /// <param name="parameter2">The second parameter to pass to the method.</param>
        /// <param name="parameter3">The third parameter to pass to the method.</param>
        /// <param name="parameter4">The fourth parameter to pass to the method.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        public async Task InvokeAsync<P1, P2, P3, P4>(Func<P1, P2, P3, P4, Task> method, P1 parameter1, P2 parameter2, P3 parameter3, P4 parameter4) {
            LogExecuting(method);

            using (var _ = GetScope(method)) {
                await method(parameter1, parameter2, parameter3, parameter4);
            }

            LogExecuted(method);
        }

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <typeparam name="P1">The type of the first parameter to pass to the method.</typeparam>
        /// <typeparam name="P2">The type of the second parameter to pass to the method.</typeparam>
        /// <typeparam name="P3">The type of the third parameter to pass to the method.</typeparam>
        /// <typeparam name="P4">The type of the fourth parameter to pass to the method.</typeparam>
        /// <typeparam name="P5">The type of the fifth parameter to pass to the method.</typeparam>
        /// <param name="method">The method to invoke.</param>
        /// <param name="parameter1">The first parameter to pass to the method.</param>
        /// <param name="parameter2">The second parameter to pass to the method.</param>
        /// <param name="parameter3">The third parameter to pass to the method.</param>
        /// <param name="parameter4">The fourth parameter to pass to the method.</param>
        /// <param name="parameter5">The fifth parameter to pass to the method.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        public async Task InvokeAsync<P1, P2, P3, P4, P5>(Func<P1, P2, P3, P4, P5, Task> method, P1 parameter1, P2 parameter2, P3 parameter3, P4 parameter4, P5 parameter5) {
            LogExecuting(method);

            using (var _ = GetScope(method)) {
                await method(parameter1, parameter2, parameter3, parameter4, parameter5);
            }

            LogExecuted(method);
        }

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <typeparam name="R">The result type of the <see cref="Task"/>.</typeparam>
        /// <param name="method">The method to invoke.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        public async Task<R> InvokeAsync<R>(Func<Task<R>> method) {
            LogExecuting(method);

            R result;
            using (var _ = GetScope(method)) {
                result = await method();
            }

            LogExecuted(method);

            return result;
        }

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <typeparam name="P1">The type of the first parameter to pass to the method.</typeparam>
        /// <typeparam name="R">The result type of the <see cref="Task"/>.</typeparam>
        /// <param name="method">The method to invoke.</param>
        /// <param name="parameter1">The first parameter to pass to the method.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        public async Task<R> InvokeAsync<P1, R>(Func<P1, Task<R>> method, P1 parameter1) {
            LogExecuting(method);

            R result;
            using (var _ = GetScope(method)) {
                result = await method(parameter1);
            }

            LogExecuted(method);

            return result;
        }

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <typeparam name="P1">The type of the first parameter to pass to the method.</typeparam>
        /// <typeparam name="P2">The type of the second parameter to pass to the method.</typeparam>
        /// <typeparam name="R">The result type of the <see cref="Task"/>.</typeparam>
        /// <param name="method">The method to invoke.</param>
        /// <param name="parameter1">The first parameter to pass to the method.</param>
        /// <param name="parameter2">The second parameter to pass to the method.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        public async Task<R> InvokeAsync<P1, P2, R>(Func<P1, P2, Task<R>> method, P1 parameter1, P2 parameter2) {
            LogExecuting(method);

            R result;
            using (var _ = GetScope(method)) {
                result = await method(parameter1, parameter2);
            }

            LogExecuted(method);

            return result;
        }

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <typeparam name="P1">The type of the first parameter to pass to the method.</typeparam>
        /// <typeparam name="P2">The type of the second parameter to pass to the method.</typeparam>
        /// <typeparam name="P3">The type of the third parameter to pass to the method.</typeparam>
        /// <typeparam name="R">The result type of the <see cref="Task"/>.</typeparam>
        /// <param name="method">The method to invoke.</param>
        /// <param name="parameter1">The first parameter to pass to the method.</param>
        /// <param name="parameter2">The second parameter to pass to the method.</param>
        /// <param name="parameter3">The third parameter to pass to the method.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        public async Task<R> InvokeAsync<P1, P2, P3, R>(Func<P1, P2, P3, Task<R>> method, P1 parameter1, P2 parameter2, P3 parameter3) {
            LogExecuting(method);

            R result;
            using (var _ = GetScope(method)) {
                result = await method(parameter1, parameter2, parameter3);
            }

            LogExecuted(method);

            return result;
        }

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <typeparam name="P1">The type of the first parameter to pass to the method.</typeparam>
        /// <typeparam name="P2">The type of the second parameter to pass to the method.</typeparam>
        /// <typeparam name="P3">The type of the third parameter to pass to the method.</typeparam>
        /// <typeparam name="P4">The type of the fourth parameter to pass to the method.</typeparam>
        /// <typeparam name="R">The result type of the <see cref="Task"/>.</typeparam>
        /// <param name="method">The method to invoke.</param>
        /// <param name="parameter1">The first parameter to pass to the method.</param>
        /// <param name="parameter2">The second parameter to pass to the method.</param>
        /// <param name="parameter3">The third parameter to pass to the method.</param>
        /// <param name="parameter4">The fourth parameter to pass to the method.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        public async Task<R> InvokeAsync<P1, P2, P3, P4, R>(Func<P1, P2, P3, P4, Task<R>> method, P1 parameter1, P2 parameter2, P3 parameter3, P4 parameter4) {
            LogExecuting(method);

            R result;
            using (var _ = GetScope(method)) {
                result = await method(parameter1, parameter2, parameter3, parameter4);
            }

            LogExecuted(method);

            return result;
        }

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <typeparam name="P1">The type of the first parameter to pass to the method.</typeparam>
        /// <typeparam name="P2">The type of the second parameter to pass to the method.</typeparam>
        /// <typeparam name="P3">The type of the third parameter to pass to the method.</typeparam>
        /// <typeparam name="P4">The type of the fourth parameter to pass to the method.</typeparam>
        /// <typeparam name="P5">The type of the fifth parameter to pass to the method.</typeparam>
        /// <typeparam name="R">The result type of the <see cref="Task"/>.</typeparam>
        /// <param name="method">The method to invoke.</param>
        /// <param name="parameter1">The first parameter to pass to the method.</param>
        /// <param name="parameter2">The second parameter to pass to the method.</param>
        /// <param name="parameter3">The third parameter to pass to the method.</param>
        /// <param name="parameter4">The fourth parameter to pass to the method.</param>
        /// <param name="parameter5">The fifth parameter to pass to the method.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        public async Task<R> InvokeAsync<P1, P2, P3, P4, P5, R>(Func<P1, P2, P3, P4, P5, Task<R>> method, P1 parameter1, P2 parameter2, P3 parameter3, P4 parameter4, P5 parameter5) {
            LogExecuting(method);

            R result;
            using (var _ = GetScope(method)) {
                result = await method(parameter1, parameter2, parameter3, parameter4, parameter5);
            }

            LogExecuted(method);

            return result;
        }

        private void LogExecuting(Delegate method) {
            var names = Names(_options.CurrentValue.MethodExecutingTemplate, method);

            Logger.Log(_options.CurrentValue.ExecutionLogLevel, _options.CurrentValue.MethodExecutingTemplate, names.Name1, names.Name2, names.Name3);
        }

        private void LogExecuted(Delegate method) {
            var names = Names(_options.CurrentValue.MethodExecutedTemplate, method);

            Logger.Log(_options.CurrentValue.ExecutionLogLevel, _options.CurrentValue.MethodExecutedTemplate, names.Name1, names.Name2, names.Name3);
        }

        private IDisposable GetScope(Delegate method) {
            var names = Names(_options.CurrentValue.ScopeTemplate, method);

            return Logger.BeginScope(_options.CurrentValue.ScopeTemplate, names.Name1, names.Name2, names.Name3);
        }

        private static ConcurrentDictionary<string, int[]> _templateOrders = new ConcurrentDictionary<string, int[]>();
        private (string Name1, string Name2, string Name3) Names(string template, Delegate method) {
            var assemblyName = method.Method.DeclaringType?.Assembly.GetName().Name ?? string.Empty;
            var className = method.Method.DeclaringType?.Name ?? string.Empty;
            var methodName = method.Method.Name;

            var order = _templateOrders
                .GetOrAdd(template, key => {
                    return new int[] {
                        key.IndexOf(Constants.AssemblyField),
                        key.IndexOf(Constants.ClassField),
                        key.IndexOf(Constants.MethodField)
                    };
                });

            var ordered = new string[3];
            if (order[0] < order[1] && order[0] < order[2]) {
                ordered[0] = assemblyName;

                if (order[1] < order[2]) {
                    ordered[1] = className;
                    ordered[2] = methodName;
                } else {
                    ordered[1] = methodName;
                    ordered[2] = className;
                }
            } else if (order[1] < order[0] && order[1] < order[2]) {
                ordered[0] = className;

                if (order[0] < order[2]) {
                    ordered[1] = assemblyName;
                    ordered[2] = methodName;
                } else {
                    ordered[1] = methodName;
                    ordered[2] = assemblyName;
                }
            } else {
                ordered[0] = methodName;

                if (order[0] < order[1]) {
                    ordered[1] = assemblyName;
                    ordered[2] = className;
                } else {
                    ordered[1] = className;
                    ordered[2] = assemblyName;
                }
            }

            return (ordered[0], ordered[1], ordered[2]);
        }
    }
}