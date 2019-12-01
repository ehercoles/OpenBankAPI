using Microsoft.EntityFrameworkCore;
using Conta.Models;

namespace Conta.Models
{
    public class MovimentacaoContext : DbContext
    {
        public MovimentacaoContext(DbContextOptions<MovimentacaoContext> options)
            : base(options)
        {
        }

        public DbSet<Movimentacao> Movimentacoes { get; set; }
    }
}