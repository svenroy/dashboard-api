using Microsoft.IdentityModel.Tokens;

namespace Dashboard.API.Application.Infrastructure.Identity
{
    public interface IValidateAuthToken
    {
        TokenValidationParameters TokenValidationParameters { get; }
    }
}
