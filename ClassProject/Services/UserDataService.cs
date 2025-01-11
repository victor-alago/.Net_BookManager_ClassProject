using ClassProject.DTOs;
using ClassProject;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ClassProject.Services
{
    public class UserDataService
    {
        private readonly ClassProjectContext _context;

        public UserDataService(ClassProjectContext context)
        {
            _context = context;
        }

        // Create user
        public async Task<User> CreateUserAsync(UserDtoIn newUserDto)
        {
            var newUser = new User
            {
                FirstName = newUserDto.FirstName,
                LastName = newUserDto.LastName,
                Email = newUserDto.Email,
                PhoneNumber = newUserDto.PhoneNumber
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }

        // Get all users
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        // Update user
        public async Task<User?> UpdateUserAsync(int userId, UserDtoIn updatedUserDto)
        {
            // Find the user by ID
            var existingUser = await _context.Users.FindAsync(userId);

            if (existingUser == null)
            {
                return null; // User not found
            }

            // Update user fields
            existingUser.FirstName = updatedUserDto.FirstName;
            existingUser.LastName = updatedUserDto.LastName;
            existingUser.Email = updatedUserDto.Email;
            existingUser.PhoneNumber = updatedUserDto.PhoneNumber;

            // Save changes
            await _context.SaveChangesAsync();
            return existingUser;
        }

        // Delete user
        public async Task<bool> DeleteUserAsync(int userId)
        {
            // Find the user by ID
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return false; // User not found
            }

            // Remove the user
            _context.Users.Remove(user);

            // Save changes
            await _context.SaveChangesAsync();
            return true; // Deletion successful
        }
    }
}