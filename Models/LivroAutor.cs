namespace Biblioteca.Models
{
    public class LivroAutor
    {
        // Chave estrangeira para Livro
        public int LivroId { get; set; }
        public Livro Livro { get; set; } = null!;  // Marcado como não nulo

        // Chave estrangeira para Autor
        public int AutorId { get; set; }
        public Autor Autor { get; set; } = null!;  // Marcado como não nulo
    }
}
