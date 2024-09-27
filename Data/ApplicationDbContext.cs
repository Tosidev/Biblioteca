using Microsoft.EntityFrameworkCore;
using Biblioteca.Models;

namespace Biblioteca.Data  // Coloque o namespace de volta para Biblioteca.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Livro> Livros { get; set; }
        public DbSet<Assunto> Assuntos { get; set; }
        public DbSet<LivroAssunto> LivroAssunto { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<LivroAutor> LivroAutor { get; set; }
        public DbSet<Preco> Precos { get; set; }
        public DbSet<VwLivrosCompleto> VwLivrosCompleto { get; set; } // Adicionando a *view* no contexto
        public DbSet<VwLivrosCompleto> VwAutorLivros { get; set; } // Adicionando a *view* no contexto

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar a chave composta para a tabela de junção LivroAssunto
            modelBuilder.Entity<LivroAssunto>()
                .HasKey(la => new { la.LivroId, la.AssuntoId });

            modelBuilder.Entity<LivroAssunto>()
                .HasOne(la => la.Livro)
                .WithMany(l => l.LivroAssuntos)
                .HasForeignKey(la => la.LivroId);

            modelBuilder.Entity<LivroAssunto>()
                .HasOne(la => la.Assunto)
                .WithMany(a => a.LivroAssuntos)
                .HasForeignKey(la => la.AssuntoId);

            // Configurar a chave composta para a tabela de junção LivroAutor
            modelBuilder.Entity<LivroAutor>()
                .HasKey(la => new { la.LivroId, la.AutorId });

            modelBuilder.Entity<LivroAutor>()
                .HasOne(la => la.Livro)
                .WithMany(l => l.LivroAutores)
                .HasForeignKey(la => la.LivroId);

            modelBuilder.Entity<LivroAutor>()
                .HasOne(la => la.Autor)
                .WithMany(a => a.LivrosAutores)
                .HasForeignKey(la => la.AutorId);

            // Configurar VwLivrosCompleto como uma entidade sem chave
            modelBuilder.Entity<VwLivrosCompleto>().HasNoKey().ToView("vw_LivrosCompleto");

            // Configurar VwAutorLivros como uma entidade sem chave
            modelBuilder.Entity<VwAutorLivros>().HasNoKey().ToView("vw_AutorLivros");

            modelBuilder.Entity<VwLivrosCompleto>()
                .Property(v => v.Preco)
                .HasColumnType("decimal(18,2)");
        }


    }

    [Keyless]
    public class VwLivrosCompleto
    {
        public int LivroId { get; set; }
        public required string Titulo { get; set; }
        public required string Autor { get; set; }
        public required string Assunto { get; set; }
        public required string Publicacao { get; set; } 
        public required string Forma_Compra { get; set; }
        public required decimal Preco { get; set; }
    }

    [Keyless]
    public class VwAutorLivros
    {
        public int LivroId { get; set; }
        public required string Titulo { get; set; }
        public required string Autor { get; set; }
        public required string Assunto { get; set; }
        public required string Publicacao { get; set; } 

    }

}
