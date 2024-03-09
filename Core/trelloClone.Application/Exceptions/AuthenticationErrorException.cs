
namespace trelloClone.Application.Exceptions
{
    public class AuthenticationErrorException : Exception
    {
        public AuthenticationErrorException() : base("Kullanici Adi Veya Sifre Hatalı")
        {
        }

        public AuthenticationErrorException(string? message) : base(message)
        {
        }
    }
}
