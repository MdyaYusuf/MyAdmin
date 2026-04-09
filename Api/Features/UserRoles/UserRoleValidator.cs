using FluentValidation;

namespace Api.Features.UserRoles;

public class CreateUserRoleRequestValidator : AbstractValidator<CreateUserRoleRequest>
{
  public CreateUserRoleRequestValidator()
  {
    RuleFor(x => x.UserId)
      .NotEmpty().WithMessage("Bir kullanıcı seçmelisiniz.");

    RuleFor(x => x.RoleId)
      .NotEmpty().WithMessage("Bir rol seçmelisiniz.");
  }
}

public class UpdateUserRoleRequestValidator : AbstractValidator<UpdateUserRoleRequest>
{
  public UpdateUserRoleRequestValidator()
  {
    RuleFor(x => x.Id)
      .NotEmpty().WithMessage("Güncellenecek ilişki kaydına ait ID boş olamaz.");

    RuleFor(x => x.UserId)
      .NotEmpty().WithMessage("Bir kullanıcı seçmelisiniz.");

    RuleFor(x => x.RoleId)
      .NotEmpty().WithMessage("Bir rol seçmelisiniz.");
  }
}
