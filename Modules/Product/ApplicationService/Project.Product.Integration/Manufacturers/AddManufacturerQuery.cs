using Project.Core.ApplicationService.Commands;


namespace Project.Product.Integration.Manufacturers
{
    public class AddManufacturerQuery : ICommand<AddManufacturerQueryResult>
    {

        public string Name { get; set; }
        public int Status { get; set; }


        public AddManufacturerQuery(

            string name,
            int status
        )
        {

            this.Name = name;
            this.Status = status;
        }
    }
}
