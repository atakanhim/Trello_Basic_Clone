using trelloClone.Presentation.Mappings;
using trelloClone.Application;
using trelloClone.Infrastructure;
using trelloClone.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using itApp.API.Extensions;
using Serilog.Core;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Data;
using System.Security.Claims;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddHttpClient();

        // ioc container islemleri
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices();
        builder.Services.AddPersistenceServices();
        // ioc container islemleri
        builder.Services.AddSession();
        //token

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = builder.Configuration["Token:Audience"],
                ValidIssuer = builder.Configuration["Token:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
                NameClaimType = ClaimTypes.Name //JWT �zerinde Name claimne kar��l�k gelen de�eri User.Identity.Name propertysinden elde edebiliriz.

            };
        });
        // Authorization politikalarını ekleyin
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("RequireLoggedIn", policy => policy.RequireAuthenticatedUser());
        });

        //log ekleme
        Log.Logger = new LoggerConfiguration()
               .WriteTo
               .MSSqlServer(
                   connectionString: builder.Configuration["ConnectionStrings:MsSQL"],
                   sinkOptions: new MSSqlServerSinkOptions { TableName = "LogEvents" })
               .CreateLogger();


        // log bitis
        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Home/Login"; // Oturum açma sayfasının yolu
            options.AccessDeniedPath = "/AccessDenied"; // Yetki reddedildiğinde yönlendirilecek sayfanın yolu
        });

        //token end
        builder.Services.AddAutoMapper(typeof(PresentationTrelloProfile));
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.ConfigureExceptionHandler(app.Services.GetRequiredService<ILogger<Program>>());

        app.UseAuthentication();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseSession();

        app.UseRouting();

        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });


        app.Run();
    }
}