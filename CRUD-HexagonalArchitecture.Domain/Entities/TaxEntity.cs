namespace CRUD_HexagonalArchitecture.Domain.Entities
{
    public class TaxEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Percentage { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
