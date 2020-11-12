namespace Appointments.Domain.External
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Equipment
    /// </summary>
    public class Equipment
    {
        /// <summary>
        /// Equipment
        /// </summary>
        public Equipment()
        { }

        /// <summary>
        /// EquipmentId
        /// </summary>
        [JsonProperty("equipmentId")]
        public int EquipmentId { get; set; }

        /// <summary>
        /// Is Equipment Available
        /// </summary>
        [JsonProperty("isAvailable")]
        public bool IsAvailable { get; set; }

        /// <summary>
        /// Date Equipments is Available
        /// </summary>
        [JsonProperty("date")]
        public DateTime DateAvailable { get; set; }
    }
}
