using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TinyBeans.Logging.Abstractions;
using TinyBeans.Logging.Defaults;
using TinyBeans.Logging.Options;

namespace TinyBeans.Logging {

    /// <summary>
    /// Extensions for <see cref="Logging"/>.
    /// </summary>
    public static class LoggingAspectExtensions {

        /// <summary>
        /// Adds services required for using <see cref="ILoggingAspect{T}"/> using the default options.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddLoggingAspect(this IServiceCollection services) {
            return services
                .AddSingleton<ILoggableStateParser, DefaultLoggableStateParser>()
                .AddSingleton(typeof(ILoggingAspect<>), typeof(DefaultLoggingAspect<>));
        }

        /// <summary>
        /// Adds services required for using <see cref="ILoggingAspect{T}"/> using the supplied options.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="options">The options type to be configured.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddLoggingAspect(this IServiceCollection services, Action<LoggingAspectOptions> options) {
            return services
                .AddSingleton<ILoggableStateParser, DefaultLoggableStateParser>()
                .AddSingleton(typeof(ILoggingAspect<>), typeof(DefaultLoggingAspect<>))
                .Configure<LoggingAspectOptions>(options);
        }

        /// <summary>
        /// Adds services required for using <see cref="ILoggingAspect{T}"/> using the supplied options.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="configurationSection">The <see cref="IConfigurationSection"/> with the <see cref="LoggingAspectOptions"/> to be configured.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddLoggingAspect(this IServiceCollection services, IConfigurationSection configurationSection) {
            return services
                .AddSingleton<ILoggableStateParser, DefaultLoggableStateParser>()
                .AddSingleton(typeof(ILoggingAspect<>), typeof(DefaultLoggingAspect<>))
                .Configure<LoggingAspectOptions>(configurationSection);
        }
    }
}