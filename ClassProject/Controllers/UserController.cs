// using Microsoft.AspNetCore.Mvc;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using ClassProject.DTOs;
// using ClassProject.Services;
//
// namespace ClassProject.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class UserController : ControllerBase
//     {
//         private readonly UserDataService _userDataService;
//
//         public UserController(UserDataService userDataService)
//         {
//             _userDataService = userDataService;
//         }
//
//         // GET: api/user
//         [HttpGet]
//         public async Task<IEnumerable<User>> Get()
//         {
//             return await _userDataService.GetAllUsersAsync();
//         }
//
//         // POST: api/user
//         [HttpPost]
//         public async Task<IActionResult> Post([FromBody] UserDtoIn newUserDto)
//         {
//             if (newUserDto == null)
//             {
//                 return BadRequest("User data is invalid.");
//             }
//
//             var createdUser = await _userDataService.CreateUserAsync(newUserDto);
//             return CreatedAtAction(nameof(Get), new { id = createdUser.Id }, createdUser);
//         }
//
//         // PUT: api/user/{id}
//         [HttpPut("{id}")]
//         public async Task<IActionResult> Put(int id, [FromBody] UserDtoIn updatedUserDto)
//         {
//             if (updatedUserDto == null)
//             {
//                 return BadRequest("Updated user data is invalid.");
//             }
//
//             var updatedUser = await _userDataService.UpdateUserAsync(id, updatedUserDto);
//
//             if (updatedUser == null)
//             {
//                 return NotFound($"User with ID {id} not found.");
//             }
//
//             return Ok(updatedUser); // Return the updated user
//         }
//
//         // DELETE: api/user/{id}
//         [HttpDelete("{id}")]
//         public async Task<IActionResult> Delete(int id)
//         {
//             var wasDeleted = await _userDataService.DeleteUserAsync(id);
//
//             if (!wasDeleted)
//             {
//                 return NotFound($"User with ID {id} not found.");
//             }
//
//             return NoContent(); // Return 204 No Content on successful deletion
//         }
//     }
// }


using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassProject.DTOs;

namespace ClassProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        // GET: api/user
        [HttpGet]
        public IActionResult Get()
        {
            var users = _userManager.Users.ToList();
            var userDtos = users.Select(user => new UserDtoOut
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            });

            return Ok(userDtos);
        }

        // POST: api/user
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDtoIn newUserDto)
        {
            if (newUserDto == null)
            {
                return BadRequest("User data is invalid.");
            }

            var newUser = new User
            {
                // UserName = newUserDto.Email,
                Email = newUserDto.Email,
                PhoneNumber = newUserDto.PhoneNumber,
                FirstName = newUserDto.FirstName,
                LastName = newUserDto.LastName
            };

            var result = await _userManager.CreateAsync(newUser, "DefaultPassword123!"); // Replace with your password logic

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
        }

        // PUT: api/user/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] UserDtoIn updatedUserDto)
        {
            if (updatedUserDto == null)
            {
                return BadRequest("Updated user data is invalid.");
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            user.FirstName = updatedUserDto.FirstName;
            user.LastName = updatedUserDto.LastName;
            user.Email = updatedUserDto.Email;
            user.PhoneNumber = updatedUserDto.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(user);
        }

        // DELETE: api/user/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return NoContent();
        }
    }
}