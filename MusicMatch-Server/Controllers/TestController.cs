using Microsoft.AspNetCore.Mvc;
using SQLServer.Models;
using SQLServer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMatch_Server.Controllers
{
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly TestRepository testRepository;

        public TestController(TestRepository testRepository) 
        {
            this.testRepository = testRepository;
        }

        [HttpPost("createtest")]
        public async Task<Responses.Test> CreateTest(Requests.CreateTest createTest) 
        {
           //Need to error check req model
           TestDbo newTestDbo =  await testRepository.CreateTest(createTest.Name, createTest.FavCheese);
            return new Responses.Test
            {
                Name = newTestDbo.Name,
                FavCheese = newTestDbo.FavCheese
           };
        }

        [HttpGet("gettests")]
        public async Task<IEnumerable<Responses.Test>> GetTests()
        {
            //Need to error check req model
            IEnumerable<TestDbo> tests = await testRepository.GetAllTests();

            return tests.Select(t => new Responses.Test
            {
                Name = t.Name,
                FavCheese = t.FavCheese
            });
        }

        [HttpGet("gettestsbycheese")]
        public async Task<IEnumerable<Responses.Test>> GetTestsByCheese(Requests.CheeseTest cheese)
        {
            IEnumerable<TestDbo> tests = await testRepository.GetTestsByCheese(cheese.Cheese);

            return tests.Select(t => new Responses.Test
            {
                Name = t.Name,
                FavCheese = t.FavCheese
            });
        }
    }
}
