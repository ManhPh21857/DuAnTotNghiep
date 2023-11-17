using Project.Core.ApplicationService.Commands;
using Project.Sales.Domain.CartDetails;
using Project.Sales.Integration.CartDetails.Command;

namespace Project.Sales.ApplicationService.CartDetails.Command
{
    public class CreateCartdetailCommandHandler : CommandHandler<CreateCartdetailCommand, CreateCartdetailCommandResult>
    {
        private readonly ICartdetailRepository cartdetailRepository;

        public CreateCartdetailCommandHandler(ICartdetailRepository cartdetail)
        {
            this.cartdetailRepository = cartdetail;
        }
        

        public async override Task<CreateCartdetailCommandResult> Handle(CreateCartdetailCommand request, CancellationToken cancellationToken)
        {
            var create = new CartDetailInfo
            {
                CartId = 1,
                ProductDetailId = request.ProductDetailId,
                Quantity = request.Quantity
            };
            var checkcartid = await this.cartdetailRepository.CheckCartId(create.CartId);
            if(checkcartid == null)
            {
                //Tạo mới id cart
                await this.cartdetailRepository.CreateCartId();
                //Thêm mới cart detail
                await this.cartdetailRepository.CreateCartdetai(create);
            }
            else
            {
                var check = await this.cartdetailRepository.CheckProductDetailId(create.CartId, create.ProductDetailId);
                if (check != null)
                {
                    await this.cartdetailRepository.UpdateQuantityCartdetail(create.CartId, create.ProductDetailId);
                }
                else
                {
                    await this.cartdetailRepository.CreateCartdetai(create);
                }
            }
            
                
            return new CreateCartdetailCommandResult(true);
        }
    }
}
