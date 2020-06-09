using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Remake_FUP.Models;
using ForeignKey_Email;
using PROJETO_FUP_Brasil.Models;

namespace PROJETO_FUP_Brasil.Data
{
    public class PROJETO_FUP_BrasilContext : DbContext
    {
        public PROJETO_FUP_BrasilContext(DbContextOptions<PROJETO_FUP_BrasilContext> options)
            : base(options)
        {
        }

        public DbSet<Remake_FUP.Models.Aluno> Aluno { get; set; }

        //public DbSet<Remake_FUP.Models.Chefia> Chefia { get; set; }

        //public DbSet<Remake_FUP.Models.Financeiro> Financeiro { get; set; }

        //public DbSet<Remake_FUP.Models.Funcionario> Funcionario { get; set; }

        public DbSet<ForeignKey_Email.Email> Email { get; set; }

        public DbSet<Remake_FUP.Models.Telefone> Telefone { get; set; }

        public DbSet<PROJETO_FUP_Brasil.Models.Cursos> Cursos { get; set; }

        public DbSet<PROJETO_FUP_Brasil.Models.Funcionario> Funcionario { get; set; }

        public DbSet<PROJETO_FUP_Brasil.Models.FinanceiroViewModel> Financeiro { get; set; }

        public DbSet<PROJETO_FUP_Brasil.Models.Chefia> Chefia { get; set; }


    }
}
