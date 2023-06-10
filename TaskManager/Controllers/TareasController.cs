using Microsoft.AspNetCore.Mvc;
using TaskManager.Servicios;

namespace TaskManager.Controllers
{
	[Route("api/tareas")]
	public class TareasController: ControllerBase
	{
		private readonly ApplicationDbContext context;
		private readonly IServicioUsuarios servicioUsuarios;

		public TareasController(ApplicationDbContext context, IServicioUsuarios servicioUsuarios)
        {
			this.context = context;
			this.servicioUsuarios = servicioUsuarios;
		}


    }
}
