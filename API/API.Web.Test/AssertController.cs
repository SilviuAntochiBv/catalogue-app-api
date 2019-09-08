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
    }
}
