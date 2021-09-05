using GenericApp.Infra.Common.Enums;
using Wis.Common.Objects;

namespace GenericApp.Domain.Dto.Return
{
    public class AuthenticationResultDto : Result
    {
        public bool FirstAcess { get; set; }
        public string Token { get; set; }
        public string Expiration { get; set; }
        public Role Roles { get; set; }
        public bool ExpiredPassword { get; set; }
    }
}
