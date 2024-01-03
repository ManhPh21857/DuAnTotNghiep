using Microsoft.Extensions.Configuration;
using Project.Core.ApplicationService.Commands;
using Project.Core.Domain;
using Project.HumanResources.Domain.Customers;
using Project.HumanResources.Integration.Customers.Command;
using SixLabors.ImageSharp;

namespace Project.HumanResources.ApplicationService.Customers.Command
{
    public class UpdateCustomerCommandHandler : CommandHandler<UpdateCustomerCommand, UpdateCustomerCommandResult>
    {
        private readonly ICustomerRepository customerRepository;
        private readonly ISessionInfo sessionInfo;
        private readonly IConfiguration configuration;

        public UpdateCustomerCommandHandler(
            ICustomerRepository customerRepository,
            ISessionInfo sessionInfo,
            IConfiguration configuration
        )
        {
            this.customerRepository = customerRepository;
            this.sessionInfo = sessionInfo;
            this.configuration = configuration;
        }

        public async override Task<UpdateCustomerCommandResult> Handle(
            UpdateCustomerCommand request,
            CancellationToken cancellationToken
        )
        {
            string userImage;

            if (this.IsBase64String(request.Image))
            {
                userImage = $"{request.Username}_avatar.png";
                var imageBytes = Convert.FromBase64String(request.Image);
                using var image = Image.Load(imageBytes);
                await image.SaveAsync(@$"{this.configuration["ImagePath"]}{userImage}", CancellationToken.None);
            }
            else
            {
                userImage = request.Image;
            }

            var param = new UpdateCustomerParam
            {
                Id = request.Id,
                LastName = request.LastName,
                FirstName = request.FirstName,
                PhoneNumber = request.PhoneNumber,
                Birthday = request.Birthday,
                Image = userImage,
                Sex = request.Sex,
                DataVersion = request.DataVersion
            };

            await this.customerRepository.UpdateCustomer(param);

            return new UpdateCustomerCommandResult(true);
        }

        public bool IsBase64String(string base64)
        {
            var buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out int _);
        }
    }
}
