using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models
{
    public class Autor
    {
        public int AutorId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        // Relacionamento muitos-para-muitos com Livro via LivroAutor
        public ICollection<LivroAutor> LivrosAutores { get; set; } = new List<LivroAutor>(); // Defina e inicialize aqui

        // Construtor padrão para garantir que a lista seja inicializada
        public Autor()
        {
            LivrosAutores = new List<LivroAutor>();
        }
    }
}
