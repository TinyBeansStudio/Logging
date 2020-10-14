using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TinyBeans.Logging.Models;
using TinyBeans.Logging.Options;

namespace TinyBeans.Logging.Defaults {

    /// <summary>
    /// The default implementation of <see cref="ILoggingAspect{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="ILogger{T}"/> logs will be written to.</typeparam>
    public class DefaultLoggingAspect<T> : ILoggingAspect<T> {
        private readonly ILoggableParser _loggableParser;
        private readonly ILoggingTemplateHelper _loggingTemplateHelper;
        private readonly IOptionsMonitor<LoggingOptions> _options;

        private static readonly ConcurrentDictionary<Delegate, string[]> _delegateCache = new ConcurrentDictionary<Delegate, string[]>();

        /// <summary>
        /// The logger used when writing additional logs.
        /// </summary>
        public ILogger<T> Logger { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">The logger used when writing additional logs.</param>
        /// <param name="loggableParser">The state parser to use when logging parameters and results.</param>
        /// <param name="loggingTemplateHelper">The logging template helper.</param>
        /// <param name="options">The <see cref="LoggingOptions"/> to use.</param>
        public DefaultLoggingAspect(ILogger<T> logger, ILoggableParser loggableParser, ILoggingTemplateHelper loggingTemplateHelper, IOptionsMonitor<LoggingOptions> options) {
            Logger = logger;
            _loggableParser = loggableParser;
            _loggingTemplateHelper = loggingTemplateHelper;
            _options = options;
        }

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <param name="method">The method to invoke.</param>
        public void Invoke(Action method) {
            var options = _options.CurrentValue;
            var executionLogEnabled = Logger.IsEnabled(options.ExecutionLogLevel);

            if (executionLogEnabled) {
                LogExecuting(method);
            }

            using (GetScope(method)) {
                method();
            }

            if (executionLogEnabled) {
                LogExecuted(method);
            }
        }

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <typeparam name="P1">The type of the first parameter to pass to the method.</typeparam>
        /// <param name="method">The method to invoke.</param>
        /// <param name="p1">The first parameter to pass to the method.</param>
        public void Invoke<P1>(Action<P1> method, P1 p1) {
            var options = _options.CurrentValue;
            var executionLogEnabled = Logger.IsEnabled(options.ExecutionLogLevel);
            var scopesEnabled = Logger.IsEnabled(options.StateItemsLogLevel);

            if (executionLogEnabled) {
                using var a = StartScope(p1, scopesEnabled);

                LogExecuting(method);
            }

            using (GetScope(method)) {
                method(p1);
            }

            if (executionLogEnabled) {
                LogExecuted(method);
            }
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
            var options = _options.CurrentValue;
            var executionLogEnabled = Logger.IsEnabled(options.ExecutionLogLevel);
            var scopesEnabled = Logger.IsEnabled(options.StateItemsLogLevel);

            if (executionLogEnabled) {
                using var b = StartScope(p2, scopesEnabled);
                using var a = StartScope(p1, scopesEnabled);

                LogExecuting(method);
            }

            using (GetScope(method)) {
                method(p1, p2);
            }

            if (executionLogEnabled) {
                LogExecuted(method);
            }
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
            var options = _options.CurrentValue;
            var executionLogEnabled = Logger.IsEnabled(options.ExecutionLogLevel);
            var scopesEnabled = Logger.IsEnabled(options.StateItemsLogLevel);

            if (executionLogEnabled) {
                using var c = StartScope(p3, scopesEnabled);
                using var b = StartScope(p2, scopesEnabled);
                using var a = StartScope(p1, scopesEnabled);

                LogExecuting(method);
            }

            using (GetScope(method)) {
                method(p1, p2, p3);
            }

            if (executionLogEnabled) {
                LogExecuted(method);
            }
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
            var options = _options.CurrentValue;
            var executionLogEnabled = Logger.IsEnabled(options.ExecutionLogLevel);
            var scopesEnabled = Logger.IsEnabled(options.StateItemsLogLevel);

            if (executionLogEnabled) {
                using var d = StartScope(p4, scopesEnabled);
                using var c = StartScope(p3, scopesEnabled);
                using var b = StartScope(p2, scopesEnabled);
                using var a = StartScope(p1, scopesEnabled);

                LogExecuting(method);
            }

            using (GetScope(method)) {
                method(p1, p2, p3, p4);
            }

            if (executionLogEnabled) {
                LogExecuted(method);
            }
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
            var options = _options.CurrentValue;
            var executionLogEnabled = Logger.IsEnabled(options.ExecutionLogLevel);
            var scopesEnabled = Logger.IsEnabled(options.StateItemsLogLevel);

            if (executionLogEnabled) {
                using var e = StartScope(p5, scopesEnabled);
                using var d = StartScope(p4, scopesEnabled);
                using var c = StartScope(p3, scopesEnabled);
                using var b = StartScope(p2, scopesEnabled);
                using var a = StartScope(p1, scopesEnabled);

                LogExecuting(method);
            }

            using (GetScope(method)) {
                method(p1, p2, p3, p4, p5);
            }

            if (executionLogEnabled) {
                LogExecuted(method);
            }
        }

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <typeparam name="R">The result type of the <see cref="Task"/>.</typeparam>
        /// <param name="method">The method to invoke.</param>
        /// <returns>The result of the invoked method.</returns>
        public R Invoke<R>(Func<R> method) {
            var options = _options.CurrentValue;
            var executionLogEnabled = Logger.IsEnabled(options.ExecutionLogLevel);
            var scopesEnabled = Logger.IsEnabled(options.StateItemsLogLevel);

            if (executionLogEnabled) {
                LogExecuting(method);
            }

            R r;
            using (GetScope(method)) {
                r = method();
            }

            if (executionLogEnabled) {
                using var _ = StartScope(r, scopesEnabled);

                LogExecuted(method);
            }

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
            var options = _options.CurrentValue;
            var executionLogEnabled = Logger.IsEnabled(options.ExecutionLogLevel);
            var scopesEnabled = Logger.IsEnabled(options.StateItemsLogLevel);

            if (executionLogEnabled) {
                using var a = StartScope(p1, scopesEnabled);

                LogExecuting(method);
            }

            R r;
            using (GetScope(method)) {
                r = method(p1);
            }

            if (executionLogEnabled) {
                using var _ = StartScope(r, scopesEnabled);

                LogExecuted(method);
            }

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
            var options = _options.CurrentValue;
            var executionLogEnabled = Logger.IsEnabled(options.ExecutionLogLevel);
            var scopesEnabled = Logger.IsEnabled(options.StateItemsLogLevel);

            if (executionLogEnabled) {
                using var b = StartScope(p2, scopesEnabled);
                using var a = StartScope(p1, scopesEnabled);

                LogExecuting(method);
            }

            R r;
            using (GetScope(method)) {
                r = method(p1, p2);
            }

            if (executionLogEnabled) {
                using var _ = StartScope(r, scopesEnabled);

                LogExecuted(method);
            }

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
            var options = _options.CurrentValue;
            var executionLogEnabled = Logger.IsEnabled(options.ExecutionLogLevel);
            var scopesEnabled = Logger.IsEnabled(options.StateItemsLogLevel);

            if (executionLogEnabled) {
                using var c = StartScope(p3, scopesEnabled);
                using var b = StartScope(p2, scopesEnabled);
                using var a = StartScope(p1, scopesEnabled);

                LogExecuting(method);
            }

            R r;
            using (GetScope(method)) {
                r = method(p1, p2, p3);
            }

            if (executionLogEnabled) {
                using var _ = StartScope(r, scopesEnabled);

                LogExecuted(method);
            }

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
            var options = _options.CurrentValue;
            var executionLogEnabled = Logger.IsEnabled(options.ExecutionLogLevel);
            var scopesEnabled = Logger.IsEnabled(options.StateItemsLogLevel);

            if (executionLogEnabled) {
                using var d = StartScope(p4, scopesEnabled);
                using var c = StartScope(p3, scopesEnabled);
                using var b = StartScope(p2, scopesEnabled);
                using var a = StartScope(p1, scopesEnabled);

                LogExecuting(method);
            }

            R r;
            using (GetScope(method)) {
                r = method(p1, p2, p3, p4);
            }

            if (executionLogEnabled) {
                using var _ = StartScope(r, scopesEnabled);

                LogExecuted(method);
            }

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
            var options = _options.CurrentValue;
            var executionLogEnabled = Logger.IsEnabled(options.ExecutionLogLevel);
            var scopesEnabled = Logger.IsEnabled(options.StateItemsLogLevel);

            if (executionLogEnabled) {
                using var e = StartScope(p5, scopesEnabled);
                using var d = StartScope(p4, scopesEnabled);
                using var c = StartScope(p3, scopesEnabled);
                using var b = StartScope(p2, scopesEnabled);
                using var a = StartScope(p1, scopesEnabled);

                LogExecuting(method);
            }

            R r;
            using (GetScope(method)) {
                r = method(p1, p2, p3, p4, p5);
            }

            if (executionLogEnabled) {
                using var _ = StartScope(r, scopesEnabled);

                LogExecuted(method);
            }

            return r;
        }

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <param name="method">The method to invoke.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        public async Task InvokeAsync(Func<Task> method) {
            var options = _options.CurrentValue;
            var executionLogEnabled = Logger.IsEnabled(options.ExecutionLogLevel);

            if (executionLogEnabled) {
                LogExecuting(method);
            }

            using (GetScope(method)) {
                await method();
            }

            if (executionLogEnabled) {
                LogExecuted(method);
            }
        }

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <typeparam name="P1">The type of the first parameter to pass to the method.</typeparam>
        /// <param name="method">The method to invoke.</param>
        /// <param name="p1">The first parameter to pass to the method.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        public async Task InvokeAsync<P1>(Func<P1, Task> method, P1 p1) {
            var options = _options.CurrentValue;
            var executionLogEnabled = Logger.IsEnabled(options.ExecutionLogLevel);
            var scopesEnabled = Logger.IsEnabled(options.StateItemsLogLevel);

            if (executionLogEnabled) {
                using var a = StartScope(p1, scopesEnabled);

                LogExecuting(method);
            }

            using (GetScope(method)) {
                await method(p1);
            }

            if (executionLogEnabled) {
                LogExecuted(method);
            }
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
            var options = _options.CurrentValue;
            var executionLogEnabled = Logger.IsEnabled(options.ExecutionLogLevel);
            var scopesEnabled = Logger.IsEnabled(options.StateItemsLogLevel);

            if (executionLogEnabled) {
                using var b = StartScope(p2, scopesEnabled);
                using var a = StartScope(p1, scopesEnabled);

                LogExecuting(method);
            }

            using (GetScope(method)) {
                await method(p1, p2);
            }

            if (executionLogEnabled) {
                LogExecuted(method);
            }
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
            var options = _options.CurrentValue;
            var executionLogEnabled = Logger.IsEnabled(options.ExecutionLogLevel);
            var scopesEnabled = Logger.IsEnabled(options.StateItemsLogLevel);

            if (executionLogEnabled) {
                using var c = StartScope(p3, scopesEnabled);
                using var b = StartScope(p2, scopesEnabled);
                using var a = StartScope(p1, scopesEnabled);

                LogExecuting(method);
            }

            using (GetScope(method)) {
                await method(p1, p2, p3);
            }

            if (executionLogEnabled) {
                LogExecuted(method);
            }
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
            var options = _options.CurrentValue;
            var executionLogEnabled = Logger.IsEnabled(options.ExecutionLogLevel);
            var scopesEnabled = Logger.IsEnabled(options.StateItemsLogLevel);

            if (executionLogEnabled) {
                using var d = StartScope(p4, scopesEnabled);
                using var c = StartScope(p3, scopesEnabled);
                using var b = StartScope(p2, scopesEnabled);
                using var a = StartScope(p1, scopesEnabled);

                LogExecuting(method);
            }

            using (GetScope(method)) {
                await method(p1, p2, p3, p4);
            }

            if (executionLogEnabled) {
                LogExecuted(method);
            }
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
            var options = _options.CurrentValue;
            var executionLogEnabled = Logger.IsEnabled(options.ExecutionLogLevel);
            var scopesEnabled = Logger.IsEnabled(options.StateItemsLogLevel);

            if (executionLogEnabled) {
                using var e = StartScope(p5, scopesEnabled);
                using var d = StartScope(p4, scopesEnabled);
                using var c = StartScope(p3, scopesEnabled);
                using var b = StartScope(p2, scopesEnabled);
                using var a = StartScope(p1, scopesEnabled);

                LogExecuting(method);
            }

            using (GetScope(method)) {
                await method(p1, p2, p3, p4, p5);
            }

            if (executionLogEnabled) {
                LogExecuted(method);
            }
        }

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <typeparam name="R">The result type of the <see cref="Task"/>.</typeparam>
        /// <param name="method">The method to invoke.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        public async Task<R> InvokeAsync<R>(Func<Task<R>> method) {
            var options = _options.CurrentValue;
            var executionLogEnabled = Logger.IsEnabled(options.ExecutionLogLevel);
            var scopesEnabled = Logger.IsEnabled(options.StateItemsLogLevel);

