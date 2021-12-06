using FluentValidation;
using LearningMaterials.Models;

namespace LearningMaterials.Validation
{
    public class MaterialsQueryValidator : AbstractValidator<MaterialsQueryModel>
    {
        public MaterialsQueryValidator()
        {
            RuleFor(r => r.SortByDate)
                .Must(s => s == false || s == true)
                .WithMessage("Value for this field must be either 'true' or 'false'");
        }
    }
}