using SQLServer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SQLServer.Repositories
{
    public class TestRepository
    {
        private readonly AppDbContext appDbContext;

        public TestRepository(AppDbContext appDbContext) {
            this.appDbContext = appDbContext;
        }

        public async Task<TestDbo> CreateTest(string name, string favCheese) 
        {
            if (name == null || name.Length == 0) 
            {
                throw new ArgumentException("Value cannot be null or empty", nameof(name));
            }

            if (favCheese == null || favCheese.Length == 0)
            {
                throw new ArgumentException("Value cannot be null or empty", nameof(favCheese));
            }

            TestDbo newTest = new TestDbo
            {
                Name = name,
                FavCheese = favCheese,
            };

            appDbContext.Testdbos.Add(newTest);

            await appDbContext.SaveChangesAsync().ConfigureAwait(false);

            return newTest;
        }

        public async Task<IEnumerable<TestDbo>> GetAllTests() 
        {
            return await appDbContext.Testdbos.ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<TestDbo>> GetTestsByCheese(string cheese) 
        {
            return await appDbContext.Testdbos.Where(t => t.FavCheese == cheese).ToListAsync().ConfigureAwait(false);
        }
    }
}