            if (executionLogEnabled) {
                LogExecuting(method);
            }

            R r;
            using (GetScope(method)) {
                r = await method();
            }

            if (executionLogEnabled) {
                using var _ = StartScope(r, scopesEnabled);

                LogExecuted(method);
            }

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
            var options = _options.CurrentValue;
            var executionLogEnabled = Logger.IsEnabled(options.ExecutionLogLevel);
            var scopesEnabled = Logger.IsEnabled(options.StateItemsLogLevel);

            if (executionLogEnabled) {
                using var _ = StartScope(p1, scopesEnabled);

                LogExecuting(method);
            }

            R r;
            using (GetScope(method)) {
                r = await method(p1);
            }

            if (executionLogEnabled) {
                using var _ = StartScope(r, scopesEnabled);

                LogExecuted(method);
            }

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
            var options = _options.CurrentValue;
            var executionLogEnabled = Logger.IsEnabled(options.ExecutionLogLevel);
            var scopesEnabled = Logger.IsEnabled(options.StateItemsLogLevel);

            if (executionLogEnabled) {
                using var b = StartScope(p2, scopesEnabled);
                using var a = StartScope(p1, scopesEnabled);

                LogExecuting(method);
            }

            R r;
            using (GetScope(method)) {
                r = await method(p1, p2);
            }

