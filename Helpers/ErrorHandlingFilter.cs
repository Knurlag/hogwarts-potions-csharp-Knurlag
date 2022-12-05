using log4net;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HogwartsPotions.Helpers;

public class ErrorHandlingFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        ILog logger = LogManager.GetLogger("logger");
        logger.Error(exception);

        context.ExceptionHandled = true; //optional 
    }
}