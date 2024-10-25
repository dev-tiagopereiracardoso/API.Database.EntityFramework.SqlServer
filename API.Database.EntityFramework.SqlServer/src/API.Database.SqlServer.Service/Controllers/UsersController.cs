using API.Database.SqlServer.Domain.Implementation.Interfaces;
using API.Database.SqlServer.Models.Input;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Database.SqlServer.Service.Controllers
{
    [Route("v1/users")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "users")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(
                IUsersService usersService
            )
        {
            _usersService = usersService;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Register User")]
        public ActionResult OnRegister(RegisterUserInput registerUserInput)
        {
            var Data = _usersService.Register(registerUserInput);

            if (!Data.Result)
                return BadRequest();

            return Created();
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Update data the User")]
        public ActionResult OnUpdate(RegisterUserInput registerUserInput)
        {
            var Data = _usersService.Update(registerUserInput);

            if (!Data.Result)
                return BadRequest();

            return Created();
        }

        [HttpGet("{documentNumber}")]
        [SwaggerOperation(Summary = "Get by document number User")]
        public ActionResult OnGetByDocumentNumber(string documentNumber)
        {
            var Data = _usersService.GetByDocumentNumber(documentNumber);

            if (Data.Result is null)
                return BadRequest();

            return Ok();
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all Users active")]
        public ActionResult OnGetAll()
        {
            var Data = _usersService.GetAllActive();

            if (!Data.Result!.Any())
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{documentNumber}")]
        [SwaggerOperation(Summary = "Delete by document number User")]
        public ActionResult OnDeleteByDocumentNumber(string documentNumber)
        {
            var Data = _usersService.DeleteByDocumentNumber(documentNumber);

            if (!Data.Result!)
                return BadRequest();

            return Ok();
        }
    }
}