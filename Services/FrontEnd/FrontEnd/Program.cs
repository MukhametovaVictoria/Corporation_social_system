using FrontEnd.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

app.ConfigureMiddleware();

app.UseSession();

app.Run();
