using Dapper;
using Project.Core.Infrastructure.SQLDB.Providers;
using Project.HumanResources.Domain.Roles;
using GroupInfo = Project.HumanResources.Domain.Roles.GroupInfo;

namespace Project.HumanResources.Infrastructure.SQLDB.Roles
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ConnectionProvider provider;

        public RoleRepository(ConnectionProvider provider)
        {
            this.provider = provider;
        }

        public async Task<IEnumerable<int>> GetGroupRole(int groupId)
        {
            await using var connect = await this.provider.Connect();

            const string query = @"
                SELECT
	                [role_id]
                FROM
	                [dbo].[group_roles]
                WHERE
	                [group_id] = @GroupId
            ";

            var result = await connect.QueryAsync<int>(query,
                new
                {
                    GroupId = groupId
                }
            );

            return result;
        }

        public async Task<IEnumerable<GroupInfo>> GetGroups()
        {
            await using var connect = await this.provider.Connect();

            const string query = @"
                SELECT
	                [Id]		   AS Id
                   ,[Name]		   AS Name
                   ,[description]  AS Description
                   ,[data_version] AS DataVersion
                FROM
	                [dbo].[groups]
            ";

            var result = await connect.QueryAsync<GroupInfo>(query);

            return result;
        }

        public async Task<IEnumerable<RoleInfo>> GetRoles()
        {
            await using var connect = await this.provider.Connect();

            const string query = @"
                SELECT
	                [Id]			AS Id
                   ,[role_name]	    AS Name
                   ,[data_version]  AS DataVersion
                FROM
	                [dbo].[roles]
            ";

            var result = await connect.QueryAsync<RoleInfo>(query);

            return result;
        }

        public async Task<IEnumerable<GroupRoleInfo>> GetGroupRoles()
        {
            await using var connect = await this.provider.Connect();

            const string query = @"
                SELECT
	                [group_id] AS GroupId
                   ,[role_id]  AS RoleId
                FROM
	                [dbo].[group_roles]
            ";

            var result = await connect.QueryAsync<GroupRoleInfo>(query);

            return result;
        }
    }
}
