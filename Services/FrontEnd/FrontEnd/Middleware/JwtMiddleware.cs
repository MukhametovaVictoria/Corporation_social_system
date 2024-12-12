public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Cookies["jwtToken"];

        if (token != null)
        {
            var isValid = await Task.FromResult(true); // Временная заглушка, в будущем - проверка токена в микросервисе авторизации.
            if (!isValid)
            {
                context.Response.Cookies.Delete("jwtToken");
                context.Response.Redirect("/Login");
                return;
            }
        }

        await _next(context);
    }
}
