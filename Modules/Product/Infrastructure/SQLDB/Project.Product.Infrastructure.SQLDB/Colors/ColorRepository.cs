using Dapper;
using Project.Core.Infrastructure.SQLDB.Providers;
using Project.Product.Domain.Colors;

namespace Project.Product.Infrastructure.SQLDB.Colors
{
    public class ColorRepository : IColorRepository
    {
        private readonly ConnectionProvider provider;

        public ColorRepository(ConnectionProvider provider)
        {
            this.provider = provider;
        }


        public async Task<IEnumerable<ColorInfo>> GetColors()
        {
            var connect = await provider.Connect();

            const string query = @"
                SELECT
	                id
                   ,color
                FROM
	                [colors]
            ";

            var result = await connect.QueryAsync<ColorInfo>(query);

            return result;
        }
    }
}
