using PersonalProject.Data.Entities;
using PersonalProject.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalProject.Repo.Abstract
{
    public interface IPersonRepo
    {
        public Task<Person> CreatePerson(Person person);
        public Task<Person> UpdatePerson(Person person);
        public Task<Person> GetPerson(long id);
        public Task<List<Person>> GetAllPersons(GetAllRequest request);

    }
}
