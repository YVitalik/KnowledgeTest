using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserTestRepository : IUserTestRepository
    {
        private readonly DomainDbContext _context;
        public UserTestRepository(DomainDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(UserTest userTest)
        {
            await _context.UserTests.AddAsync(userTest);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int userTestId)
        {
            var userTest = await _context.UserTests.FindAsync(userTestId);
            _context.Remove(userTest);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserTest>> GetAllAsync()
        {
            return await _context.UserTests.ToListAsync();
        }

        public async Task<UserTest> GetByIdAsync(int id)
        {
            return await _context.UserTests.FindAsync(id);
        }

        public async Task UpdateAsync(UserTest userTest)
        {
            var item = await _context.UserTests.FindAsync(userTest.Id);
            item.Passed = userTest.Passed;
            item.RightAnswerPercents = userTest.RightAnswerPercents;
            await _context.SaveChangesAsync();
        }
    }
}
