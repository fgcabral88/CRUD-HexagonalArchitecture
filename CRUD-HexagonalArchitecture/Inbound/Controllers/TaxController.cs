using CRUD_HexagonalArchitecture.Application.Dtos.Tax;
using CRUD_HexagonalArchitecture.Application.Ports.In;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CRUD_HexagonalArchitecture.Inbound.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class TaxController : ControllerBase
    {
        private readonly ICreateTaxUseCase _create;
        private readonly IGetTaxUseCase _get;
        private readonly IUpdateTaxUseCase _update;
        private readonly IDeleteTaxUseCase _delete;

        public TaxController(ICreateTaxUseCase create, IGetTaxUseCase get, IUpdateTaxUseCase update, IDeleteTaxUseCase delete)
        {
            _create = create;
            _get = get;
            _update = update;
            _delete = delete;
        }

        /// <summary>
        /// Retorna uma lista de impostos
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Summary = "Lista todos os impostos", Description = "Recupera todos os impostos cadastrados")]
        [ProducesResponseType(typeof(List<TaxListDto>), 200)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _get.GetAllAsync());
        }

        /// <summary>
        /// Retorna um imposto específico pelo ID
        /// </summary>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Busca um imposto por ID", Description = "Retorna um imposto específico com base no ID informado")]
        [ProducesResponseType(typeof(TaxListDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _get.GetByIdAsync(id);

            return result == null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// Cria um novo imposto
        /// </summary>
        [HttpPost]
        [SwaggerOperation(Summary = "Cria um novo imposto", Description = "Adiciona um novo imposto ao sistema")]
        [ProducesResponseType(typeof(TaxListDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Create([FromBody] TaxListDto taxListDto)
        {
            var result = await _create.ExecuteAsync(taxListDto);

            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        /// <summary>
        /// Atualiza um imposto existente
        /// </summary>
        [HttpPut]
        [SwaggerOperation(Summary = "Atualiza um imposto", Description = "Atualiza os dados de um imposto já existente")]
        [ProducesResponseType(typeof(TaxListDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Update([FromBody] TaxListDto taxListDto)
        {
            var result = await _update.ExecuteAsync(taxListDto);

            return result == null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// Deleta um imposto pelo ID
        /// </summary>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Remove um imposto", Description = "Remove um imposto com base no ID informado")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _delete.ExecuteAsync(id);

            return result ? Ok() : NotFound();
        }
    }
}
