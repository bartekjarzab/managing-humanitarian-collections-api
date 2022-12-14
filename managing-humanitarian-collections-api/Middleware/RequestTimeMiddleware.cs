using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace managing_humanitarian_collections_api.Middleware
{
    public class RequestTimeMiddleware : IMiddleware
    {
        private readonly ILogger<RequestTimeMiddleware> _logger;
        private Stopwatch _stopWatch;
        public RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger)
        {
            _stopWatch = new Stopwatch();
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _stopWatch.Start();
            await next.Invoke(context);
            _stopWatch.Stop();

            var elepsedMilliseconds = _stopWatch.ElapsedMilliseconds;

            if(elepsedMilliseconds / 1000 > 4)
            {
                var message = $"Request [{context.Request.Method}] at {context.Request.Path} took {elepsedMilliseconds} ms"; 
            }
        }
    }
}
