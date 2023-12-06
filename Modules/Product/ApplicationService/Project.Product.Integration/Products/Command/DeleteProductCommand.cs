using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Products;

namespace Project.Product.Integration.Products.Command
{
    public class DeleteProductCommand : ICommand<DeleteProductCommandResult>
    {
        public IEnumerable<DeleteProductParam> Products { get; set; }

        public DeleteProductCommand(IEnumerable<DeleteProductParam> products)
        {
            this.Products = products;
        }
    }
}
