using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Model.Entities;
using BAL.IService;
using Model;
namespace POS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {

        //[HttpPost("register")]
        //public async Task<ActionResult<Employees>> Register(AddNewEmployeeDTO input)
        //{
        //    var emp = await authService.Register(input);
        //    if(emp is null)
        //    {
        //        return BadRequest("Employee already exists.");
        //    }
        //    return Ok(emp);
        //}

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDTO>> Login(AddNewEmployeeDTO input)
        {
            var result = await authService.Login(input);
            if (result is null)
            {
                return BadRequest("Invalid employee email or password!");
            }
            return Ok(new ResponseModel
            {
                Data = new List<object> { result }
            });
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDTO>>RefreshToken(RefreshTokenRequestDTO request)
        {
            var result = await authService.RefreshTokenAsync(request); 
            if(result is null || result.AccessToken is null || result.RefreshToken is null)
            {
                return Unauthorized("Invalid refresh token.");
            }
            return Ok(result);
        }
        

    }
}
