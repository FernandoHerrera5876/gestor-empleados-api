using Microsoft.AspNetCore.Mvc;
using MiWebAPI.Data;
using MiWebAPI.Models;

namespace MiWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Proovedor2Controller : ControllerBase
    {
        private readonly Proovedor2Data _proovedor2Data;
        public Proovedor2Controller(Proovedor2Data Proovedor2Data)
        {
            _proovedor2Data = Proovedor2Data;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<Proovedor2> Lista = await _proovedor2Data.Lista();
            return StatusCode(StatusCodes.Status200OK, Lista);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Proovedor2 objeto)
        {
            bool respuesta = await _proovedor2Data.Crear(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }

        [HttpGet("{ID}")]

        public async Task<IActionResult> Obtener(long ID)
        {
            Proovedor2 objeto = await _proovedor2Data.Obtener(ID);
            return StatusCode(StatusCodes.Status200OK, objeto);
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] Proovedor2 objeto)
        {
            bool respuesta = await _proovedor2Data.Editar(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }

        [HttpDelete("{ID}")]
        public async Task<IActionResult> Eliminar(long ID)
        {
            bool respuesta = await _proovedor2Data.Eliminar(ID);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }
    }
}
