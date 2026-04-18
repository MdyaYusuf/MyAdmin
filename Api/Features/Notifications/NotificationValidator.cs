using FluentValidation;

namespace Api.Features.Notifications;

public class CreateNotificationRequestValidator : AbstractValidator<CreateNotificationRequest>
{
  public CreateNotificationRequestValidator()
  {
    RuleFor(x => x.Title)
        .NotEmpty().WithMessage("Bildirim başlığı boş olamaz.")
        .MaximumLength(150).WithMessage("Başlık en fazla 150 karakter olabilir.");

    RuleFor(x => x.Message)
        .NotEmpty().WithMessage("Bildirim mesajı boş olamaz.")
        .MaximumLength(1000).WithMessage("Mesaj en fazla 1000 karakter olabilir.");

    RuleFor(x => x.UserId)
        .NotEmpty().WithMessage("Bildirimin gönderileceği kullanıcı ID'si eksik.");

    RuleFor(x => x.Type)
        .Must(type => new[] { "INFO", "WARNING", "ERROR", "SUCCESS" }.Contains(type))
        .WithMessage("Geçersiz bildirim tipi. (INFO, WARNING, ERROR veya SUCCESS olmalı)");

    RuleFor(x => x.LinkUrl)
        .MaximumLength(500).WithMessage("Link adresi 500 karakterden fazla olamaz.");
  }
}

public class UpdateNotificationRequestValidator : AbstractValidator<UpdateNotificationRequest>
{
  public UpdateNotificationRequestValidator()
  {
    RuleFor(x => x.Id)
        .NotEmpty().WithMessage("Güncellenecek bildirimin ID bilgisi eksik.");

  }
}