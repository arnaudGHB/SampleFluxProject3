using CBS.CheckManagement.Data.Dto;
using CBS.CheckManagement.Domain.Context;
using CBS.CheckManagement.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CBS.CheckManagement.MediatR.Ping
{
    /// <summary>
    /// Handler for the AddPingCommand.
    /// </summary>
    public class AddPingCommandHandler : IRequestHandler<AddPingCommand, PingResponseDto>
    {
        private readonly CheckManagementContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddPingCommandHandler"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public AddPingCommandHandler(CheckManagementContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Handles the ping command by adding a new ping to the database.
        /// </summary>
        /// <param name="request">The ping command.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task representing the asynchronous operation with the ping response.</returns>
        public async Task<PingResponseDto> Handle(AddPingCommand request, CancellationToken cancellationToken)
        {
            if (request?.PingDto == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            // Create a new ping entity
            var ping = new Ping
            {
                Message = request.PingDto.Message,
                CreatedBy = request.Username,
                CreatedDate = DateTime.UtcNow
            };

            // Add the ping to the database
            _context.Pings.Add(ping);
            await _context.SaveChangesAsync(cancellationToken);

            // Return the response
            return new PingResponseDto
            {
                Id = ping.Id,
                Message = ping.Message,
                Timestamp = ping.CreatedDate,
                Status = "SUCCESS"
            };
        }
    }
}
