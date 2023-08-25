﻿using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Project.Common.ApplicationService;

public static class ServiceCollectionExtensions {
    public static void AddCommonApplicationService(this IServiceCollection services) {
        services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}