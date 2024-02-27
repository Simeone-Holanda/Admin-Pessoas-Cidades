using AdminPersonAndCity.Models;
using AdminPersonAndCity.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace AdminPersonAndCity.Validations
{
    public class ValidCpfCnpj : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult("Cpf/Cnpj inválido.");

            PersonModel? objectPersonModel = (PersonModel)validationContext.ObjectInstance;
            
            if (objectPersonModel.PersonType == PersonEnum.FI)
            {
                return ValidToCpf(value);
            }
            else
            {
                return ValidCnpj(value);
            }

        }
        public ValidationResult ValidToCpf(object value)
        {
            if(value == null) return new ValidationResult("Cpf é obrigatório. ");

            string cpf = value.ToString().Replace(".", "").Replace("-", "");

            if (cpf.Distinct().Count() == 1) return new ValidationResult("Cpf inválido. ");
            if (!cpf.All(char.IsDigit)) return new ValidationResult("Cpf/Cnpj deve conter apenas dígitos. ");
            if (cpf.Length != 11) return new ValidationResult("Cpf/Cnpj deve conter 11 dígitos.");

            if (cpf[9] != CalcDigtCpf(cpf, 9)) return new ValidationResult("Cpf inválido.");

            if (cpf[10] != CalcDigtCpf(cpf, 10)) return new ValidationResult("Cpf inválido.");

            return ValidationResult.Success;
        }

        public char CalcDigtCpf(string w, int DigitNumber)
        {
            int Digit = 0;
            for (int i = 0; i < DigitNumber; i++)
            { 
                Digit += Convert.ToInt32(w[i].ToString()) * (DigitNumber + 1 - i); 
            }

            Digit = (11 - (Digit %= 11)) > 9 ? 0 : (11 - (Digit %= 11));

            return (char)(Digit + '0');
        }

        public ValidationResult ValidCnpj(object value)
        {
            if (value == null) return new ValidationResult("Cnpj é obrigatório. ");
            string cnpj = value.ToString().Replace(".", "").Replace("-", "").Replace("/", "");

            Console.WriteLine(cnpj);
            if (cnpj.Distinct().Count() == 1)
                return new ValidationResult("Cnpj inválido. ");

            if (!cnpj.All(char.IsDigit))
                return new ValidationResult("Cnpj deve conter apenas dígitos. ");
 
            if (cnpj.Length != 14)
                return new ValidationResult("Cnpj deve conter 14 dígitos.");


            int[] firstWeights = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] secondWeights = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };


            if (cnpj[12] != CalcDigtCnpj(cnpj, 12, firstWeights))
                return new ValidationResult("Cnpj inválido.");

            if (cnpj[13] != CalcDigtCnpj(cnpj, 13, secondWeights))
                return new ValidationResult("Cnpj inválido.");

            return ValidationResult.Success;
        }

        public char CalcDigtCnpj(string cnpj, int digitPosition, int[] weights)
        {
            int sum = 0;

            for (int i = 0; i < digitPosition; i++)
            {
                sum += int.Parse(cnpj[i].ToString()) * weights[i];
            }

            int remainder = sum % 11;
            int digit = (remainder < 2) ? 0 : (11 - remainder);

            return (char)(digit + '0');
        }
    }

}
