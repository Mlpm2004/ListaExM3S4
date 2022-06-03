using Exercicios.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Exercicios.Controllers
{
    public class SelectEmpresasController : Controller
    {
        private readonly IDbContextFactory<ExerciciosDbContext> context;

        public SelectEmpresasController(IDbContextFactory<ExerciciosDbContext> context)
        {
            this.context = context;
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

        private IEnumerable<ListarLocalizacaoEmpresaViewModel> BuscarTodasEmpresas()
        {
            var listarLocalizacaoEmpresaViewModel = new List<ListarLocalizacaoEmpresaViewModel>();

            using (var contextLocal = context.CreateDbContext())
            {
                var localizacoes = contextLocal.Localizacoes
                            .Include(empresa => empresa.Empresa);

                localizacoes.ToList().ForEach(localizacoes =>
                {
                    listarLocalizacaoEmpresaViewModel.Add(new ListarLocalizacaoEmpresaViewModel
                    {
                        Id = localizacoes.Id,
                        Empresa = localizacoes.Empresa.Id,
                        
                    });
                });
            }

            return listarLocalizacaoEmpresaViewModel;
        }

    }
}
