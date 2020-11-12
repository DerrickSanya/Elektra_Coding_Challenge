using Microsoft.Extensions.DependencyInjection;
using Appointments.Domain.BusinessEntities.Appointments;
using Appointments.Domain.BusinessEntities.Patients;
using Appointments.Infrastructure.Data.InternalData.JobQueues;
using Appointments.Infrastructure.Data.InternalData.NotificationQueues;
using Appointments.Utilities.DependencyInjection.Binding;
using Appointments.Utilities.SqlGenerator;
using Appointments.Infrastructure.Data.DomainData.Patients;
using Appointments.Domain.BusinessEntities.Patients.Rules;
using Appointments.Infrastructure.Data.DomainData.Patients.Rules;
using Appointments.Domain.Base.Interfaces;
using Appointments.Domain.Audit;
using Appointments.Infrastructure.Data.ExternalData.Equipments;
using Appointments.Domain.BusinessEntities.Appointments.Rules.Interfaces;
using Appointments.Infrastructure.Data.DomainData.Appointments.Rules;
using Appointments.Infrastructure.Data.DomainData.Appointments;
using Appointments.Domain.External;

namespace Appointments.Infrastructure.Modules
{
    /// <summary>
    /// Data Module
    /// </summary>
    public class DataModule : ModuleBinder
    {
        /// <inheritdoc />
        /// <summary>
        /// Load
        /// </summary>
        public override void Load()
        {
            // SQL Builders
            Bind(typeof(ISqlBuilder<JobQueue>), typeof(SqlBuilder<JobQueue>), ServiceLifetime.Singleton);
            Bind(typeof(ISqlBuilder<NotificationQueue>), typeof(SqlBuilder<NotificationQueue>), ServiceLifetime.Singleton);
            Bind(typeof(ISqlBuilder<Patient>), typeof(SqlBuilder<Patient>), ServiceLifetime.Singleton);
            Bind(typeof(ISqlBuilder<Appointment>), typeof(SqlBuilder<Appointment>), ServiceLifetime.Singleton);
            Bind(typeof(ISqlBuilder<AppointmentDetail>), typeof(SqlBuilder<AppointmentDetail>), ServiceLifetime.Singleton);
            

            // ===========================   Repositories Bindings =========================================================================
            // =============================  Dapper Only No need to be tracked ===========================================================
            Bind(typeof(IBaseRepository<JobQueue>), typeof(JobQueueRepository), ServiceLifetime.Transient);
            Bind(typeof(IBaseRepository<NotificationQueue>), typeof(NotificationQueueRepository), ServiceLifetime.Transient);
            Bind(typeof(IBaseRepository<Patient>), typeof(PatientRepository), ServiceLifetime.Transient);
            Bind(typeof(IBaseRepository<AppointmentDetail>), typeof(AppointmentReadRepository), ServiceLifetime.Transient);
            Bind(typeof(IAppointmentReadRepository), typeof(AppointmentReadRepository), ServiceLifetime.Transient);
            //======================  EF Bussines Entities need to tracked =================================================================
            Bind(typeof(IPatientRepository), typeof(PatientRepository), ServiceLifetime.Transient);
            Bind(typeof(IAppointmentRepository), typeof(AppointmentRepository), ServiceLifetime.Transient);
            
            // ===========================   Repositories Bindings End =====================================================================

            // Rules
            Bind(typeof(IPatientEmailMustBeUniqueRuleValidator), typeof(PatientEmailMustBeUniqueRuleValidator), ServiceLifetime.Transient);

            // Appointment Rules
            Bind(typeof(IAppointmentMustExistValidator), typeof(AppointmentMustExistValidator), ServiceLifetime.Transient);       
            Bind(typeof(IAppointmentPatientMustExistRuleValidator), typeof(AppointmentPatientMustExistRuleValidator), ServiceLifetime.Transient);            
            Bind(typeof(IAppointmentEquipmentIsAvailableValidator), typeof(AppointmentEquipmentIsAvailableValidator), ServiceLifetime.Transient);

            // External Data
            Bind(typeof(IEquipmentService), typeof(EquipmentService), ServiceLifetime.Singleton); 

        }
    }
}
