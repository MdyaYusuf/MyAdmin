using FluentValidation;

namespace Api.Features.Activities;

public class CreateActivityRequestValidator : AbstractValidator<CreateActivityRequest>
{
  public CreateActivityRequestValidator()
  {
    RuleFor(x => x.Action)
      .NotEmpty().WithMessage("Aksiyon bilgisi (Created, Updated vb.) boş olamaz.")
      .MaximumLength(100).WithMessage("Aksiyon adı en fazla 100 karakter olabilir.");

    RuleFor(x => x.EntityName)
      .NotEmpty().WithMessage("Varlık adı (User, Role vb.) boş olamaz.")
      .MaximumLength(100).WithMessage("Varlık adı en fazla 100 karakter olabilir.");

    RuleFor(x => x.IPAddress)
      .MaximumLength(50).WithMessage("IP adresi formatı hatalı veya çok uzun.");
  }
}