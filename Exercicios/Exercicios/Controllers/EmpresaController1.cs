using Microsoft.AspNetCore.Mvc;

namespace Exercicios.Controllers
{
    public class EmpresaController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
