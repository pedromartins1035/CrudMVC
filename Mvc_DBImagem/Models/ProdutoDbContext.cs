using System.Data.Entity;

namespace Mvc_DBImagem.Models
{
    public class ProdutoDbContext : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}