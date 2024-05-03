using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using WebAPiEcotech.DATA;
using WebAPiEcotech.Models;

namespace WebAPiEcotech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioData _usuarioData;


        public UsuarioController(UsuarioData usuarioData)
        {
            _usuarioData = usuarioData;
        }

        [HttpGet]
        public async Task<IActionResult> ListaUsuarios() 
        {
            List<Usuario> Lista = await _usuarioData.ListaUsuarios ();
            return StatusCode(StatusCodes.Status200OK, Lista);
        }


        [HttpGet("{IdUsuario}")]
        public async Task<IActionResult> ListaUsuarios(int IdUsuario)
        {
            Usuario  obj = await _usuarioData.ObtenerUsuarios(IdUsuario);
            return StatusCode(StatusCodes.Status200OK, obj);
        }


        [HttpPost]
        public async Task<IActionResult> CrearUsuario([FromBody]Usuario usuario)
        {
            bool respuesta = await _usuarioData.CrearUsuario(usuario);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta});
        }


        [HttpPut]
        public async Task<IActionResult> EditarUsuario([FromBody] Usuario usuario)
        {
            bool respuesta = await _usuarioData.EditarUsuario(usuario);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }

        [HttpDelete("{IdUsuario}")]
        public async Task<IActionResult> EditarUsuario(int IdUsuario)
        {
            bool respuesta = await _usuarioData.EliminarUsuario(IdUsuario);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }
    }
}
