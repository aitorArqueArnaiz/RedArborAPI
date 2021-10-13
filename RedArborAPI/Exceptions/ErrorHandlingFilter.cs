﻿using Microsoft.AspNetCore.Mvc.Filters;

public class ErrorHandlingFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        //log your exception here

        context.ExceptionHandled = true; //optional 
    }
}