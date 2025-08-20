using MediatR;
using CBS.CheckManagementManagement.Helper;

namespace CBS.CheckManagementManagement.MediatR.Ping.Commands
{
    public class AddPingCommand : IRequest<ServiceResponse<int>>
    {
        public string Message { get; set; }
    }
}
