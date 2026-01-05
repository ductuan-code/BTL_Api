using Gateway.Dtos;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _repo;

    public AuthService(IAuthRepository repo)
    {
        _repo = repo;
    }

    public LoginResponseDto Login(LoginRequestDto dto)
    {
        return _repo.Login(dto.Email, dto.MatKhau);
    }
}
