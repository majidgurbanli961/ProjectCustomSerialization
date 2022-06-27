using PersonalProject.Data.Entities;

namespace PersonalProject.Data.Spec
{
    public class PersonSpec : BaseSpecification<Person>
    {
        public PersonSpec(string firstName):base(x=>x.FirstName==firstName)
        {

        }
        public PersonSpec(string lastName,string firstName) : base(x => x.LastName == lastName && x.FirstName == firstName)
        {

        }
        public PersonSpec(string firstName, string lastName,string city):base(x=>x.FirstName==firstName&&x.LastName ==lastName && x.Address.City == city)
        {

        }
    }
}
