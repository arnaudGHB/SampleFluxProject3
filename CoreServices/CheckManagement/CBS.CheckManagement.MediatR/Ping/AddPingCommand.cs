using CBS.CheckManagement.Data.Dto;
using MediatR;
using System;

namespace CBS.CheckManagement.MediatR.Ping
{
    /// <summary>
    /// Command to add a new ping to the system.
    /// </summary>
    public class AddPingCommand : IRequest<PingResponseDto>
    {
        /// <summary>
        /// Gets or sets the ping data transfer object.
        /// </summary>
        public PingDto PingDto { get; set; }

        /// <summary>
        /// Gets or sets the username of the user making the request.
        /// </summary>
        public string Username { get; set; }
    }
}
