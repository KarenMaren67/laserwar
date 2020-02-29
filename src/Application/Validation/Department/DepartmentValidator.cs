using FluentValidation;
using System.Linq;

namespace Application.Validation.Department
{
    public class DepartmentValidator : AbstractValidator<Department>, IValidator<Department>
    {
        public DepartmentValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }
}
