using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KeyTap_Service.Models
{
    public enum TipoUser
    {   //Definir os tipos de utilizadores
        //Secretaria (Zelador incluido)
        Master,

        //Administrador
        Admin,

        //Professor
        Prof,

        //Funcionárias de limpesa
        Limpesa,

        //Aluno
        Aluno
    }

    public enum Dia
    { //Definir os dias da semana
        Domingo,
        Segunda,
        Terça,
        Quarta,
        Quinta,
        Sexta,
        Sábado
    }

    //Cada class será uma tabela
    public class User
    { //Tabela User
        //GUID para identificar cada utilizador de forma unica 
        //GUID = Globally Unique Identifier
        [Required, Key]
        public string GUID { get; set; }

        //ID do cartão do utilizador
        [Required]
        public string IDCartao { get; set; }

        //Nome do utilizador
        [Required]
        public string Nome { get; set; }

        //Apelido do utilizador
        public string Apelido { get; set; }

        //Tipo do utilizador (Admin, Prof, Aluno, etc)
        [Required]
        public int Tipo { get; set; }

        //Ligação com a tabela permisao, no qual irá criar uma coleção de permissões para cada utilizador
        [Required]
        public ICollection<Permissao> Permissoes { get; set; } //ligação de muitos para muitos com a tabela permissoes

        //Ligação com a tabela horario, no qual irá guardar os horarios do utilizador (mesmo se tiver exenção de horario)
        [Required]
        public ICollection<Horario> Horarios { get; set; } //ligação de muitos para muitos com a tabela horarios

    }

    //Tabela Permissao (No qual irá guardar as permissões de cada utilizador)
    public class Permissao
    {
        //GUID para identificar cada permissão de forma unica
        //GUID = Globally Unique Identifier
        [Key, Required]
        public string GUID { get; set; }

        //Uma descrição sobre a permissão (nao é necessario)
        public string Detalhes { get; set; }

        //Permissão para abrir todas as portas (ex: funcionário de manutenção ir trocar uma lâmpada, fora do horario de aula)
        [Required]
        public bool ATodas { get; set; }

        //Permissão para abrir a porta dentro do horario (ex: professor entra na sala)
        [Required]
        public bool AHorario { get; set; }

        //Permissão para abrir a porta com a autorização da pessoa responsavel pela mesma na hora (ex: professor falta e pede a um aluno para abrir a porta)
        [Required]
        public bool AAutorizado { get; set; }

        //Permissão para abrir a porta antes do horario (ex: para limpesa)
        [Required]
        public bool ADepois { get; set; }

        //Ligação com a tabela user, no qual irá guardar os utilizadores que tem essa permissão (coleção de permissões)
        public ICollection<User> Users { get; set; } //ligação de muitos para muitos com a tabela user
    }

    //Tabela Porta
    public class Porta
    {
        //GUID para identificar cada porta de forma unica
        //GUID = Globally Unique Identifier
        [Key, Required]
        public string GUID { get; set; }

        //Nome da porta
        [Required]
        public string Nome { get; set; }

        //Bloco onde a porta se encontra
        [Required]
        public string Bloco { get; set; }

    }

    //Tabela Tempo
    public class Tempo
    {
        //GUID para identificar cada tempo de forma unica
        //GUID = Globally Unique Identifier
        [Key, Required]
        public string GUID { get; set; }

        //Tempo (1 a 9)
        [Required]
        public int Temp { get; set; }

        //Hora de inicio
        [Required]
        public DateTime Inicio { get; set; }

        //Hora de fim
        [Required]
        public DateTime Fim { get; set; }
    }

    //Tabela Horario
    public class Horario
    {
        //GUID para identificar cada horario de forma unica
        //GUID = Globally Unique Identifier
        [Key, Required]
        public string GUID { get; set; }

        Horario() {
            GUID = Guid.NewGuid().ToString();
        }

        //Dia da semana (1 a 6)
        [Required]
        //Mudar
        public Dia Dia { get; set; }

        //Tempo de aula (1 a 9)
        //Cada tempo de aula tem uma porta associada
        //Irá ser utilizado os hórarios oficiais da escola, tambem para garantir uma margem de entrada, caso o professor queira entrar mais cedo, tal como, uma margem de saida, caso o profssor tenha se esquecido de algum objeto dentro da sala de aula.
        [Required]
        public List<Porta> Portas = new List<Porta>(9) {
            // Desta maneira podemos mudar o tamanho incluindo mais portas correspondentes a horários sendo que a escola está aberta
            // desde as 7 da manhã às 10 da noite
        };

        // Miau
        //Para pessoas que nao tem horario especifico
        [Required]
        public bool HVazio { get; set; }

        //ligação de um para um com a tabela Porta
        [Required]
        public Porta Porta { get; set; }

        [Required]
        public bool Ativo = false;

        //ligação de um para um com a tabela user
        public ICollection<User> Users { get; set; }
    }

    //Para salas como sala de reunião, auditório, etc... Sem horario de utilização será utilizado um sistema de aluguer
    //Para laguem utilizar o auditório por exemplo, terá que fazer um pedido de aluguer
    public class Aluguer
    {
        //GUID para identificar cada aluguer de forma unica
        //GUID = Globally Unique Identifier
        [Key, Required]
        public string GUID { get; set; }

        //GUID do utilizador que alugou
        [Required]
        public string GUIDAlugador { get; set; }

        //GUID da porta que foi alugada
        [Required]
        public Porta Porta { get; set; }

        //Tempo do aluguer
        [Required]
        public DateTime TempoAluguer { get; set; }

        //Horario de inicio do aluguer
        [Required]
        public DateTime Inicio { get; set; }

        //Horario de fim do aluguer
        [Required]
        public DateTime Fim { get; set; }

        //Motivo do aluguer (não é necessario, por enquanto)
        public string Motivo { get; set; }

    }
}