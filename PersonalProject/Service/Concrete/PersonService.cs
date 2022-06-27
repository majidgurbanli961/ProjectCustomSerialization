using PersonalProject.Data.Entities;
using PersonalProject.DTO;
using PersonalProject.Helper;
using PersonalProject.Repo.Abstract;
using PersonalProject.Service.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalProject.Service.Concrete
{
    public class PersonService : IPersonService
    {
        private readonly IAddressRepo addressRepo;
        private readonly IPersonRepo personRepo;

        public PersonService(IAddressRepo addressRepo, IPersonRepo personRepo)
        {
            this.addressRepo = addressRepo;
            this.personRepo = personRepo;
        }
        public async Task<long> DesearializeAndSave(string json)
        {
            var person = (PersonDto) HelperSrlz.Deserialize(typeof(PersonDto),json);
            Data.Entities.Address address = null;
          
                address = await addressRepo.GetAddress((long)person.address.id);
                if (address == null)
                {
                    address = new Data.Entities.Address() { City = person.address.city, AddressLine = person.address.addressLine };
                    address = await addressRepo.CreateAddress(address);

                }
                else
                {
                    address.City = person.address.city;
                    address.AddressLine = person.address.addressLine;
                    address = await addressRepo.UpdateAddress(address);
                }
           
           
            Person personEnt = null;
            
                personEnt = await personRepo.GetPerson((long)person.id);
                if(personEnt == null)
                {
                    personEnt = new Person() { FirstName = person.firstName, LastName = person.lastName, Address = address };
                   personEnt = await personRepo.CreatePerson(personEnt);
                }
                else
                {
                    personEnt.FirstName = person.firstName;
                    personEnt.LastName = person.lastName;
                    personEnt.Address = address;
                    personEnt = await personRepo.UpdatePerson(personEnt);
                }
           
            //var result = await addressRepo.CreateAddress(address);
            return personEnt.Id;
        }

        public async Task<string> GetAllAndSerialize(GetAllRequest getAllRequest)
        {
            var allPersons = await personRepo.GetAllPersons(getAllRequest);
            List<PersonDto> personDtos = new List<PersonDto>();
            foreach (var allPerson in allPersons)
            {
                PersonDto person = new PersonDto();
                person.id = allPerson.Id;
                person.firstName = allPerson.FirstName;
                person.lastName=allPerson.LastName;
                DTO.AddressDto address = new DTO.AddressDto()
                {
                    id = allPerson.Address.Id,
                    addressLine = allPerson.Address.AddressLine,
                    city = allPerson.Address.City
                };
                person.address = address;
                personDtos.Add(person);
            }
            string result = "{\n";
            for (int i = 0; i < personDtos.Count; i++)
            
            {
                var personDto = personDtos[i];
                result += HelperSrlz.Serialize(personDto, typeof(PersonDto));

                if (i != personDtos.Count - 1)
                {
                    result += ",";

                }
                result += "\n";

            }
            result += "}";
            return result;
        }
    }
}
