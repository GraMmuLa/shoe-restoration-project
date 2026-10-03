namespace ShoeRestorationProject.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message = "Object not found") :
        base(message) { }
    
    public NotFoundException(Exception innerException, string message = "Object not found") :
        base(message, innerException) { }
}