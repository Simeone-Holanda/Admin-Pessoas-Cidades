using AdminPersonAndCity.Models;

namespace AdminPersonAndCity.Repositories.Interfaces
{
    public interface ICityRepository
    {
        CityModel Insert(CityModel city);

        CityModel Update(CityModel city);

        List<CityModel> FindAll();

        CityModel? FindById(int id);

        bool Remove(int id);

    }
}
