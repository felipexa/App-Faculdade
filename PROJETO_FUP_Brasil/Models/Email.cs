using PROJETO_FUP_Brasil.Models;
using Remake_FUP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ForeignKey_Email
{
    public partial class Email
    {
        [Key]
        public int Id_Email { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string _Email { get; set; }
        public int AlunoId { get; set; }
        [ForeignKey(nameof(AlunoId))]
        [InverseProperty("Email")]
        public virtual Aluno Aluno { get; set; }
        public int ChefiaId { get; set; }
        [ForeignKey("ChefiaId")]
        [InverseProperty("Email")]
        public virtual Chefia Chefia { get; set; }

        public int FuncionarioId { get; set; }
        [ForeignKey(nameof(FuncionarioId))]
        [InverseProperty("Email")]
        public Funcionario Funcionario { get; set; }


    }

}
