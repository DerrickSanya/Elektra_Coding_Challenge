using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Appointments.Infrastructure.Database
{
    /// <summary>
    /// ISql Connection Manager
    /// </summary>
    public interface ISqlConnectionManager
    {
        /// <summary>
        /// IDbConnection
        /// </summary>
        /// <returns></returns>
        IDbConnection DbConnection();
    }
}
