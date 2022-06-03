using Exercicios.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exercicios.Controllers
{
    public class EmpresaController : Controller
    {
        private readonly IDbContextFactory<ExerciciosDbContext> context;
        public EmpresaController(IDbContextFactory<ExerciciosDbContext> context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            using (var contextLocal = context.CreateDbContext())
            {
                return View(await contextLocal.Empresas.ToListAsync());
            }
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmpresaModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.DataCadastro= DateTime.Now;
            model.Ativo = true;
            using (var contextLocal = this.context.CreateDbContext())
            {
                contextLocal.Empresas.Add(model);
                await contextLocal.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> Edit(int id)
        {
            using (var contextLocal = this.context.CreateDbContext())
            {
                var EmpresaModel = await contextLocal.Empresas.Where(w => w.Id == id).FirstOrDefaultAsync();


                if (EmpresaModel == null)
                {
                    return NotFound();
                }

                return View(EmpresaModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmpresaModel model)
        {
  
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using var contextLocal = this.context.CreateDbContext();
            if (model.Ativo == false)
            {
                var LocalizacaoModel = await contextLocal.Localizacoes.Where(l => l.Empresa == model).FirstOrDefaultAsync();
                LocalizacaoModel.Ativo = false;

            }
 
            contextLocal.Update(model);
            await contextLocal.SaveChangesAsync();

            return RedirectToAction("Index");

        }
        // Falta atender a regra de desativação :Para desativar uma empresa precisa mudar a situação também da Localização e Cardápio
    }
}
