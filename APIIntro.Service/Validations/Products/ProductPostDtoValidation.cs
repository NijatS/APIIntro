using APIIntro.Service.Dtos.Categories;
using APIIntro.Service.Helpers;
using FluentValidation;

namespace APIIntro.Service.Validations.Categories
{
    public class ProductPostDtoValidation:AbstractValidator<ProductPostDto>
    {
        public ProductPostDtoValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name can not be empty")
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(30);
            RuleFor(x => x)
                .Custom((x, context) =>
                {
                    if (!Helper.isImage(x.file))
                    {
                        context.AddFailure("file", "The type of file must be image");
                    }
                    if (!Helper.isSizeOk(x.file,2))
                    {
                        context.AddFailure("file", "The size of image less than 2 mb");
                    }
                });
        }
    }
}
