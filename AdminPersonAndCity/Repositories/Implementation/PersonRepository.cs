using AdminPersonAndCity.Data;
using AdminPersonAndCity.Models;
using AdminPersonAndCity.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            return _connectionContext.Persons.Include(p => p.City).ToList();
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
                    .Include(person => person.City)
                    .FirstOrDefault();
        }

        public PersonModel Insert(PersonModel person)
        {
            PersonModel? hasPerson = FindByCpfCnpj(person.CpfCnpj);
            if (hasPerson != null) throw new Exception("Cpf ou Cnpj já existe no sistema. ");

            person.CreatedAt = DateTime.UtcNow;
            person.UpdatedAt = DateTime.UtcNow;
            _connectionContext.Persons.Add(person);
            _connectionContext.SaveChanges();
            return person;
        }

        public bool Remove(int id)
        {
            PersonModel? hasPerson = FindById(id);
            if (hasPerson == null) throw new Exception("Nenhuma pessoa com esse Id foi encontrado. ");

            _connectionContext.Persons.Remove(hasPerson);
            _connectionContext.SaveChanges();
            return true;
        }

        public PersonModel Update(PersonModel person)
        {
            PersonModel? hasPerson = FindById(person.Id);
            if (hasPerson == null) throw new Exception("Nenhuma pessoa com esse Id foi encontrado. ");


            hasPerson.Name = person.Name;
            hasPerson.PersonType = person.PersonType;
            hasPerson.Cep = person.Cep;
            hasPerson.Address = person.Address;
            hasPerson.Number = person.Number;
            hasPerson.Compl = person.Compl;
            hasPerson.District = person.District;

            hasPerson.Phone = person.Phone;
            hasPerson.RegStatus = person.RegStatus;
            hasPerson.CityId = person.CityId;

            hasPerson.UpdatedAt = DateTime.UtcNow;

            _connectionContext.Persons.Update(hasPerson);
            _connectionContext.SaveChanges();
            return hasPerson;
        }
    }
}
