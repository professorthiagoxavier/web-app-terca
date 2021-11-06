using Domain;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class AtividadeController : Controller
    {
        public async Task<IActionResult> Index([FromServices]IAtividadeService atividadeService)
        {
            var result = await atividadeService.BuscarAtividadesAsync();

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("urlGitHub, rm, numeroAtividade, nome")] AtividadeModel model, [FromServices] IAtividadeService atividadeService)
        {
            await atividadeService.SalvarAtividade(model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
