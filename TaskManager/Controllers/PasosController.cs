using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Entidades;
using TaskManager.Models;
using TaskManager.Servicios;

namespace TaskManager.Controllers
{
	[Route("api/pasos")]
	public class PasosController : ControllerBase
	{
		private readonly ApplicationDbContext context;
		private readonly IServicioUsuarios servicioUsuarios;

		public PasosController(ApplicationDbContext context, IServicioUsuarios servicioUsuarios)
        {
			this.context = context;
			this.servicioUsuarios = servicioUsuarios;
		}

		[HttpPost("{tareaId:int}")]
		public async Task<ActionResult<Paso>> Post(int tareaId, [FromBody] PasoCrearDTO pasoCrearDTO)
		{
			var usuarioId = servicioUsuarios.ObtenerUsuarioId();
			var tarea = await context.Tareas.FirstOrDefaultAsync(t => t.Id == tareaId);

			if (tarea == null)
			{
				return NotFound();
			}

			if (tarea.UsuarioCreacionId != usuarioId)
			{
				return Forbid();
			}

			var existenPasos = await context.Pasos.AnyAsync(p => p.TareaId == tareaId);
			var ordenMayor = 0;
			if (existenPasos)
			{
				ordenMayor = await context.Pasos.Where(p => p.TareaId == tareaId).Select(p => p.Orden).MaxAsync();
			}

			var paso = new Paso();
			paso.TareaId = tareaId;
			paso.Orden = ordenMayor + 1;
			paso.Descripcion = pasoCrearDTO.Descripcion;
			paso.Realizado = pasoCrearDTO.Realizado;

			context.Add(paso);

			await context.SaveChangesAsync();
			return paso;
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(Guid id, [FromBody] PasoCrearDTO pasoCrearDTO)
		{
			var usuarioId = servicioUsuarios.ObtenerUsuarioId();
			var paso = await context.Pasos.Include(p => p.Tarea).FirstOrDefaultAsync(t => t.Id == id);

			if (paso == null)
			{
				return NotFound();
			}

			if (paso.Tarea.UsuarioCreacionId != usuarioId)
			{
				return Forbid();
			}

			paso.Descripcion = pasoCrearDTO.Descripcion;
			paso.Realizado = pasoCrearDTO.Realizado;

			await context.SaveChangesAsync();
			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var usuarioId = servicioUsuarios.ObtenerUsuarioId();
			var paso = await context.Pasos.Include(p => p.Tarea).FirstOrDefaultAsync(t => t.Id == id);

			if (paso == null)
			{
				return NotFound();
			}

			if (paso.Tarea.UsuarioCreacionId != usuarioId)
			{
				return Forbid();
			}

			context.Remove(paso);
			await context.SaveChangesAsync();
			return Ok();
		}
	}
}
