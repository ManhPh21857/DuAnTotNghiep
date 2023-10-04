﻿using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Project.Sales.ApplicationService;
using Project.Sales.Domain;
using Project.Sales.Infrastructure.SQLDB;
using System.Reflection;

namespace Project.Sales.Infrastructure.WebAPI;

public static class ServiceCollectionExtensions
{
    public static void AddSalesWebAPI(this IServiceCollection services)
    {
        services.AddSalesDomain();
        services.AddSalesApplicationService();
        services.AddSalesSQLDB();
        services.AddCustomIdentity();
        services.AddValidator();
    }

    public static void AddCustomIdentity(this IServiceCollection services)
    {
    }

    public static void RegisterSalesMapsterConfiguration(this IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
    }

    private static void AddValidator(this IServiceCollection services)
    {
    }
}