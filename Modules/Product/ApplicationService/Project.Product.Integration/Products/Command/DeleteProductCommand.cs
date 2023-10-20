using Project.Core.ApplicationService.Commands;

namespace Project.Product.Integration.Products.Command
{
    public class DeleteProductCommand : ICommand<DeleteProductCommandResult>
    {
        public int Id { get; set; }

        public DeleteProductCommand(int id)
        {
            Id = id;
        }
    }
}
