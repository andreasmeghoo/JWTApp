using System.Diagnostics;
using System;
using JWTApp.Data;
using JWTApp.Models.Logging;

namespace JWTApp.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            await _next(context);
            stopwatch.Stop();

            var db = context.RequestServices.GetRequiredService<JWTAppDBContext>();

            var logEntry = new LogEntry
            {
                Method = context.Request.Method,
                Path = context.Request.Path,
                StatusCode = context.Response.StatusCode,
                DurationMs = stopwatch.ElapsedMilliseconds
            };

            db.ApiLogs.Add(logEntry);
            await db.SaveChangesAsync();
        }
    }
}
