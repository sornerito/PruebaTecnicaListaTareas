using System;

namespace PruebaTecnicaListarTareas
{
    public class Program
    {
        static void Main(string[] args)
        {
            string opcion;
            GestionTarea lista1 = new GestionTarea(); /*Creamos un objeto con la clase GestionTarea*/

            do{
                Menu(); /*Mostramos el menú cada vez que se ejecute una acción*/
                do{ /*Solicita la opción y valida que esta opción existe maediante un método*/
                    Console.WriteLine("\nElige una opción: ");
                    opcion = Console.ReadLine();
                    if (!ValidarOpcion(opcion)){
                        Console.WriteLine("\n== La opción es errónea.");
                    }
                } while (!ValidarOpcion(opcion));

                switch (opcion){ 
                    case "1":
                        /*Solicitamos los campos, validamos que no esten vacios haciendo uso del método "ValidarCampoVacio"*/
                        Console.WriteLine("\nIngrese la descripción de la tarea: ");
                        string descripcion = ValidarCampoVacio("descripción");
                        DateTime? fechaLimite = null;
                        string fecha;

                        bool fechaValida = false;
                        while (!fechaValida){ /*Mientras que no se haya ingresado una fecha valida (o una opcion diferente a "n")
                                               se preguntara de nuevo por la fecha*/
                            Console.WriteLine("\nIngrese la fecha límite (YYYY-MM-DD) de la tarea o ingrese \"n\" para omitirlo: ");
                            fecha = ValidarCampoVacio("fecha");

                            if (fecha.ToLower() == "n"){
                                fechaValida = true;
                            }
                            else{
                                if (DateTime.TryParse(fecha, out DateTime fechaParseada)){ /*Intentara parsear el valor ingresado*/
                                    fechaLimite = fechaParseada;
                                    fechaValida = true;
                                }
                                else{
                                    Console.WriteLine("\n== La fecha límite no es válida. Inténtalo de nuevo.");
                                }
                            }
                        }

                        string resultado = lista1.AgregarTarea(descripcion, fechaLimite); /*Hace uso del metodo en la clase para agregar la tarea*/
                        Console.WriteLine(resultado);
                        break;
                    case "2": /*Lista las tareas*/
                        Console.WriteLine(lista1.ListarTareas());
                        break;
                    case "3":
                        Console.WriteLine("\nIngresa la descripción de la tarea o el id: ");
                        string respuestaMarcarTarea = lista1.MarcarTarea(ValidarCampoVacio("identificador")); /*Solicita un identificador para modificar
                                                                                                               el estado de la tarea*/
                        Console.WriteLine(respuestaMarcarTarea);
                        break;
                    case "4":
                        Console.WriteLine("\nIngresa la descripción de la tarea o el id: ");
                        /*Solicita un identificador para borrar la tarea*/
                        string respuestaEliminarTarea = lista1.EliminarTarea(ValidarCampoVacio("identificador"));
                        Console.WriteLine(respuestaEliminarTarea);
                        break;
                    case "5":
                        Console.WriteLine("\n== Saliendo del sistema..."); /*Sale del ciclo, terminando con el aplicativo*/
                        return;
                    default:
                        Console.WriteLine("\n== Opción no válida.");
                        break;
                }
            } while (true);
        }

        static public string ValidarCampoVacio(string nombreCampo) /*Método que valida que la variable que se esta ingresando no este vacio*/
        {
            string variable;
            do {
                variable = Console.ReadLine();
                if (variable == null || variable == "" || variable == " "){
                    Console.WriteLine("\n== El campo " + nombreCampo + " no puede estar vacio.");
                }
            } while (variable == null || variable == "" || variable == " ");
            
            return variable;
        }
        static bool ValidarOpcion(string opcion) /*Método que valida si la opción ingresada es válida.*/
        {
            return opcion == "1" || opcion == "2" || opcion == "3" || opcion == "4" || opcion == "5";
        }

        static public void Menu()
        {
            Console.WriteLine("\n===== LISTA DE TAREAS =====\n" +
                "1. Agregar tarea\n" +
                "2. Mostrar lista de tareas\n" +
                "3. Marcar Tarea como completada\n" +
                "4. Eliminar tarea\n" +
                "5. Salir del sistema");
        }
    }
    
}
