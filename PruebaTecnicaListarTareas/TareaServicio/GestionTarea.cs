using System;
using System.Collections.Generic;
using System.Text;

namespace PruebaTecnicaListarTareas
{
    internal class GestionTarea
    {
        private List<Tarea> tareas = new List<Tarea>();
        Random r = new Random();

        private int CrearId()
        {
            int id;
            do{
                id = r.Next(1000, 9999);
            } while (tareas.Exists(t => t.Id == id));
            return id;
        }

        public string AgregarTarea(string descripcion, DateTime? fechaLimite)
        {
            if (tareas.Exists(t => t.Descripcion == descripcion)){
                return "\n== Esta tarea ya existe.";
            }
            if (fechaLimite <= DateTime.Now && fechaLimite != null){
                return "\n== La fecha límite no puede ser anterior a la fecha actual.";
            }
            Tarea nuevaTarea = new Tarea(CrearId(), descripcion, fechaLimite, false);
            tareas.Add(nuevaTarea);
            return "\n== Tarea agregada.";
        }

        public string ListarTareas()
        {
            if (tareas.Count != 0){
                StringBuilder listaTareas = new StringBuilder();
                StringBuilder tareasPendientes = new StringBuilder();
                StringBuilder tareasCompletadas = new StringBuilder();
                
                foreach (var tarea in tareas){
                    if (tarea.Estado){
                        tareasCompletadas.Append("\n  " + tarea.Id + "  " + tarea.Descripcion);
                    }
                    else{
                        string fecha = "No definida";
                        if (tarea.FechaLimite != null){
                            fecha = tarea.FechaLimite.ToString();
                        }
                        tareasPendientes.Append("\n  " + tarea.Id + "  " + tarea.Descripcion + "  \t" + fecha);
                    }
                }
                if (tareasPendientes.Length != 0){
                    listaTareas.Append("\n  ===== TAREAS PENDIENTES =====");
                    listaTareas.Append("\n\n  ID   DESCRIPCIÓN    FECHA LÍMITE");
                    listaTareas.Append(tareasPendientes.ToString());
                }
                if (tareasCompletadas.Length != 0){
                    listaTareas.Append("\n\n  ===== TAREAS COMPLETADAS=====");
                    listaTareas.Append("\n\n  ID   DESCRIPCIÓN");
                    listaTareas.Append(tareasCompletadas.ToString());
                }
                
                return listaTareas.ToString();
            }
            return "\n== No hay tareas creadas"; 
            
        }

        public string MarcarTarea(string identificador)
        {
            if (tareas.Count != 0){
                Tarea tarea = BuscarTarea(identificador);
                if (tarea == null){
                    return "\n== La tarea especificada no existe, verifica el campo enviado.";
                }
                if (!tarea.Estado){
                    tareas.Remove(tarea);
                    tarea.Estado = true;
                    tareas.Add(tarea);
                    return "\n== Tarea \"" + tarea.Descripcion + "\" marcada como completa.";
                }
                return "\n== La tarea \"" + tarea.Descripcion + "\" ya ha sido marcada como completada.";
            }
            return "\n== Primero agrega una tarea.";
        }

        public string EliminarTarea(string identificador)
        {
            if (tareas.Count != 0){
                Tarea tarea = BuscarTarea(identificador);
                if (tarea == null){
                    return "\n== La tarea especificada no existe, verifica el campo enviado.";
                }
                tareas.Remove(tarea);
                return "\n== La tarea \"" + tarea.Descripcion + "\" ha sido eliminada.";
            }
            return "\n== No hay ninguna tarea para eliminar.";
        }

        private Tarea BuscarTarea(string identidicador)
        {
            for (int i = 0; i < tareas.Count; i++){
                if (identidicador.ToLower() == tareas[i].Descripcion.ToLower() || identidicador == tareas[i].Id.ToString()){
                    return tareas[i];
                }
            }
            return null;
        }

    }
}
