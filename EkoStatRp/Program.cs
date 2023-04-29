using EkoStatRp.Helpers;

namespace EkoStatRp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // --- Add services to the container. ---

        builder.Services.AddRazorPages();

        // F�r att kunna anv�nda HttpContext.Session-metoder f�r att lagra data tillf�lligt p� servern.
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(options =>
        {
            //options.IdleTimeout = TimeSpan.FromMinutes(30);
            //options.Cookie.IsEssential = true; // G�r att cookie inte kan raderas.
            options.Cookie.HttpOnly = true; // Ska vara med av s�kerhetssk�l.
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Skicka endast cookie �ver https.
        });
        builder.Services.AddHttpContextAccessor(); // F�r att kunna injecta IHttpContextAccessor, som anv�nds f�r att n� HttpContext.Session-metoder.

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
        app.UseSession(); // F�r att kunna anv�nda HttpContext.Session-metoder f�r att lagra data tillf�lligt p� servern.
        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }
}