using Gateway.Dtos;

public interface IAuthService
{
    LoginResponseDto Login(LoginRequestDto dto);
}
