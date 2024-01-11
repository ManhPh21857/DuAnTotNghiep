using Quartz;

namespace Project.Infrastructure.WebAPI
{
    public class DemoJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("hello");
            System.Diagnostics.Debug.WriteLine("hello");
            return Task.CompletedTask;
        }
    }
}
