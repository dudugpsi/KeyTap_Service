using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using KeyTap_Service.DB;
using KeyTap_Service.Models;

namespace KeyTap_Service.Controllers
{
    public class PortaController : Controller
    {
        private KeyTapContext db;

        // GET: Porta
        //public ActionResult Index()
        //{
        //    return View();
        //}


        [HttpPost]
        public ActionResult AbrirPorta(string cartao, string porta)
        {
            // Ver se os valores são válidos
            // Este if verifica se os valores são nulos ou vazios
            if (cartao == null || porta == null || cartao.IsEmpty() || porta.IsEmpty())
            {
                return Json(
                new
                {
                    // Nega o abrir porta
                    abrir = false,

                    // Erro que indica que não não tem os valores necessários
                    erro = 1
                },
                // Permite o retorno de informações através deste método
                JsonRequestBehavior.AllowGet);
            }

            // Encontrar o utilizador através do ^cartão que foi passado como argumento
            User user = db.Users.FirstOrDefault(u => u.IDCartao == cartao);

            // Encontrar a porta através do GUID da porta
            Porta a_porta = db.Portas.FirstOrDefault(p => p.GUID == porta);

            // Se o utilizador não for encontrado
            if (user == null)
            {
                // Retornar uma resposta JSON com a negação de abrir porta
                return Json(
                new
                {
                    // Nega o abrir porta
                    abrir = false,

                    // Erro que indica que não encontrou user
                    erro = 2
                },
                // Permite o retorno de informações através deste método
                JsonRequestBehavior.AllowGet);
            }

            // Data e Hora do instante
            DateTime dateTime = DateTime.Now;

            // Encontrar o Tempo que corresponde ao 'agora', tendo em conta os intervalos de tempo
            Tempo tempo = db.Tempos.FirstOrDefault(t => t.Inicio <= dateTime && t.Fim >= DateTime.Now);

            if (tempo != null)
            {
                var dia = ((int)dateTime.DayOfWeek);

                // O horário tem um valor que é o dia da semana
                // Ele vai buscar o horario em que o dia da semana corresponde ao dia de hoje
                var horario = db.Horarios.FirstOrDefault(h => ((int)h.Dia) == dia && h.Ativo == true);


                foreach (Porta _porta in horario.Portas)
                {
                    if (a_porta == _porta)
                    {
                        return Json(
                            new
                            {
                                abrir = true,
                                erro = 0
                            },
                            JsonRequestBehavior.AllowGet
                        );

                    }
                }

                // Erro 2 Significa que nenhuma porta compativel foi encontrada
                return Json(
                    new
                    {
                        abrir = false,
                        erro = 3
                    },
                    JsonRequestBehavior.AllowGet
                );

            }

            // Erro 3 significa que não foi encontrado nenhum tempo correspondente à base de dados
            return Json(
                new {
                    abrir = false,
                    erro = 4
                },
                JsonRequestBehavior.AllowGet
            );
        }
    }
}