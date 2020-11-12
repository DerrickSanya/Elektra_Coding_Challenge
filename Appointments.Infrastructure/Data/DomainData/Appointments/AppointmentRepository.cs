using Appointments.Domain.BusinessEntities.Appointments;
using Appointments.Infrastructure.Data.Base;
using Appointments.Infrastructure.Database.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Appointments.Infrastructure.Data.DomainData.Appointments
{
    /// <summary>
    /// Appointment Repository
    /// </summary>
    internal class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        /// <summary>
        /// Appointments DbContext
        /// </summary>
        private readonly AppointmentsDbContext _dbContext;

        /// <summary>
        /// Patient Repository
        /// </summary>
        /// <param name="dbContext"></param>
        public AppointmentRepository(AppointmentsDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <summary>
        /// AddAsync
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns></returns>
        public async Task AddAsync(Appointment appointment)
        {
            await _dbContext.Appointments.AddAsync(appointment);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Appointment> GetByIdAsync(int id)
        {
            return await _dbContext.Appointments.SingleAsync(x => x.Id == id);
        }

        /// <summary>
        /// GetBy ReferenceCode Async
        /// </summary>
        /// <param name="referenceCode"></param>
        /// <returns></returns>
        public async Task<Appointment> GetByReferenceCodeAsync(string referenceCode)
        {
            return await _dbContext.Appointments.SingleAsync(x => x.ReferenceCode == referenceCode);
        }

        /// <summary>
        /// UpdateAppointmentAsync
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns></returns>
        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            _dbContext.Appointments.Update(appointment);
            await _dbContext.SaveChangesAsync();
        }
    }
}
