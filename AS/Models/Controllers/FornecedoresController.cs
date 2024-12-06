using Microsoft.AspNetCore.Mvc;
using GerenciamentoPedidosFornecedores.Models;
using GerenciamentoPedidosFornecedores.Repositories;

namespace GerenciamentoPedidosFornecedores.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IRepository<Pedido> _repository;

        public PedidosController(IRepository<Pedido> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_repository.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var pedido = _repository.GetById(id);
            return pedido == null ? NotFound() : Ok(pedido);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Pedido pedido)
        {
            _repository.Add(pedido);
            _repository.Save();
            return CreatedAtAction(nameof(GetById), new { id = pedido.Id }, pedido);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Pedido pedido)
        {
            if (id != pedido.Id) return BadRequest();

            _repository.Update(pedido);
            _repository.Save();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            _repository.Save();
            return NoContent();
        }
    }
}
