using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Project.Core.Infrastructure.SQLDB.Providers;
using Project.Product.Domain.Images;

namespace Project.Product.Infrastructure.SQLDB.Images
{
    internal class ImageRepository : IImageRepository
    {
        private readonly ConnectionProvider provider;

        public ImageRepository(ConnectionProvider provider)
        {
            this.provider = provider;
        }
        public async Task<int> CreateImage(int productId, string url)
        {
            await using var connect = await provider.Connect();
            const string command = @"
                INSERT [dbo].[images] (
                    [product_id]
	               ,[url]
                )
                VALUES (
                    @ProductId
	               ,@Url
                )
                SELECT @@IDENTITY AS Id
            ";

            int result = await connect.QueryFirstOrDefaultAsync<int>(command,
                new
                {
                    ProductId = productId,
                    Url = url
                }
            );

            return result;
        }
    }
}
