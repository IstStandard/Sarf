using Grpc.Core;
using Sarf.Logic;

namespace Sarf.Services;

public sealed class SarfAuthService : AuthService.AuthServiceBase
{
    private readonly ILogger<SarfAuthService> _logger;
    private readonly AuthLogic _logic;

    public SarfAuthService(ILogger<SarfAuthService> logger, AuthLogic logic)
    {
        _logger = logger;
        _logic = logic;
    }

    public override Task<SignInResponse> SignIn(SignInRequest request, ServerCallContext context)
    {
        var result = (AuthLogic.SignInResult)_logic.SignIn(new AuthLogic.SignInRequest
        {
            Ip = request.Ip,
            Password = request.Password,
            Username = request.Username
        });
        
        return Task.FromResult(new SignInResponse
        {
            Status = result.Status,
            Message = result.Message,
            Token = result.Token,
            RefreshToken = result.RefreshToken
        });
    }

    public override Task<GenericResponse> SignUp(SignUpRequest request, ServerCallContext context)
    {
        var result = _logic.SignUp(new AuthLogic.SignUpRequest
        {
            FirstIp = request.Ip,
            Password = request.Password,
            Username = request.Username,
            Email = request.Email,
            Age = request.Age,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Patronymic = request.Patronymic,
            Permissions = request.Permissions ?? 0L,
            Status = request.Status ?? 0
        });

        return Task.FromResult(new GenericResponse
        {
            Status = result.Status,
            Message = result.Message
        });
    }

    public override Task<RefreshResponse> Refresh(RequestWithTokenAndIp request, ServerCallContext context)
    {
        var result = _logic.Refresh(new AuthLogic.RefreshRequest
        {
            RefreshToken = request.Token,
            Ip = request.Ip
        });

        return Task.FromResult(new RefreshResponse
        {
            Status = result.Status,
            Message = result.Message,
            Token = result.Token,
            RefreshToken = result.RefreshToken
        });
    }

    public override Task<GenericResponse> Logout(RequestWithTokenAndIp request, ServerCallContext context)
    {
        var result = _logic.Logout(new AuthLogic.LogoutRequest
        {
            Token = request.Token,
            Ip = request.Ip
        });

        return Task.FromResult(new GenericResponse
        {
            Status = result.Status,
            Message = result.Message
        });
    }

    public override Task<GenericResponse> IsTokenValid(RequestWithToken request, ServerCallContext context)
    {
        var result = _logic.IsTokenValid(new AuthLogic.IsTokenValidRequest
        {
            Token = request.Token
        });
        
        return Task.FromResult(new GenericResponse
        {
            Status = result.Status,
            Message = result.Message
        });
    }
}