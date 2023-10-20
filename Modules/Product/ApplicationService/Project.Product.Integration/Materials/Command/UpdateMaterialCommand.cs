using Project.Core.ApplicationService.Commands;

namespace Project.Product.Integration.Materials.Command
{
    public class UpdateMaterialCommand : ICommand<UpdateMaterialCommandResult>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UpdateMaterialCommand(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
