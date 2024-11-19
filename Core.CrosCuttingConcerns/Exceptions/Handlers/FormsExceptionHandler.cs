using Core.CrosCuttingConcerns.Exceptions.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrosCuttingConcerns.Exceptions.Handlers;

public class FormsExceptionHandler : ExceptionHandler
{
    protected override Task HandleException(BusinessException businessException)
    {
        //TODO:: Manage Exceptions For .net Framework Desktop Applications
        throw new NotImplementedException();
    }

    protected override Task HandleException(Exception exception)
    {
        //TODO:: Manage Exceptions For .net Framework Desktop Applications
        throw new NotImplementedException();
    }
}
