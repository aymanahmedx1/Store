namespace Store.Service.HandelReponse
{
    public class CustomException : Response
    {
        public string? Details { get; set; }
        public CustomException(int stautsCode, string? message = null , string? details =null) : base(stautsCode, message)
        {
            Details  = details;
        }

    }
}
