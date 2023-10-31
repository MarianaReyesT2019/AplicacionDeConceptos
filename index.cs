using System;
using System.Collections.Generic;

public class Tarea
{
    public int Id { get; set; }
    public string Descripcion { get; set; }
    public bool Completada { get; set; }
}

public interface ITareaManager
{
    List<Tarea> ObtenerTareasPendientes();
    void AgregarTarea(string descripcion);
    void CompletarTarea(int id);
    void EliminarTarea(int id);
}

public class TareaManager : ITareaManager
{
    private List<Tarea> tareas;

    public TareaManager()
    {
        tareas = new List<Tarea>();
    }

    public List<Tarea> ObtenerTareasPendientes()
    {
        return tareas.FindAll(t => !t.Completada);
    }

    public void AgregarTarea(string descripcion)
    {
        int nuevoId = tareas.Count + 1;
        Tarea nuevaTarea = new Tarea { Id = nuevoId, Descripcion = descripcion };
        tareas.Add(nuevaTarea);
    }

    public void CompletarTarea(int id)
    {
        Tarea tarea = tareas.Find(t => t.Id == id);
        if (tarea != null)
        {
            tarea.Completada = true;
        }
    }

    public void EliminarTarea(int id)
    {
        Tarea tarea = tareas.Find(t => t.Id == id);
        if (tarea != null)
        {
            tareas.Remove(tarea);
        }
    }
}

public class Program
{
    static void Main(string[] args)
    {
        ITareaManager tareaManager = new TareaManager();
        bool salir = false;

        while (!salir)
        {
            Console.WriteLine("===== Aplicación de Gestión de Tareas =====");
            Console.WriteLine("1. Mostrar tareas pendientes");
            Console.WriteLine("2. Agregar tarea");
            Console.WriteLine("3. Completar tarea");
            Console.WriteLine("4. Eliminar tarea");
            Console.WriteLine("5. Salir");

            Console.Write("Selecciona una opción: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    List<Tarea> tareasPendientes = tareaManager.ObtenerTareasPendientes();
                    foreach (var tarea in tareasPendientes)
                    {
                        Console.WriteLine($"ID: {tarea.Id}, Descripción: {tarea.Descripcion}");
                    }
                    break;

                case "2":
                    Console.Write("Ingrese la descripción de la nueva tarea: ");
                    string descripcion = Console.ReadLine();
                    tareaManager.AgregarTarea(descripcion);
                    Console.WriteLine("Tarea agregada con éxito.");
                    break;

                case "3":
                    Console.Write("Ingrese el ID de la tarea a completar: ");
                    if (int.TryParse(Console.ReadLine(), out int idCompletar))
                    {
                        tareaManager.CompletarTarea(idCompletar);
                        Console.WriteLine("Tarea completada.");
                    }
                    else
                    {
                        Console.WriteLine("ID no válido.");
                    }
                    break;

                case "4":
                    Console.Write("Ingrese el ID de la tarea a eliminar: ");
                    if (int.TryParse(Console.ReadLine(), out int idEliminar))
                    {
                        tareaManager.EliminarTarea(idEliminar);
                        Console.WriteLine("Tarea eliminada.");
                    }
                    else
                    {
                        Console.WriteLine("ID no válido.");
                    }
                    break;

                case "5":
                    salir = true;
                    break;

                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }

            Console.WriteLine();
        }
    }
}
