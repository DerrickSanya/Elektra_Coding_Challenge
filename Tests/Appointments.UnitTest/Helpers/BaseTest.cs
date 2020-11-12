using System;
using System.Collections.Generic;
using Appointments.Domain.Base.Interfaces;
using Appointments.Domain.Base;
using System.Linq;
using NUnit.Framework;
using Appointments.Domain.Base.Exceptions;

namespace Appointments.UnitTest.Helpers
{
    /// <summary>
    /// BaseTest
    /// </summary>
    public abstract class BaseTest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="aggregate"></param>
        /// <returns></returns>
        public static T AssertPublishedDomainEvent<T>(BaseDomainEntity aggregate) where T : IBaseDomainEvent
        {
            var domainEvent = DomainEventsTestHelper.GetAllDomainEvents(aggregate).OfType<T>().SingleOrDefault();

            if (domainEvent == null)
                throw new Exception($"{typeof(T).Name} event not published");

            return domainEvent;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="aggregate"></param>
        /// <returns></returns>
        public static List<T> AssertPublishedDomainEvents<T>(BaseDomainEntity aggregate) where T : IBaseDomainEvent
        {
            var domainEvents = DomainEventsTestHelper.GetAllDomainEvents(aggregate).OfType<T>().ToList();

            if (!domainEvents.Any())
                throw new Exception($"{typeof(T).Name} event not published");

            return domainEvents;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRule"></typeparam>
        /// <param name="testDelegate"></param>
        public static void AssertBrokenRule<TRule>(TestDelegate testDelegate) where TRule : class, IBusinessRule
        {
            var message = $"Expected {typeof(TRule).Name} broken rule";
            var businessRuleValidationException = Assert.Catch<BusinessRuleViolationException>(testDelegate, message);
            if (businessRuleValidationException != null)
            {
                Assert.That(businessRuleValidationException.BusinessRule, Is.TypeOf<TRule>(), message);
            }
        }

        /// <summary>
        /// 
        /// </summary>

        [TearDown]
        public void AfterEachTest()
        {

        }
    }
}
