using ForeignKey_Email;
using Remake_FUP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PROJETO_FUP_Brasil.Models
{
    public class Funcionario
    {
        public int FuncionarioId { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Nome inválido")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Tamanho inválido")]
        public string NomeFuncionario { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Tamanho do RG inválido")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "RG inválido")]
        public string Rg { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "CPF inválido")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Tamanho do CPF inválido")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType(DataType.Date)]
        public DateTime Datanascimento { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType(DataType.Date)]
        public DateTime DataContratacao { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DataDemissao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Nome inválido")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Valor inválido")]
        public decimal SalarioFuncionario { get; set; }
        [InverseProperty("Funcionario")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public virtual Email Email { get; set; }

        [InverseProperty("Funcionario")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Telefone inválido")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Tamanho do Telefone inválido")]
        public virtual Telefone Telefone { get; set; }

        public virtual FinanceiroViewModel Financeiro { get; set; }

        public virtual int ChefiaId { get; set; }
        public virtual Chefia Chefia { get; set; }
    }
}
