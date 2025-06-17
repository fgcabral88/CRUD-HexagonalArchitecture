using CRUD_HexagonalArchitecture.Application.Dtos.Tax;
using CRUD_HexagonalArchitecture.Application.Ports.In;
using CRUD_HexagonalArchitecture.Inbound.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CRUD_HexagonalArchitecture.Tests.Controllers
{
    public class TaxControllerTest
    {
        private readonly Mock<ICreateTaxUseCase> _createMock = new();
        private readonly Mock<IGetTaxUseCase> _getMock = new();
        private readonly Mock<IUpdateTaxUseCase> _updateMock = new();
        private readonly Mock<IDeleteTaxUseCase> _deleteMock = new();

        private readonly TaxController _controller;

        public TaxControllerTest()
        {
            _controller = new TaxController(_createMock.Object, _getMock.Object, _updateMock.Object, _deleteMock.Object);
        }

        [Fact]
        public async Task Create_ReturnsOkResult_WithCreatedTax()
        {
            var input = new TaxListDto 
            { 
                Name = "ISS",
                Percentage = 5
            };

            var output = new TaxListDto 
            { 
                Id = 1,
                Name = "ISS",
                Percentage = 5
            };

            _createMock.Setup(x => x.ExecuteAsync(input)).ReturnsAsync(output);

            var result = await _controller.Create(input);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var dto = Assert.IsType<TaxListDto>(okResult.Value);

            Assert.Equal(1, dto.Id);
        }

        [Fact]
        public async Task Get_WithExistingId_ReturnsTax()
        {
            var dto = new TaxListDto 
            { 
                Id = 1,
                Name = "ISS",
                Percentage = 5
            };

            _getMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(dto);

            var result = await _controller.Get(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var tax = Assert.IsType<TaxListDto>(okResult.Value);

            Assert.Equal("ISS", tax.Name);
        }

        [Fact]
        public async Task Get_WithNonExistingId_ReturnsNotFound()
        {
            _getMock.Setup(x => x.GetByIdAsync(2)).ReturnsAsync((TaxListDto?) null);

            var result = await _controller.Get(2);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetAll_ReturnsListOfTaxes()
        {
            var list = new List<TaxListDto> 
            { 
                new() 
                { 
                    Id = 1,
                    Name = "ISS",
                    Percentage = 5
                }
            };

            _getMock.Setup(x => x.GetAllAsync()).ReturnsAsync(list);

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var values = Assert.IsAssignableFrom<IEnumerable<TaxListDto>>(okResult.Value);

            Assert.Single(values);
        }

        [Fact]
        public async Task Update_WithValidData_ReturnsUpdatedTax()
        {
            var dto = new TaxListDto 
            { 
                Id = 1, 
                Name = "ISS",
                Percentage = 7
            };

            _updateMock.Setup(x => x.ExecuteAsync(dto)).ReturnsAsync(dto);

            var result = await _controller.Update(dto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var tax = Assert.IsType<TaxListDto>(okResult.Value);
            
            Assert.Equal(7, tax.Percentage);
        }

        [Fact]
        public async Task Update_WithInvalidId_ReturnsNotFound()
        {
            var dto = new TaxListDto 
            { 
                Id = 999,
                Name = "ICMS",
                Percentage = 12
            };

            _updateMock.Setup(x => x.ExecuteAsync(dto)).ReturnsAsync((TaxListDto?)null);

            var result = await _controller.Update(dto);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_WithExistingId_ReturnsOk()
        {
            _deleteMock.Setup(x => x.ExecuteAsync(1)).ReturnsAsync(true);

            var result = await _controller.Delete(1);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Delete_WithNonExistingId_ReturnsNotFound()
        {
            _deleteMock.Setup(x => x.ExecuteAsync(999)).ReturnsAsync(false);

            var result = await _controller.Delete(999);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}