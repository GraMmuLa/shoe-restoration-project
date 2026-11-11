namespace ShoeRestorationProject.Exceptions;

public class AuthenticationException : Exception
{
    public AuthenticationException(string message = "Failed to authenticate")
        : base(message)
    { }
    
    public AuthenticationException(Exception innerException,
        string message = "Failed to authenticate")
        : base(message, innerException)
    { }
}