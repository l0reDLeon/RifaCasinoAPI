using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RifaCasinoAPI.DTOs;
using RifaCasinoAPI.Entidades;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RifaCasinoAPI.Controllers
{
    [ApiController]
    [Route("api/Cuentas")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class LoginUsuarios : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        //private readonly IConfiguration configuration; //IConfiguration config, <-esto va en el constructor si se llega a necesitar
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IMapper mapper;
        private readonly ILogger<LoginUsuarios> logger;

        public LoginUsuarios(
            UserManager<IdentityUser> userManager, IMapper mapper, ILogger<LoginUsuarios> logger,
            SignInManager<IdentityUser> signInManager, ApplicationDbContext dbContext
        ){
            this.dbContext = dbContext;
            //this.configuration = config;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
            this.logger = logger;
        }
        [AllowAnonymous]

        [HttpPost("Registrar")] //api/cuentas/registrar
        public async Task<ActionResult<RespuestaAutenticacion>> Registrar(CredencialesUsuario credenciales)
        {
            var user = new IdentityUser { UserName = credenciales.email, Email = credenciales.email };
            var result = await userManager.CreateAsync(user, credenciales.password);
            //Con esto ya tenemos el id dado a este usuario
            var iduser = await userManager.GetUserIdAsync(user);

            if (result.Succeeded)
            {
                var token = await BuildToken(credenciales);

                //mapeamos el usuario a participante DTO y luego a participante
                var participanteDTO = mapper.Map<ParticipanteCreacionDTO>(user);
                var participanteMapeado = mapper.Map<Participantes>(participanteDTO);
                dbContext.Add(participanteMapeado);
                await dbContext.SaveChangesAsync();

                logger.LogInformation("INFO: Se ha creado un nuevo usuario y se ha guardado en la base de datos ");

                return token;
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<RespuestaAutenticacion>> Login(CredencialesUsuario credenciales) { 
            var resultado = await signInManager.PasswordSignInAsync(credenciales.email, 
                credenciales.password, isPersistent: false, lockoutOnFailure:false);
            if (resultado.Succeeded)
            {
                return await BuildToken(credenciales);
            }
            else
            {
                return BadRequest("Algo salió mal... Login Incorrecto");
            }
        }
        [HttpGet("RenovarToken")]
        public async Task<ActionResult<RespuestaAutenticacion>> RenovarToken()
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim?.Value;

            var credenciales = new CredencialesUsuario()
            {
                email = email
            };
            return await BuildToken(credenciales);
        }
        private async Task<RespuestaAutenticacion> BuildToken(CredencialesUsuario credenciales)
        {
            var claims = new List<Claim>()
            {
                new Claim("email", credenciales.email)
            };

            //Agrega los claims asignados al usuario (admin o no admin) en la tabla de Claims
            var Usuario = await userManager.FindByEmailAsync(credenciales.email);
            var ClaimsDB = await userManager.GetClaimsAsync(Usuario);
            claims.AddRange(ClaimsDB);

            //Pone el claim en el token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llavejwt"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiracion = DateTime.UtcNow.AddMinutes(30);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiracion, signingCredentials: creds);

            return new RespuestaAutenticacion()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Caducidad = expiracion
            };
        }
        [HttpPost("HacerAdmin")]
        //[Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Admin(EditarAdminDTO editarAdminDTO)    
        {
            var usuario = await userManager.FindByEmailAsync(editarAdminDTO.email);

            await userManager.AddClaimAsync(usuario, new Claim("Admin", "True"));
            logger.LogWarning("INFO: Se ha cambiado el tipo de usuario de un usuario");

            return NoContent();
        }

        [HttpPost("QuitarAdmin")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> QuitarAdmin(EditarAdminDTO editarAdminDTO)
        {
            var usuario = await userManager.FindByEmailAsync(editarAdminDTO.email);

            await userManager.RemoveClaimAsync(usuario, new Claim("Admin", "True"));
            return NoContent();

        }
    }
}
