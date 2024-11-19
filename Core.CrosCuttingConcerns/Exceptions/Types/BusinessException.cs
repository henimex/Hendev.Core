namespace Core.CrosCuttingConcerns.Exceptions.Types;

public class BusinessException:Exception
{
    public BusinessException()
    {
        
    }

    public BusinessException(string? message) : base(message) { 

    }
}
