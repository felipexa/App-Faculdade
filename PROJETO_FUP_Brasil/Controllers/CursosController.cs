using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PdfSharpCore.Drawing;
using PROJETO_FUP_Brasil.Data;
using PROJETO_FUP_Brasil.Models;
using Remake_FUP.Models;

namespace PROJETO_FUP_Brasil.Controllers
{
    public class CursosController : Controller
    {
        private readonly PROJETO_FUP_BrasilContext _context;

        public CursosController(PROJETO_FUP_BrasilContext context)
        {
            _context = context;
        }

        // GET: Cursos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cursos.ToListAsync());
        }

        // GET: Cursos/Details/5
        [Authorize(Policy = "Admins")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cursos = await _context.Cursos
                .FirstOrDefaultAsync(m => m.Id_Curso == id);
            if (cursos == null)
            {
                return NotFound();
            }
            //var conteudoCurso = await _context.Aluno.ToListAsync();
            //ViewBag.Alunos = conteudoCurso;

            var listAlunos = await _context.Aluno.Include(d => d).ToListAsync();
            Cursos Curso = new Cursos();
            Curso.Aluno = listAlunos;


            return View(cursos);
        }

        // GET: Cursos/Create
        [Authorize(Policy = "Admins")]

        public IActionResult Create()
        {
            return View();
        }

        // POST: Cursos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Curso,NomeCurso,ValorCurso,ProfessorCurso")] Cursos cursos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cursos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cursos);
        }

        // GET: Cursos/Edit/5
        [Authorize(Policy = "Admins")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cursos = await _context.Cursos.FindAsync(id);
            if (cursos == null)
            {
                return NotFound();
            }
            return View(cursos);
        }

        // POST: Cursos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Admins")]

        public async Task<IActionResult> Edit(int id, [Bind("Id_Curso,NomeCurso,ValorCurso,ProfessorCurso")] Cursos cursos)
        {
            if (id != cursos.Id_Curso)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cursos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CursosExists(cursos.Id_Curso))
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
            return View(cursos);
        }

        // GET: Cursos/Delete/5
        [Authorize(Policy = "Admins")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cursos = await _context.Cursos
                .FirstOrDefaultAsync(m => m.Id_Curso == id);
            if (cursos == null)
            {
                return NotFound();
            }

            return View(cursos);
        }

        // POST: Cursos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Admins")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cursos = await _context.Cursos.FindAsync(id);
            _context.Cursos.Remove(cursos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CursosExists(int id)
        {
            return _context.Cursos.Any(e => e.Id_Curso == id);
        }

        public async Task<FileResult> DownloadAluno()
        {
            using (var doc = new PdfSharpCore.Pdf.PdfDocument())
            {
                var page = doc.AddPage();
                page.Size = PdfSharpCore.PageSize.A4;
                page.Orientation = PdfSharpCore.PageOrientation.Portrait;
                var graphics = PdfSharpCore.Drawing.XGraphics.FromPdfPage(page);
                var corFonte = PdfSharpCore.Drawing.XBrushes.Black;
                var textFormatter = new PdfSharpCore.Drawing.Layout.XTextFormatter(graphics);
                var fonteOrganizacao = new PdfSharpCore.Drawing.XFont("Ariel", 10);
                var fonteDescricao = new PdfSharpCore.Drawing.XFont("Ariel", 8, PdfSharpCore.Drawing.XFontStyle.BoldItalic);
                var tituloDetalhes = new PdfSharpCore.Drawing.XFont("Ariel", 8, PdfSharpCore.Drawing.XFontStyle.Bold);
                var fonteDetalhesDescricao = new PdfSharpCore.Drawing.XFont("Ariel", 7);
                var logo = @"C:\Users\Aparicio\Desktop\FUP_TRABALHO_29-05\PROJETO_FUP_Brasil\wwwroot\img\logo.jpg";
                var qtdPaginas = doc.PageCount;

                //imagem logotipo
                textFormatter.DrawString(qtdPaginas.ToString(), new PdfSharpCore.Drawing.XFont("Arial", 10), corFonte, new PdfSharpCore.Drawing.XRect(575, 825, page.Width, page.Height));
                XImage imagem = XImage.FromFile(logo);
                graphics.DrawImage(imagem, 20, 5, 100, 50);


                //Titulo Maior
                var descricaoFinanceira = new PdfSharpCore.Drawing.Layout.XTextFormatter(graphics);
                descricaoFinanceira.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                descricaoFinanceira.DrawString("Alunos do Curso", tituloDetalhes, corFonte, new PdfSharpCore.Drawing.XRect(0, 120, page.Width, page.Height));

                //titulo das Colunas
                var alturaTituloFinanceiroY = 140;
                var detalhes = new PdfSharpCore.Drawing.Layout.XTextFormatter(graphics);

                detalhes.DrawString("Aluno", fonteDescricao, PdfSharpCore.Drawing.XBrushes.Red, new PdfSharpCore.Drawing.XRect(20, alturaTituloFinanceiroY, page.Width, page.Height));
                detalhes.DrawString("Curso", fonteDescricao, PdfSharpCore.Drawing.XBrushes.Green, new PdfSharpCore.Drawing.XRect(220, alturaTituloFinanceiroY, page.Width, page.Height));

                //gerar dados do relátorio

                Cursos model = new Cursos();
                var alturaItens = 160;
                var alturaItens2 = 160;
                var conteudoAluno = await _context.Aluno.Include(d => d.Cursos).ToListAsync();
                model.Aluno = conteudoAluno;

                if(model.Aluno != null){

                foreach (var item in model.Aluno)
                {
                    textFormatter.DrawString("Aluno: " + item.Nome, fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(20, alturaItens, page.Width, page.Height));
                    alturaItens += 20;
                }


                foreach (var item in model.Aluno)
                {
                    textFormatter.DrawString("Curso: " + item.Cursos.NomeCurso, fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(220, alturaItens2, page.Width, page.Height));
                    alturaItens2 += 20;
                }
            }

            //cabeçalho inicio statico
            textFormatter.DrawString("Instituição: ", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(20, 95, page.Width, page.Height));
            textFormatter.DrawString("Faculdade Universitaria de Programação", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(80, 95, page.Width, page.Height));




            using (MemoryStream stream = new MemoryStream())
            {
                var contantType = "application/pdf";
                doc.Save(stream, false);
                var nomeArquivo = "relatorioFinancerio.pdf";
                return File(stream.ToArray(), contantType, nomeArquivo);
            }
        }
    }
}
}
