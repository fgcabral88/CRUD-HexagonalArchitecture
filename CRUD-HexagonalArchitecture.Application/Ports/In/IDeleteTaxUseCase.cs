namespace CRUD_HexagonalArchitecture.Application.Ports.In
{
    public interface IDeleteTaxUseCase
    {
        Task<bool> ExecuteAsync(int id);
    }
}
