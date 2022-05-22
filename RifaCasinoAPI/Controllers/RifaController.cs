using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using RifaCasinoAPI.Entidades;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RifaCasinoAPI.DTOs;
using Microsoft.AspNetCore.JsonPatch;

namespace RifaCasinoAPI.Controllers
{
    [Route("api/Rifa")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RifaController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration config;
        private readonly IMapper mapper;
        public RifaController(
            ApplicationDbContext context, IMapper mapper, UserManager<IdentityUser> userM, IConfiguration conf)
        {
            this.dbContext = context;
            this.mapper = mapper;
            this.userManager = userM;
            this.config = conf;
        }

        [AllowAnonymous]
        [HttpGet("GetDePrueba")]
        public IActionResult Get()
        {
            throw new NotImplementedException();
            //return Ok("ListaDeRifas");
        }

        [HttpGet("VerRifas")]
        [ResponseCache(Duration = 30)]
        public async Task<ActionResult<List<GetRifaDTO>>> GetLista()
        {
            var Rifas = await dbContext.Rifas.ToListAsync();
            if (Rifas.Count == 0)
            {
                return NotFound();
            }
            return mapper.Map<List<GetRifaDTO>>(Rifas);
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetRifaDTO>> GetbyId(int id)
        {
            var Rifa = await dbContext.Rifas.FirstOrDefaultAsync(RifaBD => RifaBD.id == id);

            if (Rifa == null)
            {
                return NotFound();
            }
            return mapper.Map<GetRifaDTO>(Rifa);
        }
        [AllowAnonymous]
        [HttpGet("{Buscar por nombre}")]
        [ResponseCache(Duration = 120)]
        public async Task<ActionResult<List<GetRifaDTO>>> GetByNombre([FromRoute] string nombre)
        {
            var Rifas = await dbContext.Rifas.Where(RifasBD => RifasBD.nombre.Contains(nombre)).ToListAsync();

            if (Rifas.Count == 0)
            {
                return NotFound();
            }
            return mapper.Map<List<GetRifaDTO>>(Rifas);
        }

        [HttpPost("NuevaRifa")]
        [Authorize(Policy = "AdminPolicy")]
        //Validar que se cree con vigencia falso hasta que no se ingrese un premio
        public async Task<IActionResult> Post(RifaCreacionDTO rifaCreacionDTO)
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;
            var user = await userManager.FindByNameAsync(email);
            var rifaDTO = mapper.Map<RifaDTO>(rifaCreacionDTO);
            var rifa = mapper.Map<Rifa>(rifaDTO);

            rifa.vigente = false;

            dbContext.Add(rifa);
            await dbContext.SaveChangesAsync();
            return Ok("Nueva Rifa agregada con éxito");
        }

        //-------------------------------------------------------------------------------------

        [HttpPatch("{idRifa:int}", Name = "Vigencia")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Vigencia(int idRifa, JsonPatchDocument<PatchRifaDTO> rifaPatchDocument)
        {
            if (rifaPatchDocument == null) return BadRequest();
            var rifaDB = await dbContext.Rifas.FirstOrDefaultAsync(x => x.id == idRifa);
            if (rifaDB == null) return NotFound();

            var rifaDTO = mapper.Map<PatchRifaDTO>(rifaDB);
            //Comentario: se pasa el ModelState por si hay un error, así se colocará en el model state
            rifaPatchDocument.ApplyTo(rifaDTO, ModelState); //Comentario: aquí se aplican los cambios al recurso obtenido de la DB

            var isOk = TryValidateModel(rifaDTO);
            if (!isOk) return BadRequest(ModelState);

            mapper.Map(rifaDTO, rifaDB);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        //Checar que agregue los premios, no que los reemplace -- al parecer se hace automaticament
        [HttpPost(Name = "AgregarPremio")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> PostPremio(PremioCreacionDTO crearPremioDTO)
        {
            var idRifa = crearPremioDTO.idRifa;

            if (crearPremioDTO == null) return BadRequest();
            var rifaDB = await dbContext.Rifas.FirstOrDefaultAsync(x => x.id == idRifa);
            if (rifaDB == null) return NotFound("No existe esa rifa");

            var premioDTO = mapper.Map<PremioDTO>(crearPremioDTO);

            var isOk = TryValidateModel(premioDTO);
            if (!isOk) return BadRequest();

            var premio = mapper.Map<Premio>(premioDTO);

            var banderabanderademexico = true;
            if (rifaDB.premioList.Count == 0)
            {
                rifaDB.vigente = true;
            }
            else
            {
                //aqui checo que minimo haya un premio activo, es decir, 
                //que la rifa no sea una rifa que ya haya sido usada
                //checando que sus premios no hayan sido canjeados
                banderabanderademexico = false;
                for (int i = 0; i < rifaDB.premioList.Count; i++)
                {
                    if (rifaDB.premioList[i].disponible == true)
                    {
                        banderabanderademexico = true;
                        break;
                    }
                }
                //sino hay minimo uno que esté disponible, entonces no se registra
                //suponiendo que si ya se usaron todos los premios, la rifa ya acabó y solo
                //quedó como historial
            }

            //ahora como tiene al menos un premio activo, la rifa puede elegir al menos
            //un ganador, por eso cambiamos el estado de vigente a true, sino hubiera premios
            //no habría rifa
            if (!banderabanderademexico)
            {
                return BadRequest("Esta rifa ya no está disponible para agregar más premios.");
            }

            dbContext.Add(premio);
            await dbContext.SaveChangesAsync();
            return Ok("Premio agregado a la rifa " + idRifa);
        }

        [HttpDelete("{idRifa:int}", Name = "Borrar una rifa")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Delete(int idRifa)
        {
            var exist = await dbContext.Rifas.AnyAsync(x => x.id == idRifa);
            if (!exist) return NotFound("No existe.");
            dbContext.Remove(new Rifa
            {
                id = idRifa
            });
            await dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
