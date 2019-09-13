using API.Web.Responses;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace API.Web.Test
{
    public static class AssertController
    {
        public static void Ok(IActionResult actionResult)
        {
            Assert.IsType<OkObjectResult>(actionResult);
        }

        public static void Ok<T>(IActionResult actionResult, T content)
        {
            Ok(actionResult);

            var okObjectResult = (OkObjectResult)actionResult;

            var value = (T)okObjectResult.Value;

            Assert.Equal(content, value);
        }

        public static void BadRequest(IActionResult actionResult)
        {
            Assert.IsType<BadRequestResult>(actionResult);
        }

        public static void NotFound(IActionResult actionResult)
        {
            Assert.IsType<NotFoundResult>(actionResult);
        }

        public static void InternalServerError(IActionResult actionResult)
        {
            Assert.IsType<InternalServerErrorResult>(actionResult);
        }
    }
}
