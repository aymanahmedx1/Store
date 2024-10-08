using System.ComponentModel.DataAnnotations;

namespace Store.Service.UserServices.Login
{
	public class RegisterDto
	{
        public string DisplayName { get; set; }
        public string Email { get; set; }

		[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$", ErrorMessage = "Password must Has Uniqe Chars , 2 Digits , UpperCase , LowerCase")]
		public string Password { get; set; }
    }
}
