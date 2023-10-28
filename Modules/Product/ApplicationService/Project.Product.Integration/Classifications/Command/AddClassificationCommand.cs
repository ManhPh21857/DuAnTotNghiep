using Project.Core.ApplicationService.Commands;


namespace Project.Product.Integration.Classifications.Command
{
    public class AddClassificationCommand : ICommand<AddClassificationCommandResult>
    {

        public string Name { get; set; }

        public AddClassificationCommand
            (
            string name
            )
        {

            Name = name;
        }
    }
}
