using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using ContactManagement.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;

namespace ContactManagement.UI
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
            string connectionstringName = "DefaultConnection";

            switch (Configuration["Environment"])
            {
                case "Office-Laptop":
                    connectionstringName = "DefaultConnection";
                    break;
                case "Study-Laptop":
                    connectionstringName = "Study-Laptop-ConnectionString";
                    break;
            }

            services.AddDbContext<ContactManagementDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString(connectionstringName)));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ContactManagementDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            switch (Configuration.GetSection("AuthenticationScheme").GetValue<string>("Scheme"))
            {
                case "MicrosoftIdentity":
                    services.AddAuthentication(AzureADDefaults.AuthenticationScheme)
                    .AddAzureAD(options => Configuration.Bind("AzureAd", options));

                    _ = services.Configure<OpenIdConnectOptions>(AzureADDefaults.OpenIdScheme, options =>
                      {
                          // Microsoft identity platform
                          options.Authority += "/v2.0/";

                          options.ClientSecret = Configuration["Authentication:Microsoft:ClientSecret"];

                          /* 
                           * Per the code below, this application signs in users in any Work and School 
                           * accounts and any Microsoft Personal Accounts. 
                           * If you want to direct Azure AD to restrict the users that can sign-in, change 
                           * the tenant value of the appsettings.json file in the following way: 
                           * - only Work and School accounts => 'organizations' 
                           * - only Microsoft Personal accounts => 'consumers' 
                           * - Work and School and Personal accounts => 'common' 
                           * 
                           * If you want to restrict the users that can sign-in to only one tenant 
                           * set the tenant value in the appsettings.json file to the tenant ID of this 
                           * organization, and set ValidateIssuer below to true. 
                           * 
                           * If you want to restrict the users that can sign-in to several organizations 
                           * Set the tenant value in the appsettings.json file to 'organizations', set 
                           * ValidateIssuer, above to 'true', and add the issuers you want to accept to the 
                           * 
                           * options.TokenValidationParameters.ValidIssuers collection
                           * 
                           */

                          // accept several tenants (here simplified)
                          options.TokenValidationParameters.ValidateIssuer = false;
                      });
                    break;
                default:
                    //DO NOTHING
                    break;
            };

            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            })
                .SetCompatibilityVersion(CompatibilityVersion.Latest);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
