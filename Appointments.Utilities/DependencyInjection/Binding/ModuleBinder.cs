namespace Appointments.Utilities.DependencyInjection.Binding
{
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// ModuleBinder
    /// </summary>
    public abstract class ModuleBinder
    {
        /// <summary>
        /// bindings
        /// </summary>
        private readonly List<ServiceDescriptor> _bindings;

        /// <summary>
        /// Initializes a new instance of the <see>
        ///     <cref>NinjectModule</cref>
        /// </see>
        /// class.
        /// </summary>
        protected ModuleBinder()
        {
            _bindings = new List<ServiceDescriptor>();
            Load(); // always calls the overriden method that adds the bindings
        }

        /// <summary>
        /// Gets the module's name. Only a single module with a given name can be loaded at one time.
        /// </summary>
        public virtual string Name => GetType().FullName;

        /// <summary>
        /// Gets the bindings that were registered by the module.
        /// </summary>
        public ICollection<ServiceDescriptor> Bindings => _bindings;

        /// <summary>
        /// Called after loading the modules. A module can verify here if all other required modules are loaded.
        /// </summary>
        public void OnVerifyRequiredModules()
        {
            VerifyRequiredModulesAreLoaded();
        }

        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public abstract void Load();

        /// <summary>
        /// Unloads the module from the kernel.
        /// </summary>
        public virtual void Unload()
        {
        }

        /// <summary>
        /// Called after loading the modules. A module can verify here if all other required modules are loaded.
        /// </summary>
        public virtual void VerifyRequiredModulesAreLoaded()
        {
        }

        /// <summary>
        /// Bind
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="instance"></param>
        protected void Bind(Type serviceType, object instance)
        {
            _bindings.Add(new ServiceDescriptor(serviceType, instance));
        }    

        /// <summary>
        /// Bind
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="implemenationType"></param>
        /// <param name="serviceLifetime"></param>
        protected void Bind(Type serviceType, Type implemenationType, ServiceLifetime serviceLifetime)
        {
            _bindings.Add(new ServiceDescriptor(serviceType, implemenationType, serviceLifetime));
        }

        /// <summary>
        /// Bind
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="factory"></param>
        /// <param name="lifetime"></param>
        protected void Bind(Type serviceType, Func<IServiceProvider, object> factory, ServiceLifetime lifetime)
        {
            _bindings.Add(new ServiceDescriptor(serviceType, factory, lifetime));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="services"></param>
        public static void RemoveService<TService, TImplementation>(IServiceCollection services)
        {
            var descriptorToRemove = services.FirstOrDefault(d => d.ServiceType == typeof(TService));
            services.Remove(descriptorToRemove);
        }
    }
}
