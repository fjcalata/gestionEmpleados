using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.BL.Implementations;
using EmployeeManagement.BL.Interfaces;
using EmployeeManagement.Entities.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace EmployeeManagement.Presentation
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
            var cultureES = new CultureInfo("es-ES");
            CultureInfo.CurrentCulture = cultureES;
            CultureInfo.CurrentUICulture = cultureES;
            CultureInfo.DefaultThreadCurrentCulture = cultureES;
            CultureInfo.DefaultThreadCurrentUICulture = cultureES;

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.Culture = cultureES;                
            });

            services.AddDbContext<EmployeesManagementDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("EmployeesManagementDB")));            

            services.AddScoped<IAbsenceTypeBL, AbsenceTypeBL>();
            services.AddScoped<IExtensionTypesBL, ExtensionTypesBL>();
            services.AddScoped<ICertificationsBL, CertificationsBL>();
            services.AddScoped<IDayExtensionsBL, DayExtensionsBL>();
            services.AddScoped<IDegreesBL, DegreesBL>();
            services.AddScoped<IDepartmentsBL, DepartmentsBL>();
            services.AddScoped<IDisplacementsDaysBL, DisplacementsDaysBL>();
            services.AddScoped<IDisplacementTypesBL, DisplacementsTypesBL>();
            services.AddScoped<IExtensionTypesBL, ExtensionTypesBL>();
            services.AddScoped<IManageDatesBL, ManageDatesBL>();
            services.AddScoped<IFunctionTypesBL, FunctionTypesBL>();
            services.AddScoped<IPersonsBL, PersonsBL>();
            services.AddScoped<ITrainingsBL, TrainingsBL>();
            services.AddScoped<IVacationsBL, VacationsBL>();

            services.AddScoped<IValidationService, ValidationService>();
            services.AddScoped<IManageFiles, ManageFiles>();
            
            services.AddKendo();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.LoginPath = "/login";
                options.LogoutPath = "/logout";
            });          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }           

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=Index}/{id?}");
            });           
        }
    }
}
