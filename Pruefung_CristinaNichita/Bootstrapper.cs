using System;
using System.Collections.Generic;
using System.Text;

using Autofac;
using System.Linq;
using System.Reflection;

using Pruefung_CristinaNichita.Models;
using Pruefung_CristinaNichita.Views;
using Pruefung_CristinaNichita.ViewModels;
using Pruefung_CristinaNichita.Repositories;
using Pruefung_CristinaNichita.Services;

namespace Pruefung_CristinaNichita
{
    public abstract class Bootstrapper
    {
        protected ContainerBuilder ContainerBuilder { get; set; }
        public Bootstrapper()
        {
            Initialize();
            FinishInitialize();
        }

        protected virtual void Initialize()
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            ContainerBuilder = new ContainerBuilder();

            foreach (var type in currentAssembly.DefinedTypes.Where(e => e.IsSubclassOf(typeof(Xamarin.Forms.Page)) || e.IsSubclassOf(typeof(ViewModel))))
            {
                ContainerBuilder.RegisterType(type.AsType());
            }

            ContainerBuilder.RegisterType<ForecastItem>().SingleInstance();
        }

        protected virtual void FinishInitialize()
        {
            var container = ContainerBuilder.Build();
            Resolver.initialize(container);
        }
    }
}