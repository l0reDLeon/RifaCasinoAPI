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
        [Route("api/VerListaParticipaciones")]
        [HttpGet]
        public async Task<ActionResult<List<ParticipacionesDTO>>> GetAll()
        {
            var lista = await dbContext.Participaciones.ToListAsync();
            return mapper.Map<List<ParticipacionesDTO>>(lista);
        }

        [HttpPost]
        public async Task<ActionResult> Post(int id, ParticipacionesCreacionDTO participacionCreacionDTO)
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;

            var user = await userManager.FindByNameAsync(email);
            var participacionDTO = mapper.Map<ParticipacionesDTO>(participacionCreacionDTO);
            var participacion = mapper.Map<Participaciones>(participacionDTO);

            //--------------------------------------------

            var existeTarjeta = await dbContext.Participaciones.AnyAsync(participacionRifa => participacionRifa.noLoteria == participacionCreacionDTO.noLoteria);

            if (existeTarjeta)
            {
                return BadRequest("Ya ese número de tarjeta ya está en uso.");
            }
            else
            {
                var existeRifa = await dbContext.Rifas.AnyAsync(Rifa => Rifa.id == participacionCreacionDTO.idRifa);

                if (existeRifa)
                {
                    //if (user!=null)
                    //{
                    //  dbContext.Add(participacion);
                    //}

                    dbContext.Add(participacion);
                    await dbContext.SaveChangesAsync();
                }
            }

            return Ok("Nueva Rifa agregada con éxito");
        }


        [HttpDelete("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "AdminPolicy")]
        public async Task<ActionResult> Delete(int Id)
        {
            var exist = await dbContext.Participaciones.AnyAsync(x => x.id == Id);

            if (!exist)
            {
                return NotFound("No existe esa participación en la lista");
            }

            dbContext.Remove(new Participaciones()
            {
                id = Id
            });
            await dbContext.SaveChangesAsync();
            return Ok("Participación con id " + Id + " eliminada con éxito");
        }
        
        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "AdminPolicy")]
        public async Task<ActionResult> DeleteByRifaandUser(int IdRifa, int IdUsuario, int tarjeta)
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
    }
}
