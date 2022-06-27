using Microsoft.EntityFrameworkCore;
using PersonalProject.Data.Context;
using PersonalProject.Data.Entities;
using PersonalProject.DTO;
using PersonalProject.Repo.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalProject.Repo.Concrete
{
    public class PersonRepo : IPersonRepo
    {
        private readonly MainDbContext mainDbContext;

        public PersonRepo(MainDbContext mainDbContext)
        {
            this.mainDbContext = mainDbContext;
        }
        public async Task<Person> CreatePerson(Person person)
        {
            await mainDbContext.Persons.AddAsync(person);
            await mainDbContext.SaveChangesAsync();
            return person;
        }

        public async Task<List<Person>> GetAllPersons(GetAllRequest request)
        {
            
            var result = await mainDbContext.Persons.Include(x=>x.Address).Where(x =>
                (request.FirstName == null || x.FirstName == request.FirstName) &&
                (request.LastName == null || x.LastName == request.LastName) &&
                (request.City == null || x.Address.City == request.City)
                ).ToListAsync();
            return result;
        }

        public async Task<Person> GetPerson(long id)
        {
            var result =await mainDbContext.Persons.FindAsync(id);
            return result;

        }

        public async Task<Person> UpdatePerson(Person person)
        {
            mainDbContext.Persons.Update(person);
            await mainDbContext.SaveChangesAsync();
            return person;
        }
    }
}
