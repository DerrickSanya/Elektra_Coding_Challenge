
using System.Threading.Tasks;
using Appointments.Application.Patients.Models;
using Appointments.Domain.BusinessEntities.Patients.Models;

namespace Appointments.Application.Services.Interfaces
{
    /// <summary>
    /// IPatient Service
    /// </summary>
    public interface IPatientService
    {
        /// <summary>
        /// Register
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        Task<RegisteredPatientDto> Register(RegisterPatientDto patient);
    }
}