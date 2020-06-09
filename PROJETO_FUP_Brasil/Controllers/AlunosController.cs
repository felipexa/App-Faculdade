using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PROJETO_FUP_Brasil.Data;
using PROJETO_FUP_Brasil.Models;
using Remake_FUP.Models;

namespace PROJETO_FUP_Brasil.Controllers
{
    public class AlunosController : Controller
    {
        private readonly PROJETO_FUP_BrasilContext _context;

        public AlunosController(PROJETO_FUP_BrasilContext context)
        {
            _context = context;
        }

        // GET: Alunos
        public async Task<IActionResult> Index(string alunoCpf, string searchString)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> alunoQuery = from m in _context.Aluno
                                            orderby m.Cpf
                                            select m.Cpf;

            var alunoos = from m in _context.Aluno
                          select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                alunoos = alunoos.Where(s => s.Cpf.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(alunoCpf))
            {
                alunoos = alunoos.Where(x => x.Cpf == alunoCpf);
            }

            var alunocpfVM = new AlunoViewModel
            {
                Cpf = new SelectList(await alunoQuery.Distinct().ToListAsync()),
                Alunos = await alunoos.ToListAsync()
            };

            return View(alunocpfVM);

        }

        // GET: Alunos/Details/5
        [Authorize(Policy = "Admins")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Aluno
                .Include(a => a.Cursos)
                .FirstOrDefaultAsync(m => m.Id_Matricula == id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        // GET: Alunos/Create
        [Authorize(Policy = "Admins")]
        public IActionResult Create()
        {
            ViewData["CursosID"] = new SelectList(_context.Cursos, "Id_Curso", "NomeCurso");
            return View();
        }

        // POST: Alunos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Admins")]
        public async Task<IActionResult> Create([Bind("Id_Matricula,Nome,Rg,Cpf,Datanascimento,DataInicioCurso,DataTerminoCurso,Genero,Mensalidade,CursosID")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aluno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CursosID"] = new SelectList(_context.Cursos, "Id_Curso", "NomeCurso", aluno.CursosID);
            return View(aluno);
        }

        // GET: Alunos/Edit/5
        [Authorize(Policy = "Admins")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Aluno.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }
            ViewData["CursosID"] = new SelectList(_context.Cursos, "Id_Curso", "NomeCurso", aluno.CursosID);
            return View(aluno);
        }

        // POST: Alunos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Admins")]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Matricula,Nome,Rg,Cpf,Datanascimento,DataInicioCurso,DataTerminoCurso,Genero,Mensalidade,CursosID")] Aluno aluno)
        {
            if (id != aluno.Id_Matricula)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aluno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlunoExists(aluno.Id_Matricula))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CursosID"] = new SelectList(_context.Cursos, "Id_Curso", "NomeCurso", aluno.CursosID);
            return View(aluno);
        }

        // GET: Alunos/Delete/5
        [Authorize(Policy = "Admins")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Aluno
                .Include(a => a.Cursos)
                .FirstOrDefaultAsync(m => m.Id_Matricula == id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        // POST: Alunos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Admins")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aluno = await _context.Aluno.FindAsync(id);
            _context.Aluno.Remove(aluno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlunoExists(int id)
        {
            return _context.Aluno.Any(e => e.Id_Matricula == id);
        }
    }
}
