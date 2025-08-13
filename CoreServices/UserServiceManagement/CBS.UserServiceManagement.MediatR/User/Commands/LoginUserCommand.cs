// Fichier : CBS.UserServiceManagement.MediatR/LoginUserCommand.cs

using CBS.UserServiceManagement.Helper;
using MediatR;

namespace CBS.UserServiceManagement.MediatR
{
    // Le type de retour est standardis�
    public class LoginUserCommand : IRequest<ServiceResponse<LoginResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    // Le DTO de r�ponse est d�fini ici pour plus de clart�
    public class LoginResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; } // Gard� pour une �volution future
    }
}