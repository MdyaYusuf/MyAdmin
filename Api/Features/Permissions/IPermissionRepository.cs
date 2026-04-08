using Api.Core.Repositories;

namespace Api.Features.Permissions;

public interface IPermissionRepository : IRepository<Permission, Guid>
{

}
