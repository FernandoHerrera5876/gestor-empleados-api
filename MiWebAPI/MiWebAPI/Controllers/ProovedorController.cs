using Microsoft.AspNetCore.Mvc;
using MiWebAPI.Data;
using MiWebAPI.Models;

namespace MiWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProovedorController : ControllerBase
    {
        private readonly ProovedorData _proovedorData;
        public ProovedorController(ProovedorData ProovedorData)
        {
            _proovedorData = ProovedorData;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<Proovedor> Lista = await _proovedorData.Lista();
            return StatusCode(StatusCodes.Status200OK, Lista);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Proovedor objeto)
        {
            bool respuesta = await _proovedorData.Crear(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }

        [HttpGet("{ID}")]

        public async Task<IActionResult> Obtener(long ID)
        {
            Proovedor objeto = await _proovedorData.Obtener(ID);
            return StatusCode(StatusCodes.Status200OK, objeto);
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] Proovedor objeto)
        {
            bool respuesta = await _proovedorData.Editar(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }

        [HttpDelete("{ID}")]
        public async Task<IActionResult> Eliminar(long ID)
        {
            bool respuesta = await _proovedorData.Eliminar(ID);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }
    }
}
