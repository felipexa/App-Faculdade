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
    public class ChefiasController : Controller
    {
        private readonly PROJETO_FUP_BrasilContext _context;

        public ChefiasController(PROJETO_FUP_BrasilContext context)
        {
            _context = context;
        }

        // GET: Chefias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Chefia.ToListAsync());
        }

        // GET: Chefias/Details/5
        [Authorize(Policy = "Admins")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chefia = await _context.Chefia
                .FirstOrDefaultAsync(m => m.ChefiaId == id);
            if (chefia == null)
            {
                return NotFound();
            }

            return View(chefia);
        }

        // GET: Chefias/Create
        [Authorize(Policy = "Admins")]

        public IActionResult Create()
        {
            return View();
        }

        // POST: Chefias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChefiaId,NomeChefia,Setor,FuncionarioId")] Chefia chefia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chefia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chefia);
        }

        // GET: Chefias/Edit/5
        [Authorize(Policy = "Admins")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chefia = await _context.Chefia.FindAsync(id);
            if (chefia == null)
            {
                return NotFound();
            }
            return View(chefia);
        }

        // POST: Chefias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Admins")]

        public async Task<IActionResult> Edit(int id, [Bind("ChefiaId,NomeChefia,Setor,FuncionarioId")] Chefia chefia)
        {
            if (id != chefia.ChefiaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chefia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChefiaExists(chefia.ChefiaId))
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
            return View(chefia);
        }

        // GET: Chefias/Delete/5
        [Authorize(Policy = "Admins")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chefia = await _context.Chefia
                .FirstOrDefaultAsync(m => m.ChefiaId == id);
            if (chefia == null)
            {
                return NotFound();
            }

            return View(chefia);
        }

        // POST: Chefias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Admins")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chefia = await _context.Chefia.FindAsync(id);
            _context.Chefia.Remove(chefia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChefiaExists(int id)
        {
            return _context.Chefia.Any(e => e.ChefiaId == id);
        }
    }
}
