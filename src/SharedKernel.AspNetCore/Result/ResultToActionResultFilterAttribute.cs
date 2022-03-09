using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SharedKernel.Result;

namespace SharedKernel.AspNetCore.Result
{
    /// <summary>
    /// ASP.NET Core filter to translate an <see cref="IResult"/> to an <see cref="ActionResult"/>.
    /// </summary>
    public class ResultToActionResultFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if ((context.Result as ObjectResult)?.Value is not IResult result)
                return;

            if (context.Controller is not ControllerBase controller)
                return;

            context.Result = controller.ActionResultFrom(result);
        }
    }
}