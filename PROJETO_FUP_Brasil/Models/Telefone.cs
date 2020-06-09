using PROJETO_FUP_Brasil.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Remake_FUP.Models
{
    public class Telefone
    {
        [Key]
        public int TelefoneId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Phone(ErrorMessage = "Telefone inválido")]
        public string _Telefone { get; set; }



        [InverseProperty("Telefone")]
        [ForeignKey(nameof(AlunoId))]
        public int AlunoId { get; set; }
        public virtual Aluno Aluno { get; set; }


        [InverseProperty("Telefone")]
        [ForeignKey(nameof(FuncionarioId))]
        public int FuncionarioId { get; set; }
        public Funcionario Funcionario { get; set; }

        [InverseProperty("Telefone")]
        public int ChefiaId { get; set; }
        [ForeignKey("Telefone")]
        public Chefia Chefia { get; set; }

    }
}
