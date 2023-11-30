using Microsoft.IdentityModel.Tokens;
using Project.Core.ApplicationService.Commands;
using Project.Sales.Domain.CartDetails;
using Project.Sales.Integration.CartDetails.Command;

namespace Project.Sales.ApplicationService.CartDetails.Command
{
    public class UpdateCartdetailCommandHandler : CommandHandler<UpdateCartdetailCommand, UpdateCartdetailCommandResult>
    {
        private readonly ICartdetailRepository cartdetailRepository;

        public UpdateCartdetailCommandHandler(ICartdetailRepository cartdetail)
        {
            this.cartdetailRepository = cartdetail;
        }
        public async override Task<UpdateCartdetailCommandResult> Handle(UpdateCartdetailCommand request, CancellationToken cancellationToken)
        {
            var delete = new CartDetailInfo
            {
                CartId = 1,
                ProductDetailId = request.ProductDetailId,
                DataVersion = request.DataVersion
            };
            
            var get = await this.cartdetailRepository.GetProductdetail(request.ProductId, request.ColorId, request.SizeId);
            if(get != null)
            {
                
                await this.cartdetailRepository.DeleteCartdetai(delete);

                var create = new CartDetailInfo
                {
                    CartId = 1,
                    ProductDetailId = get.Id,
                    Quantity = request.Quantity
                };

                await this.cartdetailRepository.CreateCartdetai(create);
            }
            else
            {
                throw new Exception();
            }
            
            return new UpdateCartdetailCommandResult(true);
        }
    }
}
