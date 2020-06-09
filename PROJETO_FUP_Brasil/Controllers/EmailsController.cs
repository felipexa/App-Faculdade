using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ForeignKey_Email;
using PROJETO_FUP_Brasil.Data;

namespace PROJETO_FUP_Brasil.Controllers
{
    public class EmailsController : Controller
    {
        private readonly PROJETO_FUP_BrasilContext _context;

        public EmailsController(PROJETO_FUP_BrasilContext context)
        {
            _context = context;
        }

        // GET: Emails

        public async Task<IActionResult> Index()
        {
            var pROJETO_FUP_BrasilContext = _context.Email.Include(e => e.Aluno).Include(e => e.Chefia).Include(e => e.Funcionario);
            return View(await pROJETO_FUP_BrasilContext.ToListAsync());
        }

        // GET: Emails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var email = await _context.Email
                .Include(e => e.Aluno)
                .Include(e => e.Chefia)
                .Include(e => e.Funcionario)
                .FirstOrDefaultAsync(m => m.Id_Email == id);
            if (email == null)
            {
                return NotFound();
            }

            return View(email);
        }

        // GET: Emails/Create
        public IActionResult Create()
        {
            ViewData["AlunoId"] = new SelectList(_context.Aluno, "Id_Matricula", "Cpf");
            ViewData["ChefiaId"] = new SelectList(_context.Chefia, "ChefiaId", "NomeChefia");
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionario, "FuncionarioId", "Cpf");
            return View();
        }

        // POST: Emails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Email,_Email,AlunoId,ChefiaId,FuncionarioId")] Email email)
        {
            if (ModelState.IsValid)
            {
                _context.Add(email);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlunoId"] = new SelectList(_context.Aluno, "Id_Matricula", "Cpf", email.AlunoId);
            ViewData["ChefiaId"] = new SelectList(_context.Chefia, "ChefiaId", "NomeChefia", email.ChefiaId);
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionario, "FuncionarioId", "Cpf", email.FuncionarioId);
            return View(email);
        }

        // GET: Emails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var email = await _context.Email.FindAsync(id);
            if (email == null)
            {
                return NotFound();
            }
            ViewData["AlunoId"] = new SelectList(_context.Aluno, "Id_Matricula", "Cpf", email.AlunoId);
            ViewData["ChefiaId"] = new SelectList(_context.Chefia, "ChefiaId", "NomeChefia", email.ChefiaId);
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionario, "FuncionarioId", "Cpf", email.FuncionarioId);
            return View(email);
        }

        // POST: Emails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Email,_Email,AlunoId,ChefiaId,FuncionarioId")] Email email)
        {
            if (id != email.Id_Email)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(email);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmailExists(email.Id_Email))
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
            ViewData["AlunoId"] = new SelectList(_context.Aluno, "Id_Matricula", "Cpf", email.AlunoId);
            ViewData["ChefiaId"] = new SelectList(_context.Chefia, "ChefiaId", "NomeChefia", email.ChefiaId);
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionario, "FuncionarioId", "Cpf", email.FuncionarioId);
            return View(email);
        }

        // GET: Emails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var email = await _context.Email
                .Include(e => e.Aluno)
                .Include(e => e.Chefia)
                .Include(e => e.Funcionario)
                .FirstOrDefaultAsync(m => m.Id_Email == id);
            if (email == null)
            {
                return NotFound();
            }

            return View(email);
        }

        // POST: Emails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var email = await _context.Email.FindAsync(id);
            _context.Email.Remove(email);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmailExists(int id)
        {
            return _context.Email.Any(e => e.Id_Email == id);
        }
    }
}
