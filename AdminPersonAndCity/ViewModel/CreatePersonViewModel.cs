using AdminPersonAndCity.Models;

namespace AdminPersonAndCity.ViewModel
{
    public class CreatePersonViewModel
    {
        public PersonModel Person { get; set; }
        public List<CityModel> Cities { get; set; }
    }
}
