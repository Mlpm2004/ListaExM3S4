using Microsoft.EntityFrameworkCore;

namespace Exercicios.Models
{
    public class ExerciciosDbContext : DbContext
    {
        public ExerciciosDbContext(DbContextOptions<ExerciciosDbContext> options) : base(options)
        {

        }

        public DbSet<EmpresaModel> Empresas { get; set; }
        public DbSet<LocalizacaoModel> Localizacoes { get; set; }
        public DbSet<CardapioModel> Cardapios { get; set; }
    }
}
