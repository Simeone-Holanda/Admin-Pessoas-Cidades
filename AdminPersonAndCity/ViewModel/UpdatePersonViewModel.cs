using AdminPersonAndCity.Models;

namespace AdminPersonAndCity.ViewModel
{
    public class UpdatePersonViewModel
    {
        public UpdateUserViewModel Person { get; set; }
        public List<CityModel> Cities { get; set; }

    }
}
