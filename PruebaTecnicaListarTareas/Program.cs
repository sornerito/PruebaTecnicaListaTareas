using System;

namespace PruebaTecnicaListarTareas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string opcion;
            GestionTarea lista1 = new GestionTarea();

            do{
                menu();
                do{
                    Console.WriteLine("\nElige una opción: ");
                    opcion = Console.ReadLine();
                    if (!ValidarOpcion(opcion)){
                        Console.WriteLine("\n== La opción es errónea.");
                    }
                } while (!ValidarOpcion(opcion));

                switch (opcion){
                    case "1":
                        Console.WriteLine("\nIngrese la descripción de la tarea: ");
                        string descripcion = ValidarCampoVacio("descripción");
                        DateTime? fechaLimite = null;
                        string fecha;

                        bool fechaValida = false;
                        while (!fechaValida){
                            Console.WriteLine("\nIngrese la fecha límite (YYYY-MM-DD) de la tarea o ingrese \"n\" para omitirlo: ");
                            fecha = ValidarCampoVacio("fecha");

                            if (fecha.ToLower() == "n"){
                                fechaValida = true;
                            }
                            else{
                                if (DateTime.TryParse(fecha, out DateTime fechaParseada)){
                                    fechaLimite = fechaParseada;
                                    fechaValida = true;
                                }
                                else{
                                    Console.WriteLine("\n== La fecha límite no es válida. Inténtalo de nuevo.");
                                }
                            }
                        }

                        string resultado = lista1.AgregarTarea(descripcion, fechaLimite);
                        Console.WriteLine(resultado);
                        break;
                    case "2":
                        Console.WriteLine(lista1.ListarTareas());
                        break;
                    case "3":
                        Console.WriteLine("\nIngresa la descripción de la tarea o el id: ");
                        string respuestaMarcarTarea = lista1.MarcarTarea(ValidarCampoVacio("identificador"));
                        Console.WriteLine(respuestaMarcarTarea);
                        break;
                    case "4":
                        Console.WriteLine("\nIngresa la descripción de la tarea o el id: ");
                        string respuestaEliminarTarea = lista1.EliminarTarea(ValidarCampoVacio("identificador"));
                        Console.WriteLine(respuestaEliminarTarea);
                        break;
                    case "5":
                        Console.WriteLine("\n== Saliendo del sistema...");
                        return;
                    default:
                        Console.WriteLine("\n== Opción no válida.");
                        break;
                }
            } while (true);
        }

        static public string ValidarCampoVacio(string nombreCampo)
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
        static bool ValidarOpcion(string opcion)
        {
            return opcion == "1" || opcion == "2" || opcion == "3" || opcion == "4" || opcion == "5";
        }

        static public void menu()
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
