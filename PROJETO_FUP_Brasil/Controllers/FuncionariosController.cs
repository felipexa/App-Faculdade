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

namespace PROJETO_FUP_Brasil.Controllers
{
    public class FuncionariosController : Controller
    {
        private readonly PROJETO_FUP_BrasilContext _context;

        public FuncionariosController(PROJETO_FUP_BrasilContext context)
        {
            _context = context;
        }

        // GET: Funcionarios
        public async Task<IActionResult> Index(string funcionarioCpf, string searchString)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> funcionarioQuery = from m in _context.Funcionario
                                                  orderby m.Cpf
                                                  select m.Cpf;

            var funcionarioos = from m in _context.Funcionario
                                select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                funcionarioos = funcionarioos.Where(s => s.Cpf.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(funcionarioCpf))
            {
                funcionarioos = funcionarioos.Where(x => x.Cpf == funcionarioCpf);
            }

            var alunocpfVM = new FuncionarioViewModel
            {
                Cpf = new SelectList(await funcionarioQuery.Distinct().ToListAsync()),
                Funcionarios = await funcionarioos.ToListAsync()
            };

            return View(alunocpfVM);
        }
        // GET: Funcionarios/Details/5
        [Authorize(Policy = "Admins")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionario
                .Include(a => a.Chefia)
                .FirstOrDefaultAsync(m => m.FuncionarioId == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // GET: Funcionarios/Create
        [Authorize(Policy = "Admins")]
        public IActionResult Create()
        {
            ViewData["ChefiaId"] = new SelectList(_context.Chefia, "ChefiaId", "NomeChefia");
            return View();
        }

        // POST: Funcionarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Admins")]
        public async Task<IActionResult> Create([Bind("FuncionarioId,NomeFuncionario,Rg,Cpf,Datanascimento,DataContratacao,DataDemissao,Genero,SalarioFuncionario,ChefiaId")] Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(funcionario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChefiaId"] = new SelectList(_context.Chefia, "ChefiaId", "NomeChefia", funcionario.ChefiaId);
            return View(funcionario);
        }

        // GET: Funcionarios/Edit/5
        [Authorize(Policy = "Admins")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionario.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }
            ViewData["ChefiaId"] = new SelectList(_context.Chefia, "ChefiaId", "NomeChefia", funcionario.ChefiaId);
            return View(funcionario);
        }

        // POST: Funcionarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Admins")]
        public async Task<IActionResult> Edit(int id, [Bind("FuncionarioId,NomeFuncionario,Rg,Cpf,Datanascimento,DataContratacao,DataDemissao,Genero,SalarioFuncionario")] Funcionario funcionario)
        {
            if (id != funcionario.FuncionarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcionario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionarioExists(funcionario.FuncionarioId))
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
            return View(funcionario);
        }

        // GET: Funcionarios/Delete/5
        [Authorize(Policy = "Admins")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionario
                .FirstOrDefaultAsync(m => m.FuncionarioId == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Admins")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcionario = await _context.Funcionario.FindAsync(id);
            _context.Funcionario.Remove(funcionario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionarioExists(int id)
        {
            return _context.Funcionario.Any(e => e.FuncionarioId == id);
        }
    }
}