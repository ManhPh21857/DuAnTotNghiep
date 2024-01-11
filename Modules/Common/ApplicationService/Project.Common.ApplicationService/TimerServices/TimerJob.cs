using MediatR;
using Project.Common.Domain.Garbage;
using Project.Sales.Integration.Orders.Command;
using Quartz;

namespace Project.Common.ApplicationService.TimerServices
{
    public class TimerJob : IJob
    {
        private readonly IGarbageRepository garbageRepository;
        private readonly ISender mediator;

        public TimerJob(ISender mediator, IGarbageRepository garbageRepository)
        {
            this.mediator = mediator;
            this.garbageRepository = garbageRepository;
        }
        
        public async Task Execute(IJobExecutionContext context)
        {
            var orderIds = await this.garbageRepository.GetOrderNotYet();

            foreach (int id in orderIds)
            {
                var command = new CancelOrderCommand(id, true);

                await this.mediator.Send(command);
            }
        }
    }
}
