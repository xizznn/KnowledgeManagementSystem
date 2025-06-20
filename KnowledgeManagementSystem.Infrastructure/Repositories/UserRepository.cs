using KnowledgeManagementSystem.Core.Entities;
using KnowledgeManagementSystem.Core.Interfaces;
using KnowledgeManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeManagementSystem.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<UserEntity>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context) { }

        public override async Task<UserEntity?> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<UserEntity?> GetByEmailAsync(string email)
            => await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email);

        public async Task<UserEntity?> GetByNameAndSurnameAsync(string name, string surname)
            => await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Name == name && u.Surname == surname);

        public async Task<IEnumerable<UserEntity>> GetUsersByRoleAsync(int roleId)
            => await _context.Users
                .Include(u => u.Role)
                .Where(u => u.RoleId == roleId)
                .ToListAsync();

        public async Task<IEnumerable<UserEntity>> GetUsersByBirthDateAsync(DateTime date)
            => await _context.Users
                .Include(u => u.Role)
                .Where(u => u.DateOfBirth.Date == date.Date)
                .ToListAsync();

        public async Task<UserEntity?> GetUserWithDetailsAsync(int id)
            => await _context.Users
                .Include(u => u.Role)
                .Include(u => u.Courses)
                    .ThenInclude(uc => uc.Course)
                .FirstOrDefaultAsync(u => u.Id == id);
    }
}
