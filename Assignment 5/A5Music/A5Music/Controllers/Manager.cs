using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using A5Music.Models;
using System.Security.Claims;

namespace A5Music.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        // AutoMapper components
        MapperConfiguration config;
        public IMapper mapper;

        public Manager()
        {
            // If necessary, add your own code here

            // Configure AutoMapper...
            config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Employee, EmployeeBase>();

                cfg.CreateMap<Models.Employee, Controllers.EmployeeBase>();
                cfg.CreateMap<Controllers.EmployeeAdd, Models.Employee>();

            });

            mapper = config.CreateMapper();

            // Data-handling configuration

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>

        // Suggested naming convention: Entity + task/action
        // For example:
        // ProductGetAll()
        // ProductGetById()
        // ProductAdd()
        // ProductEdit()
        // ProductDelete()


        // Method templates, used by the ExampleController class

        public IEnumerable<string> ExampleGetAll()
        {
            return new List<string> { "hello", "world" };
        }

        public string ExampleGetById(int id)
        {
            return $"id {id} was requested";
        }

        public string ExampleAdd(string newItem)
        {
            return $"new item {newItem} was added";
        }

        public string ExampleEditSomething(string editedItem)
        {
            return $"item was edited with {editedItem}";
        }

        public bool ExampleDelete(int id)
        {
            return true;
        }

        //EMPLOYEES++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public IEnumerable<EmployeeBase> EmployeeGetAll()
        {
            var c = ds.Employees.OrderBy(e => e.EmployeeId);

            return mapper.Map<IEnumerable<EmployeeBase>>(c);
        }
        public EmployeeBase EmployeeGetById(int id)
        {
            // Attempt to fetch the object
            var o = ds.Employees.Find(id);

            // Return the result, or null if not found
            return (o == null) ? null : mapper.Map<EmployeeBase>(o);
        }
        public EmployeeBase EmployeeAdd(EmployeeAdd newItem)
        {
            // Ensure that we can continue
            if (newItem == null)
            {
                return null;
            }
            else
            {
                // Add the new object
                Employee addedItem = mapper.Map<Employee>(newItem);

                ds.Employees.Add(addedItem);
                ds.SaveChanges();

                // Return the object
                return mapper.Map<EmployeeBase>(addedItem);
            }
        }
        public void SetEmployeeSupervisor(EmployeeSupervisor item)
        {
            // Attention 33 - Must get a valid reference to both objects before continuing
            var user = HttpContext.Current.User as ClaimsPrincipal;
            var userName = user.Identity.Name;
            
            // Get a reference to the employee
            var employee = ds.Employees.Find(item.Employee);
            if (employee == null) { return; }

            // Get a reference to the supervisor
            var supervisor = ds.Employees.Find(item.Supervisor);
            if (supervisor == null) { return; }

            if (supervisor.Email != userName) { return; }

            // Make the changes, save, and exit
            employee.Employee2 = supervisor;
            employee.ReportsTo = supervisor.EmployeeId;
            ds.SaveChanges();
        }


        // Programmatically-generated objects

        // Can do this in one method, or in several
        // Call the method(s) from a controller method

        public bool LoadData()
        {
            /*
            // Return immediately if there's existing data
            if (ds.[entity collection].Courses.Count() > 0) { return false; }

            // Otherwise, add objects...

            ds.[entity collection].Add(new [whatever] { Property1 = "value", Property2 = "value" });
            */

            return ds.SaveChanges() > 0 ? true : false;
        }

    }
}