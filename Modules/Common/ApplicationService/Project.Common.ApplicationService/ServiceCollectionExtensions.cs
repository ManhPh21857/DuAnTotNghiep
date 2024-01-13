using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Quartz;
using Quartz.AspNetCore;
using Project.Common.ApplicationService.TimerServices;

namespace Project.Common.ApplicationService;

public static class ServiceCollectionExtensions
{
    public static void AddCommonApplicationService(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddQuartz(q =>
            {
                // base Quartz scheduler, job and trigger configuration
                // Just use the name of your job that you created in the Jobs folder.
                var jobKey = new JobKey("TimerJob");
                q.AddJob<TimerJob>(opts => opts.WithIdentity(jobKey));

                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("TimerJob-trigger")
                    //This Cron interval can be described as "run every minute" (when second is zero)
                    .WithCronSchedule("0 0 3 * * ?").StartNow()
                );
            }
        );

        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        // ASP.NET Core hosting
        services.AddQuartzServer(options =>
        {
            // when shutting down we want jobs to complete gracefully
            options.WaitForJobsToComplete = true;
        });

    }
}