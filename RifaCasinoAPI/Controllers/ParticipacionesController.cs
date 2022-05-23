using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RifaCasinoAPI.DTOs;
using RifaCasinoAPI.Entidades;

namespace RifaCasinoAPI.Controllers
{
    [ApiController]
    [Route("api/Participar")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ParticipacionesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration config;
        private readonly IMapper mapper;
        public ParticipacionesController(
            ApplicationDbContext context, IMapper mapper, UserManager<IdentityUser> userM, IConfiguration conf)
        {
            this.dbContext = context;
            this.mapper = mapper;
            this.userManager = userM;
            this.config = conf;
        }

        ///----------------
        [AllowAnonymous]
        [Route("api/VerListaParticipaciones")]
        [HttpGet]
        public async Task<ActionResult<List<GetParticipacionesDTO>>> GetAll()
        {
            var lista = await dbContext.Participaciones
                .Include(participacion => participacion.participante).ToListAsync();
            return mapper.Map<List<GetParticipacionesDTO>>(lista);
        }

        [HttpPost("RegistrarUnaTarjeta")]
        public async Task<ActionResult> Post(ParticipacionesCreacionDTO participacionCreacionDTO)
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            if (emailClaim == null)
            {
                return Unauthorized("Se necesita un usuario para poder participar en la rifa, por favor inicie sesión");
            }
            var email = emailClaim.Value;
            var user = await userManager.FindByNameAsync(email);

            //--------------------------------------------
            //esto también está implementado como una validación del modelo
            if(participacionCreacionDTO.noLoteria < 0 || participacionCreacionDTO.noLoteria > 54) 
                return BadRequest("Lotería fuera de rango");

            var participacionDTO = mapper.Map<ParticipacionesDTO>(participacionCreacionDTO);
            participacionDTO.idParticipante = user.Id;
            participacionDTO.participante = await dbContext.Participantes.FirstOrDefaultAsync(
                userParticipante => userParticipante.idUser == user.Id);

            var participacion = mapper.Map<Participaciones>(participacionDTO);

            //var existeRifa = await dbContext.Rifas.AnyAsync(Rifa => Rifa.id == participacionCreacionDTO.idRifa);
            var rifaDB = await dbContext.Rifas.FirstOrDefaultAsync(x => x.id == participacionCreacionDTO.idRifa);
            if (rifaDB == null)
            {
                
                return BadRequest("El id de la rifa no coincide con el de ninguna en el registro");
            }
            else
            {
                participacion.rifa = rifaDB;
                var existeTarjeta = await dbContext.Participaciones.AnyAsync(
                        participacionRifa => participacionRifa.noLoteria == participacionCreacionDTO.noLoteria
                        && participacionRifa.idRifa == participacionCreacionDTO.idRifa
                    );
                if (!existeTarjeta) 
                {
                    
                    dbContext.Add(participacion);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    return BadRequest("Ese número de tarjeta ya está en uso.");
                }
            }

            return Ok("Su registro en la rifa ha sido exitoso");
        }

        [HttpDelete("{id:int}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Delete(int id)
        {
            var exist = await dbContext.Participaciones.AnyAsync(x => x.id == id);
            if (!exist) return NotFound("No existe el registro.");
            dbContext.Remove(new Participaciones
            {
                id = id
            });
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        /*
        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "AdminPolicy")]
        //este pedo está mal pensado, piensa bien en qué valores tomas y necesitas
        public async Task<ActionResult> DeleteByRifaandUser(int IdRifa, string IdUsuario, int tarjeta)
        {
            var participacion = await dbContext.Participaciones.FirstOrDefaultAsync(x =>  
                x.idRifa == IdRifa && x.idParticipante == IdUsuario && x.noLoteria == tarjeta);

            if (participacion == null)
            {
                return NotFound("No existe esa participación en la lista");
            }

            dbContext.Remove(new Participaciones()
            {
                id = participacion.id
            });
            await dbContext.SaveChangesAsync();
            return Ok("Participación del Usuario " + IdUsuario + " en la rifa " + IdRifa + " eliminada con éxito");            
        }
        */
    }
}
