using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TinyBeans.Logging.Abstractions;
using TinyBeans.Logging.Options;

namespace TinyBeans.Logging.Defaults {

    /// <summary>
    /// The default implementation of <see cref="ILoggingAspect{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="ILogger{T}"/> logs will be written to.</typeparam>
    public class DefaultLoggingAspect<T> : ILoggingAspect<T> {
        private readonly ILoggableStateParser _loggableStateParser;
        private readonly IOptionsMonitor<LoggingAspectOptions> _options;

        private static readonly ConcurrentDictionary<string, int[]> _templateOrders = new ConcurrentDictionary<string, int[]>();
        private static readonly ConcurrentDictionary<Delegate, (string AssemblyName, string ClassName, string MethodName)> _delegateCache = new ConcurrentDictionary<Delegate, (string AssemblyName, string ClassName, string MethodName)>();

        /// <summary>
        /// The logger used when writing additional logs.
        /// </summary>
        public ILogger<T> Logger { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">The logger used when writing additional logs.</param>
        /// <param name="loggableStateParser">The state parser to use when logging parameters and results.</param>
        /// <param name="options">The <see cref="LoggingAspectOptions"/> to use.</param>
        public DefaultLoggingAspect(ILogger<T> logger, ILoggableStateParser loggableStateParser, IOptionsMonitor<LoggingAspectOptions> options) {
            Logger = logger;
            _loggableStateParser = loggableStateParser;
            _options = options;
        }

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <param name="method">The method to invoke.</param>
        public void Invoke(Action method) {
            LogExecuting(method);

            using (var _ = GetScope(method)) {
                method();
            }

            LogExecuted(method);
        }

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <typeparam name="P1">The type of the first parameter to pass to the method.</typeparam>
        /// <param name="method">The method to invoke.</param>
        /// <param name="p1">The first parameter to pass to the method.</param>
        public void Invoke<P1>(Action<P1> method, P1 p1) {
            LogExecuting(method, p1);

            using (var _ = GetScope(method)) {
                method(p1);
            }

            LogExecuted(method);
        }

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <typeparam name="P1">The type of the first parameter to pass to the method.</typeparam>
        /// <typeparam name="P2">The type of the second parameter to pass to the method.</typeparam>
        /// <param name="method">The method to invoke.</param>
        /// <param name="p1">The first parameter to pass to the method.</param>
        /// <param name="p2">The second parameter to pass to the method.</param>
        public void Invoke<P1, P2>(Action<P1, P2> method, P1 p1, P2 p2) {
            LogExecuting(method, p1, p2);

            using (var _ = GetScope(method)) {
                method(p1, p2);
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
        /// <param name="p1">The first parameter to pass to the method.</param>
        /// <param name="p2">The second parameter to pass to the method.</param>
        /// <param name="p3">The third parameter to pass to the method.</param>
        public void Invoke<P1, P2, P3>(Action<P1, P2, P3> method, P1 p1, P2 p2, P3 p3) {
            LogExecuting(method, p1, p2, p3);

            using (var _ = GetScope(method)) {
                method(p1, p2, p3);
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
        /// <param name="p1">The first parameter to pass to the method.</param>
        /// <param name="p2">The second parameter to pass to the method.</param>
        /// <param name="p3">The third parameter to pass to the method.</param>
        /// <param name="p4">The fourth parameter to pass to the method.</param>
        public void Invoke<P1, P2, P3, P4>(Action<P1, P2, P3, P4> method, P1 p1, P2 p2, P3 p3, P4 p4) {
            LogExecuting(method, p1, p2, p3, p4);

            using (var _ = GetScope(method)) {
                method(p1, p2, p3, p4);
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
        /// <param name="p1">The first parameter to pass to the method.</param>
        /// <param name="p2">The second parameter to pass to the method.</param>
        /// <param name="p3">The third parameter to pass to the method.</param>
        /// <param name="p4">The fourth parameter to pass to the method.</param>
        /// <param name="p5">The fifth parameter to pass to the method.</param>
        public void Invoke<P1, P2, P3, P4, P5>(Action<P1, P2, P3, P4, P5> method, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5) {
            LogExecuting(method, p1, p2, p3, p4, p5);

            using (var _ = GetScope(method)) {
                method(p1, p2, p3, p4, p5);
            }

            LogExecuted(method);
        }

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <typeparam name="R">The result type of the <see cref="Task"/>.</typeparam>
        /// <param name="method">The method to invoke.</param>
        /// <returns>The result of the invoked method.</returns>
        public R Invoke<R>(Func<R> method) {
            LogExecuting(method);

            R r;
            using (var _ = GetScope(method)) {
                r = method();
            }

            LogExecuted(method, r);

            return r;
        }

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <typeparam name="P1">The type of the first parameter to pass to the method.</typeparam>
        /// <typeparam name="R">The result type of the <see cref="Task"/>.</typeparam>
        /// <param name="method">The method to invoke.</param>
        /// <param name="p1">The first parameter to pass to the method.</param>
        /// <returns>The result of the invoked method.</returns>
        public R Invoke<P1, R>(Func<P1, R> method, P1 p1) {
            LogExecuting(method, p1);

            R r;
            using (var _ = GetScope(method)) {
                r = method(p1);
            }

            LogExecuted(method, r);

            return r;
        }

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <typeparam name="P1">The type of the first parameter to pass to the method.</typeparam>
        /// <typeparam name="P2">The type of the second parameter to pass to the method.</typeparam>
        /// <typeparam name="R">The result type of the <see cref="Task"/>.</typeparam>
        /// <param name="method">The method to invoke.</param>
        /// <param name="p1">The first parameter to pass to the method.</param>
        /// <param name="p2">The second parameter to pass to the method.</param>
        /// <returns>The result of the invoked method.</returns>
        public R Invoke<P1, P2, R>(Func<P1, P2, R> method, P1 p1, P2 p2) {
            LogExecuting(method, p1, p2);

            R r;
            using (var _ = GetScope(method)) {
                r = method(p1, p2);
            }

            LogExecuted(method, r);

            return r;
        }

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <typeparam name="P1">The type of the first parameter to pass to the method.</typeparam>
        /// <typeparam name="P2">The type of the second parameter to pass to the method.</typeparam>
        /// <typeparam name="P3">The type of the third parameter to pass to the method.</typeparam>
        /// <typeparam name="R">The result type of the <see cref="Task"/>.</typeparam>
        /// <param name="method">The method to invoke.</param>
        /// <param name="p1">The first parameter to pass to the method.</param>
        /// <param name="p2">The second parameter to pass to the method.</param>
        /// <param name="p3">The third parameter to pass to the method.</param>
        /// <returns>The result of the invoked method.</returns>
        public R Invoke<P1, P2, P3, R>(Func<P1, P2, P3, R> method, P1 p1, P2 p2, P3 p3) {
            LogExecuting(method, p1, p2, p3);

            R r;
            using (var _ = GetScope(method)) {
                r = method(p1, p2, p3);
            }

            LogExecuted(method, r);

            return r;
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
        /// <param name="p1">The first parameter to pass to the method.</param>
        /// <param name="p2">The second parameter to pass to the method.</param>
        /// <param name="p3">The third parameter to pass to the method.</param>
        /// <param name="p4">The fourth parameter to pass to the method.</param>
        /// <returns>The result of the invoked method.</returns>
        public R Invoke<P1, P2, P3, P4, R>(Func<P1, P2, P3, P4, R> method, P1 p1, P2 p2, P3 p3, P4 p4) {
            LogExecuting(method, p1, p2, p3, p4);

            R r;
            using (var _ = GetScope(method)) {
                r = method(p1, p2, p3, p4);
            }

            LogExecuted(method, r);

            return r;
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
        /// <param name="p1">The first parameter to pass to the method.</param>
        /// <param name="p2">The second parameter to pass to the method.</param>
        /// <param name="p3">The third parameter to pass to the method.</param>
        /// <param name="p4">The fourth parameter to pass to the method.</param>
        /// <param name="p5">The fifth parameter to pass to the method.</param>
        /// <returns>The result of the invoked method.</returns>
        public R Invoke<P1, P2, P3, P4, P5, R>(Func<P1, P2, P3, P4, P5, R> method, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5) {
            LogExecuting(method, p1, p2, p3, p4, p5);

            R r;
            using (var _ = GetScope(method)) {
                r = method(p1, p2, p3, p4, p5);
            }

            LogExecuted(method, r);

            return r;
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
        /// <param name="p1">The first parameter to pass to the method.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        public async Task InvokeAsync<P1>(Func<P1, Task> method, P1 p1) {
            LogExecuting(method, p1);

            using (var _ = GetScope(method)) {
                await method(p1);
            }

            LogExecuted(method);
        }

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <typeparam name="P1">The type of the first parameter to pass to the method.</typeparam>
        /// <typeparam name="P2">The type of the second parameter to pass to the method.</typeparam>
        /// <param name="method">The method to invoke.</param>
        /// <param name="p1">The first parameter to pass to the method.</param>
        /// <param name="p2">The second parameter to pass to the method.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        public async Task InvokeAsync<P1, P2>(Func<P1, P2, Task> method, P1 p1, P2 p2) {
            LogExecuting(method, p1, p2);

            using (var _ = GetScope(method)) {
                await method(p1, p2);
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
        /// <param name="p1">The first parameter to pass to the method.</param>
        /// <param name="p2">The second parameter to pass to the method.</param>
        /// <param name="p3">The third parameter to pass to the method.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        public async Task InvokeAsync<P1, P2, P3>(Func<P1, P2, P3, Task> method, P1 p1, P2 p2, P3 p3) {
            LogExecuting(method, p1, p2, p3);

            using (var _ = GetScope(method)) {
                await method(p1, p2, p3);
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
        /// <param name="p1">The first parameter to pass to the method.</param>
        /// <param name="p2">The second parameter to pass to the method.</param>
        /// <param name="p3">The third parameter to pass to the method.</param>
        /// <param name="p4">The fourth parameter to pass to the method.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        public async Task InvokeAsync<P1, P2, P3, P4>(Func<P1, P2, P3, P4, Task> method, P1 p1, P2 p2, P3 p3, P4 p4) {
            LogExecuting(method, p1, p2, p3, p4);

            using (var _ = GetScope(method)) {
                await method(p1, p2, p3, p4);
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
        /// <param name="p1">The first parameter to pass to the method.</param>
        /// <param name="p2">The second parameter to pass to the method.</param>
        /// <param name="p3">The third parameter to pass to the method.</param>
        /// <param name="p4">The fourth parameter to pass to the method.</param>
        /// <param name="p5">The fifth parameter to pass to the method.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        public async Task InvokeAsync<P1, P2, P3, P4, P5>(Func<P1, P2, P3, P4, P5, Task> method, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5) {
            LogExecuting(method, p1, p2, p3, p4, p5);

            using (var _ = GetScope(method)) {
                await method(p1, p2, p3, p4, p5);
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

            R r;
            using (var _ = GetScope(method)) {
                r = await method();
            }

            LogExecuted(method, r);

            return r;
        }

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <typeparam name="P1">The type of the first parameter to pass to the method.</typeparam>
        /// <typeparam name="R">The result type of the <see cref="Task"/>.</typeparam>
        /// <param name="method">The method to invoke.</param>
        /// <param name="p1">The first parameter to pass to the method.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        public async Task<R> InvokeAsync<P1, R>(Func<P1, Task<R>> method, P1 p1) {
            LogExecuting(method, p1);

            R r;
            using (var _ = GetScope(method)) {
                r = await method(p1);
            }

            LogExecuted(method, r);

            return r;
        }

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <typeparam name="P1">The type of the first parameter to pass to the method.</typeparam>
        /// <typeparam name="P2">The type of the second parameter to pass to the method.</typeparam>
        /// <typeparam name="R">The result type of the <see cref="Task"/>.</typeparam>
        /// <param name="method">The method to invoke.</param>
        /// <param name="p1">The first parameter to pass to the method.</param>
        /// <param name="p2">The second parameter to pass to the method.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        public async Task<R> InvokeAsync<P1, P2, R>(Func<P1, P2, Task<R>> method, P1 p1, P2 p2) {
            LogExecuting(method, p1, p2);

            R r;
            using (var _ = GetScope(method)) {
                r = await method(p1, p2);
            }

            LogExecuted(method, r);

            return r;
        }

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <typeparam name="P1">The type of the first parameter to pass to the method.</typeparam>
        /// <typeparam name="P2">The type of the second parameter to pass to the method.</typeparam>
        /// <typeparam name="P3">The type of the third parameter to pass to the method.</typeparam>
        /// <typeparam name="R">The result type of the <see cref="Task"/>.</typeparam>
        /// <param name="method">The method to invoke.</param>
        /// <param name="p1">The first parameter to pass to the method.</param>
        /// <param name="p2">The second parameter to pass to the method.</param>
        /// <param name="p3">The third parameter to pass to the method.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        public async Task<R> InvokeAsync<P1, P2, P3, R>(Func<P1, P2, P3, Task<R>> method, P1 p1, P2 p2, P3 p3) {
            LogExecuting(method, p1, p2, p3);

            R r;
            using (var _ = GetScope(method)) {
                r = await method(p1, p2, p3);
            }

            LogExecuted(method, r);

            return r;
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
        /// <param name="p1">The first parameter to pass to the method.</param>
        /// <param name="p2">The second parameter to pass to the method.</param>
        /// <param name="p3">The third parameter to pass to the method.</param>
        /// <param name="p4">The fourth parameter to pass to the method.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        public async Task<R> InvokeAsync<P1, P2, P3, P4, R>(Func<P1, P2, P3, P4, Task<R>> method, P1 p1, P2 p2, P3 p3, P4 p4) {
            LogExecuting(method, p1, p2, p3, p4);

            R r;
            using (var _ = GetScope(method)) {
                r = await method(p1, p2, p3, p4);
            }

            LogExecuted(method, r);

            return r;
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
        /// <param name="p1">The first parameter to pass to the method.</param>
        /// <param name="p2">The second parameter to pass to the method.</param>
        /// <param name="p3">The third parameter to pass to the method.</param>
        /// <param name="p4">The fourth parameter to pass to the method.</param>
        /// <param name="p5">The fifth parameter to pass to the method.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        public async Task<R> InvokeAsync<P1, P2, P3, P4, P5, R>(Func<P1, P2, P3, P4, P5, Task<R>> method, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5) {
            LogExecuting(method, p1, p2, p3, p4, p5);

            R r;
            using (var _ = GetScope(method)) {
                r = await method(p1, p2, p3, p4, p5);
            }

            LogExecuted(method, r);

            return r;
        }

        private void LogExecuting(Delegate method, params object?[] parameters) {
            if (!Logger.IsEnabled(_options.CurrentValue.ExecutionLogLevel)) {
                return;
            }

            List<IDisposable> scopes = null!;
            try {
                if (Logger.IsEnabled(_options.CurrentValue.StateItemsLogLevel)) {
                    scopes = new List<IDisposable>(5);

                    foreach (var parameter in parameters.Reverse().Where(p => p is object)) {
                        var items = _loggableStateParser.ParseLoggableItems(parameter);

                        if (items.Count > 0) {
                            scopes.Add(Logger.BeginScope(items));
                        }
                    }
                }

                var (name1, name2, name3) = Names(_options.CurrentValue.MethodExecutingTemplate, method);

                Logger.Log(_options.CurrentValue.ExecutionLogLevel, _options.CurrentValue.MethodExecutingTemplate, name1, name2, name3);
            } finally {
                scopes?.ForEach(scope => scope.Dispose());
            }
        }

        private void LogExecuted(Delegate method) => LogExecuted(method, null);
        private void LogExecuted(Delegate method, object? result) {
            if (!Logger.IsEnabled(_options.CurrentValue.ExecutionLogLevel)) {
                return;
            }

            IDisposable scope = null!;
            try {
                if (Logger.IsEnabled(_options.CurrentValue.StateItemsLogLevel) && result is object) {
                    var items = _loggableStateParser.ParseLoggableItems(result);

                    if (items.Count > 0) {
                        scope = Logger.BeginScope(items);
                    }
                }

                var (name1, name2, name3) = Names(_options.CurrentValue.MethodExecutedTemplate, method);

                Logger.Log(_options.CurrentValue.ExecutionLogLevel, _options.CurrentValue.MethodExecutedTemplate, name1, name2, name3);
            } finally {
                scope?.Dispose();
            }
        }

        private IDisposable GetScope(Delegate method) {
            var (name1, name2, name3) = Names(_options.CurrentValue.ScopeTemplate, method);

            return Logger.BeginScope(_options.CurrentValue.ScopeTemplate, name1, name2, name3);
        }

        private (string Name1, string Name2, string Name3) Names(string template, Delegate method) {
            var (assemblyName, className, methodName) = _delegateCache.GetOrAdd(method, key => {
                return (
                    method.Method.DeclaringType?.Assembly.GetName().Name ?? string.Empty,
                    method.Method.DeclaringType?.Name ?? string.Empty,
                    method.Method.Name
                );
            });

            var order = _templateOrders
                .GetOrAdd(template, key => {
                    return new int[3] {
                        key.IndexOf(Constants.AssemblyField, StringComparison.OrdinalIgnoreCase),
                        key.IndexOf(Constants.ClassField, StringComparison.OrdinalIgnoreCase),
                        key.IndexOf(Constants.MethodField, StringComparison.OrdinalIgnoreCase)
                    };
                });

            var names = new string[3];
            if (order[0] < order[1] && order[0] < order[2]) {
                names[0] = assemblyName;

                if (order[1] < order[2]) {
                    names[1] = className;
                    names[2] = methodName;
                } else {
                    names[1] = methodName;
                    names[2] = className;
                }
            } else if (order[1] < order[0] && order[1] < order[2]) {
                names[0] = className;

                if (order[0] < order[2]) {
                    names[1] = assemblyName;
                    names[2] = methodName;
                } else {
                    names[1] = methodName;
                    names[2] = assemblyName;
                }
            } else {
                names[0] = methodName;

                if (order[0] < order[1]) {
                    names[1] = assemblyName;
                    names[2] = className;
                } else {
                    names[1] = className;
                    names[2] = assemblyName;
                }
            }

            return (names[0], names[1], names[2]);
        }
    }
}