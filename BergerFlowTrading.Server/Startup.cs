using BergerFlowTrading.Model.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using BergerFlowTrading.BusinessTier.Services;
using BergerFlowTrading.DataTier.Context;
using Microsoft.EntityFrameworkCore;
using BergerFlowTrading.Model.Mappers;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using BergerFlowTrading.DataTier.Repository;
using BergerFlowTrading.BusinessTier.Services.Logging;
using BergerFlowTrading.BusinessTier.Services.Encryption;
using BergerFlowTrading.Shared.Encryption;

namespace BergerFlowTrading.Server
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        private IWebHostEnvironment env { get; }

        public Startup(IWebHostEnvironment env)
        { 
            this.env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath);

            if (env.IsDevelopment())
            {
                builder.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
                builder.AddUserSecrets<Startup>();
            }
            else
            {
                builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new IdentityMapper());
                mc.AddProfile(new DataMapper());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddDbContext<ApplicationDbContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
              b => b.MigrationsAssembly("BergerFlowTrading.DataTier")));


            // add identity
            var builder = services.AddIdentityCore<AppUser>(o =>
            {
                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 1;
                o.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                o.User.RequireUniqueEmail = true;
            });
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder.AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            services.AddSingleton<StringCipher>();
            services.AddSingleton<ILoggingService, LoggingService>();

            services.AddScoped<ExchangeRepository>();
            services.AddScoped<LimitStrategy4SettingsRepository>();
            services.AddScoped<UserExchangeSecretRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<RoleManager<IdentityRole>>();
            services.AddScoped<UserManager<AppUser>>();
            services.AddScoped<SignInManager<AppUser>>();
            services.AddScoped<IdentityService>();



            // ===== Add Jwt Authentication ========
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = !this.env.IsDevelopment();
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JWT_Auth:JwtIssuer"],
                    ValidAudience = Configuration["JWT_Auth:JwtAudience"],
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT_Auth:JwtKey"]))
                };
            });


            services.AddMvc().AddNewtonsoftJson();
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            UpdateDatabase(app);

            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                Task.Run(() => CreateUserRoles(app)).Wait();
                app.UseDeveloperExceptionPage();
                app.UseBlazorDebugging();
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            app.UseBlazor<Client.Startup>();
        }


        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    context.Database.Migrate();
                    context.Database.EnsureCreated();
                }
            }
        }

        private async Task CreateUserRoles(IApplicationBuilder app)
        {
            try
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    ////Resolve ASP .NET Core Identity with DI help
                    //var userManager = (UserManager<ApplicationUser>)scope.ServiceProvider.GetService(typeof(UserManager<ApplicationUser>));
                    //// do you things here

                    var RoleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                    var UserManager = scope.ServiceProvider.GetService<UserManager<AppUser>>();

                    //Adding Admin Role
                    if (!await RoleManager.RoleExistsAsync("Admin"))
                    {
                        //create the roles and seed them to the database
                        await RoleManager.CreateAsync(new IdentityRole("Admin"));
                    }


                    //Adding BasicPlan Role
                    if (!await RoleManager.RoleExistsAsync("BasicPlan"))
                    {
                        //create the roles and seed them to the database
                        await RoleManager.CreateAsync(new IdentityRole("BasicPlan"));
                    }


                    //Assign Admin role to the main User here we have given our newly registered 
                    //login id for Admin management
                    AppUser userAdmin = await UserManager.FindByEmailAsync("jpberger.1989@gmail.com");

                    if (userAdmin != null)
                    {
                        if (!(await UserManager.IsInRoleAsync(userAdmin, "Admin")))
                        {
                            await UserManager.AddToRoleAsync(userAdmin, "Admin");
                        }

                        if (!(await UserManager.IsInRoleAsync(userAdmin, "BasicPlan")))
                        {
                            await UserManager.AddToRoleAsync(userAdmin, "BasicPlan");
                        }
                    }

                    AppUser userBasicPlan = await UserManager.FindByEmailAsync("maza@maza.com");

                    if (userBasicPlan != null && !(await UserManager.IsInRoleAsync(userBasicPlan, "BasicPlan")))
                    {
                        await UserManager.AddToRoleAsync(userBasicPlan, "BasicPlan");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
