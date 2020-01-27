﻿using Microsoft.AspNetCore.Mvc;
using SQLServer.Models;
using SQLServer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMatch_Server.Controllers
{
    [ApiController]
    public class TestController
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
    }
}