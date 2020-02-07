using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment_3.Models;

namespace Assignment_3.Controllers
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

                cfg.CreateMap<Models.InvoiceLine, Controllers.InvoiceLineBase>();

                cfg.CreateMap<Models.Invoice, Controllers.InvoiceBase>();
                cfg.CreateMap<Models.Invoice, Controllers.InvoiceWithData>();

                cfg.CreateMap<Models.Employee, Controllers.EmployeeBase>();
                cfg.CreateMap<Models.Employee, Controllers.EmployeeWithData>();
                cfg.CreateMap<Controllers.EmployeeAdd, Models.Employee> ();

                cfg.CreateMap<Models.Employee, Controllers.EmployeeWithMedia>();

                cfg.CreateMap<Models.Album, Controllers.AlbumBase>();
                cfg.CreateMap<Models.Album, Controllers.AlbumWithMedia>();
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
        // InvoiceLine ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public IEnumerable<InvoiceLine> InvoiceLineGetAll()
        {
            var c = ds.InvoiceLines.OrderBy(e => e.InvoiceLineId).Take(5);

            return mapper.Map<IEnumerable<InvoiceLine>>(c);
        }
        // Invoice ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public IEnumerable<InvoiceBase> InvoiceGetAll()
        {
            var c = ds.Invoices.OrderBy(e => e.InvoiceId).Take(5);

            return mapper.Map<IEnumerable<InvoiceBase>>(c);
        }
        public InvoiceWithData InvoiceGetById(int id)
        {
            var o = ds.Invoices
                .Include("InvoiceLines.Track.MediaType")
                .Include("InvoiceLines.Track.Album.Artist")
                .Include("Customer.Employee")
                .SingleOrDefault(e => e.InvoiceId == id);

            return (o == null) ? null : mapper.Map<InvoiceWithData>(o);
        }
        public bool InvoiceSetPDF(int id, string contentType, byte[] PDF)
        {
            // Notice the return type of this method - bool
            // This is an incremental attempt at improving the command pattern
            // In the controller, we could use the return value, if we wished

            // Ensure that we can continue
            if (string.IsNullOrEmpty(contentType) | PDF == null) { return false; }

            // Attempt to find the matching object
            var storedItem = ds.Invoices.Find(id);

            // Ensure that we can continue
            if (storedItem == null) { return false; }

            // Save the photo
            storedItem.ContentType = contentType;
            storedItem.PDF = PDF;

            // Attempt to save changes
            // Do you understand the following? If not, ask
            return (ds.SaveChanges() > 0) ? true : false;
        }
        // Employee ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public IEnumerable<EmployeeBase> EmployeeGetAll()
        {
            var c = ds.Employees.OrderBy(e => e.EmployeeId).Take(5);

            return mapper.Map<IEnumerable<EmployeeBase>>(c);
        }
        public EmployeeWithData EmployeeGetById(int id)
        {
            var o = ds.Employees
                .Include("Employee1")
                .Include("Employee2")
                .SingleOrDefault(e => e.EmployeeId == id);

            return (o == null) ? null : mapper.Map<EmployeeWithData>(o);
        }
        public EmployeeWithMedia EmployeeGetByIdWithMedia(int id)
        {
            // Attempt to fetch the object
            var o = ds.Employees
               .Include("Employee1")
               .Include("Employee2")
               .SingleOrDefault(e => e.EmployeeId == id);

            // Return the result, or null if not found
            return (o == null) ? null : mapper.Map<EmployeeWithMedia>(o);
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
        public void SetEmployeeSupervisor(EmployeeSupervisor item)
        {
            // Attention 33 - Must get a valid reference to both objects before continuing

            // Get a reference to the employee
            var employee = ds.Employees.Find(item.Employee);
            if (employee == null) { return; }

            // Get a reference to the supervisor
            var supervisor = ds.Employees.Find(item.Supervisor);
            if (supervisor == null) { return; }

            // Make the changes, save, and exit
            employee.Employee2 = supervisor;
            employee.ReportsTo = supervisor.EmployeeId;
            ds.SaveChanges();
        }
        public void SetDirectReports(EmployeeDirectReports item)
        {
            // Attention 33 - Must get a valid reference to both objects before continuing

            // Get a reference to the employee
            var employee = ds.Employees.Find(item.EmployeeId);
            if (employee == null) { return; }

            // Get a reference to the supervisor
            for (var i = 0; i < item.DirectReports.Count(); i++)
            { 
                var subordinate = ds.Employees.Find(item.DirectReports[i]);
                if (subordinate == null) { return; }
            }

            for (var i = 0; i < item.DirectReports.Count(); i++)
            {
                var subordinate = ds.Employees.Find(item.DirectReports[i]);
                subordinate.ReportsTo = item.EmployeeId;
                employee.Employee1.Add(subordinate);
            }

            // Make the changes, save, and exit
            ds.SaveChanges();
        }
        public void ClearDirectReports(EmployeeDirectReports item)
        {
            // Get a reference to the employee
            var employee = ds.Employees.Find(item.EmployeeId);
            if (employee == null) { return; }

            // Make the changes, save, and exit
                employee.Employee1 = null;
                ds.SaveChanges();

        }
        public bool EmployeeSetPhoto(int id, string contentType, byte[] photo)
        {
            // Notice the return type of this method - bool
            // This is an incremental attempt at improving the command pattern
            // In the controller, we could use the return value, if we wished

            // Ensure that we can continue
            if (string.IsNullOrEmpty(contentType) | photo == null) { return false; }

            // Attempt to find the matching object
            var storedItem = ds.Employees.Find(id);

            // Ensure that we can continue
            if (storedItem == null) { return false; }

            // Save the photo
            storedItem.ContentType = contentType;
            storedItem.Photo = photo;

            // Attempt to save changes
            // Do you understand the following? If not, ask
            return (ds.SaveChanges() > 0) ? true : false;
        }
        // Album ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public IEnumerable<AlbumBase> AlbumGetAll()
        {
            var c = ds.Albums.OrderBy(e => e.AlbumId);

            return mapper.Map<IEnumerable<AlbumBase>>(c);
        }
        public AlbumWithMedia AlbumGetByIdWithMedia(int id)
        {
            // Attempt to fetch the object
            var o = ds.Albums
               .SingleOrDefault(e => e.AlbumId == id);

            // Return the result, or null if not found
            return (o == null) ? null : mapper.Map<AlbumWithMedia>(o);
        }
        public bool AlbumSetPhoto(int id, string contentType, byte[] photo)
        {
            // Notice the return type of this method - bool
            // This is an incremental attempt at improving the command pattern
            // In the controller, we could use the return value, if we wished

            // Ensure that we can continue
            if (string.IsNullOrEmpty(contentType) | photo == null) { return false; }

            // Attempt to find the matching object
            var storedItem = ds.Albums.Find(id);

            // Ensure that we can continue
            if (storedItem == null) { return false; }

            // Save the photo
            storedItem.ContentType = contentType;
            storedItem.Photo = photo;

            // Attempt to save changes
            // Do you understand the following? If not, ask
            return (ds.SaveChanges() > 0) ? true : false;
        }
        // Track ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public IEnumerable<TrackBase> TrackGetAll()
        {
            var c = ds.Tracks.OrderBy(e => e.TrackId);

            return mapper.Map<IEnumerable<TrackBase>>(c);
        }
        public TrackWithMedia TrackGetByIdWithMedia(int id)
        {
            // Attempt to fetch the object
            var o = ds.Tracks
               .SingleOrDefault(e => e.TrackId == id);

            // Return the result, or null if not found
            return (o == null) ? null : mapper.Map<TrackWithMedia>(o);
        }
        public bool TrackSetAudio(int id, string contentType, byte[] Audio)
        {
            // Notice the return type of this method - bool
            // This is an incremental attempt at improving the command pattern
            // In the controller, we could use the return value, if we wished

            // Ensure that we can continue
            if (string.IsNullOrEmpty(contentType) | Audio == null) { return false; }

            // Attempt to find the matching object
            var storedItem = ds.Tracks.Find(id);

            // Ensure that we can continue
            if (storedItem == null) { return false; }

            // Save the photo
            storedItem.ContentType = contentType;
            storedItem.Audio = Audio;

            // Attempt to save changes
            // Do you understand the following? If not, ask
            return (ds.SaveChanges() > 0) ? true : false;
        }
    }
}