using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using DotNetify;

namespace SayHiVM
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddMemoryCache();
            services.AddSignalR(options => options.Hubs.EnableDetailedErrors = true);
            services.AddDotNetify();
        }

        public void Configure(IApplicationBuilder app)
        {
            // Configure SignalR to use web sockets and to allow cross-origin to support external app.
            app.UseWebSockets();
            app.Map("/signalr", map =>
            {
                map.UseCors("CorsPolicy");
                map.RunSignalR();
            });

            app.UseStaticFiles();

            app.UseDotNetify();
        }
    }
}