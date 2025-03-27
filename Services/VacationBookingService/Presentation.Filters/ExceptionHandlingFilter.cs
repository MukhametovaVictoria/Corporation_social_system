using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.Filters
{
    public class ExceptionHandlingFilter : IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            // Логика обработки исключений
            context.Result = new ObjectResult(new { Error = context.Exception.Message })
            {
                StatusCode = 500
            };
            await Task.CompletedTask;
        }
    }
}