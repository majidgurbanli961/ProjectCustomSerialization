using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalProject.DTO;
using PersonalProject.Service.Abstract;
using System.Text.Json;
using System.Threading.Tasks;

namespace PersonalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService personService;

        public PersonController(IPersonService personService)
        {
            this.personService = personService;
        }
        [HttpPost("save")]
        public async Task<int> Save( [FromBody]string json)
             
        {
            
            var result = (int)await personService.DesearializeAndSave(json);
            return result;
        }
        [HttpPost("getall")]
        public async Task<string> GetAll(GetAllRequest request)
        {
           var result =  await personService.GetAllAndSerialize(request);
            return result;
        }

    }
}
