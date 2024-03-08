﻿using AutoMapper;
using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Extensions;
using ChoreographyBuilder.Core.Services;
using ChoreographyBuilder.Infrastructure.Data;
using ChoreographyBuilder.Infrastructure.Data.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class ServiceCollectionExtension
	{
		public static IServiceCollection AddMappingServices (this IServiceCollection services) 
		{
			services.AddAutoMapper(typeof(MappingProfile));

			return services;
		}

		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped<IVerseTypeService, VerseTypeService>();

			services.AddScoped<IFigureService, FigureService>();

			return services;
		}

		public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration config)
		{
			var connectionString = config.GetConnectionString("DefaultConnection");
			services.AddDbContext<ChoreographyBuilderDbContext>(options =>
				options.UseSqlServer(connectionString));

			services.AddScoped<IRepository, Repository>();

			services.AddDatabaseDeveloperPageExceptionFilter();

			return services;
		}

		public static IServiceCollection AddApplicationIdentity(this IServiceCollection services, IConfiguration config)
		{
			services
				.AddDefaultIdentity<IdentityUser>(options =>
				{
					options.SignIn.RequireConfirmedAccount = false;
					options.Password.RequireNonAlphanumeric = false;
					options.Password.RequireUppercase = false;
					options.Password.RequireLowercase = false;
					options.Password.RequireDigit = false;
				})
				.AddEntityFrameworkStores<ChoreographyBuilderDbContext>();

			return services;
		}
	}
}
