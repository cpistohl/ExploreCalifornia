﻿using ExploreCalifornia.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ExploreCalifornia
{
	public class Startup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddTransient<SpecialsDataContext>();
			services.AddTransient<FormattingService>();
			services.AddMvc();
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
				app.UseExceptionHandler("/error.htm");
			}

			app.Use(async (context, next) =>
			{
				if (context.Request.Path.Value.Contains("invalid"))
				{
					throw new Exception("Error!");
				}

				await next();
			});

			app.UseMvc(route =>
			{
				route.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
			});

			app.UseStaticFiles();
		}
	}
}
