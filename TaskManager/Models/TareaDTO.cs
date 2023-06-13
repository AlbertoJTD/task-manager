﻿namespace TaskManager.Models
{
	public class TareaDTO // DTO = Data Transfer Object
	{
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int PasosRealizados { get; set; }
        public int PasosTotal { get; set; }
    }
}
