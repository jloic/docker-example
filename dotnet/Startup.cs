using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MySqlConnector;

namespace dotnet
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "dotnet", Version = "v1" });
            });

            string host = Environment.GetEnvironmentVariable("MYSQL_HOST") ;
            if(host == null) host= "127.0.0.1";
            string port = Environment.GetEnvironmentVariable("MYSQL_PORT");
            if (port == null) port = "3308";
            string database = Environment.GetEnvironmentVariable("MYSQL_DATABASE");
            if (database == null) database = "test_database";

            string user = Environment.GetEnvironmentVariable("MYSQL_USER");
            if (user == null) user = "root";
            string password = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");
            if (password == null) password = "root";
            string connection = "server=" + host + ":" + port  + ";user=" + user + ";password=" + password + ";database=" + database;
            services.AddTransient<MySqlConnection>(_ => new MySqlConnection(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "dotnet v1"));
            }


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
