
using FundaAssignment.WebApi.Domain.ViewModels;
using System;

namespace FundaAssignment.WebApi.Infrastructure.Extensions
{
    public static class ExceptionExtensions
    {
        public static ErrorObject ToErrorObject(this Exception e)
        {
            return new ErrorObject
            {
                Message = e.Message
            };
        }
    }
}
