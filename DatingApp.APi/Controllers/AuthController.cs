using System.Threading.Tasks;
using DatingApp.APi.Data;
using DatingApp.APi.Dtos;
using DatingApp.APi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.APi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;

        public AuthController(IAuthRepository repo)

        {
            _repo = repo;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegistorDto userForRegistorDto)
        {
            //validate request
            userForRegistorDto.Username = userForRegistorDto.Username.ToLower();
            if (await _repo.UseExist(userForRegistorDto.Username))
                return BadRequest("Username already exitst.");

            var userToCreate = new User
            {
                Username = userForRegistorDto.Username
            };

            var createdUser = await _repo.Register(userToCreate, userForRegistorDto.Password);
            return StatusCode(201);
        }
    }
}