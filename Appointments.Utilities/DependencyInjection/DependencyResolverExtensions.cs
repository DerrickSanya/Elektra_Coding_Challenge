namespace Appointments.Utilities.DependencyInjection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    /// <summary>
    /// Dependency Resolver Extensions class
    /// </summary>
    public static class DependencyResolverExtensions
    {
       /// <summary>
       /// Rebinds the specified services.
       /// </summary>
       /// <typeparam name="TService">The type of the service.</typeparam>
       /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
       /// <param name="services">The services.</param>
       /// <param name="lifetime">The lifetime.</param>
       /// <returns></returns>
       public static IServiceCollection Rebind<TService, TImplementation>(this IServiceCollection services, ServiceLifetime lifetime) where TService : class where TImplementation : class, TService
       {
           var descriptorToRemove = services.FirstOrDefault(d => d.ServiceType == typeof(TService));
           services.Remove(descriptorToRemove);
           var descriptorToAdd = new ServiceDescriptor(typeof(TService), typeof(TImplementation), lifetime);
           services.Add(descriptorToAdd);
           return services;
       }

       /// <summary>
       /// Rebinds the specified service descriptor.
       /// </summary>
       /// <typeparam name="TService">The type of the service.</typeparam>
       /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
       /// <param name="services">The services.</param>
       /// <param name="serviceDescriptor">The service descriptor.</param>
       public static void Rebind<TService, TImplementation>(this IServiceCollection services, ServiceDescriptor serviceDescriptor)
       {
           var descriptorToRemove = services.FirstOrDefault(d => d.ServiceType == typeof(TService));
           services.Remove(descriptorToRemove);
           // var descriptorToAdd = new ServiceDescriptor(typeof(TService), typeof(TImplementation), lifetime);
           services.Add(serviceDescriptor);
           // return services;
       }

       /// <summary>
       /// Rebinds the specified services.
       /// </summary>
       /// <typeparam name="TService">The type of the service.</typeparam>
       /// <param name="services">The services.</param>
       /// <param name="implementationFactory">The implementation factory.</param>
       /// <returns></returns>
       /// <exception cref="System.ArgumentNullException">
       /// services
       /// or
       /// implementationFactory
       /// </exception>
       public static IServiceCollection Rebind<TService>(this IServiceCollection services, Func<IServiceProvider, TService> implementationFactory) where TService : class
       {
           if (services == null)
               throw new ArgumentNullException(nameof(services));
           if (implementationFactory == null)
               throw new ArgumentNullException(nameof(implementationFactory));
           return services.AddTransient(typeof(TService), implementationFactory);
       }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="services"></param>
        /// <param name="serviceDescriptor"></param>
       public static void Rebind<TService>(this IServiceCollection services, ServiceDescriptor serviceDescriptor)
       {
           var descriptorToRemove = services.FirstOrDefault(d => d.ServiceType == typeof(TService));
           services.Remove(descriptorToRemove);
           services.Add(serviceDescriptor);
           DependencyResolver.SetResolver(services.BuildServiceProvider());
        }


       public static void Rebind<TService>(this IServiceCollection services, object implementationInstance, ServiceLifetime serviceLifetime)
       {
           var descriptorToRemove = services.FirstOrDefault(d => d.ServiceType == typeof(TService));
           services.Remove(descriptorToRemove);
           var descriptorToAdd = new ServiceDescriptor(typeof(TService), p => implementationInstance, serviceLifetime);
           services.Add(descriptorToAdd);
           var test = services;
       }

        /// <summary>
        /// Replaces the specified services.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection Replace<TService, TImplementation>(this IServiceCollection services) where TImplementation : TService
        {
            return services.Replace<TService>(typeof(TImplementation));
        }

        /// <summary>
        /// Replaces the specified implementation type.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="services">The services.</param>
        /// <param name="implementationType">Type of the implementation.</param>
        /// <returns></returns>
        public static IServiceCollection Replace<TService>(this IServiceCollection services, Type implementationType)
        {
            return services.Replace(typeof(TService), implementationType);
        }

        /// <summary>
        /// Replaces the specified service type.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="implementationType">Type of the implementation.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// services
        /// or
        /// serviceType
        /// or
        /// implementationType
        /// </exception>
        /// <exception cref="ArgumentException">No services found for {serviceType.FullName}. - serviceType</exception>
        public static IServiceCollection Replace(this IServiceCollection services, Type serviceType, Type implementationType)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (implementationType == null)
            {
                throw new ArgumentNullException(nameof(implementationType));
            }

            if (!services.TryGetDescriptors(serviceType, out var descriptors))
            {
                throw new ArgumentException($"No services found for {serviceType.FullName}.", nameof(serviceType));
            }

            foreach (var descriptor in descriptors)
            {
                var index = services.IndexOf(descriptor);

                services.Insert(index, descriptor.WithImplementationType(implementationType));

                services.Remove(descriptor);
            }

            return services;
        }

        /// <summary>
        /// Tries the get descriptors.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="descriptors">The descriptors.</param>
        /// <returns></returns>
        private static bool TryGetDescriptors(this IServiceCollection services, Type serviceType, out ICollection<ServiceDescriptor> descriptors)
        {
            return (descriptors = services.Where(service => service.ServiceType == serviceType).ToArray()).Any();
        }

        /// <summary>
        /// Withes the type of the implementation.
        /// </summary>
        /// <param name="descriptor">The descriptor.</param>
        /// <param name="implementationType">Type of the implementation.</param>
        /// <returns></returns>
        private static ServiceDescriptor WithImplementationType(this ServiceDescriptor descriptor, Type implementationType)
        {
            return new ServiceDescriptor(descriptor.ServiceType, implementationType, descriptor.Lifetime);
        }
    }
}
