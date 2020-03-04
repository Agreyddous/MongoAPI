using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoAPI.Domain.Handlers;
using MongoAPI.Infra.Repositories;
using ORQ.Infra.CommonContext.DataContexts;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace MongoAPI
{
	public class Startup
	{
		public static IConfiguration Configuration { get; set; }

		public void ConfigureServices(IServiceCollection services)
		{
			Configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", false, false).Build();

			services.AddControllers().AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.IgnoreNullValues = true;
			});

			services.AddResponseCompression();

			Settings.ConnectionString = Configuration["connectionString"];
			Settings.DatabaseName = Configuration["databaseName"];

			services.AddScoped<MongoDataContext, MongoDataContext>();

			services.AddTransient<UserCommandHandler, UserCommandHandler>();

			services.AddTransient<UserRepository, UserRepository>();

			services.AddDistributedMemoryCache();

			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "MongoDB API",
					Description = "An API for testing and improving knowledge on MongoDB",
					Contact = new OpenApiContact
					{
						Name = "Fernando Velloso Borges de Mélo Gomes",
						Email = "fernandovbmgomes@hotmail.com"
					}
				});

				options.DescribeAllParametersInCamelCase();

				options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));

				options.OrderActionsBy(description => description.RelativePath);
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();

			app.UseRouting();
			app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

			app.UseResponseCompression();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			app.UseSwagger();
			app.UseSwaggerUI(options =>
			{
				options.DocumentTitle = "MongoDB APIs";

				options.SwaggerEndpoint("swagger/v1/swagger.json", "ORQ - V1");
				options.RoutePrefix = string.Empty;

				options.DisplayRequestDuration();
				options.EnableFilter();

				options.DefaultModelsExpandDepth(-1);
				options.DocExpansion(DocExpansion.List);
			});
		}
	}
}
