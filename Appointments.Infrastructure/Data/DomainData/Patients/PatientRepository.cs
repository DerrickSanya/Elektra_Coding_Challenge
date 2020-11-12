using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Appointments.Domain.BusinessEntities.Patients;
using Appointments.Infrastructure.Database.DbContexts;
using Appointments.Infrastructure.Data.Base;

namespace Appointments.Infrastructure.Data.DomainData.Patients
{
    /// <summary>
    /// Patient Repository
    /// </summary>
    internal class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        /// <summary>
        /// Appointments DbContext
        /// </summary>
        private readonly AppointmentsDbContext _dbContext;

        /// <summary>
        /// Patient Repository
        /// </summary>
        /// <param name="dbContext"></param>
        public PatientRepository(AppointmentsDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <summary>
        /// Get By Id Async
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public async Task<Patient> GetByIdAsync(int patientId)
        {
            return await _dbContext.Patients.SingleAsync(x => x.Id == patientId);
        }

        /// <summary>
        /// Add Patient Async
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public async Task AddAsync(Patient patient)
        {
            await _dbContext.Patients.AddAsync(patient);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Update Patient sync
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public async Task UpdatePatientAsync(Patient patient)
        {
            _dbContext.Patients.Update(patient);
            await _dbContext.SaveChangesAsync();
        }
    }
}
