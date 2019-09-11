using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Web.Responses
{
    public class InternalServerErrorResult : StatusCodeResult
    {
        public InternalServerErrorResult() : base(StatusCodes.Status500InternalServerError)
        {
        }
    }
}
