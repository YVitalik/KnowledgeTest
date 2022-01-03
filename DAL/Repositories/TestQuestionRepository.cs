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
    public class TestQuestionRepository : ITestQuestionRepository
    {
        private readonly DomainDbContext _context;
        public TestQuestionRepository(DomainDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TestQuestion test)
        {
            await _context.TestQuestions.AddAsync(test);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int testId)
        {
            var test = await _context.TestQuestions.FindAsync(testId);
            _context.Remove(test);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TestQuestion>> GetAllAsync()
        {
            return await _context.TestQuestions.ToListAsync();
        }

        public async Task<TestQuestion> GetByIdAsync(int id)
        {
            return await _context.TestQuestions.FindAsync(id);
        }

        public async Task UpdateAsync(TestQuestion test)
        {
            var item = await _context.TestQuestions.FindAsync(test.Id);
            item.Question = test.Question;
            item.Answear = test.Answear;
            await _context.SaveChangesAsync();
        }
    }
}
