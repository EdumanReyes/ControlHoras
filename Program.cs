using controlHoras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controlHoras
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Developer> developers = new List<Developer>();
            List<Project> projects = new List<Project>();
            List<Assignment> assignments = new List<Assignment>();

            // Método para registrar desarrolladores
            void AddDeveloper(string name, DateTime dateAdmission, string email, string category)
            {
                Developer developer = new Developer(name, dateAdmission, email, category);
                developers.Add(developer);
            }

            // Método para crear proyectos
            void AddProject(string name, string category, int duration, DateTime dateStart)
            {
                Project project = new Project(name, category, duration, dateStart);
                projects.Add(project);
            }

            // Método para dar de baja a desarrolladores
            void DeleteDeveloper(Developer developer)
            {
                developers.Remove(developer);
            }

            // Método para eliminar proyectos
            void DeleteProject(Project project)
            {
                projects.Remove(project);
            }

            // Método para ver todos los desarrolladores registrados
            void ViewAllDevelopers(List<Developer> desarrolladores)
            {
                Console.WriteLine("Desarrolladores registrados:");
                foreach (Developer developer in desarrolladores)
                {
                    Console.WriteLine($"Nombre: {developer.GetName()}, Fecha de ingreso: {developer.GetFechaIngreso()}, Email: {developer.GetEmail()}, Categoría: {developer.GetCategory()}");
                }
            }

            // Método para ver todos los proyectos registrados
            void ViewAllProjects(List<Project> proyectos)
            {
                Console.WriteLine("Proyectos registrados:");
                foreach (Project proyecto in proyectos)
                {
                    Console.WriteLine($"Nombre: {proyecto.GetName()}, Categoría: {proyecto.GetCategory()}, Duración en horas: {proyecto.GetDuration()}, Fecha de inicio: {proyecto.GetDateStart()}");
                }
            }

            // Agregar desarrollador
            AddDeveloper("Guillermo", new DateTime(2023, 2, 05), "dev1@example.com", "Nivel 3");
            AddDeveloper("Mario", new DateTime(2023, 2, 05), "dev1@example.com", "Nivel 2");
            AddDeveloper("Jose", new DateTime(2023, 2, 05), "dev1@example.com", "Nivel 1");
            // Crear proyecto
            AddProject("Proyecto 1", "Nivel 2", 50, new DateTime(2023, 2, 3));
            AddProject("Proyecto 2", "Nivel 1", 50, new DateTime(2023, 2, 3));
            AddProject("Proyecto 3", "Nivel 3", 50, new DateTime(2023, 2, 3));
            
            // Asignaciones de proyectos a desarrolladores
            Developer desarrollador1 = developers[0];
            Developer desarrollador2 = developers[1];
            Developer desarrollador3 = developers[2];

            Project proyecto1 = projects[0];
            Project proyecto2 = projects[1];
            Project proyecto3 = projects[2];

            desarrollador1.AssignmentProject(proyecto1);
            desarrollador2.AssignmentProject(proyecto2);
            desarrollador3.AssignmentProject(proyecto3);

            Assignment asignacion1 = new Assignment(desarrollador1, proyecto1, 0);
            Assignment asignacion2 = new Assignment(desarrollador2, proyecto2, 0);
            Assignment asignacion3 = new Assignment(desarrollador3, proyecto3, 0);
            
            assignments.Add(asignacion1);
            assignments.Add(asignacion2);
            assignments.Add(asignacion3);



            // Capturar las horas
            Console.WriteLine("Ingrese las horas trabajadas en el proyecto:");
            int horasTrabajadas = Convert.ToInt32(Console.ReadLine());

            // Asignación del proyecto actual para el desarrollador
            Assignment asignacionActual = asignacion1;

            // Registrar horas trabajadas en el proyecto
            asignacionActual.recordHours(horasTrabajadas);

            // Consultar horas registradas 
            asignacionActual.checkHours();


            // Consultar todos los desarrolladores registrados
            Console.WriteLine("\nConsulta de Desarrolladores\n");
            ViewAllDevelopers(developers);


            // Ver todos los proyectos registrados
            Console.WriteLine("\nConsulta de Proyectos\n");
            ViewAllProjects(projects);



            Console.ReadKey();
        }
    }

    public class Developer {
        private string name;
        private DateTime dateAdmission;
        private string email;
        protected string category;

        public Developer(string name, DateTime dateAdmission, string email, string category)
        {
            this.name = name;
            this.dateAdmission = dateAdmission;
            this.email = email;
            this.category = category;
        }
        
        public string GetName()
        {
            return name;
        }
        public void SetName(string nombre)
        {
            name = nombre;
        }

        public DateTime GetFechaIngreso()
        {
            return dateAdmission;
        }

        public void SetFechaIngreso(DateTime fechaIngreso)
        {
            dateAdmission = fechaIngreso;
        }

        public string GetEmail()
        {
            return email;
        }

        public void SetEmail(string new_email)
        {
            email = new_email;
        }

        public string GetCategory()
        {
            return category;
        }

        public void SetCategory(string categoria)
        {
            category = categoria;
        }

        public void AssignmentProject(Project proyecto)
        {
            // Verificación: proyecto ya tiene un desarrollador asignado
            if (proyecto.GetDeveloper() != null)
            {
                Console.WriteLine("Error!... El proyecto ya tiene un desarrollador asignado.");
                return;
            }

            // Se asigna el proyecto al desarrollador
            proyecto.SetDesarrollador(this);
            Console.WriteLine($"El proyecto {proyecto.GetName()} ha sido asignado al desarrollador {name}");
        }


    }


    public class Project
    {
        private string name;
        private string category;
        private int duration;
        private DateTime dateStart;
        private Developer developer;

        public Project(string name, string category, int duration, DateTime dateStart)
        {
            this.name = name;
            this.category = category;
            this.duration = duration;
            this.dateStart = dateStart;
            developer = null;
        }


        public string GetName()
        {
            return name;
        }

        public void SetName(string nombre)
        {
            this.name = nombre;
        }

        public string GetCategory()
        {
            return category;
        }

        public void SetCategory(string categoria)
        {
            this.category = categoria;
        }

        public int GetDuration()
        {
            return duration;
        }

        public void SetDuration(int duracion)
        {
            this.duration = duracion;
        }

        public DateTime GetDateStart()
        {
            return dateStart;
        }

        public void SetDateStart(DateTime fechaInicio)
        {
            this.dateStart = fechaInicio;
        }


        public Developer GetDeveloper()
        {
            return developer;
        }

        public void SetDesarrollador(Developer desarrollador)
        {
            this.developer = desarrollador;
        }

    }

    public class Assignment {
        public Developer developer;
        public Project project;
        public int assignedHours;

        public Assignment(Developer developer, Project project, int assignedHours) {
            this.developer = developer;
            this.project = project;
            this.assignedHours = assignedHours;
        }

        public void recordHours(int horasTrabajadas)
        {
            if (assignedHours + horasTrabajadas <= project.GetDuration())
            {
                assignedHours += horasTrabajadas;
                Console.WriteLine("Se han registrado las horas con éxito");
            }
            else
            {
                Console.WriteLine("¡Error!\nLas horas registradas exceden la duración del proyecto");
            }
        }

        public void checkHours(){
            Console.WriteLine($"Horas registradas por {developer.GetName()} en el proyecto {project.GetName()}: {assignedHours} horas");
        }
    }


   
}
