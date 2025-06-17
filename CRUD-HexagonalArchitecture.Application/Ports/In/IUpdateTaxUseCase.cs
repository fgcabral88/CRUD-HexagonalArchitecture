using CRUD_HexagonalArchitecture.Application.Dtos.Tax;

namespace CRUD_HexagonalArchitecture.Application.Ports.In
{
    public interface IUpdateTaxUseCase
    {
        Task<TaxListDto?> ExecuteAsync(TaxListDto taxListDto);
    }
}
