using AdminPersonAndCity.Models;
using AdminPersonAndCity.Models.Enums;
using AdminPersonAndCity.Validations;
using System.ComponentModel.DataAnnotations;

namespace AdminPersonAndCity.ViewModel
{
    public class UpdateUserViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome da pessoa é obrigatório")]
        public string Name { get; set; }

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

        [Required(ErrorMessage = "Email da pessoa é obrigatório")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido. ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Celular da pessoa é obrigatório")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Situação cadastral da pessoa é obrigatório")]
        public RegistrationStatusEnum RegStatus { get; set; }

        // [Required(ErrorMessage = "Cidade da pessoa é obrigatório")]
        public int CityId { get; set; }
    }
}
