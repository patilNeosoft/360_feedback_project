using Feedback360_Frontend.IServices;
using Feedback360_Frontend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigurationManager Configuration = new ConfigurationManager();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<Captcha>();
builder.Services.AddSession();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ISendMailService, SendMailService>();
builder.Services.AddScoped<IOtpService, OtpService>();
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
});
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.WithOrigins("https://localhost:5000"));
});
var app = builder.Build();
AppSettingsHelper.AppSettingsConfigure(app.Services.GetRequiredService<IConfiguration>());

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404)
    {
        context.Request.Path = "/Error/PageNotFoundError";
        await next();
    }
    if (context.Response.StatusCode == 500)
    {
        context.Request.Path = "/Error/InternalServerError";
        await next();
    }
});


//app.Use((ctx, next) =>
//{
//    var headers = ctx.Response.Headers;

//    headers.Add("X-Frame-Options", "DENY");
//    headers.Add("X-XSS-Protection", "1; mode=block");
//    headers.Add("X-Content-Type-Options", "nosniff");
//    headers.Add("Strict-Transport-Security", "max-age=31536000; includeSubDomains; preload");

//    headers.Remove("X-Powered-By");
//    headers.Remove("x-aspnet-version");

//    // Some headers won't remove
//    headers.Remove("Server");

//    return next();
//});
app.UseRouting();
app.UseSession();
app.UseCors(options => options.AllowAnyOrigin());
app.UseCors(options => options.WithOrigins("https://localhost:5000"));


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=UserLogin}/{id?}");

app.Run();