            if (executionLogEnabled) {
                using var _ = StartScope(r, scopesEnabled);

                LogExecuted(method);
            }

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
            var options = _options.CurrentValue;
            var executionLogEnabled = Logger.IsEnabled(options.ExecutionLogLevel);
            var scopesEnabled = Logger.IsEnabled(options.StateItemsLogLevel);

            if (executionLogEnabled) {
                using var c = StartScope(p3, scopesEnabled);
                using var b = StartScope(p2, scopesEnabled);
                using var a = StartScope(p1, scopesEnabled);

                LogExecuting(method);
            }

            R r;
            using (GetScope(method)) {
                r = await method(p1, p2, p3);
            }

            if (executionLogEnabled) {
                using var _ = StartScope(r, scopesEnabled);

                LogExecuted(method);
            }

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
            var options = _options.CurrentValue;
            var executionLogEnabled = Logger.IsEnabled(options.ExecutionLogLevel);
            var scopesEnabled = Logger.IsEnabled(options.StateItemsLogLevel);

            if (executionLogEnabled) {
                using var d = StartScope(p4, scopesEnabled);
                using var c = StartScope(p3, scopesEnabled);
                using var b = StartScope(p2, scopesEnabled);
                using var a = StartScope(p1, scopesEnabled);

                LogExecuting(method);
            }

