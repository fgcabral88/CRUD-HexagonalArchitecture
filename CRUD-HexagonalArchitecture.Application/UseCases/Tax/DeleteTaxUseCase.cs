using CRUD_HexagonalArchitecture.Application.Ports.In;
using CRUD_HexagonalArchitecture.Application.Ports.Out;

namespace CRUD_HexagonalArchitecture.Application.UseCases.Tax
{
    public class DeleteTaxUseCase : IDeleteTaxUseCase
    {
        private readonly ITaxRepository _repository;

        public DeleteTaxUseCase(ITaxRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> ExecuteAsync(int id) => await _repository.DeleteAsync(id);
    }
}
