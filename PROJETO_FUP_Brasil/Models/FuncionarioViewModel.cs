using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROJETO_FUP_Brasil.Models
{
    public class FuncionarioViewModel
    {
        public List<Funcionario> Funcionarios { get; set; }
        public SelectList Cpf { get; set; }
        public string FuncionarioCpf { get; set; }
        public string SearchString { get; set; }
    }
}