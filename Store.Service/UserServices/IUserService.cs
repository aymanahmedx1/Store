using Store.Service.UserServices.Login;

namespace Store.Service.UserService
{
    public interface IUserService
	{
		Task<UserDto> Login(LoginDto input);
		Task<UserDto> Register(RegisterDto input);
	}
}
