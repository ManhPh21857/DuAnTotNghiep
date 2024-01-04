using Dapper;
using Project.Core.Infrastructure.SQLDB.Extensions;
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

        public async Task<(IEnumerable<GroupInfo>, int)> GetGroups(int skip, int take)
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
                ORDER BY
	                Id
                OFFSET @Skip ROWS
                FETCH NEXT @Take ROWS ONLY;

                SELECT
	                COUNT(id)
                FROM
	                [dbo].[groups]
            ";

            var result = await connect.QueryMultipleAsync(query,
                new
                {
                    Skip = skip,
                    Take = take
                }
            );

            var group = result.Read<GroupInfo>();
            var total = result.ReadFirstOrDefault<int>();

            return (group, total);
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

        public async Task<int> CreateGroup(string name, string description)
        {
            await using var connect = await this.provider.Connect();

            const string command = @"
                INSERT INTO [dbo].[groups] (
	                [name]
                   ,[description]
                )
                OUTPUT Inserted.Id
                VALUES (
	                @Name
                   ,@Description
                )
            ";

            var result = await connect.QueryFirstOrDefaultAsync<int>(command,
                new
                {
                    Name = name,
                    Description = description
                }
            );

            return result;
        }

        public async Task<int> DeleteGroup(int id, byte[]? dataVersion)
        {
            await using var connect = await this.provider.Connect();

            const string command = @"
                DELETE FROM [dbo].[groups]
                WHERE
	                id = @Id
	                AND data_version = @DataVersion
            ";

            var result = await connect.QueryFirstOrDefaultAsync<int>(command,
                new
                {
                    Id = id,
                    DataVersion = dataVersion
                }
            );

            return result;
        }

        public async Task UpdateGroup(int id, string name, string description, byte[]? dataVersion)
        {
            await using var connect = await this.provider.Connect();

            const string command = @"
                UPDATE [dbo].[groups]
                SET
	                [name]		  = @Name
                   ,[description] = @Description

                WHERE
	                id = @Id
	                AND data_version = @DataVersion
            ";

            var result = await connect.ExecuteAsync(command,
                new
                {
                    Id = id,
                    Name = name,
                    Description = description,
                    DataVersion = dataVersion
                }
            );

            result.IsOptimisticLocked();
        }

        public async Task CreateGroupRole(int groupId, int roleId)
        {
            await using var connect = await this.provider.Connect();

            const string command = @"
                INSERT INTO [dbo].[group_roles] (
	                [group_id]
                   ,[role_id]
                )
                VALUES (
	                @GroupId
                   ,@RoleId
                )
            ";

            await connect.ExecuteAsync(command,
                new
                {
                    GroupId = groupId,
                    RoleId = roleId
                }
            );
        }

        public async Task DeleteGroupRole(int groupId)
        {
            await using var connect = await this.provider.Connect();

            const string command = @"
                DELETE FROM [dbo].[group_roles]
                WHERE
	                group_id = @GroupId
            ";

            await connect.ExecuteAsync(command,
                new
                {
                    GroupId = groupId
                }
            );
        }

        public async Task<(GroupInfo, IEnumerable<RoleInfo>)> GetGroupRoles(int groupId)
        {
            await using var connect = await this.provider.Connect();

            const string query = @"
                SELECT
	                [Id]		   AS Id
                   ,[Name]		   AS Name
                   ,[Description]  AS Description
                   ,[data_version] AS DataVersion
                FROM
	                [dbo].[groups]
                WHERE
	                Id = @GroupId
                ;
                SELECT
	                gr.role_id  AS Id
                   ,r.role_name AS Name
                FROM
	                group_roles AS gr
	                LEFT JOIN roles AS r
		                ON gr.role_id = r.Id
                WHERE
	                group_id = @GroupId
            "
            ;

            var result = await connect.QueryMultipleAsync(query, new { GroupId = groupId });
            var group = result.ReadFirstOrDefault<GroupInfo>();
            var roles = result.Read<RoleInfo>();

            return (group, roles);
        }
    }
}
