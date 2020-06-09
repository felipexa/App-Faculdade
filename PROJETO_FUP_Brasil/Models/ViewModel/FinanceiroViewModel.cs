using Remake_FUP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROJETO_FUP_Brasil.Models
{
    public class FinanceiroViewModel 
    {
        public int FinanceiroViewModelId { get; set; }
        public virtual List<Funcionario> Funcionarios { get; set; }
        public virtual List<Aluno> Alunos { get; set; }       
    }
}
