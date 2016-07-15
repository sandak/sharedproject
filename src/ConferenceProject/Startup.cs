using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ConferenceProject.Models;
using ConferenceProject.Services;
using Microsoft.AspNet.Identity;


namespace ConferenceProject
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();

                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859
                try
                {
                    using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                        .CreateScope())
                    {
                        serviceScope.ServiceProvider.GetService<ApplicationDbContext>()
                             .Database.Migrate();
                    }
                }
                catch { }
            }

            app.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseIdentity();

            // To configure external authentication please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=ConferenceItems}/{action=Home}/{id?}");
            });
          
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    


    
    /// ////////////////////////////////////////////////////////////////////////
   



    // In this method we will create default User roles and Admin user for login   
    private void createRolesandUsers()
    {
        ApplicationDbContext context = new ApplicationDbContext();

            // var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
           var passGen = new PasswordHasher<ApplicationUser>();
        var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context), null, null, null, null, null, null, null, null, null);
        var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context), null, null, null, null, null);


            //create admin role//
            var tmp = context.IdentityRoleList.ToList();
            int flag = 0;
            foreach (var item in tmp)
                if (item.Name == "admin")
                    flag = 1;
            if (flag == 0)
            {
                var role = new IdentityRole();
                role.Name = "admin";
                role.NormalizedName = "ADMINROLE";
                context.IdentityRoleList.Add(role);
                context.SaveChanges();

                var user = new ApplicationUser();
                user.UserName = "sysAdmin";
                user._fname = "sysAdmin";
                user._lname = "sysAdmin";
                user.Email = "sysAdmin@sysAdmin.com";
                user.EmailConfirmed = true;
                user.PasswordHash = passGen.HashPassword(user,"sysAdmin1!");

                context.Users.Add(user);
                context.SaveChanges();

                var userRole = new IdentityUserRole<string>();
                userRole.RoleId = context.IdentityRoleList.First(m => m.Name == "admin").Id;
                userRole.UserId = context.Users.First(m => m.UserName == "sysAdmin").Id;
                context.UserRoles.Add(userRole);
                context.SaveChanges();

            }
                    }
}
}
