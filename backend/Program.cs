using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using QuizMania.WebAPI.Data;

namespace QuizMania.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            DatabaseInitializer.SeedAsync(host).Wait();

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}