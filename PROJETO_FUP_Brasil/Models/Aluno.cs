using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ForeignKey_Email;
using PROJETO_FUP_Brasil.Models;

namespace Remake_FUP.Models
{
    public partial class Aluno
    {
        [Key]
        public int Id_Matricula { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Nome inválido")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Tamanho inválido")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Valor inválido")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Tamanho do RG inválido")]
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
        public DateTime DataInicioCurso { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DataTerminoCurso { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Nome inválido")]
        [StringLength(9, MinimumLength = 8, ErrorMessage = "Tamanho inválido")]
        public string Genero { get; set; }

        [InverseProperty("Aluno")]
        public int CursosID { get; set; }
        public virtual Cursos Cursos { get; set; }

        [InverseProperty("Aluno")]
        public virtual Email Email { get; set; }


        [InverseProperty("Aluno")]
        public virtual Telefone Telefone { get; set; }

        //[InverseProperty("Aluno")]
        //public virtual FinanceiroViewModel Financeiro { get; set; }
    }
}
