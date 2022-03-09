using Microsoft.AspNetCore.Mvc;
using SharedKernel.Result;

namespace SharedKernel.AspNetCore.Result
{
    public static class ResultExtensions
    {
        /// <summary>
        /// Convert an <see cref="IResult"/> to an <see cref="ActionResult"/>.
        /// </summary>
        /// <param name="controller">The controller this is called from.</param>
        /// <param name="result">The Result to convert to an ActionResult.</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public static ActionResult ActionResultFrom(this ControllerBase controller, IResult result)
        {
            switch (result.Status)
            {
                case ResultStatus.Ok:
                    return controller.Ok(result.GetValue());

                case ResultStatus.Invalid:
                    foreach (var error in result.ValidationErrors)
                        controller.ModelState.AddModelError(error.Identifier, error.ErrorMessage);
                    return controller.UnprocessableEntity(controller.ModelState);

                case ResultStatus.Unauthorized:
                    return controller.Unauthorized();

                case ResultStatus.Forbidden:
                    return controller.Forbid();

                case ResultStatus.NotFound:
                    return controller.NotFound();

                default:
                    throw new NotSupportedException($"Result {result.Status} conversion is not supported.");
            }
        }
    }
}