using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using QuizMania.WebAPI.Services;
using Microsoft.Data.Sqlite;

namespace QuizMania.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private SqliteConnection inMemorySqlite;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IQuizAsyncRepository, MockQuizRepository>();
            services.AddScoped<IQuizService, QuizService>();

            inMemorySqlite = new SqliteConnection(Configuration.GetConnectionString("SqliteInMemoryConnection"));
            inMemorySqlite.Open();

            //services.AddDbContext<QuizContext>(opt => { opt.UseInMemoryDatabase("InMemory Quizzes Database"); });
            //services.AddDbContext<QuizContext>(opt => { opt.UseSqlServer(Configuration.GetConnectionString("AzureSqlConnection")); });
            services.AddDbContext<QuizContext>(opt => { opt.UseSqlite(inMemorySqlite); });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}