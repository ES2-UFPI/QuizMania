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
using QuizMania.WebAPI.Data;
using System.Reflection;
using System.IO;

namespace QuizMania.WebAPI
{
    public class Startup
    {
        readonly string AllowNullOriginCors = "_allowNullOriginCors";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public static SqliteConnection inMemorySqliteConnection { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>{ options.AddPolicy(name: AllowNullOriginCors, builder => { builder.WithOrigins("null").AllowAnyHeader().AllowAnyMethod(); }); });

            services.AddControllers();

            services.AddScoped<IQuizAsyncRepository, QuizRepository>();
            services.AddScoped<ICharacterAsyncRepository, CharacterRepository>();
            services.AddScoped<IItemAsyncRepository, ItemRepository>();

            services.AddScoped<IQuizService, QuizService>();
            services.AddScoped<ICharacterService, CharacterService>();
            
            services.AddScoped<DatabaseInitializer>();

            inMemorySqliteConnection = new SqliteConnection(Configuration.GetConnectionString("SqliteInMemoryConnection"));
            inMemorySqliteConnection.Open();

            services.AddDbContext<DatabaseContext>(opt => { opt.UseSqlite(inMemorySqliteConnection); });
           
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen(opt =>
                {
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    opt.IncludeXmlComments(xmlPath);
                }
            );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DatabaseInitializer dbInitializer)
        {
            dbInitializer.SeederAsync().Wait();

            app.UseSwagger();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "QuizMania Web API v0.2.1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(AllowNullOriginCors);

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}