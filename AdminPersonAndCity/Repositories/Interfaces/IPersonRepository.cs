using AdminPersonAndCity.Models;
using AdminPersonAndCity.ViewModel;

namespace AdminPersonAndCity.Repositories.Interfaces
{
    public interface IPersonRepository
    {
        PersonModel Insert(PersonModel person);

        PersonModel Update(UpdateUserViewModel person);

        List<PersonModel> FindAll();

        PersonModel? FindById(int id);

        PersonModel? FindByCpfCnpj(string cpfCnpj);

        bool Remove(int id);
    }
}
