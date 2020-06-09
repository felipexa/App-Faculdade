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
    public class Chefia
    {
        public int ChefiaId { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Nome inválido")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Tamanho inválido")]
        public string NomeChefia { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Nome inválido")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Tamanho inválido")]
        public string Setor { get; set; }

        [InverseProperty("Chefia")]
        public int FuncionarioId { get; set; }
        [ForeignKey("Funcionario")]

        public virtual List<Funcionario> Funcionarios { get; set; }
        [EmailAddress(ErrorMessage = "Email Inválido")]
        [InverseProperty("Chefia")]
        public virtual Email Email { get; set; }

        [InverseProperty("Chefia")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Numero inválido")]
        public virtual Telefone Telefone { get; set; }

    }
}
