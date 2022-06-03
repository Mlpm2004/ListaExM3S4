using Exercicios.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public IActionResult Index()
        {
            var listaLocalizacoes = BuscarTodasLocalizacoes();
            return View(listaLocalizacoes);
        }


        public async Task<IActionResult> Create()
        {
            ViewBag.TodasEmpresas = MontarSelect();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ListarCardapioEmpresaViewModel model)
        {
            CardapioModel cardapio = model;
            cardapio.DataCadastro = DateTime.Now;
            cardapio.Ativo = true;
            using (var contextLocal = context.CreateDbContext())
            {
                cardapio.EmpresaId = contextLocal.Empresas.Where(w => w.Id == cardapio.EmpresaId.Id).First();
                contextLocal.Cardapios.Add(cardapio);
                await contextLocal.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }



        [NonAction]
        private SelectList MontarSelect()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            using (var contextLocal = context.CreateDbContext())
            {
                var empresas = contextLocal.Empresas;

                foreach (var empresa in empresas)
                {
                    list.Add(new SelectListItem
                    {
                        Text = empresa.Descricao,
                        Value = empresa.Id.ToString()
                    });
                }
            }

            return new SelectList(list, "Value", "Text");
        }

        private IEnumerable<ListarCardapioEmpresaViewModel> BuscarTodasLocalizacoes()
        {
            var listarCardapioEmpresaViewModel = new List<ListarCardapioEmpresaViewModel>();

            using (var contextLocal = context.CreateDbContext())
            {
                var cardapios = contextLocal.Cardapios
                            .Include(empresa => empresa.EmpresaId);

                cardapios.ToList().ForEach(cardapios =>
                {
                    listarCardapioEmpresaViewModel.Add(new ListarCardapioEmpresaViewModel
                    {
                        Id = cardapios.Id,
                        Descricao = cardapios.Descricao,
                        Valor = cardapios.Valor,
                        EmpresaId = cardapios.EmpresaId.Id,
                        Ativo = cardapios.Ativo,
                        DescricaoEmpresa = $"{cardapios.EmpresaId.Id} - {cardapios.EmpresaId.Descricao}"

                    });
                });
            }

            return listarCardapioEmpresaViewModel;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ListarCardapioEmpresaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.TodasEmpresas = MontarSelect();
                return View(model);
            }

            using var contextLocal = context.CreateDbContext();

            CardapioModel cardapio = model;
            cardapio.EmpresaId = contextLocal.Empresas.Where(w => w.Id == model.EmpresaId).FirstOrDefault();
            contextLocal.Update(cardapio);
            await contextLocal.SaveChangesAsync();

            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.TodasEmpresas = MontarSelect();
            using (var contextLocal = this.context.CreateDbContext())
            {
                var ListarCardapioEmpresaViewModel = await contextLocal.Cardapios.Where(w => w.Id == id).FirstOrDefaultAsync();

                if (ListarCardapioEmpresaViewModel == null)
                {
                    
                    return NotFound();
                }

                return View(ListarCardapioEmpresaViewModel);
            }
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


        //Falta atender a regra : Ao Criar e Editar não pode existir a mesma empresa no endereço
    }
}
