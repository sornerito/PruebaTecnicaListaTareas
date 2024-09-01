using System;
using System.Collections.Generic;
using System.Text;

namespace PruebaTecnicaListarTareas
{
    /*Clase que usaremos para instanciar un objeto en el Main, sirviendo como una lista de tareas; incluye los requisitos funcionales especificados*/
    public class GestionTarea
    {
        private List<Tarea> tareas = new List<Tarea>(); /*Lista generica que utiliza la clase "Tarea"*/
        Random r = new Random(); /*Objeto instanciado con la clase Random, lo usaremos para crear aleatoriamente los IDS*/

        private int CrearId()
        {
            int id;
            /*Se valida que el id no coincida (de casualidad) con uno anteriormente creado*/
            do{
                id = r.Next(1000, 9999);
            } while (tareas.Exists(t => t.Id == id));
            return id;
        }

        public string AgregarTarea(string descripcion, DateTime? fechaLimite)
        {
            if (tareas.Exists(t => t.Descripcion == descripcion)){ /*Valida que la descripcion sea diferente a las ya existentes*/
                return "\n== Esta tarea ya existe.";
            }
            if (fechaLimite <= DateTime.Now && fechaLimite != null){ /*Valida que la fecha no sea anterior a la actual siempre y cuando haya una fecha por validar*/
                return "\n== La fecha límite no puede ser anterior a la fecha actual.";
            }
            Tarea nuevaTarea = new Tarea(CrearId(), descripcion, fechaLimite, false); /*Creamos un objeto de clase "Tarea" con los datos proporcionados */
            tareas.Add(nuevaTarea);
            return "\n== Tarea agregada.";
        }

        public string ListarTareas()
        {
            if (tareas.Count != 0){
                /*Hacemos uso de StringBuilder para separar las citas segun su estado y para hacer un metodo que retorne un string en vez de un procedimiento (void)*/
                StringBuilder listaTareas = new StringBuilder();
                StringBuilder tareasPendientes = new StringBuilder();
                StringBuilder tareasCompletadas = new StringBuilder();
                
                foreach (var tarea in tareas) /*Si el estado es true, la tarea esta completa, de lo contrario esta pendiente*/
                {             
                    if (tarea.Estado){
                        tareasCompletadas.Append("\n  " + tarea.Id + "  " + tarea.Descripcion);
                    }
                    else{
                        string fecha = "No definida"; /*Si no se agrego una fecha se mostrara como "No definida"*/
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
                Tarea tarea = BuscarTarea(identificador); /*Busca la tarea segun el identificador enviado (la descripción o el ID)*/
                if (tarea == null){
                    return "\n== La tarea especificada no existe, verifica el campo enviado.";
                }
                if (!tarea.Estado){ /*Valida si la tarea ya esta completada, de lo contrario se eliminara y se agregara con su nuevo estado (true)*/
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
                Tarea tarea = BuscarTarea(identificador); /*Trae la tarea segun su descripción o su ID*/
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
            for (int i = 0; i < tareas.Count; i++){ /*Busca la primera tarea que conincida con el identificador*/
                if (identidicador.ToLower() == tareas[i].Descripcion.ToLower() || identidicador == tareas[i].Id.ToString()){
                    return tareas[i];
                }
            }
            return null;
        }

    }
}
