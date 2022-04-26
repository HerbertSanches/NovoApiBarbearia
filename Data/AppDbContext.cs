using Microsoft.EntityFrameworkCore;
using NovoApiBarbearia.Models;

namespace NovoApiBarbearia.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Funcionario> Funcionario { get; set; }
        public DbSet<Produto> Produto { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
                .HasData(
                new Cliente { Id = 1, Nome = "Jhonny", Telefone = "69992939145" },
                new Cliente { Id = 2, Nome = "Roberto", Telefone = "69125125" });

            modelBuilder.Entity<Funcionario>()
                .HasData(
                new Funcionario { Id = 1, Nome = "Thiaho", Cpf = "1", Senha = "123" });

            modelBuilder.Entity<Produto>()
                .HasData(new Produto { Id = 1, Nome = "Corte", Valor = 25 });
        }



    }
}
