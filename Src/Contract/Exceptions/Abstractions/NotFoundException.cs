namespace Contract.Exceptions.Abstractions;
public class NotFoundException : Exception
{
    public NotFoundException(string message)
        : base(message)
    {
    }
}
