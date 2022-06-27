using PersonalProject.Data.Context;
using PersonalProject.Data.Entities;
using PersonalProject.Repo.Abstract;
using System.Threading.Tasks;

namespace PersonalProject.Repo.Concrete
{

    public class AddressRepo : IAddressRepo
    {
        private readonly MainDbContext dbContext;

        public AddressRepo(MainDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Address> CreateAddress(Address address)
        {
             await  dbContext.Addresses.AddAsync(address);
            await dbContext.SaveChangesAsync();
            return address;
        }

        public async Task<Address> GetAddress(long id)
        {
            var result  = await dbContext.Addresses.FindAsync(id);
            return result;
        }
        public async Task<Address> UpdateAddress(Address address)
        {
            dbContext.Addresses.Update(address);
            await dbContext.SaveChangesAsync();
            return address;

        }
    }
}
