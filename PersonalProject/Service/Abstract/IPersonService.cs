using PersonalProject.DTO;
using System.Threading.Tasks;

namespace PersonalProject.Service.Abstract
{
    public interface IPersonService
    {
        public Task<long> DesearializeAndSave(string json);
        public Task<string> GetAllAndSerialize(GetAllRequest getAllRequest);
    }
}
