using AdminPersonAndCity.Models;

namespace AdminPersonAndCity.Repositories.Interfaces
{
    public interface IPersonRepository
    {
        PersonModel Insert(PersonModel person);

        PersonModel Update(PersonModel person, int id);

        List<PersonModel> FindAll();

        PersonModel? FindById(int id);

        PersonModel? FindByCpfCnpj(int id);

        bool Remove(int id);
    }
}
