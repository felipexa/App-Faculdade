using Microsoft.AspNetCore.Mvc.Rendering;
using Remake_FUP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROJETO_FUP_Brasil.Models
{
    public class AlunoViewModel
    {
        public List<Aluno> Alunos { get; set; }
        public SelectList Cpf { get; set; }
        public string AlunoCpf { get; set; }
        public string SearchString { get; set; }
    }
}