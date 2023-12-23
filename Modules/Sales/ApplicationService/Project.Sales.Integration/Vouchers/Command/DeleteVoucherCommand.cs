using Project.Core.ApplicationService.Commands;

namespace Project.Sales.Integration.Vouchers.Command
{
    public class DeleteVoucherCommand : ICommand<DeleteVoucherCommandResult>
    {
        public int Id { get; set; }
        public byte[]? DataVersion { get; set; }
        public bool IsDelete { get; set; }

        public DeleteVoucherCommand(int id, byte[]? dataVersion, bool isDelete)
        {
            this.Id = id;
            this.DataVersion = dataVersion;
            this.IsDelete = isDelete;
        }
    }
}
