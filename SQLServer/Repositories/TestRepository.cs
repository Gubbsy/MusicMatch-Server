using SQLServer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
    }
}
