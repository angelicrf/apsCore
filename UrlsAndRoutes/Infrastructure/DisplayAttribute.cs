using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace UrlsAndRoutes.Infrastructure
{
    public class DisplayAttribute : ResultFilterAttribute
    {
        private string display;
        public DisplayAttribute(string msg)
        {
            display = msg;
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            WriteMessage(context, $"<div>Before Result:{display}</div>");
        }
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            WriteMessage(context, $"<div>After Result:{display}</div>");
        }
        private void WriteMessage(FilterContext context, string msg)
        {
            byte[] bytes = Encoding.ASCII
            .GetBytes($"<div>{msg}</div>");
            context.HttpContext.Response
            .Body.Write(bytes, 0, bytes.Length);
        }
    }
}