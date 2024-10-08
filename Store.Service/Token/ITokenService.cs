using Store.data.Entity.IdentityEntity;

namespace Store.Service.Token
{
	public interface ITokenService
	{
		string GenerateToken(AppUser user);
	}
}
