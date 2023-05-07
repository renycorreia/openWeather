using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace openWeather.Apis.Suporte
{
    public static class ServiceLocator
    {
        private static ServiceLocatorProvider currentProvider;

        public static void SetLocatorProvider(ServiceLocatorProvider newProvider)
        {
            currentProvider = newProvider;
        }

        public static IServiceLocator Current
        {
            get
            {
                return currentProvider();
            }
        }

        public delegate IServiceLocator ServiceLocatorProvider();
    }
}
