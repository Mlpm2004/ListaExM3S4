using Exercicios.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        /*
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
       */
        public IActionResult Index()
        {
            var listaLocalizacoes = BuscarTodasLocalizacoes();
            return View(listaLocalizacoes);
        }
        public IActionResult Erro()
        {
            
            return View();
        }


        public async Task<IActionResult> Create()
        {
            ViewBag.TodasEmpresas = MontarSelect();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ListarLocalizacaoEmpresaViewModel model)
        {
            LocalizacaoModel localizacao = model;
             using (var contextLocal = context.CreateDbContext())
            {
                localizacao.EmpresaId = contextLocal.Empresas.Where(w => w.Id == localizacao.EmpresaId.Id).First();
                foreach (LocalizacaoModel nome in contextLocal.Localizacoes) 
                {
                   if(  nome.Logradouro     ==  localizacao.Logradouro      && 
                        nome.Bairro         ==  localizacao.Bairro          &&
                        nome.Cidade         ==  localizacao.Cidade          &&
                        nome.EmpresaId      == localizacao.EmpresaId)
                    {
                        return RedirectToAction("Erro");
                    }

                }
                
                contextLocal.Localizacoes.Add(localizacao);
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

        private IEnumerable<ListarLocalizacaoEmpresaViewModel> BuscarTodasLocalizacoes()
        {
            var listarLocalizacaoEmpresaViewModel = new List<ListarLocalizacaoEmpresaViewModel>();

            using (var contextLocal = context.CreateDbContext())
            {
                var localizacoes = contextLocal.Localizacoes
                            .Include(empresa => empresa.EmpresaId);

                localizacoes.ToList().ForEach(localizacoes =>
                {
                    listarLocalizacaoEmpresaViewModel.Add(new ListarLocalizacaoEmpresaViewModel
                    {
                        Id = localizacoes.Id,
                        Logradouro = localizacoes.Logradouro,
                        Bairro =   localizacoes.Bairro,
                        Cidade= localizacoes.Cidade,
                        UF = localizacoes.UF,
                        EmpresaId = localizacoes.EmpresaId.Id,
                        Ativo = localizacoes.Ativo,
                        DescricaoEmpresa = $"{localizacoes.EmpresaId.Id} - {localizacoes.EmpresaId.Descricao}"

                    });
                });
            }

            return listarLocalizacaoEmpresaViewModel;
        }
        
        
    }
}
