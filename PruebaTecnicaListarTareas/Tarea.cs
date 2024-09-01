using System;

namespace PruebaTecnicaListarTareas
{
    /*Clase de tarea encapsulada, utilizamos propiedades para manipular las variables
     Contiene un constructor */
    public class Tarea
    {
        private int id;
        private string descripcion;
        private DateTime? fechaLimite;
        private bool estado; /*Utilizado para diferenciar las tareas completadas de las tareas pendientes*/

        public int Id {
            get { return id; }
        }
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        public DateTime? FechaLimite
        {
            get { return fechaLimite; }
            set { fechaLimite = value; }
        }
        public bool Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        /*Constructor de tarea, permite fechas nulas*/
        public Tarea(int id, string descripcion, DateTime? fechaLimite, bool estado)
        {
            this.id = id;
            this.descripcion = descripcion;
            this.fechaLimite = fechaLimite;
            this.estado = estado;
        }
    }
}
   
