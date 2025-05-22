using Gvn.SmartCertify.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace Gvn.SmartCertify.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<SmartCertifyContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DbContext"),
                    p => p.EnableRetryOnFailure());
            });
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                
                app.MapOpenApi();
                app.MapScalarApiReference(options =>
                {
                    options.WithTitle("Smart Certify");
                    options.Title = "Smart Certify";
                    options.Theme = ScalarTheme.Default;
                    options.Favicon = "/favicon.svg";
                    options.Layout = ScalarLayout.Modern;
                    options.DarkMode = true;
                    options.CustomCss = "* { font-family: 'Monaco'; }";
                    options.Metadata = new Dictionary<string, string>()
                    {
                        { "ogDescription", "Open Graph description" },
                        { "ogTitle", "Open Graph title" },
                        { "twitterCard", "summary_large_image" },
                        { "twitterSite", "https://example.com/api" },
                        { "twitterTitle", "My Api documentation" },
                        { "twitterDescription", "This is the description for the twitter card" },
                        { "twitterImage", "https://dotnet.microsoft.com/blob-assets/images/illustrations/swimlane-build-scalable-web-apps.svg" }
                    };
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}