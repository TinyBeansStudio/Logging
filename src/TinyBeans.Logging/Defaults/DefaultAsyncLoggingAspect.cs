using System;
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
            var (assemblyName, className, methodName) = Descriptors(method);

            Logger.LogInformation(_options.CurrentValue.MethodExecutingTemplate, assemblyName, className, methodName);

            using (var _ = Logger.BeginScope(_options.CurrentValue.ScopeTemplate, assemblyName, className, methodName)) {
                await method();
            }

            Logger.LogInformation(_options.CurrentValue.MethodExecutedTemplate, assemblyName, className, methodName);
        }

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <typeparam name="P1">The type of the first parameter to pass to the method.</typeparam>
        /// <param name="method">The method to invoke.</param>
        /// <param name="parameter1">The first parameter to pass to the method.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        public async Task InvokeAsync<P1>(Func<P1, Task> method, P1 parameter1) {
            var (assemblyName, className, methodName) = Descriptors(method);

            Logger.LogInformation(_options.CurrentValue.MethodExecutingTemplate, assemblyName, className, methodName);

            using (var _ = Logger.BeginScope(_options.CurrentValue.ScopeTemplate, assemblyName, className, methodName)) {
                await method(parameter1);
            }

            Logger.LogInformation(_options.CurrentValue.MethodExecutedTemplate, assemblyName, className, methodName);
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
            var (assemblyName, className, methodName) = Descriptors(method);

            Logger.LogInformation(_options.CurrentValue.MethodExecutingTemplate, assemblyName, className, methodName);

            using (var _ = Logger.BeginScope(_options.CurrentValue.ScopeTemplate, assemblyName, className, methodName)) {
                await method(parameter1, parameter2);
            }

            Logger.LogInformation(_options.CurrentValue.MethodExecutedTemplate, assemblyName, className, methodName);
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
            var (assemblyName, className, methodName) = Descriptors(method);

            Logger.LogInformation(_options.CurrentValue.MethodExecutingTemplate, assemblyName, className, methodName);

            using (var _ = Logger.BeginScope(_options.CurrentValue.ScopeTemplate, assemblyName, className, methodName)) {
                await method(parameter1, parameter2, parameter3);
            }

            Logger.LogInformation(_options.CurrentValue.MethodExecutedTemplate, assemblyName, className, methodName);
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
            var (assemblyName, className, methodName) = Descriptors(method);

            Logger.LogInformation(_options.CurrentValue.MethodExecutingTemplate, assemblyName, className, methodName);

            using (var _ = Logger.BeginScope(_options.CurrentValue.ScopeTemplate, assemblyName, className, methodName)) {
                await method(parameter1, parameter2, parameter3, parameter4);
            }

            Logger.LogInformation(_options.CurrentValue.MethodExecutedTemplate, assemblyName, className, methodName);
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
            var (assemblyName, className, methodName) = Descriptors(method);

            Logger.LogInformation(_options.CurrentValue.MethodExecutingTemplate, assemblyName, className, methodName);

            using (var _ = Logger.BeginScope(_options.CurrentValue.ScopeTemplate, assemblyName, className, methodName)) {
                await method(parameter1, parameter2, parameter3, parameter4, parameter5);
            }

            Logger.LogInformation(_options.CurrentValue.MethodExecutedTemplate, assemblyName, className, methodName);
        }

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <typeparam name="R">The result type of the <see cref="Task"/>.</typeparam>
        /// <param name="method">The method to invoke.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        public async Task<R> InvokeAsync<R>(Func<Task<R>> method) {
            var (assemblyName, className, methodName) = Descriptors(method);

            Logger.LogInformation(_options.CurrentValue.MethodExecutingTemplate, assemblyName, className, methodName);

            R result;
            using (var _ = Logger.BeginScope(_options.CurrentValue.ScopeTemplate, assemblyName, className, methodName)) {
                result = await method();
            }

            Logger.LogInformation(_options.CurrentValue.MethodExecutedTemplate, assemblyName, className, methodName);

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
            var (assemblyName, className, methodName) = Descriptors(method);

            Logger.LogInformation(_options.CurrentValue.MethodExecutingTemplate, assemblyName, className, methodName);

            R result;
            using (var _ = Logger.BeginScope(_options.CurrentValue.ScopeTemplate, assemblyName, className, methodName)) {
                result = await method(parameter1);
            }

            Logger.LogInformation(_options.CurrentValue.MethodExecutedTemplate, assemblyName, className, methodName);

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
            var (assemblyName, className, methodName) = Descriptors(method);

            Logger.LogInformation(_options.CurrentValue.MethodExecutingTemplate, assemblyName, className, methodName);

            R result;
            using (var _ = Logger.BeginScope(_options.CurrentValue.ScopeTemplate, assemblyName, className, methodName)) {
                result = await method(parameter1, parameter2);
            }

            Logger.LogInformation(_options.CurrentValue.MethodExecutedTemplate, assemblyName, className, methodName);

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
            var (assemblyName, className, methodName) = Descriptors(method);

            Logger.LogInformation(_options.CurrentValue.MethodExecutingTemplate, assemblyName, className, methodName);

            R result;
            using (var _ = Logger.BeginScope(_options.CurrentValue.ScopeTemplate, assemblyName, className, methodName)) {
                result = await method(parameter1, parameter2, parameter3);
            }

            Logger.LogInformation(_options.CurrentValue.MethodExecutedTemplate, assemblyName, className, methodName);

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
            var (assemblyName, className, methodName) = Descriptors(method);

            Logger.LogInformation(_options.CurrentValue.MethodExecutingTemplate, assemblyName, className, methodName);

            R result;
            using (var _ = Logger.BeginScope(_options.CurrentValue.ScopeTemplate, assemblyName, className, methodName)) {
                result = await method(parameter1, parameter2, parameter3, parameter4);
            }

            Logger.LogInformation(_options.CurrentValue.MethodExecutedTemplate, assemblyName, className, methodName);

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
            var (assemblyName, className, methodName) = Descriptors(method);

            Logger.LogInformation(_options.CurrentValue.MethodExecutingTemplate, assemblyName, className, methodName);

            R result;
            using (var _ = Logger.BeginScope(_options.CurrentValue.ScopeTemplate, assemblyName, className, methodName)) {
                result = await method(parameter1, parameter2, parameter3, parameter4, parameter5);
            }

            Logger.LogInformation(_options.CurrentValue.MethodExecutedTemplate, assemblyName, className, methodName);

            return result;
        }

        private (string AssemblyName, string ClassName, string MethodName) Descriptors(Delegate method) {
            var assemblyName = method.Method.DeclaringType?.Assembly.GetName().Name ?? string.Empty;
            var className = method.Method.DeclaringType?.Name ?? string.Empty;
            var methodName = method.Method.Name;

            return (assemblyName, className, methodName);
        }
    }
}