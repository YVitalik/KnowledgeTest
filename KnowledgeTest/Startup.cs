using Administration;
using Administration.Interfaces;
using Administration.Services;
using AutoMapper;
using BLL.Helpers.JwtHelper;
using BLL.Interfaces;
using BLL.Services;
using DAL;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace KnowledgeTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITestDetailRepository, TestDetailRepository>();
            services.AddScoped<ITestQuestionRepository, TestQuestionRepository>();
            services.AddScoped<ITestRepository, TestRepository>();
            services.AddScoped<IUserTestRepository, UserTestRepository>();
            services.AddScoped<ITestService, TestService>();

            services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<AdministrationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AdministrationDB")));
            services.AddDbContext<DomainDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DomainDB")));

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 5;
            }).AddEntityFrameworkStores<AdministrationDbContext>();

            services.Configure<JwtSettings>(Configuration.GetSection("Jwt"));

            var jwtSettings = Configuration.GetSection("Jwt").Get<JwtSettings>();
            services
                .AddAuthorization()
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                        ClockSkew = TimeSpan.Zero
                    };
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}