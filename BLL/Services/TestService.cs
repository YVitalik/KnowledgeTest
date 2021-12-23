using AutoMapper;
using BLL.CustomExceptions;
using BLL.DTOs.TestServiceDTOs;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data;

namespace BLL.Services
{
    public class TestService : ITestService
    {
        private readonly IMapper _mapper;
        private readonly ITestRepository _repository;
        private readonly IUserTestService _userTestService;

        public TestService(ITestRepository repository, IMapper mapper, IUserTestService userTestService)
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.AddProfile<AutomapperProfile>()
            );
            mapper = new Mapper(configuration);
            _mapper = mapper;
            _repository = repository;
            _userTestService = userTestService;
        }
        
        public IEnumerable<ReadTestDto> FindTestAsync(string testToFind)
        {
            if (testToFind is null)
            {
                throw new ThisFieldCanNotBeEmptyException("This field can not be empty, please enter smth here");
            }
            var result = new List<ReadTestDto>();
            foreach (var test in _repository.GetAll())
            {
                if (test.TestName.ToUpper().Contains(testToFind.ToUpper()))
                {
                    var toAdd = _mapper.Map<ReadTestDto>(test);
                    result.Add(toAdd);
                }
            }
            return result;
        }

        public List<string> SendTestQuestions(int testId)
        {
            var questions = _repository.GetAll().Where(x => x.Id == testId).Include(x => x.TestQuestions);
            var questionList = new List<string>();
            
            foreach (var item in questions)
            {
                foreach (var question in item.TestQuestions)
                {
                    questionList.Add(question.Question);
                }
            }
            
            return questionList;
        }  
        
        public ReadUserTestDto CheckUserAnswers(string[] answers, int testId)
        {
            return _userTestService.CheckUserTest(answers, testId);
        }
    }
}
