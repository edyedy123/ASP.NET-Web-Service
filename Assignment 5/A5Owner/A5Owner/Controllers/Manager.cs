using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using A5Owner.Models;
using System.Security.Claims;

namespace A5Owner.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        // AutoMapper components
        MapperConfiguration config;
        public IMapper mapper;

        // Request user property...

        // Backing field for the property
        private RequestUser _user;

        // Getter only, no setter
        public RequestUser User
        {
            get
            {
                // On first use, it will be null, so set its value
                if (_user == null)
                {
                    _user = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);
                }
                return _user;
            }
        }
        public Manager()
        {
            // If necessary, add your own code here

            // Configure AutoMapper...
            config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Employee, EmployeeBase>();

                cfg.CreateMap<Models.Smartphone, Controllers.SmartphoneBase>();
                cfg.CreateMap<Controllers.SmartphoneAdd, Models.Smartphone>();

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
        public IEnumerable<SmartphoneBase> SmartphoneGetAll()
        {
            var c = ds.Smartphones.Where(n => n.Owner == User.Name);

            return mapper.Map<IEnumerable<SmartphoneBase>>(c.OrderBy(n => n.Id));
        }
        public SmartphoneBase SmartphoneGetById(int id)
        {
            // Attempt to fetch the object
            var o = ds.Smartphones.SingleOrDefault
                (n => n.Id == id && n.Owner == User.Name);

            // Return the result, or null if not found
            return (o == null) ? null : mapper.Map<SmartphoneBase>(o);

        }
        public SmartphoneBase SmartphoneAdd(SmartphoneAdd newItem)
        {
            // Ensure that we can continue
            if (newItem == null)
            {
                return null;
            }
            else
            {
                // Add the new object
                Smartphone addedItem = mapper.Map<Smartphone>(newItem);

                // Assign the owner
                addedItem.Owner = User.Name;

                ds.Smartphones.Add(addedItem);
                ds.SaveChanges();

                // Return the object
                return mapper.Map<SmartphoneBase>(addedItem);
            }
        }

    }
}