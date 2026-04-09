using FluentValidation;

namespace Api.Features.Permissions;

public class CreatePermissionRequestValidator : AbstractValidator<CreatePermissionRequest>
{
  public CreatePermissionRequestValidator()
  {
    RuleFor(x => x.Name)
      .NotEmpty().WithMessage("İzin adı boş olamaz.")
      .MinimumLength(3).WithMessage("İzin adı en az 3 karakter olmalıdır.")
      .MaximumLength(100).WithMessage("İzin adı en fazla 100 karakter olabilir.");

    RuleFor(x => x.Description)
      .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");
  }
}

public class UpdatePermissionRequestValidator : AbstractValidator<UpdatePermissionRequest>
{
  public UpdatePermissionRequestValidator()
  {
    RuleFor(x => x.Id)
      .NotEmpty().WithMessage("Güncellenecek iznin ID bilgisi eksik.");

    RuleFor(x => x.Name)
      .NotEmpty().WithMessage("İzin adı boş olamaz.")
      .MinimumLength(3).WithMessage("İzin adı en az 3 karakter olmalıdır.")
      .MaximumLength(100).WithMessage("İzin adı en fazla 100 karakter olabilir.");

    RuleFor(x => x.Description)
      .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");
}
}
