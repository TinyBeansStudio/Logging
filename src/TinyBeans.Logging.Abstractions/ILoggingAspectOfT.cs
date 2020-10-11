using System;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Logging {

    /// <summary>
    /// Represents a type used to wrap and enhance method calls with additional logging.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="ILogger{T}"/> logs will be written to.</typeparam>
    public interface ILoggingAspect<T> {

        /// <summary>
        /// The logger used when writing additional logs.
        /// </summary>
        ILogger<T> Logger { get; }

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <param name="method">The method to invoke.</param>
        void Invoke(Action method);

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <typeparam name="P1">The type of the first parameter to pass to the method.</typeparam>
        /// <param name="method">The method to invoke.</param>
        /// <param name="p1">The first parameter to pass to the method.</param>
        void Invoke<P1>(Action<P1> method, P1 p1);

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <typeparam name="P1">The type of the first parameter to pass to the method.</typeparam>
        /// <typeparam name="P2">The type of the second parameter to pass to the method.</typeparam>
        /// <param name="method">The method to invoke.</param>
        /// <param name="p1">The first parameter to pass to the method.</param>
        /// <param name="p2">The second parameter to pass to the method.</param>
        void Invoke<P1, P2>(Action<P1, P2> method, P1 p1, P2 p2);

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
        void Invoke<P1, P2, P3>(Action<P1, P2, P3> method, P1 p1, P2 p2, P3 p3);

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
        void Invoke<P1, P2, P3, P4>(Action<P1, P2, P3, P4> method, P1 p1, P2 p2, P3 p3, P4 p4);

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
        void Invoke<P1, P2, P3, P4, P5>(Action<P1, P2, P3, P4, P5> method, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5);

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <typeparam name="R">The result type of the <see cref="Task"/>.</typeparam>
        /// <param name="method">The method to invoke.</param>
        /// <returns>The result of the invoked method.</returns>
        R Invoke<R>(Func<R> method);

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <typeparam name="P1">The type of the first parameter to pass to the method.</typeparam>
        /// <typeparam name="R">The result type of the <see cref="Task"/>.</typeparam>
        /// <param name="method">The method to invoke.</param>
        /// <param name="p1">The first parameter to pass to the method.</param>
        /// <returns>The result of the invoked method.</returns>
        R Invoke<P1, R>(Func<P1, R> method, P1 p1);

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
        R Invoke<P1, P2, R>(Func<P1, P2, R> method, P1 p1, P2 p2);

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
        R Invoke<P1, P2, P3, R>(Func<P1, P2, P3, R> method, P1 p1, P2 p2, P3 p3);

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
        R Invoke<P1, P2, P3, P4, R>(Func<P1, P2, P3, P4, R> method, P1 p1, P2 p2, P3 p3, P4 p4);

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
        R Invoke<P1, P2, P3, P4, P5, R>(Func<P1, P2, P3, P4, P5, R> method, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5);

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
        /// <param name="p1">The first parameter to pass to the method.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        Task InvokeAsync<P1>(Func<P1, Task> method, P1 p1);

        /// <summary>
        /// Invokes the supplied method with additional logging.
        /// </summary>
        /// <typeparam name="P1">The type of the first parameter to pass to the method.</typeparam>
        /// <typeparam name="P2">The type of the second parameter to pass to the method.</typeparam>
        /// <param name="method">The method to invoke.</param>
        /// <param name="p1">The first parameter to pass to the method.</param>
        /// <param name="p2">The second parameter to pass to the method.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        Task InvokeAsync<P1, P2>(Func<P1, P2, Task> method, P1 p1, P2 p2);

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
        Task InvokeAsync<P1, P2, P3>(Func<P1, P2, P3, Task> method, P1 p1, P2 p2, P3 p3);

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
        Task InvokeAsync<P1, P2, P3, P4>(Func<P1, P2, P3, P4, Task> method, P1 p1, P2 p2, P3 p3, P4 p4);

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
        Task InvokeAsync<P1, P2, P3, P4, P5>(Func<P1, P2, P3, P4, P5, Task> method, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5);

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
        /// <param name="p1">The first parameter to pass to the method.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        Task<R> InvokeAsync<P1, R>(Func<P1, Task<R>> method, P1 p1);

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
        Task<R> InvokeAsync<P1, P2, R>(Func<P1, P2, Task<R>> method, P1 p1, P2 p2);

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
        Task<R> InvokeAsync<P1, P2, P3, R>(Func<P1, P2, P3, Task<R>> method, P1 p1, P2 p2, P3 p3);

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
        Task<R> InvokeAsync<P1, P2, P3, P4, R>(Func<P1, P2, P3, P4, Task<R>> method, P1 p1, P2 p2, P3 p3, P4 p4);

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
        Task<R> InvokeAsync<P1, P2, P3, P4, P5, R>(Func<P1, P2, P3, P4, P5, Task<R>> method, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5);
    }
}