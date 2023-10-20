using Project.Core.ApplicationService.Commands;
namespace Project.Product.Integration.Materials.Command
{
    public class CreateMaterialCommand : ICommand<CreateMaterialCommandResult>
    {
        public string Name { get; set; }
        public CreateMaterialCommand(string name)
        {
            this.Name = name;
        }
    }

}
