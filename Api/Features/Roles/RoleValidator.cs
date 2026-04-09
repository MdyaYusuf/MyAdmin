using FluentValidation;

namespace Api.Features.Roles;

public class CreateRoleRequestValidator : AbstractValidator<CreateRoleRequest>
{
  public CreateRoleRequestValidator()
  {
    RuleFor(x => x.Name)
      .NotEmpty().WithMessage("Rol adı boş olamaz.")
      .MinimumLength(2).WithMessage("Rol adı en az 2 karakter olmalıdır.")
      .MaximumLength(50).WithMessage("Rol adı en fazla 50 karakter olabilir.");

    RuleFor(x => x.Label)
      .MaximumLength(100).WithMessage("Rol etiketi en fazla 100 karakter olabilir.");

    RuleFor(x => x.Description)
      .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");
  }
}

public class UpdateRoleRequestValidator : AbstractValidator<UpdateRoleRequest>
{
  public UpdateRoleRequestValidator()
  {
    RuleFor(x => x.Id)
      .NotEmpty().WithMessage("Güncellenecek rolün ID bilgisi eksik.");

    RuleFor(x => x.Name)
      .NotEmpty().WithMessage("Rol adı boş olamaz.")
      .MinimumLength(2).WithMessage("Rol adı en az 2 karakter olmalıdır.")
      .MaximumLength(50).WithMessage("Rol adı en fazla 50 karakter olabilir.");

    RuleFor(x => x.Label)
      .MaximumLength(100).WithMessage("Rol etiketi en fazla 100 karakter olabilir.");

    RuleFor(x => x.Description)
      .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");
  }
}
