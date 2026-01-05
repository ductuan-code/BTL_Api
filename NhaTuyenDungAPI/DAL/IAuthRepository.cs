public interface IAuthRepository
{
    LoginResponseDto Login(string email, string matKhau);
}
