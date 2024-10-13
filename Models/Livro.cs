using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models
{
    public class Livro
    {
        [Key]
        public int LivroId { get; set; }

        [Required]
        [StringLength(40)]
        public required string Titulo { get; set; }

        public DateTime DataPublicacao { get; set; }

        // Relacionamento N:N (muitos-para-muitos) com Assunto via LivroAssunto
        public ICollection<LivroAssunto> LivroAssuntos { get; set; } = new List<LivroAssunto>();

        // Propriedade de navegação para o relacionamento N:N (muitos-para-muitos) com Autor
        public ICollection<LivroAutor> LivroAutores { get; set; } = new List<LivroAutor>();

        // Adicionar a coleção de preços
        public ICollection<Preco> Precos { get; set; } = new List<Preco>();

        // Construtor padrão para inicializar as listas
        public Livro()
        {
            LivroAutores = new List<LivroAutor>();
            LivroAssuntos = new List<LivroAssunto>();
            Precos = new List<Preco>(); // Inicializa a lista de preços
        }
        
    }
}
