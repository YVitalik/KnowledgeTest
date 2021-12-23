using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class TestDetailRepository : ITestDetailRepository
    {
        private readonly DomainDbContext _context;
        public TestDetailRepository(DomainDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TestDetail test)
        {
            await _context.TestDetails.AddAsync(test);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int testDetailId)
        {
            var test = await _context.TestDetails.FindAsync(testDetailId);
            _context.Remove(test);
            await _context.SaveChangesAsync();
        }

        public IQueryable<TestDetail> GetAll()
        {
            return _context.TestDetails.AsQueryable();
        }

        public async Task<TestDetail> GetByIdAsync(int id)
        {
            return await _context.TestDetails.FindAsync(id);
        }

        public async Task UpdateAsync(TestDetail test)
        {
            var item = await _context.TestDetails.FindAsync(test.Id);
            item.PassTime = test.PassTime;
            item.RightAnswears = test.RightAnswears;
            await _context.SaveChangesAsync();
        }
    }
}
