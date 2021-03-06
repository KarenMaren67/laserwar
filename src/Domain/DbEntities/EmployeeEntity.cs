﻿namespace Domain.DbEntities
{
    public class EmployeeEntity : AbstractEntity 
    {
        /// <summary>
        /// ФИО.
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
