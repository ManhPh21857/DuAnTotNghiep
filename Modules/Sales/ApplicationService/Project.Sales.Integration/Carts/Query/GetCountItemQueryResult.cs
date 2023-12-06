namespace Project.Sales.Integration.Carts.Query
{
    public class GetCountItemQueryResult
    {
        public int Count { get; set; }

        public GetCountItemQueryResult(int count)
        {
            this.Count = count;
        }
    }
}
