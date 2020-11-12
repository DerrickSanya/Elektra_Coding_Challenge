using Appointments.Domain.Base;
using Appointments.Domain.Base.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Appointments.UnitTest.Helpers
{
    /// <summary>
    /// DomainEvents TestHelper
    /// </summary>
    public class DomainEventsTestHelper
    {
        /// <summary>
        /// GetAllDomainEvents
        /// </summary>
        /// <param name="aggregate"></param>
        /// <returns></returns>
        public static List<IBaseDomainEvent> GetAllDomainEvents(BaseDomainEntity aggregate)
        {
            List<IBaseDomainEvent> domainEvents = new List<IBaseDomainEvent>();

            if (aggregate.DomainEvents != null)
            {
                domainEvents.AddRange(aggregate.DomainEvents);
            }

            var fields = aggregate.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public).Concat(aggregate.GetType().BaseType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)).ToArray();

            foreach (var field in fields)
            {
                var isEntity = field.FieldType.IsAssignableFrom(typeof(BaseDomainEntity));

                if (isEntity)
                {
                    var entity = field.GetValue(aggregate) as BaseDomainEntity;
                    domainEvents.AddRange(GetAllDomainEvents(entity).ToList());
                }

                if (field.FieldType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(field.FieldType))
                {
                    if (field.GetValue(aggregate) is IEnumerable enumerable)
                    {
                        foreach (var en in enumerable)
                        {
                            if (en is BaseDomainEntity entityItem)
                            {
                                domainEvents.AddRange(GetAllDomainEvents(entityItem));
                            }
                        }
                    }
                }
            }

            return domainEvents;
        }

        /// <summary>
        /// ClearAllDomainEvents
        /// </summary>
        /// <param name="aggregate"></param>
        public static void ClearAllDomainEvents(BaseDomainEntity aggregate)
        {
            aggregate.ClearDomainEvents();

            var fields = aggregate.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public).Concat(aggregate.GetType().BaseType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)).ToArray();

            foreach (var field in fields)
            {
                var isEntity = field.FieldType.IsAssignableFrom(typeof(BaseDomainEntity));

                if (isEntity)
                {
                    var entity = field.GetValue(aggregate) as BaseDomainEntity;
                    ClearAllDomainEvents(entity);
                }

                if (field.FieldType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(field.FieldType))
                {
                    if (field.GetValue(aggregate) is IEnumerable enumerable)
                    {
                        foreach (var en in enumerable)
                        {
                            if (en is BaseDomainEntity entityItem)
                            {
                                ClearAllDomainEvents(entityItem);
                            }
                        }
                    }
                }
            }
        }
    }
}