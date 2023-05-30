using controlHoras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace controlHoras
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Developer> developers = new List<Developer>();
            List<Project> projects = new List<Project>();
        

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
            AddProject("Proyecto 1", "Nivel 2", 30, new DateTime(2023, 2, 3));
            AddProject("Proyecto 2", "Nivel 1", 50, new DateTime(2023, 2, 3));
            AddProject("Proyecto 3", "Nivel 3", 80, new DateTime(2023, 2, 3));
            AddProject("Proyecto 4", "Nivel 2", 40, new DateTime(2022, 5, 23));
            // Asignaciones de proyectos a desarrolladores
            Developer desarrollador1 = developers[0];
            Developer desarrollador2 = developers[1];
            Developer desarrollador3 = developers[2];

            Project proyecto1 = projects[0];
            Project proyecto2 = projects[1];
            Project proyecto3 = projects[2];
            Project proyecto4 = projects[3];

            Console.WriteLine("\nAsignar Proyectos a desarrolladores\n");
            desarrollador1.AssignmentProject(proyecto1);
            desarrollador2.AssignmentProject(proyecto2);
            desarrollador3.AssignmentProject(proyecto3);

            desarrollador3.AssignmentProject(proyecto2); //Asignar al proyecto2 mas de un desarrollador (ERROR)
            desarrollador1.AssignmentProject(proyecto4);
           
            // Capturar las horas
            desarrollador1.RecordHours(proyecto1, 10);
            desarrollador2.RecordHours(proyecto2, 20);
            desarrollador3.RecordHours(proyecto3, 30);

            desarrollador3.RecordHours(proyecto3, 90); //Asignar mas de las horas de duración del proyecto(ERROR)

            //Consultar horas trabajadas en el proyecto asignado
            Console.WriteLine("\nConsultar Horas trabajadas\n");
            desarrollador1.checkHours(proyecto1);
            desarrollador1.checkHours(proyecto2);
            desarrollador2.checkHours(proyecto2);
            desarrollador3.checkHours(proyecto3);

            // Consultar todos los desarrolladores registrados
            Console.WriteLine("\nConsulta de Desarrolladores\n");
            ViewAllDevelopers(developers);


            // Ver todos los proyectos registrados
            Console.WriteLine("\nConsulta de Proyectos\n");
            ViewAllProjects(projects);

            // Ver los proyectos asignados al desarrollador
            Console.WriteLine("\nConsulta de Proyectos Asignados\n");
            desarrollador1.ViewAssignmentProyects();


            Console.ReadKey();
        }
    }

    public class Developer {
        private string name;
        private DateTime dateAdmission;
        private string email;
        protected string category;
        public List<Project> Projects { get; set; }

        public Developer(string name, DateTime dateAdmission, string email, string category)
        {
            this.name = name;
            this.dateAdmission = dateAdmission;
            this.email = email;
            this.category = category;
            Projects= new List<Project>();
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

        public void AssignmentProject(Project proyecto )
        {
            // Verificación: proyecto ya tiene un desarrollador asignado
            if (proyecto.GetDeveloper() != null)
            {
                Console.WriteLine("Error!... El proyecto ya tiene un desarrollador asignado");
             
                return;
            }

            // Se asigna el proyecto al desarrollador
            Projects.Add( proyecto );
            proyecto.SetDesarrollador(this);
            Console.WriteLine($"El proyecto {proyecto.GetName()} ha sido asignado al desarrollador {name}");
        }

        public void RecordHours(Project project, int hours) {
            if (Projects.Contains(project))
            {
                project.RecordHoursWorked(hours);
            }
            else {
                Console.WriteLine("\nError!... El proyecto no ha sido asignado a este desarrollador\n");
            }
        }

        public void checkHours(Project project)
        {
            if (Projects.Contains(project))
            {
                Console.WriteLine($"Horas trabajadas en el proyecto '{project.GetName()}': {project.GetHoursWorked()}");
                Console.WriteLine($"Horas restantes en el proyecto '{project.GetName()}': {project.GetDuration() - project.GetHoursWorked()}");
            }
            else { 
                Console.WriteLine("\nError!... El proyecto no ha sido asignado a este desarrollador\n");
            }
        }

        public void ViewAssignmentProyects()
        {
            Console.WriteLine($"Proyectos asignados al desarrollador '{name}':");
            foreach (var proyecto in Projects)
            {
                Console.WriteLine($"-> {proyecto.GetName()}");
            }
        }

    }


    public class Project
    {
        private string name;
        private string category;
        private int duration;
        private DateTime dateStart;
        private Developer developer;
        private int hoursWorked;

        public Project(string name, string category, int duration, DateTime dateStart)
        {
            this.name = name;
            this.category = category;
            this.duration = duration;
            this.dateStart = dateStart;
            hoursWorked= 0;
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

        public int GetHoursWorked()
        {
            return hoursWorked;
        }

        public void SetHoursWorked(int horas)
        {
            this.hoursWorked = horas;
        }

        public Developer GetDeveloper()
        {
            return developer;
        }

        public void SetDesarrollador(Developer desarrollador)
        {
            this.developer = desarrollador;
        }

        public void RecordHoursWorked(int hours) {
            if (hoursWorked + hours <= duration)
            {
                hoursWorked += hours;
            }
            else
            {
                Console.WriteLine("¡Error!\nLas horas registradas exceden la duración del proyecto");
            }

        }
    }
   
}
