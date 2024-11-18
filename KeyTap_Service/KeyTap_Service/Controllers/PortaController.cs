using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KeyTap_Service.DB;
using KeyTap_Service.Models;

namespace KeyTap_Service.Controllers
{
    public class PortaController : Controller
    {
        private KeyTapContext db;

        // GET: Porta
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AbrirPorta(string cartao, string porta)
        {
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
                    erro = 1
                },
                // Permite o retorno de informações através deste método
                JsonRequestBehavior.AllowGet);
            }

            // Data e Hora do instante
            DateTime dateTime = DateTime.Now;

            // Encontrar o Tempo que corresponde ao 'agora', tendo em conta os intervalos de tempo
            Tempo tempo = db.Tempos.FirstOrDefault(t => t.Inicio <= dateTime && t.Fim >= DateTime.Now);

            if (tempo != null) //Se o tempo nao for nullo
            {
                //Coloca a data, as horas e o dia da semana
                var dia = ((int)dateTime.DayOfWeek);

                //Procura na base de dados em horarios, se a data for maior ou igual ao dia colcado vai ser igual ao dia e o horario vai estar ativo
                var horario = db.Horarios.FirstOrDefault(h => ((int)h.Dia) == dia && h.Ativo == true);

                //vai abrir a porta
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
                        erro = 2
                    },
                    JsonRequestBehavior.AllowGet
                );

            }

            // Erro 3 significa que não foi encontrado nenhum tempo correspondente à base de dados
            return Json(
                new {
                    abrir = false,
                    erro = 3
                },
                JsonRequestBehavior.AllowGet
            );
        }
    }
}