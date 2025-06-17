using CRUD_HexagonalArchitecture.Application.Dtos.Tax;
using CRUD_HexagonalArchitecture.Application.Ports.In;
using CRUD_HexagonalArchitecture.Application.Ports.Out;
using CRUD_HexagonalArchitecture.Domain.Entities;

namespace CRUD_HexagonalArchitecture.Application.UseCases.Tax
{
    public class CreateTaxUseCase : ICreateTaxUseCase
    {
        private readonly ITaxRepository _repository;

        public CreateTaxUseCase(ITaxRepository repository)
        {
            _repository = repository;
        }

        public async Task<TaxListDto> ExecuteAsync(TaxListDto taxListDto)
        {
            var tax = new TaxEntity
            {
                Name = taxListDto.Name,
                Percentage = taxListDto.Percentage
            };

            var result = await _repository.CreateAsync(tax);

            return new TaxListDto
            {
                Id = result.Id,
                Name = result.Name,
                Percentage = result.Percentage
            };
        }
    }
}
