using Exercicios.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exercicios.Controllers
{
    public class CardapioController : Controller
    {
        private readonly IDbContextFactory<ExerciciosDbContext> context;
        public CardapioController(IDbContextFactory<ExerciciosDbContext> context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            using (var contextLocal = context.CreateDbContext())
            {
                return View(await contextLocal.Cardapios.ToListAsync());
            }
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CardapioModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.Ativo = true;
            using (var contextLocal = this.context.CreateDbContext())
            {
                contextLocal.Cardapios.Add(model);
                await contextLocal.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> Edit(int id)
        {
            using (var contextLocal = this.context.CreateDbContext())
            {
                var CardapioModel = await contextLocal.Cardapios.Where(w => w.Id == id).FirstOrDefaultAsync();

                if (CardapioModel == null)
                {
                    return NotFound();
                }

                return View(CardapioModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CardapioModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using var contextLocal = context.CreateDbContext();

            contextLocal.Update(model);
            await contextLocal.SaveChangesAsync();

            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult Delete(int id, string descricao)
        {
            ViewBag.Id = id;
            ViewBag.Descricao = descricao;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRegistro(int id)
        {
            using var contextLocal = context.CreateDbContext();
            var categoriaModel = await contextLocal.Cardapios.Where(w => w.Id == id).FirstOrDefaultAsync();

            if (categoriaModel == null)
            {
                return NotFound();
            }

            contextLocal.Cardapios.Remove(categoriaModel);
            await contextLocal.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
