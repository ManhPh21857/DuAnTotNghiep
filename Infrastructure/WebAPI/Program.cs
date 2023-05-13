using Microsoft.AspNetCore.Mvc;
using Project.Common.Infrastructure.WebAPI;
using Project.Core.Infrastructure.WebAPI;
using Project.Core.Infrastructure.WebAPI.Models;

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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCoreWebAPI();
builder.Services.AddCommonWebAPI();

builder.Services.RegisterMapsterConfiguration();

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
        builder =>
        {
            builder
               .SetIsOriginAllowed((host) => true)
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

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

if(app.Environment.IsDevelopment()) {
    app.UseCors("AllowAll");
}

app.Run();