using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Entidades;
using TaskManager.Models;
using TaskManager.Servicios;

namespace TaskManager.Controllers
{
	[Route("api/tareas")]
	public class TareasController: ControllerBase
	{
		private readonly ApplicationDbContext context;
		private readonly IServicioUsuarios servicioUsuarios;
		private readonly IMapper mapper;

		public TareasController(ApplicationDbContext context, IServicioUsuarios servicioUsuarios, IMapper mapper)
        {
			this.context = context;
			this.servicioUsuarios = servicioUsuarios;
			this.mapper = mapper;
		}

		[HttpGet]
		public async Task<List<TareaDTO>> Get()
		{
			var usuarioId = servicioUsuarios.ObtenerUsuarioId();
			return await context.Tareas.Where(x => x.UsuarioCreacionId == usuarioId)
										.OrderBy(x => x.Orden)
										.ProjectTo<TareaDTO>(mapper.ConfigurationProvider)
										.ToListAsync();
		}

		[HttpPost]
		public async Task<ActionResult<Tarea>> Post([FromBody] string titulo)
		{
			var usuarioId = servicioUsuarios.ObtenerUsuarioId();
			var existenTareas = await context.Tareas.AnyAsync(t => t.UsuarioCreacionId == usuarioId);

			var ordenMayor = 0;
			if (existenTareas)
			{
				ordenMayor = await context.Tareas.Where(t => t.UsuarioCreacionId == usuarioId).Select(t => t.Orden).MaxAsync();
			}

			var tarea = new Tarea
			{
				Titulo = titulo,
				UsuarioCreacionId = usuarioId,
				FechaCreacion = DateTime.UtcNow,
				Orden = ordenMayor + 1
			};

			context.Add(tarea);
			await context.SaveChangesAsync();

			return tarea;
		}
	}
}
