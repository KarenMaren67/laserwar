using Application.Enums;

namespace Application.Validation.Emplyee
{
    public class Employee
    {
        public int? Id { get; set; }

        /// <summary>
        /// Ф.И.О.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Должность.
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Номер телефона.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Подразделение сотрудника.
        /// </summary>
        public int ParentDepartmentId { get; set; }
    }
}
