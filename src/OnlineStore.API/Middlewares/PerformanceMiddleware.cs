using System.Diagnostics;

namespace OnlineStore.Api.Middlewares
{
    public class PerformanceMiddleware
    {
        private readonly ILogger<PerformanceMiddleware> _logger;
        private readonly RequestDelegate _next;

        private const int PerformanceTimeLog = 500;

        public PerformanceMiddleware(RequestDelegate next, ILogger<PerformanceMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var sw = new Stopwatch();

            sw.Start();

            await _next(context);

            sw.Stop();

            if (sw.ElapsedMilliseconds > PerformanceTimeLog)
            {
                _logger.LogWarning("Request {method} {path} it took about {elapsed} ms",
                    context.Request?.Method,
                    context.Request?.Path.Value,
                    sw.ElapsedMilliseconds);
            }
        }
    }
}
