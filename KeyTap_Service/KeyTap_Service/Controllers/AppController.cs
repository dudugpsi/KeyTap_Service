using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KeyTap_Service.Controllers
{
    public class AppController : Controller
    {
        // Método para a página inicial
        public ActionResult Index()
        {
            // Passa dados dinâmicos para a view
            ViewBag.Title = "Página Inicial";  // Título da página
            ViewBag.Info = "Este é o sistema de abertura de portas!";  // Informação que será exibida na página
            return View();  // Retorna a view Index.cshtml dentro da pasta Views/App
        }

        // Método para abrir a porta
        [HttpPost]
        public ActionResult AbrirPorta(string info)
        {
            // Modifica a informação recebida
            info += " vroom vroom";

            // Retorna uma resposta JSON
            return Json(new
            {
                success = true,
                text = "Vroom",
                info = info,
                portaAbrir = true,
            },
            JsonRequestBehavior.AllowGet);  // Permite que o método POST retorne dados em formato JSON
        }
    }
}
