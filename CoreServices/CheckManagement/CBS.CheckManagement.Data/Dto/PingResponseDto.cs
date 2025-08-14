using System;

namespace CBS.CheckManagement.Data.Dto
{
    /// <summary>
    /// Data transfer object for the ping response.
    /// </summary>
    public class PingResponseDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the ping.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the message included in the ping.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when the ping was received.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the server timestamp when the response was generated.
        /// </summary>
        public DateTime ServerTimestamp { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the status of the ping operation.
        /// </summary>
        public string Status { get; set; } = "SUCCESS";
    }
}
