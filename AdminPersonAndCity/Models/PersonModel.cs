using AdminPersonAndCity.Models.Enums;
using AdminPersonAndCity.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminPersonAndCity.Models
{
    [Table("persons")]
    public class PersonModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome da pessoa é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Tipo de pessoa é obrigatório")]
        public PersonEnum PersonType { get; set; }

        [Required(ErrorMessage = "Cpf/Cnpj da pessoa é obrigatório")]
        [ValidCpfCnpj]
        public string CpfCnpj { get; set; }


        [Required(ErrorMessage = "Cep da pessoa é obrigatório")]
        public string Cep { get; set; }        
        
        [Required(ErrorMessage = "Endereço da pessoa é obrigatório")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Número do endereço é obrigatório")]
        public int Number { get; set; }        
        
        [Required(ErrorMessage = "Complement do endereço é obrigatório")]
        public string Compl { get; set; }

        [Required(ErrorMessage = "Bairro do endereço é obrigatório")]
        public string District { get; set; }

        [Required(ErrorMessage = "Data de nascimento ou fundação é obrigatório")]
        public DateTime BirthDateFoundation { get; set; }

        [Required(ErrorMessage = "Email da pessoa é obrigatório")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido. ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Celular da pessoa é obrigatório")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Situação cadastral da pessoa é obrigatório")]
        public RegistrationStatusEnum RegStatus { get; set; }

        // [Required(ErrorMessage = "Cidade da pessoa é obrigatório")]
        public int CityId { get; set; }

        public CityModel? City { get; set; }

        // Substitui dataCadastro
        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public void formatCpjCnpj()
        {
            if(PersonType == PersonEnum.JU)
                CpfCnpj = CpfCnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            else
                CpfCnpj = CpfCnpj.Replace(".", "").Replace("-", "")
        }
    }
}
