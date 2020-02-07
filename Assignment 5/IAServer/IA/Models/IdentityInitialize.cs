﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using IA.Controllers;

namespace IA.Models
{
    public static class IdentityInitialize
    {
        // Load user accounts
        public static async void LoadUserAccounts()
        {
            // Get a reference to the objects we need
            var ds = new ApplicationDbContext();
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(ds));

            // Add the user(s) that the app needs when loaded for the first time
            // Copy-paste a code block below, and edit as necessary
            // Change any of the data below to better match your app's needs
            // Notice - the app already includes the "uam" and "dev" user accounts
            // That's why the comparison below uses the value "2" 
            if (userManager.Users.Count() == 2)
            {
                /*
                // User account manager...
                var uam = new ApplicationUser { UserName = "uam@example.com", Email = "uam@example.com" };
                var uamResult = await userManager.CreateAsync(uam, "Password123!");
                if (uamResult.Succeeded)
                {
                    // Add claims
                    await userManager.AddClaimAsync(uam.Id, new Claim(ClaimTypes.Email, "uam@example.com"));
                    await userManager.AddClaimAsync(uam.Id, new Claim(ClaimTypes.Role, "UserAccountManager"));
                    await userManager.AddClaimAsync(uam.Id, new Claim(ClaimTypes.GivenName, "User Account"));
                    await userManager.AddClaimAsync(uam.Id, new Claim(ClaimTypes.Surname, "Manager"));
                }

                // Developer/programmer...
                var dev = new ApplicationUser { UserName = "dev@example.com", Email = "dev@example.com" };
                var devResult = await userManager.CreateAsync(dev, "Password123!");
                if (devResult.Succeeded)
                {
                    // Add claims
                    await userManager.AddClaimAsync(dev.Id, new Claim(ClaimTypes.Email, "dev@example.com"));
                    await userManager.AddClaimAsync(dev.Id, new Claim(ClaimTypes.Role, "Developer"));
                    await userManager.AddClaimAsync(dev.Id, new Claim(ClaimTypes.GivenName, "App"));
                    await userManager.AddClaimAsync(dev.Id, new Claim(ClaimTypes.Surname, "Developer"));
                }
                */

                // Eduardo's User...
                var easorozabal = new ApplicationUser { UserName = "easorozabal@example.com", Email = "easorozabal@example.com" };
                var easorozabalResult = await userManager.CreateAsync(easorozabal, "Password123!");
                if (easorozabalResult.Succeeded)
                {
                    // Add claims
                    await userManager.AddClaimAsync(easorozabal.Id, new Claim(ClaimTypes.Email, "easorozabal@example.com"));
                    await userManager.AddClaimAsync(easorozabal.Id, new Claim(ClaimTypes.Role, "Developer"));
                    await userManager.AddClaimAsync(easorozabal.Id, new Claim(ClaimTypes.GivenName, "Web Services"));
                    await userManager.AddClaimAsync(easorozabal.Id, new Claim(ClaimTypes.Surname, "easorozabal"));
                }
            }
        }

        // Load app claims

        public static void LoadAppClaims()
        {
            // Get a reference to the manager
            Manager m = new Manager();

            // If there are no claims, add them
            if (m.AppClaimGetAll().Count() == 0)
            {
                // Add the app's allowed claims here

                //ROLES
                var Employee = new AppClaimAdd();
                Employee.Description = "Employee";
                Employee.ClaimType = "Role";
                Employee.ClaimTypeUri = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
                Employee.ClaimValue = "Employee";
                m.AppClaimAdd(Employee);

                var Customer = new AppClaimAdd();
                Customer.Description = "Customer";
                Customer.ClaimType = "Role";
                Customer.ClaimTypeUri = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
                Customer.ClaimValue = "Customer";
                m.AppClaimAdd(Customer);

                //OU Departments
                var Sales = new AppClaimAdd();
                Sales.Description = "Sales";
                Sales.ClaimType = "OU";
                Sales.ClaimTypeUri = "http://schemas.microsoft.com/ws/2008/06/identity/claims/OU";
                Sales.ClaimValue = "Sales";
                m.AppClaimAdd(Customer);

                var Returns = new AppClaimAdd();
                Returns.Description = "Returns";
                Returns.ClaimType = "OU";
                Returns.ClaimTypeUri = "http://schemas.microsoft.com/ws/2008/06/identity/claims/OU";
                Returns.ClaimValue = "Returns";
                m.AppClaimAdd(Returns);

                var HumanResourses = new AppClaimAdd();
                HumanResourses.Description = "HumanResourses";
                HumanResourses.ClaimType = "OU";
                HumanResourses.ClaimTypeUri = "http://schemas.microsoft.com/ws/2008/06/identity/claims/OU";
                HumanResourses.ClaimValue = "HumanResourses";
                m.AppClaimAdd(HumanResourses);

                //OU Cities
                var Toronto = new AppClaimAdd();
                Toronto.Description = "Toronto";
                Toronto.ClaimType = "OU";
                Toronto.ClaimTypeUri = "http://schemas.microsoft.com/ws/2008/06/identity/claims/OU";
                Toronto.ClaimValue = "Toronto";
                m.AppClaimAdd(Toronto);

                var Quebec = new AppClaimAdd();
                Quebec.Description = "Quebec";
                Quebec.ClaimType = "OU";
                Quebec.ClaimTypeUri = "http://schemas.microsoft.com/ws/2008/06/identity/claims/OU";
                Quebec.ClaimValue = "Quebec";
                m.AppClaimAdd(Quebec);

                var NewYork = new AppClaimAdd();
                NewYork.Description = "NewYork";
                NewYork.ClaimType = "OU";
                NewYork.ClaimTypeUri = "http://schemas.microsoft.com/ws/2008/06/identity/claims/OU";
                NewYork.ClaimValue = "NewYork";
                m.AppClaimAdd(NewYork);

                //Tasks
                var CustomerEdit = new AppClaimAdd();
                CustomerEdit.Description = "CustomerEdit- edit a customer";
                CustomerEdit.ClaimType = "Task";
                CustomerEdit.ClaimTypeUri = "http://schemas.microsoft.com/ws/2008/06/identity/claims/Task";
                CustomerEdit.ClaimValue = "CustomerEdit";
                m.AppClaimAdd(CustomerEdit);

                var SalesView = new AppClaimAdd();
                SalesView.Description = "SalesView - see all sales";
                SalesView.ClaimType = "Task";
                SalesView.ClaimTypeUri = "http://schemas.microsoft.com/ws/2008/06/identity/claims/Task";
                SalesView.ClaimValue = "SalesView";
                m.AppClaimAdd(SalesView);

                var CustomerView = new AppClaimAdd();
                CustomerView.Description = "CustomerView - see al customers";
                CustomerView.ClaimType = "Task";
                CustomerView.ClaimTypeUri = "http://schemas.microsoft.com/ws/2008/06/identity/claims/Task";
                CustomerView.ClaimValue = "CustomerView";
                m.AppClaimAdd(CustomerView);
            }
        }

    }
}