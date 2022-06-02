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
    }
}
