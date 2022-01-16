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
    public class TestRepository : ITestRepository
    {
        private readonly DomainDbContext _context;
        public TestRepository(DomainDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Test test)
        {
            await _context.Tests.AddAsync(test);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int testId)
        {
            var test = await _context.Tests.FindAsync(testId);
            _context.Remove(test);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Test>> GetAllAsync()
        {
            return await _context.Tests.ToListAsync();
        }

        public async Task<Test> GetByIdAsync(int id)
        {
            return await _context.Tests.FindAsync(id);
        }

        public async Task UpdateAsync(Test test)
        {
            var item = await _context.Tests.FindAsync(test.Id);
            item.TestName = test.TestName;
            item.TimeInMin = test.TimeInMin;
            await _context.SaveChangesAsync();
        }
    }
}
