
using Autofac;
using Autofac.Integration.Wcf;
using Business.Services;
using Contracts.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Repository;
using System.IO;
using System;
using System.Web.UI.WebControls.WebParts;

namespace PersonWebService
{
    public static class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            // Registrar dependencias
            builder.RegisterType<PersonRepository>().As<IPersonRepository>()
                   .WithParameter("jsonFilePath", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "Persons.json"));

            builder.RegisterType<PersonService>();

            // Construir el contenedor y configurarlo para WCF
            var container = builder.Build();
            AutofacHostFactory.Container = container;
        }
    }
}
