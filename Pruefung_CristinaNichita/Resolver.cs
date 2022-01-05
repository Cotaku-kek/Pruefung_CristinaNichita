using System;
using System.Collections.Generic;
using System.Text;

using Autofac;

namespace Pruefung_CristinaNichita
{
    static class Resolver
    {
        private static IContainer container;
        public static void initialize(IContainer inContainer)
        {
            Resolver.container = inContainer;
        }

        public static T Resolve<T>()
        {
            return Resolver.container.Resolve<T>();
        }
    }
}
