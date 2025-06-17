namespace CRUD_HexagonalArchitecture.Application.Dtos.Tax
{
    public class TaxListDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Percentage { get; set; }
    }
}
