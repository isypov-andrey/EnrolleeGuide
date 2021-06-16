namespace Entities.Domain
{
    public class UniversityForList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProgramsCount { get; set; }
        public int BudgetPlacesCount { get; set; }
        public decimal PriceFrom { get; set; }
        public int BudgetExamPointsFrom { get; set; }
    }
}
