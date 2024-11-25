namespace Application.Infrastructure.Exceptions;

public class LoginException : Exception
{
    public LoginException(string? message = default) : base(message)
    {
    }
}