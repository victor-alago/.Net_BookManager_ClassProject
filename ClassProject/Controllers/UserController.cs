using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassProject.DTOs;
using ClassProject.Services;

namespace ClassProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserDataService _userDataService;

        public UserController(UserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        // GET: api/user
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _userDataService.GetAllUsersAsync();
        }

        // POST: api/user
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDtoIn newUserDto)
        {
            if (newUserDto == null)
            {
                return BadRequest("User data is invalid.");
            }

            var createdUser = await _userDataService.CreateUserAsync(newUserDto);
            return CreatedAtAction(nameof(Get), new { id = createdUser.Id }, createdUser);
        }

        // PUT: api/user/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserDtoIn updatedUserDto)
        {
            if (updatedUserDto == null)
            {
                return BadRequest("Updated user data is invalid.");
            }

            var updatedUser = await _userDataService.UpdateUserAsync(id, updatedUserDto);

            if (updatedUser == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            return Ok(updatedUser); // Return the updated user
        }

        // DELETE: api/user/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var wasDeleted = await _userDataService.DeleteUserAsync(id);

            if (!wasDeleted)
            {
                return NotFound($"User with ID {id} not found.");
            }

            return NoContent(); // Return 204 No Content on successful deletion
        }
    }
}