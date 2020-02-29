using Domain.DbEntities;
using System.Collections.Generic;

namespace Application.Validation.Department
{
    public class Department
    {
        public int? Id { get; set; }

        /// <summary>
        /// Название подразделения.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Наличие работников
        /// </summary>
        public bool HasEmployees { get; set; }

        /// <summary>
        /// Идентификатор вышестоящего подразделений
        /// </summary>
        public int? ParentDepartmentId { get; set; }
    }
}
