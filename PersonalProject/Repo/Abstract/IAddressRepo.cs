using PersonalProject.Data.Entities;
using System.Threading.Tasks;

namespace PersonalProject.Repo.Abstract
{
    public interface IAddressRepo
    {
        public Task<Address> CreateAddress(Address address);
        public Task<Address> GetAddress(long id);
        public Task<Address> UpdateAddress(Address address);
    }
}
