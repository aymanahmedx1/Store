using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using Store.data.Entity.IdentityEntity;

namespace Store.Service.Token
{
	public class TokenService : ITokenService
	{
		private readonly IConfiguration _configuration;
		private readonly SymmetricSecurityKey _Key;
		public TokenService(IConfiguration configuration)
		{
			_configuration = configuration;
			_Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]));
		}


		public string GenerateToken(AppUser user)
		{
			var claims = new List<Claim> {
				new Claim(ClaimTypes.GivenName , user.DisplayName),
				new Claim(ClaimTypes.Email, user.Email),
				new Claim("Id", user.Id),
				new Claim("UserName", user.UserName),
			};

			var credeintial = new SigningCredentials(_Key, SecurityAlgorithms.HmacSha256);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				IssuedAt = DateTime.Now,
				Issuer = _configuration["Token:Issuer"],
				Expires = DateTime.Now.AddDays(1),
				SigningCredentials = credeintial
			};
			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);

		}
	}
}
