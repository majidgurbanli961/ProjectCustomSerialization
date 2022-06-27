namespace PersonalProject.DTO
{
    public class PersonDto
    {
        public long id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public AddressDto address { get; set; }
    }
}
