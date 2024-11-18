using KeyTap_Service.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

//Tranforma os nomes singurales na base de dados em nomes plural para serem utilizados em codigo
namespace KeyTap_Service.DB
{
    public class KeyTapContext : DbContext
    {
        public KeyTapContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<KeyTapContext>());
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Porta> Portas { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Tempo> Tempos { get; set; }
        public DbSet<Aluguer> Alugueres { get; set; }
        public DbSet<Permissao> Permissoes { get; set; }
    }
}