using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TinyBeans.Logging.Abstractions;
using TinyBeans.Logging.Options;

namespace TinyBeans.Logging.Mvc.Filters {

    /// <summary>
    /// Mvc filter used to wrap and enhance controller calls with additional logging.
    /// </summary>
    public class LoggingFilter : IAsyncActionFilter {
        private readonly IOptionsMonitor<LoggingOptions> _options;
        private readonly ILogger<LoggingFilter> _logger;
        private readonly ILoggableParser _loggableParser;

        private static readonly ConcurrentDictionary<string, string[]> _actionInformationCache = new ConcurrentDictionary<string, string[]>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">The <see cref="LoggingOptions"/> to use.</param>
        /// <param name="logger">The logger used when writing additional logs.</param>
        /// <param name="loggableParser">The state parser to use when logging parameters and results.</param>
        public LoggingFilter(IOptionsMonitor<LoggingOptions> options, ILogger<LoggingFilter> logger, ILoggableParser loggableParser) {
            _options = options;
            _logger = logger;
            _loggableParser = loggableParser;
        }

        /// <summary>
        /// Called asynchronously before the action, after model binding is complete.
        /// </summary>
        /// <param name="context">The <see cref="ActionExecutingContext"/>.</param>
        /// <param name="next">The <see cref="ActionExecutionDelegate"/>. Invoked to execute the next action filter or the action itself.</param>
        /// <returns>A <see cref="Task"/> that on completion indicates the filter has executed.</returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
            LogExecuting(context);

            ObjectResult? objectResult;
            using (var _ = _logger.BeginScope(GetScope(context))) {
                objectResult = (await next()).Result as ObjectResult;
            }

            LogExecuted(context, objectResult?.Value);
        }

        private void LogExecuting(ActionExecutingContext context) {
            if (!_logger.IsEnabled(_options.CurrentValue.ExecutionLogLevel)) {
                return;
            }

            List<IDisposable> scopes = null!;
            try {
                if (context.ActionArguments.Count > 0 && _logger.IsEnabled(_options.CurrentValue.StateItemsLogLevel)) {
                    scopes = new List<IDisposable>(context.ActionArguments.Count);

                    foreach (var parameter in context.ActionArguments.Reverse()) {
                        var items = _loggableParser.ParseLoggable(parameter.Value);

                        if (items.Count() > 0) {
                            scopes.Add(_logger.BeginScope(items));
                        }
                    }
                }

                var names = Names(_options.CurrentValue.MethodExecutingTemplate, context);

                _logger.Log(_options.CurrentValue.ExecutionLogLevel, _options.CurrentValue.MethodExecutingTemplate, names[0], names[1], names[2]);
            } finally {
                scopes?.ForEach(scope => scope?.Dispose());
            }
        }

        private void LogExecuted(ActionExecutingContext context, object? result) {
            if (!_logger.IsEnabled(_options.CurrentValue.ExecutionLogLevel)) {
                return;
            }

            IDisposable scope = null!;
            try {
                if (_logger.IsEnabled(_options.CurrentValue.StateItemsLogLevel) && result is object) {
                    var items = _loggableParser.ParseLoggable(result);

                    if (items.Count() > 0) {
                        scope = _logger.BeginScope(items);
                    }
                }

                var names = Names(_options.CurrentValue.MethodExecutedTemplate, context);

                _logger.Log(_options.CurrentValue.ExecutionLogLevel, _options.CurrentValue.MethodExecutedTemplate, names[0], names[1], names[2]);
            } finally {
                scope?.Dispose();
            }
        }

        private IDisposable GetScope(ActionExecutingContext context) {
            var names = Names(_options.CurrentValue.ScopeTemplate, context);

            return _logger.BeginScope(_options.CurrentValue.ScopeTemplate, names[0], names[1], names[2]);
        }

        private string[] Names(string template, ActionExecutingContext context) {
            var names = _actionInformationCache.GetOrAdd(context.ActionDescriptor.Id, key => {
                return new string[] {
                    context.Controller.GetType().Assembly.GetName().Name,
                    context.Controller.GetType().Name,
                    context.ActionDescriptor.DisplayName
                };
            });

            return Template.OrderNames(template, names[0], names[1], names[2]);
        }
    }
}