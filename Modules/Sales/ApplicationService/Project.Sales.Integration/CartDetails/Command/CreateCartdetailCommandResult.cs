namespace Project.Sales.Integration.CartDetails.Command
{
    public class CreateCartdetailCommandResult
    {
        public bool IsSuccess { get; set; }
        public CreateCartdetailCommandResult(bool issuccess)
        {
            IsSuccess = issuccess;
        }
    }
}
