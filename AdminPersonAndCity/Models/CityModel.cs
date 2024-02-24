using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminPersonAndCity.Models
{
    [Table("cities")]
    public class CityModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Nome da cidade é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Estado é obrigatório")]
        public string State { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public List<PersonModel> Persons { get; set; }

    }
}
