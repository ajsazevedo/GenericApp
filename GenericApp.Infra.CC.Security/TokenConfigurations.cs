using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GenericApp.Infra.CC.Security
{
    public class TokenConfigurations
    {
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }
        public string Audience { get; }
        public string Issuer { get; }
        public int Seconds { get; }
        public int FinalExpiration { get; set; }

        public TokenConfigurations(IConfigurationSection _configuration)
        {
            Key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["SecurityKey"]));
            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature);
            Audience = _configuration["Audience"];
            Issuer = _configuration["Issuer"];
            Seconds = int.Parse(_configuration["Seconds"]);
        }
    }
}
