﻿using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
	public class PasoCrearDTO
	{
		[Required]
        public string Descripcion { get; set; }

        public bool Realizado { get; set; }
    }
}
