using Project.Core.ApplicationService.Commands;


namespace Project.Product.Integration.Classifications
{
    public class AddClassificationQuery : ICommand<AddClassificationQueryResult>
    {

        public string Name { get; set; }

        public AddClassificationQuery
            (
            string name
            )
        {

            this.Name = name;
        }
    }
}
