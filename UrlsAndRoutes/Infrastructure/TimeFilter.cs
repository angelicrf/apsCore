using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Concurrent;

namespace UrlsAndRoutes.Infrastructure
{
    public class TimeFilter: IAsyncActionFilter, IAsyncResultFilter {

        private ConcurrentQueue<double> actionTimes = new ConcurrentQueue<double>();
        private ConcurrentQueue<double> resultTimes = new ConcurrentQueue<double>();
       
        private FilterDiagnostics diagnostics;
    public TimeFilter(FilterDiagnostics diags)
    {
        diagnostics = diags;
    }
    public async Task OnActionExecutionAsync(ActionExecutingContext context,ActionExecutionDelegate next)
    {
        Stopwatch timer = Stopwatch.StartNew();
        await next();
        actionTimes.Enqueue(timer.Elapsed.TotalMilliseconds);

        diagnostics.AddMessage($@"Action time:
                                 {timer.Elapsed.TotalMilliseconds}
                                  Average: { actionTimes.Average():F2}");
    }
    public async Task OnResultExecutionAsync(ResultExecutingContext context,ResultExecutionDelegate next)
    {
        Stopwatch timer = Stopwatch.StartNew();
        await next();
        timer.Stop();

        resultTimes.Enqueue(timer.Elapsed.TotalMilliseconds);

        diagnostics.AddMessage($@"Result time:
                               {timer.Elapsed.TotalMilliseconds}
                               Average: {resultTimes.Average():F2}");
        }
}
}
