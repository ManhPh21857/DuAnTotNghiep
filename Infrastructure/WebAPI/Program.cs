using Microsoft.AspNetCore.Mvc;
using Project.Common.Infrastructure.WebAPI;
using Project.Core.Infrastructure.WebAPI;
using Project.Core.Infrastructure.WebAPI.Middlewares;
using Project.Core.Infrastructure.WebAPI.Models;
using Project.HumanResources.Infrastructure.WebAPI;
using Project.Product.Infrastructure.WebAPI;
using Project.Sales.Infrastructure.WebAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
       .ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory =
                context =>
                {
                    var problems = new CustomBadRequestModel(context);
                    return new BadRequestObjectResult(problems);
                };
        });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddMemoryCache();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCoreWebAPI();
builder.Services.AddCommonWebAPI();
builder.Services.AddHumanResourcesWebAPI();
builder.Services.AddSalesWebAPI();
builder.Services.AddProductWebAPI();


builder.Services.RegisterCommonMapsterConfiguration();
builder.Services.RegisterHumanResourcesMapsterConfiguration();
builder.Services.RegisterSalesMapsterConfiguration();
builder.Services.RegisterProductMapsterConfiguration();

builder.Services.AddApiVersioning(x =>
{
    x.DefaultApiVersion = new ApiVersion(1, 0);
    x.AssumeDefaultVersionWhenUnspecified = true;
    x.ReportApiVersions = true;
});

builder.Services.AddCors(options =>
{
    var webClientUrl = builder.Configuration["WebClient:Url"];
    options.AddPolicy("AllowAll",
        b =>
        {
            b
               .SetIsOriginAllowed(_ => true)
               .WithOrigins(webClientUrl)
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleWare>();

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<SessionInfoMiddleware>();

app.MapControllers();

if(app.Environment.IsDevelopment()) {
    app.UseCors("AllowAll");
}

app.UseMiddleware<NotFoundHandlingMiddleware>();

app.Run();