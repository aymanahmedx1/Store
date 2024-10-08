using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Store.data.Entity.IdentityEntity;
using Store.Service.HandelReponse;
using Store.Service.UserService;
using Store.Service.UserServices.Login;

namespace Store.WebAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class AccountController(IUserService _userService , UserManager<AppUser> _userManager) : ControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> Login(LoginDto input)
		{

			var user = await _userService.Login(input);
			if (user == null)
				return BadRequest(new CustomException(400, "User Not Found"));
			return Ok(user);
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterDto input)
		{

			var user = await _userService.Register(input);
			if (user == null)
				return BadRequest(new CustomException(400, "Email Already Exist"));
			return Ok(user);
		}
		[HttpGet]
		public async Task<IActionResult> getCurrentUserInfo()
		{
			var userId = User?.FindFirst("Id");
			var user = await _userManager.FindByIdAsync(userId.Value);
			 return Ok(
					new UserDto
					{
						DisplayName = user.DisplayName,
						Email = user.Email,
						Id = Guid.Parse(user.Id)
					}
				 );
		}

	}
}
