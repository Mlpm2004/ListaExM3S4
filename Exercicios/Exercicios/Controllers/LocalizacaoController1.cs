using Exercicios.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exercicios.Controllers
{
    public class LocalizacaoController : Controller
    {
        private readonly IDbContextFactory<ExerciciosDbContext> context;
        public LocalizacaoController(IDbContextFactory<ExerciciosDbContext> context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            using (var contextLocal = context.CreateDbContext())
            {
                return View(await contextLocal.Localizacoes.ToListAsync());
            }
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LocalizacaoModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.DataCadastro = DateTime.Now;
            model.Ativo = true;
            using (var contextLocal = this.context.CreateDbContext())
            {
                contextLocal.Localizacoes.Add(model);
                await contextLocal.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> Edit(int id)
        {
            using (var contextLocal = this.context.CreateDbContext())
            {
                var LocalizacaoModel = await contextLocal.Localizacoes.Where(w => w.Id == id).FirstOrDefaultAsync();

                if (LocalizacaoModel == null)
                {
                    return NotFound();
                }

                return View(LocalizacaoModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LocalizacaoModel model)
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
        //Falta atender a regra : Ao Criar e Editar não pode existir a mesma empresa no endereço
    }
}
