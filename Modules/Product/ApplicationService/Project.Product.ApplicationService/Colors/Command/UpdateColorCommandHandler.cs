using Microsoft.IdentityModel.Tokens;
using Project.Core.ApplicationService.Commands;
using Project.Core.Domain;
using Project.Product.Domain.Colors;
using Project.Product.Integration.Colors.Command;

namespace Project.Product.ApplicationService.Colors.Command
{
    public class UpdateColorCommandHandler : CommandHandler<UpdateColorCommand, UpdateColorCommandResult>
    {
        private readonly IColorRepository colorRepository;

        public UpdateColorCommandHandler(IColorRepository colorRepository)
        {
            this.colorRepository = colorRepository;
        }

        public async override Task<UpdateColorCommandResult> Handle(
            UpdateColorCommand request,
            CancellationToken cancellationToken
        )
        {
            foreach (var item in request.Colors)
            {
                if (item.DataVersion.IsNullOrEmpty())
                {
                    var color = await this.colorRepository.GetColorByName(item.Color ?? "");

                    if (color is not null)
                    {
                        throw new DomainException("", "Màu sắc đã tồn tại");
                    }

                    await this.colorRepository.CreateColor(item);
                }
                else
                {
                    await this.colorRepository.UpdateColor(item);
                }
            }

            return new UpdateColorCommandResult(true);
        }
    }
}
