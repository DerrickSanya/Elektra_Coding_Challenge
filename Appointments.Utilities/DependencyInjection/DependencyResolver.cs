namespace Appointments.Utilities.DependencyInjection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// DependencyResolver
    /// </summary>
    public sealed class DependencyResolver
    {
        /// <summary>
        /// duplicateBindingError
        /// </summary>
        private const string DuplicateBindingError = "The bind you are trying to add already exists {0}";

        /// <summary>
        /// _currentServiceProvider
        /// </summary>
        private readonly ServiceProvider _currentServiceProvider;

        /// <summary>
        /// _serviceProvider
        /// </summary>
        private static ServiceProvider _serviceProvider;

        /// <summary>
        /// DependencyResolver
        /// </summary>
        /// <param name="currentServiceProvider"></param>
        public DependencyResolver(ServiceProvider currentServiceProvider)
        {
            _currentServiceProvider = currentServiceProvider;
        }

        /// <summary>
        /// 
        /// </summary>
        public static DependencyResolver Current => new DependencyResolver(_serviceProvider);

        /// <summary>
        /// SetResolver
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static void SetResolver(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// GetInstance
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public object GetInstance(Type serviceType)
        {
            return _currentServiceProvider.GetService(serviceType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public TService GetInstance<TService>()
        {
            return (TService)_currentServiceProvider.GetService(typeof(TService));
        }

        /// <summary>
        /// Load
        /// </summary>
        public void Load()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// AddModule
        /// </summary>
        /// <param name="services"></param>
        /// <param name="bindings"></param>
        /// <returns></returns>
        public static void AddModules(IServiceCollection services, IList<ServiceDescriptor> bindings)
        {
           // object servicesinCollection = _currentServiceProvider.GetServices;
            if (!bindings.Any())
                return;

            foreach (var binding in bindings)
            {
                if (services.Contains(binding))
                    throw new ArgumentException(DuplicateBindingError, binding.ToString());
                services.Add(binding);
            }
        }

        /// <summary>
        /// AddBinding
        /// </summary>
        /// <param name="services"></param>
        /// <param name="binding"></param>
        public static void AddBinding(IServiceCollection services, ServiceDescriptor binding)
        {
            // object servicesinCollection = _currentServiceProvider.GetServices;
            if (binding == null)
                throw new ArgumentNullException("");
   
             if (services.Contains(binding))
                    throw new ArgumentException(DuplicateBindingError, binding.ToString());
              services.Add(binding);          
        }

        /// <summary>
        /// GetAllBindings
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IList<ServiceDescriptor> GetAllBindings(IServiceCollection services)
        {
            return services.ToList<ServiceDescriptor>();
        }
    }
}