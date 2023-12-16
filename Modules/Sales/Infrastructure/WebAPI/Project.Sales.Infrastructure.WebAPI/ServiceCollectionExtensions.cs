﻿using FluentValidation;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Project.Sales.ApplicationService;
using Project.Sales.Domain;
using Project.Sales.Infrastructure.SQLDB;
using Project.Sales.Infrastructure.WebAPI.Controllers.v1.CartDetails.Post;
using Project.Sales.Infrastructure.WebAPI.Controllers.v1.Carts.Post;
using Project.Sales.Infrastructure.WebAPI.Controllers.v1.Carts.Put;
using Project.Sales.Infrastructure.WebAPI.Controllers.v1.Orders.Post;
using Project.Sales.Infrastructure.WebAPI.Controllers.v1.Payments.Post;
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
        //Cartdetail
        services.AddScoped<IValidator<CreateCartdetailModel>, CreateCartdetailModelValidator>();

        //cart
        services.AddScoped<IValidator<CartAdditionRequestModel>, CartAdditionRequestModelValidator>();
        services.AddScoped<IValidator<DeleteCartDetailModel>, DeleteCartDetailModelValidator>();
        services.AddScoped<IValidator<DeleteCartDetailRequestModel>, DeleteCartDetailRequestModelValidator>();
        services.AddScoped<IValidator<UpdateCartDetailRequestModel>, UpdateCartDetailRequestModelValidator>();

        services.AddScoped<IValidator<OrderModel>, OrderModelValidator>();
        services.AddScoped<IValidator<CartDetail>, CartDetailValidator>();
        services.AddScoped<IValidator<CreateOrderRequestModel>, CreateOrderRequestModelValidator>();
        services.AddScoped<IValidator<FinishOrderRequestModel>, FinishOrderRequestModelValidator>();
    }
}