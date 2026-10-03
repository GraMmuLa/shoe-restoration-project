namespace ShoeRestorationProject.Exceptions;

public class UniqueObjectException : Exception
{
    public UniqueObjectException(string message = "Object already exists") :
        base(message) { }
    
    public UniqueObjectException(Exception innerException, string message = "Object already exists") :
        base(message, innerException) { }
}