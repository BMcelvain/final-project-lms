using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lms.Daos;
using Lms.Wrappers;

namespace Lms
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
            services.AddScoped<ISqlWrapper, SqlWrapper>();
            services.AddScoped<ICourseDao, CourseDao>();
            services.AddScoped<ISemesterDao, SemesterDao>();    
            services.AddScoped<ITeacherDao, TeacherDao>();
            services.AddScoped<IStudentDao, StudentDao>();
            services.AddScoped<IStudentEnrollmentDao, StudentEnrollmentDao>();
            services.AddScoped<IStudentActiveEnrollmentDao, StudentActiveEnrollmentDao>();

            services.AddControllers().AddNewtonsoftJson();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LMS Api", Description = "A CRUD Api for Learning Management System", Version = "v1" });
            }).AddSwaggerGenNewtonsoftSupport();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(opt => opt.SwaggerEndpoint("/swagger/v1/swagger.json", "LMS Api V1"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
