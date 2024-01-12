namespace Project.Product.Integration.Products.Command
{
    public class CreateProductCommandResult
    {
        public int Id { get; set; }

        public CreateProductCommandResult(int id)
        {
            this.Id = id;
        }
    }
}
