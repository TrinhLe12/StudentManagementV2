using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StudentManagementV2.Business.BusinessModules;
using StudentManagementV2.Business.Services;
using StudentManagementV2.Business.Validators;
using StudentManagementV2.Core.Models;
using StudentManagementV2.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementV2.Api
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
            
            services.AddDbContext<StudentManagementContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddScoped(typeof(IDepartmentRepository), typeof(DepartmentRepository));
            services.AddScoped(typeof(IRepository<Department, string>), typeof(DepartmentRepository));
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IService<Department, string>, DepartmentService>();

            services.AddScoped(typeof(IAssignClassRepository), typeof(AssignClassRepository));
            services.AddScoped(typeof(IRepository<AssignClass, string>), typeof(AssignClassRepository));
            services.AddTransient<IAssignClassService, AssignClassService>();
            services.AddTransient<IService<AssignClass, string>, AssignClassService>();

            services.AddScoped(typeof(IStudentRepository), typeof(StudentRepository));
            services.AddScoped(typeof(IRepository<Student, string>), typeof(StudentRepository));
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IService<Student, string>, StudentService>();

            services.AddScoped(typeof(IRoleRepository), typeof(RoleRepository));
            services.AddScoped(typeof(IRepository<Role, string>), typeof(RoleRepository));
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IService<Role, string>, RoleService>();

            services.AddScoped(typeof(ISemesterRepository), typeof(SemesterRepository));
            services.AddScoped(typeof(IRepository<Semester, string>), typeof(SemesterRepository));
            services.AddTransient<ISemesterService, SemesterService>();
            services.AddTransient<IService<Semester, string>, SemesterService>();

            services.AddScoped(typeof(ISubjectRepository), typeof(SubjectRepository));
            services.AddScoped(typeof(IRepository<Subject, string>), typeof(SubjectRepository));
            services.AddTransient<ISubjectService, SubjectService>();
            services.AddTransient<IService<Subject, string>, SubjectService>();

            services.AddScoped(typeof(ISubjectRegisterRepository), typeof(SubjectRegisterRepository));
            services.AddScoped(typeof(IRepository<SubjectRegister, object>), typeof(SubjectRegisterRepository));
            services.AddTransient<ISubjectRegisterService, SubjectRegisterService>();
            services.AddTransient<IService<SubjectRegister, object>, SubjectRegisterService>();

            services.AddTransient<ICommonValidator, CommonValidator>();

            services.AddAutoMapper(typeof(Startup));

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Student Management API"
                });
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: "MyAllowSpecificOrigins",
                    builder =>
                    {
                        builder
                            .WithOrigins("https://localhost:44303", "https://localhost:44369")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("MyAllowSpecificOrigins");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Student Management API");
                c.RoutePrefix = string.Empty;
            });

        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule<BusinessModule>();
        }
    }
}
