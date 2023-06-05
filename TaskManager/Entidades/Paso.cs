namespace TaskManager.Entidades
{
    public class Paso
    {
        public Guid Id { get; set; } // 0ebf3033-aa2c...
        public int TareaId { get; set; }
        public Tarea Tarea { get; set; } // Propiedad de navegacion
        public string Descripcion { get; set; }
        public bool Realizado { get; set; }
    }
}
