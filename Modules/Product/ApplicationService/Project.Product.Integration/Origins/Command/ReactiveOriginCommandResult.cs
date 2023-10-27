

namespace Project.Product.Integration.Origins.Command
{
    public class ReactiveOriginCommandResult
    {
        public bool Result { get; set; }
        public ReactiveOriginCommandResult(bool result)
        {
            Result = result;
        }
    }
}
