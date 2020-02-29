using Application;
using AutoMapper;
using Departments.Views.Departments.DepartmentDetails;
using Departments.Views.Employees.AddEmployee;
using Departments.Views.Main;
using DryIoc;
using Microsoft.Extensions.Configuration;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using DAL.EF;
using Departments.Views.Employees.EditEmployee;

namespace Departments
{
    class DepartmentModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IApplicationStateManager, ApplicationStateManager>();

            containerRegistry.RegisterForNavigation<MainView>();
            containerRegistry.RegisterForNavigation<DepartmentDetailsView>();
            containerRegistry.RegisterForNavigation<AddEmployeeView>();
            containerRegistry.RegisterForNavigation<EditEmployeeView>();

            RegisterConfig(containerRegistry);
        }

        private void RegisterConfig(IContainerRegistry containerRegistry)
        {
            var configurationBuilder = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json");

            var configuration = configurationBuilder.Build();
            containerRegistry.RegisterInstance<IConfiguration>(configuration);
        }
    }
}
