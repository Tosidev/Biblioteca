using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models
{
    public class Autor
    {
        public int AutorId { get; set; }

        [Required]
        [StringLength(40)]
        public string Nome { get; set; } = string.Empty;

        // Relacionamento N:N (muitos-para-muitos) com Livro via LivroAutor
        public ICollection<LivroAutor> LivrosAutores { get; set; } = new List<LivroAutor>(); 

        // Construtor padrão para garantir que a lista seja inicializada
        public Autor()
        {
            LivrosAutores = new List<LivroAutor>();
        }
    }
}
