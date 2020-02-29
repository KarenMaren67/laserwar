namespace Domain.DbEntities
{
    public class DepartmentEntity: AbstractEntity 
    {
        /// <summary>
        /// Название подразделения
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Наличие работников
        /// </summary>
        public bool HasEmployees{ get; set; }

        /// <summary>
        /// Идентификатор вышестоящего подразделений
        /// </summary>
        public int? ParentDepartmentId { get; set; }
    }
}
