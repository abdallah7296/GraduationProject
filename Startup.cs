using AutoMapper;
using GraduationProject.Helpers;
using GraduationProject.Models;
using GraduationProject.Repository.AnalysisCentersRepository;
using GraduationProject.Repository.AuthService;
 using GraduationProject.Repository.PharmacieRepository;
using GraduationProject.Repository.SuperMarketRepository;
using GraduationProject.Services;
using GraduationProject.Services.AnalysisCentersServices;
using GraduationProject.Services.DoctorsServices;
 using GraduationProject.Services.IAuthServices;
using GraduationProject.Services.PharmaciesServices;
using GraduationProject.Services.PlayStationServices;
using GraduationProject.Services.RestaurantServices;
using GraduationProject.Services.SuperMarketServices;
//using GraduationProject.Services.RestaurantServices;
using GraduationProject.Services.WorkSpaceServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GraduationProject
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
            services.Configure<JWT>(Configuration.GetSection("JWT"));
            services.AddAutoMapper(typeof(Program));
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());s

          
            services.AddDbContext<Context>(
             option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));



            services.AddCors();
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<Context>();
           // services.AddIdentity<User, IdentityRole>(
           // options => {
           //     options.Tokens.ProviderMap.Add("Default", new
           //           TokenProviderDescriptor(typeof(IUserTwoFactorTokenProvider<User>)));
           // }
          
           //).AddEntityFrameworkStores<Context>().AddDefaultTokenProviders();
            services.AddControllers();
            services.AddIdentityCore<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<Context>()
                .AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultProvider);
            //Authorize  used jwt token on authorization
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudiance"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                    (Configuration["JWT:Secret"])),
                    ClockSkew = TimeSpan.Zero
                };
            });
            /*
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader(); 
                    });
            }); 
            */
            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromDays(1);
            });
            services.AddTransient<IRestaurantService, RestaurantRepository>() ;
            services.AddTransient<IDoctorService, DoctorRepository>();
            services.AddTransient<IPlayStationRepository, PlayStationRepository>();
            services.AddTransient<IWorkspaceRepository, WorkspaceRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ISuperMarketServices, SuperMarketRepository>();
            services.AddScoped<IPharmaciesServices, PharmacieRepository>();
            services.AddScoped<IAnalysisCentersServices, AnalysisCentersRepository>();

            // services.AddAutoMapper(typeof(Program));
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
            });
        

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter JWT with Bearer into the field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
 {
  {
   new OpenApiSecurityScheme
   {
    Reference = new OpenApiReference
    {
     Type = ReferenceType.SecurityScheme,
     Id = "Bearer"
    }
   },
   Array.Empty<string>()
  }
 });
            });
            ///////////////////
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           // if (env.IsDevelopment())
            //{
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GraduationProject v1"));
            //}
            
             app.UseHttpsRedirection();
          
            //  app.UseCors("MyPolicy");
            app.UseRouting();
            app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseAuthentication();//check jwt token
            app.UseAuthorization();
            app.UseStaticFiles();
          //  app.UseCors("AllowAll");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
          
        }
    }
}
