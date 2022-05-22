namespace RifaCasinoAPI.Servicios
{
    public class LogEnArchivo : IHostedService
    {
        private readonly IWebHostEnvironment env;
        private readonly string archivo = "log.txt";
        public LogEnArchivo(IWebHostEnvironment env)
        {
            this.env = env;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Escribir("Ejecución: " + DateTime.Now.ToString("dd/MM/yy HH:mm:ss"));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Escribir("Ejecución finalizada: " + DateTime.Now.ToString("dd/MM/yy HH:mm:ss"));
            return Task.CompletedTask;
        }

        public void Escribir(string log)
        {
            var ruta = $@"{env.ContentRootPath}\wwwroot\archivo";
            using (StreamWriter writer = new StreamWriter(ruta, append: true))
            {
                writer.WriteLine(log);
            };

        }
    }
}
