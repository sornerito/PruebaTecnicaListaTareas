using System;
using System.Text;
namespace PruebaTecnicaListarTareas
{
    internal class Tarea
    {
        private int id;
        private string descripcion;
        private DateTime? fechaLimite;
        private bool estado;

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

        public Tarea(int id, string descripcion, DateTime? fechaLimite, bool estado)
        {
            this.id = id;
            this.descripcion = descripcion;
            this.fechaLimite = fechaLimite;
            this.estado = estado;
        }
    }
}
   
