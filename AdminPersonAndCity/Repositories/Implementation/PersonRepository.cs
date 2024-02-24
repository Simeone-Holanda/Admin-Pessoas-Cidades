using AdminPersonAndCity.Data;
using AdminPersonAndCity.Models;
using AdminPersonAndCity.Repositories.Interfaces;

namespace AdminPersonAndCity.Repositories.Implementation
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ConnectionContext _connectionContext;

        public PersonRepository(ConnectionContext connectionContext)
        {
            _connectionContext = connectionContext;
        }

        public List<PersonModel> FindAll()
        {
            return _connectionContext.Persons.ToList();
        }

        public PersonModel? FindByCpfCnpj(string cpfCnpj)
        {
            return _connectionContext
                    .Persons
                    .Where(person => person.CpfCnpj == cpfCnpj)
                    .FirstOrDefault();
        }

        public PersonModel? FindById(int id)
        {
            return _connectionContext
                    .Persons
                    .Where(person => person.Id == id)
                    .FirstOrDefault();
        }

        public PersonModel Insert(PersonModel person)
        {
            PersonModel? hasPerson = FindByCpfCnpj(person.CpfCnpj);
            if (hasPerson == null) throw new Exception("Cpf ou Cnpj já existe no sistema. ");

            _connectionContext.Add(hasPerson);
            _connectionContext.SaveChanges();
            return person;
        }

        public bool Remove(int id)
        {
            PersonModel? hasPerson = FindById(id);
            if (hasPerson == null) throw new Exception("Nenhuma pessoa com esse Id foi encontrado. ");

            return true;
        }

        public PersonModel Update(PersonModel person)
        {
            PersonModel? hasPerson = FindById(person.Id);
            if (hasPerson == null) throw new Exception("Nenhuma pessoa com esse Id foi encontrado. ");

            hasPerson.Email = person.Email;
            hasPerson.Address = person.Address;
            hasPerson.Number = person.Number;
            hasPerson.Compl = person.Compl;
            hasPerson.District = person.District;
            hasPerson.City = person.City;

            _connectionContext.Persons.Update(hasPerson);
            _connectionContext.SaveChanges();
            return hasPerson;
        }
    }
}
