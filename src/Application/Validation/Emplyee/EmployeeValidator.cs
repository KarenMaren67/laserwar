using FluentValidation;

namespace Application.Validation.Emplyee
{
    public class EmployeeValidator : AbstractValidator<Employee>, IValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Position).NotEqual(Enums.Positions.None.ToString());
            RuleFor(x => x.ParentDepartmentId).Must(x => x > 0);
        }
    }
}
