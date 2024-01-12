namespace Project.Product.Domain.Origins
{
    public class OriginInfo
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int? IsDeleted { get; set; }
        public byte[]? DataVersion { get; set; }
    }
}
