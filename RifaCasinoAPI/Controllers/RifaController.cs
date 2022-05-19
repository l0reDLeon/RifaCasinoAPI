using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using RifaCasinoAPI.Entidades;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RifaCasinoAPI.DTOs;

namespace RifaCasinoAPI.Controllers
{
    [Route("api/Rifa")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RifaController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration config;
        private readonly IMapper mapper;
        public RifaController(
            ApplicationDbContext context, IMapper mapper, UserManager<IdentityUser> userM, IConfiguration conf)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userM;
            this.config = conf;
        }

        [HttpGet("VerRifas")]
        public async Task<IActionResult> Get()
        {
            return Ok("ListaDeRifas");
        }
        /*
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetRifaDTO>> Get(int id)
        {
            var Rifa = await dbContext.Rifa.FirstOrDefaultAsync(RifaBD => RifaBD.Id == id);

            if (Rifa == null)
            {
                return NotFound();
            }
            return mapper.Map<GetRifaDTO>(Rifa);
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<List<GetRifaDTO>>> Get([FromRoute] string nombre)
        {
            var Rifas = await DbContext.Rifa.Where(RifasBD => RifasBD.nombre.Contains(nombre)).ToListAsync();

            if (Rifas == null)
            {
                return NotFound();
            }   
            return mapper.Map<List<GetRifaDTO>>(Rifas);
        }
        
        */

        [HttpPost("NuevaRifa")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Post(RifaCreacionDTO rifaCreacionDTO)
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;

            var user = await userManager.FindByNameAsync(email);

            var rifaDTO = mapper.Map<RifaDTO>(rifaCreacionDTO);

            var rifa = mapper.Map<Rifa>(rifaDTO);

            //falta implementar el ingreso a la BD y registro de la nueva rifa
            return Ok("Nueva Rifa agregada con éxito");
        }
    }
}