            R r;
            using (GetScope(method)) {
                r = await method(p1, p2, p3, p4);
            }

            if (executionLogEnabled) {
                using var _ = StartScope(r, scopesEnabled);

                LogExecuted(method);
            }

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
            var options = _options.CurrentValue;
            var executionLogEnabled = Logger.IsEnabled(options.ExecutionLogLevel);
            var scopesEnabled = Logger.IsEnabled(options.StateItemsLogLevel);

            if (executionLogEnabled) {
                using var e = StartScope(p5, scopesEnabled);
                using var d = StartScope(p4, scopesEnabled);
                using var c = StartScope(p3, scopesEnabled);
                using var b = StartScope(p2, scopesEnabled);
                using var a = StartScope(p1, scopesEnabled);

                LogExecuting(method);
            }

            R r;
            using (GetScope(method)) {
                r = await method(p1, p2, p3, p4, p5);
            }

            if (executionLogEnabled) {
                using var _ = StartScope(r, scopesEnabled);

                LogExecuted(method);
            }

            return r;
        }

        private IDisposable StartScope<State>(State state, bool enabled) {
            if (enabled && state is object) {
                var items = _loggableParser.ParseLoggable(state);

                if (items.Count() > 0) {
                    return Logger.BeginScope(items);
                }
            }

            return null!;
        }

        private void LogExecuting(Delegate method) {
            var names = Names(_options.CurrentValue.MethodExecutingTemplate, method);

            Logger.Log(_options.CurrentValue.ExecutionLogLevel, _options.CurrentValue.MethodExecutingTemplate.Value, names[0], names[1], names[2]);
        }

        private void LogExecuted(Delegate method) {
            var names = Names(_options.CurrentValue.MethodExecutedTemplate, method);

            Logger.Log(_options.CurrentValue.ExecutionLogLevel, _options.CurrentValue.MethodExecutedTemplate.Value, names[0], names[1], names[2]);
        }

        private IDisposable GetScope(Delegate method) {
            var names = Names(_options.CurrentValue.ScopeTemplate, method);

            return Logger.BeginScope(_options.CurrentValue.ScopeTemplate.Value, names[0], names[1], names[2]);
        }

        private string[] Names(Template template, Delegate method) {
            var names = _delegateCache.GetOrAdd(method, key => {
                return new string[] {
                    method.Method.DeclaringType?.Assembly.GetName().Name ?? string.Empty,
                    method.Method.DeclaringType?.Name ?? string.Empty,
                    method.Method.Name
                };
            });

            return _loggingTemplateHelper.OrderNames(template, names[0], names[1], names[2]);
        }
    }
}