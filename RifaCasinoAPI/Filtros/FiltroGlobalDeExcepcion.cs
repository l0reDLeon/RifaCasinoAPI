using Microsoft.AspNetCore.Mvc.Filters;

namespace RifaCasinoAPI.Filtros
{
    public class FiltroGlobalDeExcepcion : ExceptionFilterAttribute
    {
        private readonly ILogger<FiltroGlobalDeExcepcion> logger;

        public FiltroGlobalDeExcepcion(ILogger<FiltroGlobalDeExcepcion> log)
        {
            this.logger = log;
        }

        public override void OnException(ExceptionContext context)
        {
            logger.LogError(context.Exception,context.Exception.Message);
            base.OnException(context);
        }
    }
}
