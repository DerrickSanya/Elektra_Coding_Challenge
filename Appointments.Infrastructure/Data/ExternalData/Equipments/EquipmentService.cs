using Appointments.Domain.External;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Appointments.Infrastructure.Data.ExternalData.Equipments
{
    /// <summary>
    /// Equipment Service is a sngleton for now
    /// </summary>
    public class EquipmentService : IEquipmentService
    {
        /// <summary>
        /// 
        /// </summary>
        private const string CacheKey = "equipmentAvailabilityList";

        /// <summary>
        /// _memoryCache
        /// </summary>
        private static IMemoryCache _memoryCache;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memoryCache"></param>
        public EquipmentService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Equipment> GetEquipmentById(int id)
        {
            return await Task.Run(() =>
            {
                return GetEquipmentList().FirstOrDefault(x => x.EquipmentId == id);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dateAvailable"></param>
        /// <param name="isAvailable"></param>
        /// <returns></returns>
        public async Task<Equipment> GetEquipment(int id, DateTime dateAvailable, bool isAvailable)
        {
            return await Task.Run(() =>
            {
                return GetEquipmentList().FirstOrDefault(x => x.EquipmentId == id & x.DateAvailable == dateAvailable && x.IsAvailable == isAvailable);
            });
        }

        /// <summary>
        /// GetEquipments
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Equipment>> GetEquipments()
        {
            return await Task.Run(() =>
            {
                return GetEquipmentList();
            });
        }

        /// <summary>
        /// Mock method to simulate a call that would be made to the external provider for equipment data
        /// </summary>
        /// <param name="appointmentDate"></param>
        /// <returns></returns>
        public async Task<Equipment> GetEquipmentAvailableOnAppointmentDateAsync(DateTime appointmentDate, TimeSpan startTime, TimeSpan endTime)
        {
            bool IsInRangeFunction(TimeSpan equipmentAvailabilityTime)
            {
                return ((equipmentAvailabilityTime >= startTime) && (equipmentAvailabilityTime <= endTime));
            }

            return await Task.Run(() =>
            {
                // get available equipment for available date
                var dummyEquipmentList = GenerateDummyEquipmentAvailabilityList();

                // get appointments avilable on the appointment Date
                var equipmentAvailabilitiesOnAppointDate = dummyEquipmentList.Where(x => x.DateAvailable.Date == appointmentDate.Date && x.IsAvailable);
                if (!equipmentAvailabilitiesOnAppointDate.Any())
                {
                    throw new Exception($"No Appointments available on: {appointmentDate.ToShortDateString()} at {startTime:hh\\:mm}");
                }

                // get the appointments for date that are within the given time range.
                var equipmentAvailableForSelectedTime = equipmentAvailabilitiesOnAppointDate.Where(x => IsInRangeFunction(x.DateAvailable.TimeOfDay));

                return equipmentAvailableForSelectedTime.Any() ? equipmentAvailableForSelectedTime.First() : null;
            });
        }

        /// <summary>
        /// Remove Item
        /// </summary>
        /// <param name="equipment"></param>
        /// <returns></returns>
        public async Task MarkAsUnAvailable(Equipment equipment)
        {
            await Task.Run(() =>
            {
                var items = GenerateDummyEquipmentAvailabilityList().ToList();
                items.FirstOrDefault(x => x.EquipmentId == equipment.EquipmentId && x.DateAvailable == equipment.DateAvailable && x.IsAvailable == equipment.IsAvailable).IsAvailable = false;
                _memoryCache.Set(CacheKey, items);
            });
        }

        /// <summary>
        /// MarkAsAvailable
        /// </summary>
        /// <param name="equipment"></param>
        /// <returns></returns>
        public async Task MarkAsAvailable(Equipment equipment)
        {
            await Task.Run(() =>
            {
                var items = GenerateDummyEquipmentAvailabilityList().ToList();
                items.FirstOrDefault(x => x.EquipmentId == equipment.EquipmentId && x.DateAvailable == equipment.DateAvailable && x.IsAvailable == equipment.IsAvailable).IsAvailable = true;
                _memoryCache.Set(CacheKey, items);
            });
        }

        /// <summary>
        /// GetEquipmentList
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<Equipment> GetEquipmentList()
        {
            var jsonDataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\\ExternalData\\EquipmentData.json");
            var jsonData = File.ReadAllText(jsonDataFilePath);
            return JsonConvert.DeserializeObject<IEnumerable<Equipment>>(jsonData);
        }

        /// <summary>
        /// This is a mock call to the  Equipment Availability system. For ease this shall be cached.
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<Equipment> GenerateDummyEquipmentAvailabilityList()
        {
            IEnumerable<Equipment> equipments;

            if (!_memoryCache.TryGetValue(CacheKey, out equipments))
            {
                var numberOfEquipments = 3; // specified 3 equipments deployed.
                var startDate = DateTime.Now;  // starts today because you cannot book appointments in the past
                var endDate = DateTime.Now.AddDays(14); // generate 14 days of data as appointments can be booked 14 days in advance.
                var dates = Enumerable.Range(0, (endDate - startDate).Days + 1).Select(day => startDate.AddDays(day)); // dates for 14 days.
                var equipmentsList = new List<Equipment>();
                for (int i = 1; i <= numberOfEquipments; i++)
                {
                    equipmentsList.AddRange(from date in dates
                                            let workingHours = Enumerable.Range(8, 9).Select(x => TimeSpan.FromHours(x)).ToList() // generate hours between 08:00-16:00
                                            from time in workingHours
                                            let isAvailable = (workingHours.IndexOf(time) % 2 != 0) ? false : true// if the index of t is odd then make machine is unvailable.
                                            let equipment = new Equipment { EquipmentId = i, IsAvailable = isAvailable, DateAvailable = new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, time.Seconds) }
                                            select equipment);
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(2)); // Keep in cache for this time, reset time if accessed.
                _memoryCache.Set(CacheKey, equipmentsList, cacheEntryOptions);
                equipments = equipmentsList;
             }

            return equipments;
        }      
    }
}
