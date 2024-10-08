using Microsoft.AspNetCore.Identity;

using Store.data.Entity.IdentityEntity;
using Store.Service.Token;
using Store.Service.UserService;
using Store.Service.UserServices.Login;

namespace Store.Service.UserServices
{
	public class UserService
		(SignInManager<AppUser> _signInManager,
		UserManager<AppUser> _userManager,
		ITokenService _tokenService)
		: IUserService
	{

		public async Task<UserDto> Login(LoginDto input)
		{
			var user = await _userManager.FindByEmailAsync(input.Email);
			if (user == null)
				return null;
			var result = await _signInManager.CheckPasswordSignInAsync(user, input.Password, false);
			if (!result.Succeeded)
				throw new Exception("Login Faild ");
			return new UserDto
			{
				DisplayName = user.DisplayName,
				Email = input.Email,
				Token = _tokenService.GenerateToken(user),
				Id = Guid.Parse(user.Id)
			};
		}

		public async Task<UserDto> Register(RegisterDto input)
		{
			var user = await _userManager.FindByEmailAsync(input.Email);
			if (user is not null)
				return null;
			var appUser = new AppUser {
				DisplayName = input.DisplayName,
				Email = input.Email,
				UserName = input.Email.Split('@')[0],
			};
			var result =await _userManager.CreateAsync(appUser,input.Password);
			if (!result.Succeeded)
				throw new Exception(result.Errors.Select(x=>x.Description).FirstOrDefault());
			return new UserDto
			{
				DisplayName = appUser.DisplayName,
				Email = appUser.Email,
				Token = _tokenService.GenerateToken(appUser),
				Id = Guid.Parse(appUser.Id)
			};
		}
	}
}
