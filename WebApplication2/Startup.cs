using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using FirstProject.Data;
using Microsoft.OpenApi.Models;
using FirstProject.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using FirstProject.Api.Middleware;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Authorization;
using Newtonsoft.Json.Converters;
using Microsoft.AspNetCore.Http.Features;
using AutoMapper;

namespace FirstProject

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
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
			services.AddSingleton(Configuration);

			services.AddDbContext<MyDBContext>(options =>
					options.UseSqlServer(Configuration.GetConnectionString("MyDBContext")));

			services.AddAutoMapper(typeof(Startup));


			services.AddIdentity<Domain.ApplicationUser, IdentityRole>()
			.AddEntityFrameworkStores<MyDBContext>()
			.AddDefaultTokenProviders();

			// Configure identity options
			services.Configure<IdentityOptions>(config =>
			{
				// user
				var user = config.User;
				user.RequireUniqueEmail = true;
				// password
				var password = config.Password;
				password.RequiredLength = 5;
				password.RequireDigit = false;
				password.RequireUppercase = false;
				password.RequireLowercase = false;
				password.RequireNonAlphanumeric = false;
			});

			services.AddSession(options => {
				options.Cookie.HttpOnly = true;
				// Make the session cookie essential
				options.Cookie.IsEssential = true;
			});

		  services.AddMvc();

			// Add swagger gen
			services.AddSwaggerGen(c =>
			{
				c.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["controller"]}_{ e.ActionDescriptor.RouteValues["action"] }");
				c.MapType<System.DateTime>(() => new OpenApiSchema()
				{
					Type = "string",
					Format = "YYYY-MM-dd HH:mm:ss"
				});
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger API", Version = "v1" });
				// Bearer token authentication
				OpenApiSecurityScheme securityDefinition = new OpenApiSecurityScheme()
				{
					Name = "Bearer",
					BearerFormat = "JWT",
					Scheme = "bearer",
					Description = "Specify the authorization token.",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.Http,
				};
				c.AddSecurityDefinition("jwt_auth", securityDefinition);



				// Make sure swagger UI requires a Bearer token specified
				OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme()
				{
					Reference = new OpenApiReference()
					{
						Id = "jwt_auth",
						Type = ReferenceType.SecurityScheme
					}
				};
				OpenApiSecurityRequirement securityRequirements = new OpenApiSecurityRequirement()
				{
					{securityScheme, new string[] { }},
				};
				c.AddSecurityRequirement(securityRequirements);
			});
			// Enable swagger enum to string conversions
			services.AddSwaggerGenNewtonsoftSupport();
			services.Configure<FormOptions>(options =>
			{
				options.MultipartBodyLengthLimit = 60000000;
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
				app.UseHsts();
			}
			app.UseSession();
			app.UseAuthentication();
			app.UseCors(b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowAnyMethod().Build());
			app.UseHttpsRedirection();
			app.UseMvc();
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "TransportWise APIs V1");
			});

			using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
			using (var context = scope.ServiceProvider.GetService<MyDBContext>())
			{
				context.Database.Migrate();
			}
		}
	}
}
