using EkoStatRp.Helpers;

namespace EkoStatRp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // --- Add services to the container. ---

        builder.Services.AddRazorPages();

        // För att kunna använda HttpContext.Session-metoder för att lagra data tillfälligt på servern.
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(options =>
        {
            //options.IdleTimeout = TimeSpan.FromMinutes(30);
            //options.Cookie.IsEssential = true; // Gör att cookie inte kan raderas.
            options.Cookie.HttpOnly = true; // Ska vara med av säkerhetsskäl.
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Skicka endast cookie över https.
        });
        builder.Services.AddHttpContextAccessor(); // För att kunna injecta IHttpContextAccessor, som används för att nå HttpContext.Session-metoder.

        builder.Services.AddTransient<HttpHelper>();
        builder.Services.AddScoped<UserHelper>();

        var app = builder.Build();
        // --- Configure the HTTP request pipeline. ---

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseSession(); // För att kunna använda HttpContext.Session-metoder för att lagra data tillfälligt på servern.
        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }
}