using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CBS.CheckManagementManagement.Repository;
using CBS.CheckManagementManagement.Data.Entity;
using CBS.CheckManagementManagement.Helper;
using CBS.CheckManagementManagement.Common.UnitOfWork;
using CBS.CheckManagementManagement.Domain.Context;
using CBS.CheckManagementManagement.Dto;

namespace CBS.CheckManagementManagement.MediatR.Ping.Handlers
{
    public class AddPingCommandHandler : IRequestHandler<Commands.AddPingCommand, ServiceResponse<int>>
    {
        private readonly IPingRepository _repository;
        private readonly IUnitOfWork<CheckManagementContext> _uow;
        private readonly UserInfoToken _userInfoToken;

        public AddPingCommandHandler(IPingRepository repository, IUnitOfWork<CheckManagementContext> uow, UserInfoToken userInfoToken)
        {
            _repository = repository;
            _uow = uow;
            _userInfoToken = userInfoToken;
        }

        public async Task<ServiceResponse<int>> Handle(Commands.AddPingCommand request, CancellationToken cancellationToken)
        {
            var entity = new Data.Entity.Ping
            {
                Message = request.Message
            };

            _repository.Add(entity);
            await _uow.SaveAsync(_userInfoToken);

            return new ServiceResponse<int> { Data = entity.Id, Message = "Ping created successfully." };
        }
    }
}
