using AutoMapper;
using TaskManager.Entidades;
using TaskManager.Models;

namespace TaskManager.Servicios
{
	public class AutoMapperProfiles: Profile
	{
		public AutoMapperProfiles()
		{
			CreateMap<Tarea, TareaDTO>();
		}
	}
}
