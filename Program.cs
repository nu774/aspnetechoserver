var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options => {
    options.ListenAnyIP(8080);
    options.Limits.MinRequestBodyDataRate = null;
});
var app = builder.Build();

app.Run(async context =>
{
    context.Response.Headers.ContentType = context.Request.Headers.ContentType;
    using (var istream = context.Request.Body)
    using (var ostream = context.Response.Body)
    {
        await istream.CopyToAsync(ostream);
    }
});
app.Run();
