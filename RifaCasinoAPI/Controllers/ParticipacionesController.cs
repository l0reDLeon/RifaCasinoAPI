using Microsoft.AspNetCore.Mvc;

namespace RifaCasinoAPI.Controllers
{
    [ApiController]
    public class ParticipacionesController:ControllerBase
    {
        public ParticipacionesController()
        {

        }

        [HttpPost]
        public async Task<ActionResult> Post(int id)
        {
            var email = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();

        }

    }
}
