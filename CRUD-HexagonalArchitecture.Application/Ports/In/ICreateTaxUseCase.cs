using CRUD_HexagonalArchitecture.Application.Dtos.Tax;

namespace CRUD_HexagonalArchitecture.Application.Ports.In
{
    public interface ICreateTaxUseCase
    {
        Task<TaxListDto> ExecuteAsync(TaxListDto taxListDto);
    }
}
