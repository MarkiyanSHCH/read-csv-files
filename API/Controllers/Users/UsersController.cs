using Core.Entities.Users;

using Domain.Constants;
using Domain.Result;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.IO;

namespace _1Konnection.API.Api.Users
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersBoundary _usersBoundary;

        public UsersController(IUsersBoundary usersBoundary)
            => this._usersBoundary = usersBoundary;

        [HttpGet]
        public IActionResult GetAll()
            => base.Ok(this._usersBoundary.GetAll());

        [HttpPost("import")]
        [Consumes(ContentTypes.FormData)]
        public IActionResult PostDocumentPartialData([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return base.BadRequest(new ProblemDetails { Title = "Invalid request. Please try again." });

            string extension = Path.GetExtension(file.FileName);
            if (extension != ".csv")
                return base.BadRequest(new ProblemDetails { Title = "Invalid request. Please try again." });

            IResult result = this._usersBoundary.ParseCSV(file);
            if (result.IsFailed)
                return result.Error.Type switch
                {
                    ErrorType.BadRequest => base.BadRequest(new ProblemDetails { Title = "Invalid request. Please try again." }),
                    _ => base.UnprocessableEntity(new ProblemDetails { Title = "Action failed. Please try again." })
                };

            return base.Ok();
        }
    }
}