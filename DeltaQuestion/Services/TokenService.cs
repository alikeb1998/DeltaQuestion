using JWT;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DeltaQuestion
{

    public class TokenService : ITokenService
    {
        //Injecting IConfiguration into this class in order to read Token Key from the configuration file
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public string CreateToken(string username)
        {
            //Creating Claims. You can add more information in these claims. For example email id.
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId, username),
            new Claim(JwtRegisteredClaimNames.AuthTime,((Int64)DateTime.Now.AddMinutes(1).Ticks).ToString(System.Globalization.CultureInfo.InvariantCulture))
        };

            //Creating credentials. Specifying which type of Security Algorithm we are using
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            //Creating Token description
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            //Finally returning the created token
            return tokenHandler.WriteToken(token);
        }
    }

}
