using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudentManagementV2.Business.Services;
using StudentManagementV2.Core.Models;
using StudentManagementV2.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementV2
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
            services.AddRazorPages();

            services.AddDbContext<StudentManagementContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IDepartmentRepository), typeof(DepartmentRepository));
            services.AddScoped(typeof(IRepository<Department, string>), typeof(DepartmentRepository));
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IService<Department, string>, DepartmentService>();

            services.AddScoped(typeof(IAssignClassRepository), typeof(AssignClassRepository));
            services.AddScoped(typeof(IRepository<AssignClass, string>), typeof(AssignClassRepository));
            services.AddTransient<IAssignClassService, AssignClassService>();
            services.AddTransient<IService<AssignClass, string>, AssignClassService>();

            services.AddScoped(typeof(IRoleRepository), typeof(RoleRepository));
            services.AddScoped(typeof(IRepository<Role, string>), typeof(RoleRepository));
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IService<Role, string>, RoleService>();

            services.AddScoped(typeof(IStudentRepository), typeof(StudentRepository));
            services.AddScoped(typeof(IRepository<Student, string>), typeof(StudentRepository));
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IService<Student, string>, StudentService>();

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

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            }).AddEntityFrameworkStores<StudentManagementContext>();

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.Name = "StudentManagementV2Cookie";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("MyAllowSpecificOrigins");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}");
            });

            Task.Run(() => this.CreateRoles(roleManager)).Wait();

            
        }

        private async Task CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            foreach (string rol in Configuration["UserRoles"].Split(",").ToList<string>())
            //foreach (string rol in Configuration.GetSection("Roles").Get<List<string>>())
            {
                if (!await roleManager.RoleExistsAsync(rol))
                {
                    await roleManager.CreateAsync(new IdentityRole(rol));
                }
            }
        }
    }
}
