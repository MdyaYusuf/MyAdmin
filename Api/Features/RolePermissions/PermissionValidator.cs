using FluentValidation;

namespace Api.Features.RolePermissions;

public class CreateRolePermissionRequestValidator : AbstractValidator<CreateRolePermissionRequest>
{
  public CreateRolePermissionRequestValidator()
  {
    RuleFor(x => x.RoleId)
      .NotEmpty().WithMessage("Bir rol seçmelisiniz.");

    RuleFor(x => x.PermissionId)
      .NotEmpty().WithMessage("Bir izin seçmelisiniz.");
  }
}

public class UpdateRolePermissionRequestValidator : AbstractValidator<UpdateRolePermissionRequest>
{
  public UpdateRolePermissionRequestValidator()
  {
    RuleFor(x => x.Id)
       .NotEmpty().WithMessage("Güncellenecek ilişki ID'si boş olamaz.");

    RuleFor(x => x.RoleId)
      .NotEmpty().WithMessage("Bir rol seçmelisiniz.");

    RuleFor(x => x.PermissionId)
      .NotEmpty().WithMessage("Bir izin seçmelisiniz.");
  }
}
