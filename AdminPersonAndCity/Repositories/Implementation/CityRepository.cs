using AdminPersonAndCity.Data;
using AdminPersonAndCity.Models;
using AdminPersonAndCity.Repositories.Interfaces;

namespace AdminPersonAndCity.Repositories.Implementation
{
    public class CityRepository : ICityRepository
    {
        private readonly ConnectionContext _connectionContext;

        public CityRepository(ConnectionContext connectionContext)
        {
            _connectionContext = connectionContext;
        }


        public List<CityModel> FindAll()
        {
            return _connectionContext.Cities.ToList();
        }

        public CityModel? FindByName(string name)
        {
            return _connectionContext
                    .Cities
                    .Where(city => city.Name == name)
                    .FirstOrDefault();
        }

        public CityModel? FindById(int id)
        {
            return _connectionContext
                    .Cities
                    .Where(city => city.Id == id)
                    .FirstOrDefault();
        }

        public CityModel Insert(CityModel city)
        {
            CityModel? hasCity = FindByName(city.Name);

            if (hasCity != null) throw new Exception("Nome da cidade já existe no sistema. ");

            city.CreatedAt = DateTime.UtcNow;
            _connectionContext.Cities.Add(city);
            _connectionContext.SaveChanges();
            return city;
        }

        public bool Remove(int id)
        {
            CityModel? hasCity = FindById(id);
            if (hasCity == null) throw new Exception("Nenhuma cidade com esse Id foi encontrado. ");

            return true;
        }

        public CityModel Update(CityModel city)
        {
            CityModel? hasCity = FindById(city.Id);

            if (hasCity == null) throw new Exception("Nenhuma pessoa com esse Id foi encontrado. ");

            if (hasCity.Name != city.Name)
            {
                CityModel? hasNewCity = FindByName(city.Name);
                if (hasNewCity != null) throw new Exception($"A cidade {city.Name} já existe no sistema. ");
            }

            hasCity.Name = city.Name;
            hasCity.State = city.State;
            hasCity.UpdatedAt = DateTime.UtcNow;

            _connectionContext.Cities.Update(hasCity);
            _connectionContext.SaveChanges();
            return hasCity;
        }
    }
}