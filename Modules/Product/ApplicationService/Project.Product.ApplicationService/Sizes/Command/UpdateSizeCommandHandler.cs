using Microsoft.IdentityModel.Tokens;
using Project.Core.ApplicationService.Commands;
using Project.Core.Domain;
using Project.Product.Domain.Sizes;
using Project.Product.Integration.Sizes.Command;

namespace Project.Product.ApplicationService.Sizes.Command
{
    public class UpdateSizeCommandHandler : CommandHandler<UpdateSizeCommand, UpdateSizeCommandResult>
    {
        private readonly ISizeRepository sizeRepository;

        public UpdateSizeCommandHandler(ISizeRepository sizeRepository)
        {
            this.sizeRepository = sizeRepository;
        }

        public async override Task<UpdateSizeCommandResult> Handle(UpdateSizeCommand request, CancellationToken cancellationToken)
        {
            foreach (var item in request.Sizes)
            {
                if (item.DataVersion.IsNullOrEmpty())
                {
                    var color = await this.sizeRepository.GetSizeByName(item.Size ?? "");

                    if (color is not null)
                    {
                        throw new DomainException("", "Size đã tồn tại");
                    }

                    await this.sizeRepository.CreateSize(item);
                }
                else
                {
                    await this.sizeRepository.UpdateSize(item);
                }
            }

            return new UpdateSizeCommandResult(true);
        }
    }
}
