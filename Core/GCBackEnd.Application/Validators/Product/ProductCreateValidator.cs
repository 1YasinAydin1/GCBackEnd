using FluentValidation;
using GCBackEnd.Application.ViewModels.Products;

namespace GCBackEnd.Application.Validators.Product
{
	public class ProductCreateValidator : AbstractValidator<Product_Create_ViewModel>
	{
		public ProductCreateValidator()
		{
			RuleFor(i => i.Name)
				.NotEmpty()
				.NotNull()
				.WithMessage("Lütfen Ürün Adı Alanını Doldurunuz.")
				.MaximumLength(150)
				.MinimumLength(2)
				.WithMessage("Ürün Adı Alanının Uzunluğu 2-150 Arası Olmalıdır");

			RuleFor(i => i.OnHand)
				.NotEmpty()
				.NotNull()
				.WithMessage("Lütfen Ürün Stok Alanını Doldurunuz.")
				.Must(i => i >= 0)
				.WithMessage("Ürün Stok Alanını Pozitif Sayı Olmalıdır");

			RuleFor(i => i.Price)
				.NotEmpty()
				.NotNull()
				.WithMessage("Lütfen Ürün Fiyatı Alanını Doldurunuz.")
				.Must(i => i >= 0)
				.WithMessage("Ürün Stok Fiyatı Pozitif Sayı Olmalıdır");
				}
	}
}
