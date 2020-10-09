using System;
using System.Threading.Tasks;

namespace TinyBeans.Logging {

    /// <summary>
    /// Represents a type used to wrap and enhance method calls with additional logging.
    /// </summary>
    public interface IAsyncLoggingAspect {

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <param name="method">The method to invoke.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        Task InvokeAsync(Func<Task> method);

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <typeparam name="P1">The type of the first parameter to pass to the method.</typeparam>
        /// <param name="method">The method to invoke.</param>
        /// <param name="parameter1">The first parameter to pass to the method.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        Task InvokeAsync<P1>(Func<P1, Task> method, P1 parameter1);

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <typeparam name="P1">The type of the first parameter to pass to the method.</typeparam>
        /// <typeparam name="P2">The type of the second parameter to pass to the method.</typeparam>
        /// <param name="method">The method to invoke.</param>
        /// <param name="parameter1">The first parameter to pass to the method.</param>
        /// <param name="parameter2">The second parameter to pass to the method.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        Task InvokeAsync<P1, P2>(Action<P1, P2, Task> method, P1 parameter1, P2 parameter2);

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
        Task InvokeAsync<P1, P2, P3>(Action<P1, P2, P3, Task> method, P1 parameter1, P2 parameter2, P3 parameter3);

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
        Task InvokeAsync<P1, P2, P3, P4>(Action<P1, P2, P3, P4, Task> method, P1 parameter1, P2 parameter2, P3 parameter3, P4 parameter4);

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
        Task InvokeAsync<P1, P2, P3, P4, P5>(Action<P1, P2, P3, P4, P5, Task> method, P1 parameter1, P2 parameter2, P3 parameter3, P4 parameter4, P5 parameter5);

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <typeparam name="R">The result type of the <see cref="Task"/>.</typeparam>
        /// <param name="method">The method to invoke.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        Task<R> InvokeAsync<R>(Func<Task<R>> method);

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <typeparam name="P1">The type of the first parameter to pass to the method.</typeparam>
        /// <typeparam name="R">The result type of the <see cref="Task"/>.</typeparam>
        /// <param name="method">The method to invoke.</param>
        /// <param name="parameter1">The first parameter to pass to the method.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        Task<R> InvokeAsync<P1, R>(Func<P1, Task<R>> method, P1 parameter1);

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
        Task<R> InvokeAsync<P1, P2, R>(Func<P1, P2, Task<R>> method, P1 parameter1, P2 parameter2);

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
        Task<R> InvokeAsync<P1, P2, P3, R>(Func<P1, P2, P3, Task<R>> method, P1 parameter1, P2 parameter2, P3 parameter3);

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
        Task<R> InvokeAsync<P1, P2, P3, P4, R>(Func<P1, P2, P3, P4, Task<R>> method, P1 parameter1, P2 parameter2, P3 parameter3, P4 parameter4);

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
        Task<R> InvokeAsync<P1, P2, P3, P4, P5, R>(Func<P1, P2, P3, P4, P5, Task<R>> method, P1 parameter1, P2 parameter2, P3 parameter3, P4 parameter4, P5 parameter5);
    }
}