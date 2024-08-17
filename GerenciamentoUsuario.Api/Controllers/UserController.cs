using GerenciamentoUsuario.Domain.Entities;
using GerenciamentoUsuario.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoUsuario.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserManagerService _userService;

        public UserController(IUserManagerService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] User user)
        {
            if (user.Name is null || user.Password is null || user.Email is null)
            {
                return BadRequest("Dados user vazio!");
            }

            await _userService.RegisterUser(user);
            return Ok();
        }

        [HttpGet("listar")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsuarios()
        {
            var usuarios = await _userService.GetAllUsers();
            return Ok(usuarios);
        }

        [HttpGet("listarByEmail")]
        public async Task<ActionResult> GetUserByEmail([FromHeader] string email)
        {
            if (email == null)
            {
                return BadRequest("Email user vazio!");
            }
            var user = await _userService.GetUserByEmail(email);
            return Ok(user);

        }

        [HttpGet("test-connection")]
        public IActionResult TestConnection()
        {
            try
            {
                _userService.TesteConexao();
                return Ok("Conexão com o MongoDB bem-sucedida!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao conectar ao MongoDB: {ex.Message}");
            }
        }
    }
}
