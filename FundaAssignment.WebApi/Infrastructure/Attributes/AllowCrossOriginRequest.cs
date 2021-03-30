using Microsoft.AspNetCore.Mvc.Filters;

namespace FundaAssignment.WebApi.Infrastructure.Attributes
{
    public class AllowCrossOriginRequest : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            
            base.OnActionExecuting(filterContext);
        }
    }
}
