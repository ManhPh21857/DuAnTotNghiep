

namespace Project.Product.Domain.CartDetails
{
    public class GetCartdetailInfo
    {
        public int Cart_id { get; set; }
        public int Product_detail_id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public byte[]? Data_version { get; set; }
    }
}
