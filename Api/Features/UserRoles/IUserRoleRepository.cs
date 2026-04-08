using Api.Core.Repositories;

namespace Api.Features.UserRoles;

public interface IUserRoleRepository : IRepository<UserRole, Guid>
{

}
